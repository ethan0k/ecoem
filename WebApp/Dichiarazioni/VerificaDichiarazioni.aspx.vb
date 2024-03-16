Imports System.Data
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Imports System.Threading

Partial Class WebApp_Dichiarazioni_VerificaDichiarazioni
    Inherits System.Web.UI.Page

    Private trd As Thread

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            txtData.Text = Today
            If Page.User.IsInRole("Operatore") Then
                cmdInviaMail.Visible = False
            End If

        End If

        'Genera()

    End Sub

    Protected Sub CreaDataTable()

        Dim myDt As New DataTable()

        myDt = CreateDataTable()

        Session("myDatatable") = myDt

    End Sub

    Private Function CreateDataTable() As DataTable

        Dim dt As New DataTable()

        dt.Columns.Add("Data", System.Type.[GetType]("System.String"))
        dt.Columns.Add("IdProduttore", System.Type.[GetType]("System.String"))
        dt.Columns.Add("Produttore", System.Type.[GetType]("System.String"))
        dt.Columns.Add("Periodicità", System.Type.[GetType]("System.String"))
        dt.Columns.Add("Mail", System.Type.[GetType]("System.String"))

        Return dt

    End Function

    Protected Sub cmdVerifica_Click(sender As Object, e As EventArgs) Handles cmdVerifica.Click

        Genera()

    End Sub

    Private Function CalcolaDataDichiarazione(Periodicita As Integer, DataRiferimento As Date) As Date

        Dim DataDichiarazione As Date

        Select Case Periodicita
            Case 1 ' Mensile
                DataDichiarazione = DateSerial(Year(DataRiferimento), Month(DataRiferimento), 1)
                DataDichiarazione = DataDichiarazione.AddDays(-1)

            Case 2 ' Bimestrale
                Select Case Month(DataRiferimento)
                    Case 1, 3, 5, 7, 9, 11
                        DataDichiarazione = DateSerial(Year(DataRiferimento), Month(DataRiferimento), 1)
                        DataDichiarazione = DataDichiarazione.AddDays(-1)

                    Case Else
                        DataDichiarazione = DateSerial(Year(DataRiferimento), Month(DataRiferimento) - 1, 1)
                        DataDichiarazione = DataDichiarazione.AddDays(-1)

                End Select

            Case 3 ' Trimestrale
                Select Case (Month(DataRiferimento))
                    Case 1, 2, 3
                        DataDichiarazione = DateSerial(Year(DataRiferimento) - 1, 12, 31)
                    Case 4, 5, 6
                        DataDichiarazione = DateSerial(Year(DataRiferimento), 3, 31)

                    Case 7, 8, 9
                        DataDichiarazione = DateSerial(Year(DataRiferimento), 6, 30)
                    Case 10, 11, 12
                        DataDichiarazione = DateSerial(Year(DataRiferimento), 9, 30)
                End Select
        End Select

        Return DataDichiarazione

    End Function

    Private Function DichMancanti(Produttore As Produttore) As Integer

        Dim Dichiarazione As Dichiarazione
        Dim DataDichiarazione As Date
        Dim AnnoRif, DichiarazioniMancanti As Integer

        ' Se gennaio anno di riferimento è quello precedente
        If Month(txtData.Text) = 1 Then
            AnnoRif = Year(txtData.Text) - 1
        Else
            AnnoRif = Year(txtData.Text)
        End If

        DataDichiarazione = CalcolaDataDichiarazione(Produttore.Periodicita, txtData.Text)

        Do Until Year(DataDichiarazione) <> AnnoRif
            Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione)
            If Dichiarazione Is Nothing Then
                DichiarazioniMancanti += 1
            End If

            DataDichiarazione = CalcolaDataDichiarazione(Produttore.Periodicita, DataDichiarazione)
        Loop

        Return DichiarazioniMancanti - 1

    End Function
    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        CreaDataTable()

        Dim myTable As DataTable
        Dim ListaProduttori As List(Of Produttore)
        Dim addToList As Boolean

        myTable = DirectCast(Session("myDatatable"), DataTable)

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            addToList = False
            If Produttore.Attivo And Not Produttore.SoloProfessionale Then
                Dim DataDichiarazione As Date
                DataDichiarazione = CalcolaDataDichiarazione(Produttore.Periodicita, txtData.Text)
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione)
                If CurrentDichiarazione Is Nothing Then
                    addToList = True
                ElseIf Not CurrentDichiarazione.Confermata Then
                    addToList = True
                End If

                If addToList Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataDichiarazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    row("Mail") = Produttore.EmailNotifiche
                    Select Case Produttore.Periodicita

                        Case 1
                            row("Periodicità") = "Mensile"
                        Case 2
                            row("Periodicità") = "Bimestrale"
                        Case 3
                            row("Periodicità") = "Trimestrale"

                    End Select
                    myTable.Rows.Add(row)

                End If
            End If
        Next

        Session("myDatatable") = myTable

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("VerificaDichirazioni")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(myTable, True)

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=VerificaDichirazioni.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

    Private Sub Genera()

        CreaDataTable()

        Dim myTable As DataTable
        Dim ListaProduttori As List(Of Produttore)
        Dim addToList As Boolean

        myTable = DirectCast(Session("myDatatable"), DataTable)

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            addToList = False
            If Produttore.Attivo And Not Produttore.SoloProfessionale Then
                Dim DataDichiarazione As Date
                DataDichiarazione = CalcolaDataDichiarazione(Produttore.Periodicita, txtData.Text)
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione)
                If CurrentDichiarazione Is Nothing Then
                    addToList = True
                ElseIf Not CurrentDichiarazione.Confermata Then
                    addToList = True
                End If

                If addToList Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataDichiarazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    Select Case Produttore.Periodicita

                        Case 1
                            row("Periodicità") = "Mensile"
                        Case 2
                            row("Periodicità") = "Bimestrale"
                        Case 3
                            row("Periodicità") = "Trimestrale"

                    End Select

                    myTable.Rows.Add(row)

                End If
            End If
        Next

        Session("myDatatable") = myTable

        Listview1.DataSource = myTable
        Listview1.DataBind()

        Session("myDatatable") = Nothing
        Session.Contents.Remove("myDatatable")

    End Sub

    Protected Sub cmdInviaMail_Click(sender As Object, e As EventArgs) Handles cmdInviaMail.Click

        trd = New Thread(AddressOf ThreadTask)
        trd.IsBackground = True
        trd.Start()

    End Sub

    Private Sub ThreadTask()

        CreaDataTable()

        Dim myTable As DataTable
        Dim ListaProduttori As List(Of Produttore)
        Dim smtp As New SmtpClient
        Dim Opzione As Opzione = Opzione.Carica

        myTable = DirectCast(Session("myDatatable"), DataTable)

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            If Produttore.Attivo And Not Produttore.SoloProfessionale And Produttore.EmailNotifiche <> "" Then
                'If Produttore.Attivo And Produttore.EmailNotifiche <> "" Then
                Dim DataDichiarazione As Date
                DataDichiarazione = CalcolaDataDichiarazione(Produttore.Periodicita, txtData.Text)
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione)
                If CurrentDichiarazione Is Nothing Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataDichiarazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    Select Case Produttore.Periodicita

                        Case 1
                            row("Periodicità") = "Mensile"
                        Case 2
                            row("Periodicità") = "Bimestrale"
                        Case 3
                            row("Periodicità") = "Trimestrale"

                    End Select

                    If Produttore.Email = "" Then
                        row("Mail") = "NO"
                    End If

                    smtp.Host = Opzione.Smtp
                    smtp.Port = Opzione.Porta
                    smtp.EnableSsl = Opzione.SSL
                    smtp.UseDefaultCredentials = False
                    smtp.Credentials = New NetworkCredential(Opzione.NomeUtente, Opzione.Password)
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

                    Dim NuovoMessaggio As New MailMessage

                    NuovoMessaggio.From = New MailAddress(Opzione.Mittente, "Gestore")
                    NuovoMessaggio.To.Add(New MailAddress(Produttore.EmailNotifiche, ""))
                    NuovoMessaggio.IsBodyHtml = True
                    NuovoMessaggio.Subject = Opzione.Oggetto
                    NuovoMessaggio.Body = Server.HtmlDecode(Opzione.TestoMail)
                    Try
                        smtp.Send(NuovoMessaggio)
                        row("Mail") = "Inviata"

                    Catch ex As Exception
                        row("Mail") = ex.Message

                    End Try

                    myTable.Rows.Add(row)
                    'Thread.Sleep(10000)
                End If
            End If
        Next

        Session("myDatatable") = myTable

        Listview1.DataSource = myTable
        Listview1.DataBind()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Operazione completata', 'Conferma');", True)

    End Sub

    Private Sub cmdAutodichiarazione_Click(sender As Object, e As EventArgs) Handles cmdAutodichiarazione.Click

        Dim conn As SqlConnection = New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString)
        Dim dtListaDichiarazioni As New DataTable()
        Dim dtRigheDichiarazioni As New DataTable()
        Dim ListaProduttori As List(Of Produttore)
        Dim addToList As Boolean
        Dim conta, NrDichTotali, NrDichMancanti As Integer
        Dim Divisore As Decimal

        conn.Open()

        dtListaDichiarazioni = CreateDataTable()

        dtRigheDichiarazioni.Columns.Add("IdCategoria", GetType(Integer))
        dtRigheDichiarazioni.Columns.Add("TipoDiDato", GetType(String))
        dtRigheDichiarazioni.Columns.Add("Pezzi", GetType(Integer))
        dtRigheDichiarazioni.Columns.Add("Kg", GetType(Decimal))
        dtRigheDichiarazioni.Columns.Add("KgDichiarazione", GetType(Decimal))
        dtRigheDichiarazioni.Columns.Add("CostoUnitario", GetType(Decimal))
        dtRigheDichiarazioni.Columns.Add("Importo", GetType(Decimal))

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            addToList = False

            If Produttore.Attivo And Not Produttore.SoloProfessionale Then
                Dim DataDichiarazione As Date
                Dim AnnoCertificazione, AnnoDichiarazione As Integer
                Dim UltimaAutocertificazione As Autocertificazione
                Dim CatTrovata As Categoria_ProduttoreNew
                Dim Periodicita As Integer

                If Produttore.Id = 929 Then
                    Produttore.Attivo = Produttore.Attivo
                End If

                DataDichiarazione = CalcolaDataDichiarazione(Produttore.Periodicita, txtData.Text)
                AnnoDichiarazione = Year(DataDichiarazione)
                AnnoCertificazione = Year(DataDichiarazione) - 1
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione)
                If CurrentDichiarazione Is Nothing Then
                    addToList = True
                End If

                If addToList Then
                    conta += 1
                    Dim row As DataRow
                    row = dtListaDichiarazioni.NewRow()
                    row("Data") = DataDichiarazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    Select Case Produttore.Periodicita

                        Case 1
                            row("Periodicità") = "Mensile"
                            Periodicita = 12
                        Case 2
                            row("Periodicità") = "Bimestrale"
                            Periodicita = 6
                        Case 3
                            row("Periodicità") = "Trimestrale"
                            Periodicita = 4

                    End Select

                    dtListaDichiarazioni.Rows.Add(row)

                    ' IDentifica le categorie abbinate al produttore
                    Dim ListaCat As List(Of Categoria_ProduttoreNew)
                    ListaCat = Categoria_ProduttoreNew.Lista(Produttore.Id, False)

                    If ListaCat.Count = 0 Then
                        Exit Sub
                    End If

                    ' Verifica sia presente un'autocertificazione per l'anno precedente
                    UltimaAutocertificazione = Autocertificazione.Carica(Produttore.Id, AnnoCertificazione)
                    If Not UltimaAutocertificazione Is Nothing Then
                        ' Carica la lista delle righe
                        Dim ListaRigheCert As List(Of RigaAutocertificazione)

                        ListaRigheCert = RigaAutocertificazione.Lista(UltimaAutocertificazione.Id)
                        If ListaRigheCert Is Nothing Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Non ci sono righe nella certificazione del produttore " & Produttore.RagioneSociale & ".', 'Errore');", True)
                            Exit Sub
                        End If

                        For Each riga In ListaRigheCert
                            CatTrovata = ListaCat.Find(Function(p) p.IdCategoria = riga.IdCategoria)
                            If Not CatTrovata Is Nothing Then

                                ' Aggiunge la categoria alla lista delle righe della nuova dichiarazioni
                                Dim NewRow As DataRow

                                NewRow = dtRigheDichiarazioni.NewRow()
                                NewRow("IdCategoria") = CatTrovata.IdCategoria
                                NewRow("TipoDiDato") = riga.TipoDiDato
                                If riga.PezziRettifica > 0 Then
                                    NewRow("Pezzi") = Math.Round(riga.PezziRettifica / Periodicita * 1.1, 1)
                                Else
                                    NewRow("Pezzi") = 0
                                End If
                                If riga.KgRettifica > 0 Then
                                    NewRow("Kg") = Math.Round(riga.KgRettifica / Periodicita * 1.1, MidpointRounding.AwayFromZero)
                                Else
                                    NewRow("Kg") = 0
                                End If
                                NewRow("CostoUnitario") = CatTrovata.Costo

                                If riga.KgRettifica > 0 Then
                                    NewRow("KgDichiarazione") = Math.Round(riga.KgRettifica * 1.1)
                                Else
                                    NewRow("KgDichiarazione") = 0
                                End If

                                dtRigheDichiarazioni.Rows.Add(NewRow)

                                'Elimina dalla lista 
                                ListaCat.Remove(CatTrovata)
                            End If
                        Next

                    End If

                    ' Se ultima certificazione assente verifica le dichiarazioni mancanti
                    If UltimaAutocertificazione Is Nothing Then
                        NrDichMancanti = DichMancanti(Produttore)

                        'If NrDichMancanti > 0 Then
                        Divisore = Periodicita - NrDichMancanti
                        'End If

                    End If

                    For Each CatProd In ListaCat

                        ' Aggiunge la categoria alla lista delle righe della nuova dichiarazioni
                        Dim NewRow As DataRow
                        Dim CatNew As CategoriaNew
                        Dim ValoreForecast As Decimal

                        CatNew = CategoriaNew.Carica(CatProd.IdCategoria)

                        If Divisore <> 0 Then
                            ValoreForecast = CatProd.ValoreDiForecast / Divisore
                        Else
                            ValoreForecast = CatProd.ValoreDiForecast
                        End If

                        NewRow = dtRigheDichiarazioni.NewRow()
                        NewRow("IdCategoria") = CatProd.IdCategoria
                        NewRow("TipoDiDato") = CatNew.TipoDiDato
                        If CatNew.TipoDiDato = "Valore" Then
                            NewRow("Kg") = Math.Round(ValoreForecast, MidpointRounding.AwayFromZero)
                            NewRow("KgDichiarazione") = Math.Round(ValoreForecast, MidpointRounding.AwayFromZero)
                            NewRow("Pezzi") = 0
                        Else
                            NewRow("Pezzi") = Math.Round(ValoreForecast, 1)
                            NewRow("Kg") = 0
                        End If

                        NewRow("CostoUnitario") = CatProd.Costo

                        dtRigheDichiarazioni.Rows.Add(NewRow)

                    Next

                    ' Aggiunge la dichiarazione
                    If dtRigheDichiarazioni.Rows.Count > 0 Then
                        Dim NuovaDichiarazione As New Dichiarazione

                        NuovaDichiarazione.IdProduttore = Produttore.Id
                        NuovaDichiarazione.Data = DataDichiarazione
                        NuovaDichiarazione.DataRegistrazione = Today.ToShortDateString
                        NuovaDichiarazione.Utente = Page.User.Identity.Name
                        NuovaDichiarazione.Confermata = True
                        NuovaDichiarazione.DataConferma = Today.ToShortDateString
                        NuovaDichiarazione.Autostimata = True
                        NuovaDichiarazione.Save()

                        For Each RigaDichiarazione In dtRigheDichiarazioni.Rows
                            Dim NuovaRiga As New RigaDichiarazione

                            Dim CategorieProduttore As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(RigaDichiarazione("IdCategoria"), Produttore.Id, False)

                            If  CategorieProduttore Isnot Nothing Then

                                NuovaRiga.IdCategoria = CategorieProduttore.IdCategoria
                                NuovaRiga.IdDichiarazione = NuovaDichiarazione.Id
                                NuovaRiga.TipoDiDato = RigaDichiarazione("TipoDiDato")
                                NuovaRiga.CostoUnitario = CategorieProduttore.Costo
                                If RigaDichiarazione("Pezzi") <> 0 Then
                                    NuovaRiga.Pezzi = RigaDichiarazione("Pezzi")
                                    NuovaRiga.KgDichiarazione = NuovaRiga.Pezzi * CategorieProduttore.Peso
                                    NuovaRiga.Importo = Math.Round(NuovaRiga.CostoUnitario * NuovaRiga.Pezzi, 2)
                                End If
                                If RigaDichiarazione("Kg") <> 0 Then
                                    NuovaRiga.Kg = RigaDichiarazione("Kg")
                                    NuovaRiga.KgDichiarazione = RigaDichiarazione("Kg")
                                    NuovaRiga.Importo = Math.Round(NuovaRiga.CostoUnitario * NuovaRiga.Kg, 2)
                                End If
                                NuovaRiga.UtenteAggiornamento = "Autostima"
                                NuovaRiga.DataAggiornamento = Today

                                NuovaRiga.Save()
                            End If
                        Next
                    End If
                    dtListaDichiarazioni.Clear()
                    dtRigheDichiarazioni.Clear()

                End If
            End If
        Next

        conn.Close()

        Genera()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Operazione completata. " & conta.ToString & " dichiarazioni generate.', 'Conferma');", True)

    End Sub

    Private Sub Listview1_PreRender(sender As Object, e As EventArgs) Handles Listview1.PreRender
        Genera()
    End Sub
End Class
