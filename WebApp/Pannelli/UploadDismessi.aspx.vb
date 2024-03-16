Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Partial Class WebApp_Pannelli_UploadDismessi
    Inherits System.Web.UI.Page

    Private Sub WebApp_Pannelli_UploadDismessi_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            SqlDatasource1.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString
            SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 0

            If IsStagingEmpty() Then
                lblEsito.Visible = False
                cmdImporta.Visible = False
                cmdEsporta.Visible = False
                cmdEliminaTutti.Visible = False
                dvGroup.Visible = False
            ElseIf ErroreImportazione.IsInError(Page.User.Identity.Name.ToString, 2) Then
                lblEsito.Visible = True
                cmdImporta.Visible = False
                lblEsito.Text = "Sono presenti errori nell'importazione"
                lblEsito.ForeColor = System.Drawing.Color.Red
                lblEsito.Visible = True
                dvGroup.Visible = True
            Else
                lblEsito.Visible = False
                lblEsito.Visible = True
                lblEsito.Text = "Nessun errore nei dati importati"
                lblEsito.ForeColor = System.Drawing.Color.Black
                dvGroup.Visible = True
            End If
        End If

    End Sub

    Private Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim IdMarca As Integer
        Dim table As New DataTable

        If Not fileUpload.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        Dim fileName As String = fileUpload.FileName

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
            fileUpload.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Exit Sub

        End Try

        pck.Load(New IO.FileInfo(PathFile).OpenRead)
        If pck.Workbook.Worksheets.Count <> 0 Then
            Dim ws = pck.Workbook.Worksheets.First

            If ws.Name <> "Dismessi" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve avere nome = Dismessi', 'Errore');", True)
                Exit Sub
            End If

            If ws.Dimension.End.Column <> 3 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve contenere 3 colonne', 'Errore');", True)
                Exit Sub
            End If

            If ws.Dimension.End.Row < 2 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve contenere almeno una riga di dati', 'Errore');", True)
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

            If table.Columns(1).ColumnName.ToString <> "Data ritiro" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La seconda colonna del foglio di lavoro deve avere nome = Data ritiro', 'Errore');", True)
                Exit Sub
            End If

            If table.Columns(2).ColumnName.ToString <> "Numero FIR" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La terza colonna del foglio di lavoro deve avere nome = Numero FIR', 'Errore');", True)
                Exit Sub
            End If
        End If

        Dim row As DataRow
        Dim Count As Integer
        Dim Matricola As String
        Dim NumeroFIR As String
        Dim DataRitiro As Date
        Dim CurrProduttore As Produttore
        Dim Errore As String
        Dim Importabile As Boolean
        Dim NrImportabili As Integer
        Dim NrErrori As Integer
        Dim i As Integer

        CurrProduttore = Produttore.Carica(ddlProduttore.SelectedValue)

        For i = 0 To table.Rows.Count - 1

            row = table.Rows(i)
            Count += 1

            Try
                If Not row.IsNull("Matricola") Then
                    Matricola = Convert.ToString(row("Matricola"))
                Else
                    Matricola = ""
                End If

                If Not row.IsNull("Numero FIR") Then
                    NumeroFIR = Convert.ToString(row("Numero FIR"))
                Else
                    NumeroFIR = ""
                End If

                If Not row.IsNull("Data Ritiro") Then
                    DataRitiro = Convert.ToDateTime(row("Data Ritiro"))
                Else
                    DataRitiro = DefaultValues.GetDateTimeMinValue
                End If

                Importabile = True
                Errore = String.Empty
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message.Replace("'", "") & " alla riga " & Count.ToString & "'" & ", 'Messaggio errore');", True)
                ' Aggiungere testo da visualizzare
                cmdImporta.Visible = False
                cmdEsporta.Visible = True
                cmdEliminaTutti.Visible = True
                lblEsito.Visible = True
                lblEsito.Text = "Sono presenti errori nell'importazione"
                lblEsito.ForeColor = System.Drawing.Color.Red
                dvGroup.Visible = True
                SqlDatasource1.DataBind()
                Exit Sub
            End Try


            If Matricola <> "" Then

                Dim CurrPannello As Pannello = Pannello.Carica(Matricola, CurrProduttore.Codice)
                If CurrPannello Is Nothing Then
                    Errore = "Matricola inesistente."
                    Importabile = False
                ElseIf CurrPannello.Dismesso Then
                    Errore = "Matricola già dismessa."
                    Importabile = False
                End If

                Dim newErrore As New ErroreImportazione

                If Importabile Then
                    NrImportabili += 1
                Else
                    NrErrori += 1
                    newErrore.InErrore = True
                End If

                newErrore.Matricola = Matricola
                newErrore.Errore = Errore
                newErrore.IdProduttore = ddlProduttore.SelectedValue
                newErrore.Produttore = CurrProduttore.RagioneSociale
                newErrore.DataCaricamento = Today
                newErrore.IdMarca = ddlProduttore.SelectedValue
                newErrore.UserName = Page.User.Identity.Name.ToString
                newErrore.DataInizioGaranzia = #01/01/1900#
                newErrore.DataCaricamento = Today.ToShortDateString()
                newErrore.IdImpianto = 0
                newErrore.TipoImportazione = 2
                newErrore.Protocollo = ""
                newErrore.Stato =""
                newErrore.Impianto = ""
                newErrore.IdTipologia = 0
                newErrore.IdFascia = 0
                newErrore.CostoMatricola = 0
                newErrore.DataRitiro = DataRitiro
                newErrore.NumeroFIR = NumeroFIR
                newErrore.Save()
            End If
        Next

        SqlDatasource1.DataBind()
        ListaErrori.DataBind()

        If NrErrori > 0 Then
            cmdImporta.Visible = False
            cmdEsporta.Visible = True
            cmdEliminaTutti.Visible = True
            lblEsito.Visible = True
            lblEsito.Text = "Sono presenti errori nell'importazione"
            lblEsito.ForeColor = System.Drawing.Color.Red
            dvGroup.Visible = True
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Caricamento completato! " & NrErrori.ToString & " record importati con errori.');", True)
        ElseIf NrImportabili > 0 Then
            cmdEsporta.Visible = True
            cmdEliminaTutti.Visible = True
            cmdImporta.Visible = True
            dvGroup.Visible = True
            lblEsito.Visible = True
            lblEsito.Text = "Nessun errore nei dati importati"
            lblEsito.ForeColor = System.Drawing.Color.Black
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Non sono presenti errori, procedi con la registrazione cliccando su REGISTRA.');", True)

        End If

    End Sub

    Private Sub cmdImporta_Click(sender As Object, e As EventArgs) Handles cmdImporta.Click

        Dim ListaDaImportare As List(Of ErroreImportazione)
        Dim CurrentProduttore As Produttore
        Dim i As Integer

        If ErroreImportazione.IsInError(Page.User.Identity.Name.ToString, 2) Then
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

        ListaDaImportare = ErroreImportazione.GetByIdImportazione(Page.User.Identity.Name.ToString, 2)

        For Each ErroreDaImportare In ListaDaImportare
            Dim PannelloDismesso As Pannello = Pannello.Carica(ErroreDaImportare.Matricola, CurrentProduttore.Codice)
            PannelloDismesso.Dismesso = True
            PannelloDismesso.DataRitiro = ErroreDaImportare.DataRitiro
            PannelloDismesso.NumeroFIR = ErroreDaImportare.NumeroFIR

            Try
                PannelloDismesso.Save()
                ErroreDaImportare.Elimina()
                i += 1
            Catch ex As Exception

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & " '" & ", 'Messaggio errore');", True)
                Exit Sub
            End Try

        Next

        ddlProduttore.SelectedIndex = 0
        ListaErrori.DataBind()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Importazione completata! " & i & " pannelli dismessi.'" & ", 'Messaggio di conferma');", True)

    End Sub

    Private Sub cmdEliminaTutti_Click(sender As Object, e As EventArgs) Handles cmdEliminaTutti.Click

        ErroreImportazione.EliminaTuttiDismessi(Page.User.Identity.Name.ToString)
        ListaErrori.DataBind()

        cmdEsporta.Visible = False
        cmdImporta.Visible = False
        cmdEliminaTutti.Visible = False
        lblEsito.Visible = False
        dvGroup.Visible = False

    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaRecordImportati")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:l1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("j1:J1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("G1:G1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaRecordImportati.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

    End Sub

    Private Sub BTeRRORI_Click(sender As Object, e As EventArgs) Handles btErrori.Click

        SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 1
        SqlDatasource1.DataBind()
        btErrori.CssClass = "btn btn-danger"
        btTutti.CssClass = "btn btn-default"

    End Sub

    Private Sub btTutti_Click(sender As Object, e As EventArgs) Handles btTutti.Click

        SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 0
        SqlDatasource1.DataBind()
        btErrori.CssClass = "btn btn-default"
        btTutti.CssClass = "btn btn-danger"

    End Sub

    Private Function IsStagingEmpty() As Boolean

        Dim dv As System.Data.DataView = CType(SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView)

        Return dv.Count = 0

    End Function
End Class
