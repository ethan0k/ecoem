Imports System.Data
Imports System.Data.OleDb
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Globalization
Imports System.IO

Partial Class WebApp_Pannelli_Upload
    Inherits System.Web.UI.Page

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

        If Not Page.IsPostBack Then
            SqlDatasource1.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString
            SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 0

            If IsStagingEmpty() Then
                lblEsito.Visible = False
                cmdImporta.Visible = False
                cmdEsporta.Visible = False
                cmdEliminaTutti.Visible = False
                dvGroup.Visible = False
            ElseIf ErroreImportazione.IsInError(Page.User.Identity.Name.ToString, 3) Then
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

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim IdImpianto As Integer

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
                If Not row.IsNull("Matricola") Then
                    Matricola = Convert.ToString(row("Matricola"))
                Else
                    Matricola = ""
                End If
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
                newErrore.UserName = Page.User.Identity.Name.ToString
                newErrore.Errore = Errore
                newErrore.IdMarca = 0
                newErrore.TipoImportazione = 3
                newErrore.Protocollo = String.Empty
                newErrore.Stato = String.Empty
                newErrore.Impianto = String.Empty
                newErrore.IdTipologia = 0
                newErrore.IdFascia = 0
                newErrore.CostoMatricola = 0
                newErrore.DataRitiro = DefaultValues.GetDateTimeMinValue
                newErrore.NumeroFIR = String.Empty
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

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        ddlImpianti.SelectedIndex = 0
    End Sub

    Protected Function GetDataFile() As DataTable

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("UploadResourcePath"))
        'Dim fileName As String = fileUpload.FileName
        Dim Ext As String = Path.GetExtension(fileUpload.FileName)
        Dim fileName As String = Path.GetFileNameWithoutExtension(fileUpload.FileName) & DateTime.Now.ToString("ddMMyy_HHMMss")
        fileName = fileName & "." & Ext

        Dim Table As New DataTable
        Dim pck = New OfficeOpenXml.ExcelPackage()


        Try
            fileUpload.SaveAs(folder & "\" & fileName)
            pck.Load(New IO.FileInfo(folder & "\" & fileName).OpenRead)

        Catch ex As Exception
            Throw New System.Exception(ex.Message)
            Exit Function

        End Try

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

    Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged
        ddlImpianti.Items.Clear()
        ddlImpianti.Items.Add(New ListItem("Selezionare..", "0", True))
        ddlImpianti.DataBind()
    End Sub

    Private Function IsStagingEmpty() As Boolean

        Dim dv As System.Data.DataView = CType(SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView)

        Return dv.Count = 0

    End Function

    Private Sub cmdImporta_Click(sender As Object, e As EventArgs) Handles cmdImporta.Click

        Dim ListaDaImportare As List(Of ErroreImportazione)
        Dim i As Integer

        If ErroreImportazione.IsInError(Page.User.Identity.Name.ToString, 3) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Sono presenti errori di importazione! Impossibile procedere.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        ' Verifica impianto
        If ddlImpianti.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un impianto!', 'Errore');", True)
            Exit Sub
        End If

        ListaDaImportare = ErroreImportazione.GetByIdImportazione(Page.User.Identity.Name.ToString, 3)

        For Each ErroreDaImportare In ListaDaImportare
            Dim PannellDaAbbinare As Pannello = Pannello.Carica(ErroreDaImportare.Matricola, txtCodiceProduttore.Text)

            PannellDaAbbinare.IdImpianto = ddlImpianti.SelectedValue

            Try
                PannellDaAbbinare.Save()
                ErroreDaImportare.Elimina()
                i += 1
            Catch ex As Exception

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & " '" & ", 'Messaggio errore');", True)
                Exit Sub
            End Try

        Next

        ListaErrori.DataBind()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Importazione completata! " & i & " pannelli abbinati.'" & ", 'Messaggio di conferma');", True)

    End Sub

    Private Sub cmdEliminaTutti_Click(sender As Object, e As EventArgs) Handles cmdEliminaTutti.Click

        ErroreImportazione.EliminaTuttiImpianto(Page.User.Identity.Name.ToString)
        ListaErrori.DataBind()

        cmdEsporta.Visible = False
        cmdImporta.Visible = False
        cmdEliminaTutti.Visible = False
        lblEsito.Visible = False
        dvGroup.Visible = False

    End Sub
End Class
