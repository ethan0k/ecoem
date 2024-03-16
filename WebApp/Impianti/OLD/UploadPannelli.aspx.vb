Imports System.Data
Imports System.Data.OleDb
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Globalization

Partial Class WebApp_Pannelli_Upload
    Inherits System.Web.UI.Page

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim IdImpianto As Integer
        Dim IdCaricamento As Integer = ErroreImportazione.GetLastIdCaricamento

        IdCaricamento += 1

        Session("IdCaricamento") = IdCaricamento

        If Not fileUpload.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        ' Verifica Utente
        If ddlImpianti.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un impianto!', 'Errore');", True)
            Exit Sub
        Else
            IdImpianto = ddlImpianti.SelectedValue
        End If

        divError.Visible = False

        Dim table As DataTable = GetDataFile()

        If table Is Nothing Then
            divError.Visible = True
            lblError.Text = "Errore di lettura file."
            Exit Sub
        End If

        If table.Rows.Count = 0 Then
            divError.Visible = True
            lblError.Text = "Errore di lettura file."
            Exit Sub
        End If
        
        Dim row As DataRow
        Dim Count As Integer

        Dim Matricola As String
        Dim Errore As String
        Dim Importabile As Boolean
        Dim NrImportabili As Integer
        Dim NrErrori As Integer

        For i = 0 To table.Rows.Count - 1

            row = table.Rows(i)
            Count += 1

            Importabile = True

            Try
                Matricola = Convert.ToString(row("Matricola"))
                Errore = String.Empty
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
                Exit Sub
            End Try


            If Matricola = String.Empty Then
                Errore = "Matricola vuota"
                Importabile = False
            End If

            Dim PannelloDaCaricare As Pannello = Pannello.Carica(Matricola, txtCodiceProduttore.Text)
            If PannelloDaCaricare Is Nothing Then
                Errore = "Matricola non trovata"
                Importabile = False

            Else

                If PannelloDaCaricare.IdImpianto <> 0 Then
                    Errore = "Pannello abbinato ad altro impianto."
                    Importabile = False
                End If

            End If

            Dim newErrore As New ErroreImportazione

            If Importabile Then
                NrImportabili += 1
            Else
                NrErrori += 1
            End If

            newErrore.Matricola = Matricola
            newErrore.DataCaricamento = Today
            newErrore.DataInizioGaranzia = Today
            newErrore.IdCaricamento = IdCaricamento
            newErrore.Errore = Errore
            newErrore.IdMarca = 0
            newErrore.Save()
        Next

        If NrErrori = 0 Then
            Dim ListaDaImportare As List(Of ErroreImportazione)

            Dim i As Integer

            ListaDaImportare = ErroreImportazione.GetByIdImportazione(Session("IdCaricamento"))

            For Each ErroreDaImportare In ListaDaImportare
                Dim PannellDaAbbinare As Pannello = Pannello.Carica(ErroreDaImportare.Matricola, txtCodiceProduttore.Text)

                PannellDaAbbinare.IdImpianto = ddlImpianti.SelectedValue
                PannellDaAbbinare.Save()
                i += 1
            Next

            ErroreImportazione.Elimina(Session("IdCaricamento"))
            Session.Remove("IdCaricamento")

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Importazione completata! " & i & " pannelli importati.'" & ", 'Messaggio di conferma');", True)


        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & NrErrori & " errori di importazione! nessun pannello importato.'" & ", 'Messaggio errore');", True)
            ListaErrori.DataBind()
        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        ddlImpianti.SelectedIndex = 0
    End Sub

    Protected Function GetDataFile() As DataTable

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("UploadResourcePath"))
        Dim fileName As String = fileUpload.FileName
        Dim Table As New DataTable
        Dim pck = New OfficeOpenXml.ExcelPackage()


        Try
            fileUpload.SaveAs(folder & "\" & fileUpload.FileName)
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
            Exit Function

        End Try

        pck.Load(New IO.FileInfo(folder & "\" & fileUpload.FileName).OpenRead)

        If pck.Workbook.Worksheets.Count <> 0 Then
            Dim ws = pck.Workbook.Worksheets.First

            If ws.Name <> "Pannelli" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve avere nome = Pannelli', 'Errore');", True)
                Return Table
                Exit Function
            End If

            If ws.Dimension.End.Column <> 1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve contenere 1 colonna', 'Errore');", True)
                Return Table
                Exit Function
            End If

            Dim hasHeader = True ' adjust accordingly '


            For Each firstRowCell In ws.Cells(1, 1, 1, ws.Dimension.End.Column)
                Table.Columns.Add(If(hasHeader, firstRowCell.Text, String.Format("Column {0}", firstRowCell.Start.Column)))
            Next

            Dim startRow = If(hasHeader, 2, 1)
            For rowNum = startRow To ws.Dimension.End.Row
                Dim wsRow = ws.Cells(rowNum, 1, rowNum, ws.Dimension.End.Column)
                Table.Rows.Add(wsRow.Select(Function(cell) cell.Text).ToArray)
            Next

            If Table.Columns(0).ColumnName.ToString <> "Matricola" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La prima colonna del foglio di lavoro deve avere nome = Matricola', 'Errore');", True)
                Return Table
                Exit Function
            End If

        End If

        Return Table

    End Function

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Page.User.IsInRole("Utente") Then

            Dim CurrentUser = Membership.GetUser(Page.User.Identity.Name)
            Dim UtenteCliente As UtenteCliente = UtenteCliente.Carica(CurrentUser.ProviderUserKey)
            ddlCliente.SelectedValue = UtenteCliente.IdCliente
            divUtente.Attributes.CssStyle.Add("Display", "None")
            'ddlImpianti.Items.Clear()
            sqlImpianti.SelectCommand = ""

            Dim ListaImpianti As List(Of Impianto) = Impianto.CaricaDaIdCliente(UtenteCliente.IdCliente)

            If Not ListaImpianti Is Nothing Then
                For Each Impianto In ListaImpianti
                    ddlImpianti.Items.Add(New ListItem(Impianto.Descrizione, Impianto.Id))
                Next

            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        

    End Sub

    Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged
        ddlImpianti.Items.Clear()
        ddlImpianti.Items.Add(New ListItem("Selezionare..", "0", True))
        ddlImpianti.DataBind()
    End Sub

End Class
