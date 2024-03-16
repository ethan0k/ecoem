
Imports System.IO
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports iTextSharp.text.pdf
Imports iTextSharp.text

Partial Class WebApp_Tariffari_Certificati
    Inherits System.Web.UI.Page

    Dim CurrentUser As MembershipUser
    Private Sub WebApp_Dichiarazioni_Certificati_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.IsInRole("Produttore") Then
            Dim Produttore As Produttore
            CurrentUser = Membership.GetUser()
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)
            Produttore = Produttore.Carica(UtenteProduttore.IdProduttore)
            ddlProduttore.SelectedValue = Produttore.Id
            ddlProduttore.Enabled = False
            GeneraCertificati(Produttore.Id)
        End If

    End Sub

    Private Sub GeneraCertificati(ByVal IdProduttore As Integer)

        Dim DataIscrizione As Date?
        Dim DataDisiscrizione As Date?
        Dim AnnoInizio As Integer
        Dim AnnoFine As Integer
        Dim i As Integer

        ' Genera se iscritto certificato domestici
        If chkProduttore(False, DataIscrizione, DataDisiscrizione, IdProduttore) Then
            AnnoInizio = Year(DataIscrizione)
            If (DataDisiscrizione Is Nothing) Or (DataDisiscrizione = DefaultValues.GetDateTimeMinValue) Then
                AnnoFine = Year(Today)
            Else
                AnnoFine = Year(DataDisiscrizione)
            End If

            For i = AnnoInizio To AnnoFine
                Dim Certificato As Certificato = Certificato.Carica(IdProduttore, i)
                If Certificato Is Nothing Then
                    Certificato = New Certificato
                    Certificato.Anno = i
                    Certificato.IdProduttore = IdProduttore
                    Certificato.Protocollo = "AD0101" & i.ToString & "/EO-" & IdProduttore.ToString
                    Certificato.Save()
                End If
            Next

        End If


        SqlDatasource1.SelectCommand = "Select Id, Anno, Protocollo FROM tbl_Certificati Where IdProduttore = " & IdProduttore.ToString

        Listview1.DataBind()

    End Sub

    Private Sub ddlProduttore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduttore.SelectedIndexChanged

        GeneraCertificati(ddlProduttore.SelectedValue)

        SqlDatasource1.SelectCommand = "Select Id, Anno, Protocollo FROM tbl_Certificati Where IdProduttore = " & ddlProduttore.SelectedValue

        Listview1.DataBind()

    End Sub

    Private Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Certificato") Then

            Dim Certificato As Certificato = Certificato.Carica(e.CommandArgument.ToString())
            Dim Produttore As Produttore = Produttore.Carica(Certificato.IdProduttore)
            Dim outputPdfStream As Stream = Response.OutputStream
            Dim strTemplateStampa As String

            Response.Charset = "utf-8"
            Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
            Response.CacheControl = "public"
            Response.AddHeader("Content-Disposition", "attachment; filename=Certificato_" & Produttore.RagioneSociale & "_" & Certificato.Anno & ".pdf")
            Response.ContentType = "application/pdf"

            strTemplateStampa = Server.MapPath("~/Template/CertificatoAdesione.pdf")

            Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
            Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)

            Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
            Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibri.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

            Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)
            bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibrib.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)


            WriteToPdf(cb, bf, 10, 85, 184, Certificato.Protocollo)
            WriteToPdf(cb, bf, 10, 460, 470, Certificato.Anno)

            WriteToPdf(cb, bf, 20, 245, 495, Produttore.RagioneSociale)
            WriteToPdf(cb, bf, 20, 245, 515, Produttore.PartitaIVA)


            stamper.Close()

            Response.End()

        End If


    End Sub

    Function chkProduttore(Professionale As Boolean, ByRef DataIscrizione As Date?, ByRef DataDisiscrizione As Date?, ByVal IdProduttore As Integer) As Boolean

        Dim LogIscrizione As LogIscrizione

        ' Verifica se al produttore è assegnata la categoria 4.14 (ID 30)
        LogIscrizione = LogIscrizione.CaricaDaProduttore(IdProduttore)

        If LogIscrizione Is Nothing Then
            Return False
        Else
            DataIscrizione = LogIscrizione.DataIscrizione
            DataDisiscrizione = LogIscrizione.DataDisiscrizione
            Return True
        End If


    End Function

    Private Sub WriteToPdf(cb As PdfContentByte, bf As BaseFont, fSize As Integer, x As Single, y As Single, text As String)
        cb.BeginText()
        cb.SetFontAndSize(bf, fSize)
        cb.SetTextMatrix(x, (PageSize.A4.Height - (y)))
        cb.ShowText(text)
        cb.EndText()
    End Sub
End Class
