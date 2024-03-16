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
        dt.Columns.Add("Periodicità", System.Type.[GetType]("System.String"))
        dt.Columns.Add("Mail", System.Type.[GetType]("System.String"))

        Return dt

    End Function

    Protected Sub cmdVerifica_Click(sender As Object, e As EventArgs) Handles cmdVerifica.Click

        Genera()

    End Sub

    Private Function CalcolaDataDichiarazione() As Date

        Dim DataDichiarazione As Date

        DataDichiarazione = DateSerial(Year(txtData.Text) - 1, 12, 31)

        Return DataDichiarazione

    End Function

    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        CreaDataTable()

        Dim myTable As DataTable
        Dim ListaProduttori As List(Of Produttore)

        myTable = DirectCast(Session("myDatatable"), DataTable)

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            If Produttore.Attivo And Produttore.Professionale Then
                Dim DataDichiarazione As Date
                DataDichiarazione = CalcolaDataDichiarazione()
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione, True)
                If CurrentDichiarazione Is Nothing Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataDichiarazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    row("Mail") = Produttore.EmailNotifiche
                    row("Periodicità") = "Annuale"
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

        myTable = DirectCast(Session("myDatatable"), DataTable)

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            If Produttore.Attivo And Produttore.Professionale Then
                Dim DataDichiarazione As Date
                DataDichiarazione = CalcolaDataDichiarazione()
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione, True)
                If CurrentDichiarazione Is Nothing Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataDichiarazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale

                    row("Periodicità") = "Annuale"

                    myTable.Rows.Add(row)

                End If
            End If
        Next

        Session("myDatatable") = myTable

        Listview1.DataSource = myTable
        Listview1.DataBind()


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
            If Produttore.Attivo And Produttore.Professionale And Produttore.EmailNotifiche <> "" Then
                Dim DataDichiarazione As Date
                DataDichiarazione = CalcolaDataDichiarazione()
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione, True)
                If CurrentDichiarazione Is Nothing Then
                    Dim row As DataRow
                    row = myTable.NewRow()
                    row("Data") = DataDichiarazione.ToShortDateString
                    row("IdProduttore") = Produttore.Id
                    row("Produttore") = Produttore.RagioneSociale
                    row("Periodicità") = "Annuale"
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
                    Thread.Sleep(10000)
                End If
            End If
        Next

        Session("myDatatable") = myTable

        Listview1.DataSource = myTable
        Listview1.DataBind()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Operazione completata', 'Conferma');", True)

    End Sub

    Private Sub cmdAutodichiarazione_Click(sender As Object, e As EventArgs) Handles cmdAutodichiarazione.Click



    End Sub
End Class
