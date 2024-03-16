Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Partial Class WebApp_Pannelli_Upload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.IsInRole("Produttore") Then
            Dim CurrentUser = Membership.GetUser(Page.User.Identity.Name)
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)
            ddlProduttore.SelectedValue = UtenteProduttore.IdProduttore
            ddlProduttore.Enabled = False
        ElseIf Page.User.Identity.Name = "admin" Then
            divAnno.Visible = True
            divProtocollo.Visible=True
        ElseIf Page.User.IsInRole("Amministratore") Then
                divProtocollo.Visible=True
        End If

        If Not Page.IsPostBack Then
            SqlDatasource1.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString
            SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 0

            If IsStagingEmpty() Then
                lblEsito.Visible = False
                cmdImporta.Visible = False
                cmdEsporta.Visible = False
                cmdEliminaTutti.Visible = False
                dvGroup.Visible = False
            ElseIf ErroreImportazione.IsInError(Page.User.Identity.Name.ToString, 1) Then
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

        Dim IdMarca As Integer
        Dim table As New DataTable

        If Not IsStagingEmpty() Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Sono presenti record importati da eliminare!', 'Errore');", True)
            Exit Sub
        End If

        If Not fileUpload.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        ' Verifica produttore
        If ddlProduttore.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal2", "jAlert('Selezionare un produttore!', 'Errore');", True)
            Exit Sub
        Else
            IdMarca = ddlProduttore.SelectedValue
        End If

        ' Verifica tipologia
        If ddlTipologia.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessuna tipologia selezionata!', 'Errore');", True)
            Exit Sub
        End If

        ' Verifica fascia
        If ddlFasciaDiPeso.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessuna fascia di peso selezionata!', 'Errore');", True)
            Exit Sub
        End If

        If Not (UCase(Right(fileUpload.FileName, 4)) = "XLSX") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('L\'estensione del file deve essere .XLSX', 'Errore');", True)
            Exit Sub
        End If

        If Not (UCase(Right(fileUpload.FileName, 4)) = "XLSX") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('L\'estensione del file deve essere .XLSX', 'Errore');", True)
            Exit Sub
        End If

        Dim pck = New OfficeOpenXml.ExcelPackage()

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("UploadResourcePath"))
        Dim fileName As String = fileUpload.FileName & DateTime.Now.ToString("ddMMyy_HHMMss")
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

            If ws.Name <> "Pannelli" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve avere nome = Pannelli', 'Errore');", True)
                Exit Sub
            End If

            If ws.Dimension.End.Column <> 5 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve contenere 5 colonne', 'Errore');", True)
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

            If table.Columns(1).ColumnName.ToString <> "Modello" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La prima colonna del foglio di lavoro deve avere nome = Modello', 'Errore');", True)
                Exit Sub
            End If

            If table.Columns(2).ColumnName.ToString <> "Marca" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La prima colonna del foglio di lavoro deve avere nome = Marca', 'Errore');", True)
                Exit Sub
            End If

            If table.Columns(3).ColumnName.ToString <> "Peso" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La prima colonna del foglio di lavoro deve avere nome = Peso', 'Errore');", True)
                Exit Sub
            End If

            If table.Columns(4).ColumnName.ToString <> "Data inizio garanzia" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La prima colonna del foglio di lavoro deve avere nome = Data inizio garanzia', 'Errore');", True)
                Exit Sub
            End If
        End If

        Dim row As DataRow
        Dim Count As Integer

        Dim Matricola As String
        Dim Modello As String
        Dim Marca As String
        Dim Peso As Decimal
        Dim DataInizioGaranzia As DateTime
        Dim Errore As String
        Dim Importabile As Boolean
        Dim NrImportabili As Integer
        Dim NrErrori As Integer
        Dim AnnoCaricamento As Integer
        Dim RigaVuota As Integer
        Dim i As Integer
        Dim MyAbbinamento As AbbinamentoTipologiaFascia = AbbinamentoTipologiaFascia.Carica(ddlProduttore.SelectedValue, ddlTipologia.SelectedValue, ddlFasciaDiPeso.SelectedValue)

        If txtAnno.Text = String.Empty Then
            AnnoCaricamento = Today.Year()
        Else
            AnnoCaricamento = CInt(txtAnno.Text)
        End If

        For i = 0 To table.Rows.Count - 1

            row = table.Rows(i)
            Count += 1
            RigaVuota = 0
            Try
                If Not row.IsNull("Matricola") Then
                    Matricola = Convert.ToString(row("Matricola"))
                Else
                    Matricola = ""
                End If

                If Not row.IsNull("Modello") Then
                    Modello = Convert.ToString(row("Modello"))
                Else
                    Modello = ""
                End If

                If Not row.IsNull("Marca") Then
                    Marca = Convert.ToString(row("Marca"))
                Else
                    Marca = ""
                End If

                If Not row.IsNull("Peso") Then
                    Decimal.TryParse(row("Peso"), Peso)
                Else
                    Peso = 0
                End If

                If Not row.IsNull("Data inizio garanzia") Then
                    DateTime.TryParse(row("Data inizio garanzia"), DataInizioGaranzia)
                Else
                    DataInizioGaranzia = DefaultValues.GetDateTimeMinValue
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

            If Modello = String.Empty Then
                Errore = "Modello assente"
                Importabile = False
                RigaVuota += 1
            End If

            If Marca = String.Empty Then
                Errore = "Marca assente"
                Importabile = False
                RigaVuota += 1
            End If

            If Peso = 0 Then
                Errore = "Peso non valido"
                Importabile = False
                RigaVuota += 1
            End If

            If DataInizioGaranzia <= DefaultValues.GetDateTimeMinValue Then
                Errore = "Data inizio garanzia non valida"
                Importabile = False
                RigaVuota += 1
            End If

            If Matricola = String.Empty Then
                Errore = "Matricola assente"
                Importabile = False
                RigaVuota += 1
            End If

            If Pannello.Exists(IdMarca, Matricola) Then
                Errore = "Matricola pannello già importata."
                Importabile = False
            End If

            Dim GiaImportato As ErroreImportazione = ErroreImportazione.Carica(Matricola, Page.User.Identity.ToString)

            If Not GiaImportato Is Nothing Then
                Errore = "Matricola pannello già presente nel file di caricamento."
                Importabile = False
            End If

            Dim newErrore As New ErroreImportazione

            If RigaVuota < 5 Then
                If Importabile Then
                    NrImportabili += 1
                Else
                    NrErrori += 1
                    newErrore.InErrore = True
                End If

                newErrore.Matricola = Matricola
                newErrore.Errore = Errore
                newErrore.Modello = Modello
                newErrore.Peso = Peso
                newErrore.Importabile = Importabile
                If Not DataInizioGaranzia = Nothing Then
                    newErrore.DataInizioGaranzia = DataInizioGaranzia
                Else
                    newErrore.DataInizioGaranzia = DefaultValues.GetDateTimeMinValue
                End If
                newErrore.DataCaricamento = DateTime.Now
                newErrore.IdMarca = ddlProduttore.SelectedValue
                newErrore.Produttore = Marca
                newErrore.UserName = Page.User.Identity.Name.ToString
                newErrore.IdImpianto = 0
                newErrore.AnnoCaricamento = AnnoCaricamento
                newErrore.IdProduttore = ddlProduttore.SelectedValue
                newErrore.TipoImportazione = 1
                newErrore.Protocollo = ""
                newErrore.Impianto = ""
                newErrore.IdTipologia = ddlTipologia.SelectedValue
                newErrore.IdFascia = ddlFasciaDiPeso.SelectedValue
                newErrore.CostoMatricola = MyAbbinamento.CostoMatricola
                newErrore.Stato = ""
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
        ddlProduttore.SelectedIndex = 0
        txtAnno.Text = String.Empty
        ListaErrori.DataBind()
    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        dt.Columns.Remove("Id")
        dt.Columns.Remove("Peso")
        dt.Columns.Remove("IdMarca")
        dt.Columns.Remove("UserName")
        dt.Columns.Remove("IdProduttore")
        dt.Columns.Remove("InErrore")
        dt.Columns.Remove("DataCaricamento")
        dt.Columns.Remove("DataInizioGaranzia")
        dt.Columns.Remove("AnnoCaricamento")

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaRecordImportati")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:e1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("a1:i5000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaRecordImportati.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

    End Sub

    Private Sub cmdImporta_Click(sender As Object, e As EventArgs) Handles cmdImporta.Click

        Dim ListaDaImportare As List(Of ErroreImportazione)
        Dim AnnoCaricamento As Integer
        Dim ProtocolloCaricamento As String
        Dim CurrentProduttore As Produttore
        Dim CostoServizio, CostoSingolo As Decimal
        Dim Fascia As FasciaDiPeso
        Dim Tipologia As TipologiaCella
        Dim i As Integer

        If ErroreImportazione.IsInError(Page.User.Identity.Name.ToString, 1) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Sono presenti errori di importazione! Impossibile procedere.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        AnnoCaricamento = GetYearFromImported()
        CurrentProduttore = Produttore.Carica(GetProduttoreFromImported())

        ListaDaImportare = ErroreImportazione.GetByIdImportazione(Page.User.Identity.Name.ToString, 1)

        ' Controllo preliminare
        For Each ErroreDaImportare In ListaDaImportare
            If Pannello.Exists(ErroreDaImportare.IdMarca, ErroreDaImportare.Matricola) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La matricola " & ErroreDaImportare.Matricola & " è già stata importata. Operazione interrotta'" & ", 'Messaggio errore');", True)
                Exit Sub
            ElseIf ErroreDaImportare.Exists(ErroreDaImportare.Matricola, Page.User.Identity.Name.ToString, ErroreDaImportare.id) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La matricola " & ErroreDaImportare.Matricola & " è presente più volte nel file di importazione. Operazione interrotta'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If
        Next

        Dim UltimoNrComunicazione As Integer = CurrentProduttore.GetLastNrComunicazione(AnnoCaricamento)

        If (txtProtDaAssegnare.Text = "") Then
            ProtocolloCaricamento = CurrentProduttore.Codice & AnnoCaricamento.ToString & CStr(UltimoNrComunicazione + 1).PadLeft(3, "0")
        Else
            ProtocolloCaricamento = txtProtDaAssegnare.Text
        End If

        ' Crea il nuovo protocollo
        Dim NewProtocollo As New Protocollo
        NewProtocollo.Protocollo = ProtocolloCaricamento
        NewProtocollo.Data = Today
        NewProtocollo.UserName = Page.User.Identity.Name.ToString
        NewProtocollo.DataAttestato = DefaultValues.GetDateTimeMinValue
        NewProtocollo.CostoServizio = CostoServizio
        NewProtocollo.Save()

        For Each ErroreDaImportare In ListaDaImportare

            Dim nuovoPannello As New Pannello

            i += 1

            If i = 1 Then

                CostoSingolo = ErroreDaImportare.CostoMatricola
                Fascia = FasciaDiPeso.Carica(ErroreDaImportare.IdFascia)
                Tipologia = TipologiaCella.Carica(ErroreDaImportare.IdTipologia)

            End If
            nuovoPannello.Matricola = Trim(ErroreDaImportare.Matricola)
            nuovoPannello.DataInizioGaranzia = ErroreDaImportare.DataInizioGaranzia
            nuovoPannello.IdMarca = ErroreDaImportare.IdMarca
            nuovoPannello.Modello = ErroreDaImportare.Modello
            nuovoPannello.Peso = ErroreDaImportare.Peso
            nuovoPannello.Produttore = ErroreDaImportare.Produttore
            nuovoPannello.DataCaricamento = ErroreDaImportare.DataCaricamento
            nuovoPannello.Anno = ErroreDaImportare.AnnoCaricamento
            nuovoPannello.Protocollo = ProtocolloCaricamento
            nuovoPannello.NrComunicazione = UltimoNrComunicazione + 1
            nuovoPannello.DataConformità = DefaultValues.GetDateTimeMinValue
            nuovoPannello.IdTipologiaCella = ErroreDaImportare.IdTipologia
            nuovoPannello.IdFasciaDiPeso = ErroreDaImportare.IdFascia
            nuovoPannello.CostoMatricola = ErroreDaImportare.CostoMatricola
            CostoServizio += ErroreDaImportare.CostoMatricola

            Try
                nuovoPannello.Save()
                ErroreDaImportare.Elimina()
            Catch ex As Exception

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & " '" & ", 'Messaggio errore');", True)
                Exit Sub
            End Try

        Next

        NewProtocollo.CostoServizio = CostoServizio
        NewProtocollo.Save()

        ' Svuota la maschera            
        ddlProduttore.SelectedIndex = 0
        txtAnno.Text = String.Empty
        ListaErrori.DataBind()

        ' Invia Mail di notifica
        Dim smtp As New SmtpClient
        Dim Opzione As Opzione = Opzione.Carica

        smtp.Host = Opzione.Smtp
        'smtp.Port = 587
        smtp.Port = Opzione.Porta
        'smtp.EnableSsl = True
        smtp.EnableSsl = Opzione.SSL
        smtp.UseDefaultCredentials = False
        smtp.Credentials = New NetworkCredential(Opzione.NomeUtente, Opzione.Password)
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

        Dim NuovoMessaggio As New MailMessage

        'NuovoMessaggio.From = New MailAddress("registrazione@ecoem.it", "Gestore")
        NuovoMessaggio.From = New MailAddress("registrazione@ecoem.it", "Gestore")
        NuovoMessaggio.To.Add(New MailAddress("registrazione@ecoem.it", ""))
        NuovoMessaggio.IsBodyHtml = False
        NuovoMessaggio.Subject = "Caricate Matricole pannelli utente " & Page.User.Identity.Name
        If CostoServizio > 0 Then
            NuovoMessaggio.Body = "Notifica caricamento nr. " & i.ToString & " matricole da parte dell'utente " & Page.User.Identity.Name.ToString & vbCrLf & _
                                "Quantità: " & i.ToString("#,##0") & vbCrLf & _
                                "Nr. protocollo " & ProtocolloCaricamento & vbCrLf & _
                                "Produttore abbinato: " & CurrentProduttore.RagioneSociale & vbCrLf & _
                                "Tipologia: " & Tipologia.Descrizione & vbCrLf & _
                                "Fascia di peso: " & Fascia.Descrizione & vbCrLf & _
                                "Tariffa: " & CostoSingolo.ToString("#,##0.00") & vbCrLf & _
                                "Costo del servizio € " & CostoServizio.ToString("#,##0.00") & vbCrLf & _
                                "Utente: " & Page.User.Identity.Name.ToString
        Else
            NuovoMessaggio.Body = "Notifica caricamento nr. " & i.ToString & " matricole da parte dell'utente " & Page.User.Identity.Name.ToString & vbCrLf & _
                                "Nr. protocollo " & ProtocolloCaricamento & vbCrLf & _
                                "Produttore abbinato: " & CurrentProduttore.RagioneSociale & vbCrLf & _
                                "Utente: " & Page.User.Identity.Name.ToString
        End If
        Try
            smtp.Send(NuovoMessaggio)

        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Errore: " & ex.Message & "'" & ", 'Errore invio messaggio');", True)
            Exit Sub
        End Try

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Importazione completata! " & i & " pannelli importati. Protocollo assegnato: " & ProtocolloCaricamento & "'" & ", 'Messaggio di conferma');", True)
        cmdImporta.Visible = False
        cmdEsporta.Visible = False
        cmdEliminaTutti.Visible = False
        dvGroup.Visible = False

    End Sub

    Private Sub cmdEliminaTutti_Click(sender As Object, e As EventArgs) Handles cmdEliminaTutti.Click

        ErroreImportazione.EliminaTutti(Page.User.Identity.Name.ToString)
        ListaErrori.DataBind()

        cmdEsporta.Visible = False
        cmdImporta.Visible = False
        cmdEliminaTutti.Visible = False
        lblEsito.Visible = False
        dvGroup.Visible = False

    End Sub

    Private Sub SqlDatasource1_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDatasource1.Selecting

        e.Command.Parameters("@userName").Value = Page.User.Identity.Name

    End Sub

    Private Sub BTeRRORI_Click(sender As Object, e As EventArgs) Handles BTeRRORI.Click

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

    Private Sub ddlProduttore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduttore.SelectedIndexChanged

        ddlTipologia.Items.Clear()
        ddlTipologia.Items.Add(New ListItem("Selezionare ..", 0))

        ddlFasciaDiPeso.Items.Clear()
        ddlFasciaDiPeso.Items.Add(New ListItem("Selezionare ..", 0))

    End Sub

    Private Sub ddlTipologia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipologia.SelectedIndexChanged
        ddlFasciaDiPeso.Items.Clear()
        ddlFasciaDiPeso.Items.Add(New ListItem("Selezionare ..", 0))
        ddlFasciaDiPeso.DataBind()

    End Sub

    Private Function ConvertDate(strDate As String) As Date

        Dim culture As CultureInfo
        Dim styles As DateTimeStyles
        Dim DateResult As Date

        culture = CultureInfo.CreateSpecificCulture("it-IT")
        styles = DateTimeStyles.None

        If DateTime.TryParse(strDate, culture, styles, DateResult) Then
            Return DateResult
        Else

            Dim giorno As Integer = Left(strDate, InStr(strDate, "/") - 1)

        End If

    End Function

    Private Function IsStagingEmpty() As Boolean

        Dim dv As System.Data.DataView = CType(SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView)

        Return dv.Count = 0

    End Function

    Private Function GetYearFromImported() As Integer

        Dim ListaDaImportare As List(Of ErroreImportazione)

        ListaDaImportare = ErroreImportazione.GetByIdImportazione(Page.User.Identity.Name.ToString, 1)

        For Each ErroreDaImportare In ListaDaImportare
            Return ErroreDaImportare.AnnoCaricamento
        Next

    End Function

    Private Function GetProduttoreFromImported() As Integer

        Dim ListaDaImportare As List(Of ErroreImportazione)

        ListaDaImportare = ErroreImportazione.GetByIdImportazione(Page.User.Identity.Name.ToString, 1)

        For Each ErroreDaImportare In ListaDaImportare
            Return ErroreDaImportare.IdProduttore
        Next

    End Function
End Class
