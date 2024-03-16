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

Partial Class WebApp_Dichiarazioni_VerificaAutocertificazioni
    Inherits System.Web.UI.Page

    Private trd As Thread

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            txtData.Text = CalcolaDataCertificazione()
            If Page.User.IsInRole("Operatore") Then
                cmdInviaMail.Visible = False
            End If

        End If

        Genera()

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
        dt.Columns.Add("Mail", System.Type.[GetType]("System.String"))

        Return dt

    End Function

    Protected Sub cmdVerifica_Click(sender As Object, e As EventArgs) Handles cmdVerifica.Click

        Genera()

    End Sub

    Private Function CalcolaDataCertificazione() As Date

        Dim DataCertificazione As Date

        DataCertificazione = CDate("31/12/" & (Today.Year - 1).ToString)

        Return DataCertificazione

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
                Dim DataCertificazione As Date
                DataCertificazione = CalcolaDataCertificazione()
                Dim CurrentCertificazione As Autocertificazione = Autocertificazione.Carica(Produttore.Id, Year(DataCertificazione))
                If CurrentCertificazione Is Nothing Then
                    addToList = True
                ElseIf Not CurrentCertificazione.Confermata Then
                    addToList = True
                End If

                If addToList Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataCertificazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    row("Mail") = Produttore.EmailNotifiche
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
        Dim Conteggio As Integer

        myTable = DirectCast(Session("myDatatable"), DataTable)

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            addToList = False
            If Produttore.Attivo And Not Produttore.SoloProfessionale Then
                Dim DataCertificazione As Date
                DataCertificazione = CalcolaDataCertificazione()
                Dim CurrentCertificazione As Autocertificazione = Autocertificazione.Carica(Produttore.Id, Year(DataCertificazione))
                If Not CurrentCertificazione Is Nothing Then
                    If Not CurrentCertificazione.Confermata Then
                        addToList = True
                    End If
                End If

                If addToList Then
                    Conteggio += 1

                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataCertificazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    myTable.Rows.Add(row)
                End If
            End If
        Next

        Session("myDatatable") = myTable

        Listview1.DataSource = myTable
        Listview1.DataBind()

        Session("myDatatable") = Nothing
        Session.Contents.Remove("myDatatable")

        If Conteggio = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun record estratto', 'Conferma');", True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Generati " & Conteggio.ToString & " record.', 'Conferma');", True)
        End If


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
                Dim DataCertificazione As Date
                DataCertificazione = CalcolaDataCertificazione()
                Dim CurrentCertificazione As Autocertificazione = Autocertificazione.Carica(Produttore.Id, Year(DataCertificazione))
                If CurrentCertificazione Is Nothing Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataCertificazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale

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

    Private Sub Listview1_PreRender(sender As Object, e As EventArgs) Handles Listview1.PreRender
        Genera()
    End Sub
End Class
