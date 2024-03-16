Imports System.Data
Imports System.Data.OleDb
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Globalization
Partial Class WebApp_Pannelli_UploadDisabbinati
    Inherits System.Web.UI.Page

    Private Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim IdMarca As Integer
        Dim table As New DataTable
        Dim i As Integer

        If Not fileUpload.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        Dim fileName As String = FileUpload.FileName

        If Not Right(fileName, 4) = "xlsx" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Estensione file errata!', 'Errore');", True)
            Exit Sub
        Else
            fileName = Left(fileName, Len(fileName) - 5) + Today.ToBinary.ToString + TimeOfDay.ToBinary.ToString + ".xlsx"
        End If

        ' Verifica produttore
        If ddlProduttore.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un produttore!', 'Errore');", True)
            Exit Sub
        Else
            IdMarca = ddlProduttore.SelectedValue
        End If

        Dim pck = New OfficeOpenXml.ExcelPackage()

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("UploadResourcePath"))

        Dim PathFile As String = folder & "\" & fileName

        Try
            FileUpload.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Exit Sub

        End Try

        pck.Load(New IO.FileInfo(PathFile).OpenRead)
        If pck.Workbook.Worksheets.Count <> 0 Then
            Dim ws = pck.Workbook.Worksheets.First

            If ws.Name <> "Disabbinati" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve avere nome = Disabbinati', 'Errore');", True)
                Exit Sub
            End If

            If ws.Dimension.End.Column <> 1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve contenere 1 colonna', 'Errore');", True)
                Exit Sub
            End If

            Dim hasHeader = True ' adjust accordingly '
            For Each firstRowCell In ws.Cells(1, 1, 1, ws.Dimension.End.Column)
                table.Columns.Add(If(hasHeader, firstRowCell.Text, String.Format("Column {0}", firstRowCell.Start.Column)))
            Next

            Dim startRow = If(hasHeader, 2, 1)
            For rowNum = startRow To ws.Dimension.End.Row
                Dim wsRow = ws.Cells(rowNum, 1, rowNum, ws.Dimension.End.Column)
                table.Rows.Add(wsRow.Select(Function(cell) cell.Text).ToArray)
            Next

            If table.Columns(0).ColumnName.ToString <> "Matricola" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La prima colonna del foglio di lavoro deve avere nome = Matricola', 'Errore');", True)
                Exit Sub
            End If
        End If

        Dim row As DataRow
        Dim Count As Integer
        Dim Matricola As String
        Dim CurrProduttore As Produttore
        Dim Errore As String
        Dim Importabile As Boolean
        Dim NrImportabili As Integer
        Dim NrErrori As Integer

        CurrProduttore = Produttore.Carica(ddlProduttore.SelectedValue)

        For i = 0 To table.Rows.Count - 1

            row = table.Rows(i)
            Count += 1

            Try
                Matricola = Convert.ToString(row("Matricola"))

                Importabile = True
                Errore = String.Empty
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message.Replace("'", "") & " alla riga " & Count.ToString & "'" & ", 'Messaggio errore');", True)
                Exit Sub
            End Try


            If Matricola = String.Empty Then
                Errore = "Matricola assente"
                Importabile = False
            End If

            Dim CurrPannello As Pannello = Pannello.Carica(Matricola, CurrProduttore.Codice)
            If CurrPannello Is Nothing Then
                Errore = "Matricola inesistente."
                Importabile = False
            End If

            If CurrPannello.Dismesso Then
                Errore = "Pannello dismesso"
                Importabile = False
            End If

            Dim newErrore As New ErroreImportazione

            If Importabile Then
                NrImportabili += 1
            Else
                NrErrori += 1
            End If

            newErrore.Matricola = Matricola
            newErrore.Errore = Errore
            newErrore.UserName = Page.User.Identity.Name.ToString
            newErrore.DataCaricamento = Today
            newErrore.IdMarca = ddlProduttore.SelectedValue
            newErrore.UserName = Page.User.Identity.Name.ToString
            newErrore.DataInizioGaranzia = #1/1/1900#
            newErrore.DataCaricamento = Today.ToShortDateString()
            newErrore.IdImpianto = 0
            newErrore.Impianto = ""
            newErrore.Stato = ""
            newErrore.DataRitiro = DefaultValues.GetDateTimeMinValue
            newErrore.NumeroFIR = String.Empty
            newErrore.Save()
        Next

        ListaErrori.DataBind()

        ' Svuota la maschera
        ddlProduttore.SelectedIndex = 0

        If NrErrori > 0 Then
            cmdImporta.Visible = False
            cmdEsporta.Visible = True
            cmdEliminaTutti.Visible = True
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Importazione completata! " & i & " record importati con errori.');", True)
        ElseIf NrImportabili > 0 Then
            cmdEsporta.Visible = True
            cmdEliminaTutti.Visible = True
            cmdImporta.Visible = True
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Importazione completata! " & i & " record importati.');", True)
        End If

    End Sub

    Private Sub cmdImporta_Click(sender As Object, e As EventArgs) Handles cmdImporta.Click

        Dim ListaDaImportare As List(Of ErroreImportazione)
        Dim CurrentProduttore As Produttore
        Dim i As Integer

        If ErroreImportazione.IsInError(Page.User.Identity.Name.ToString, 3) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Sono presenti errori di importazione! Impossibile procedere.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        ' Verifica produttore
        If ddlProduttore.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un produttore!', 'Errore');", True)
            Exit Sub
        Else
            CurrentProduttore = Produttore.Carica(ddlProduttore.SelectedValue)
        End If

        ListaDaImportare = ErroreImportazione.GetByIdImportazione(Page.User.Identity.Name.ToString, 3)

        For Each ErroreDaImportare In ListaDaImportare
            Dim PannelloDisabbinato As Pannello = Pannello.Carica(ErroreDaImportare.Matricola, CurrentProduttore.Codice)
            PannelloDisabbinato.IdImpianto = 0

            Try
                PannelloDisabbinato.Save()
                ErroreDaImportare.Elimina()
            Catch ex As Exception

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & " '" & ", 'Messaggio errore');", True)
                Exit Sub
            End Try

            i += 1
        Next

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Importazione completata! " & i & " pannelli importati.'" & ", 'Messaggio di conferma');", True)

        ' Svuota la maschera
        ddlProduttore.SelectedIndex = 0

        ListaErrori.DataBind()


    End Sub
End Class
