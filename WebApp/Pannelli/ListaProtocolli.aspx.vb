Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Data
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.IO
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports System.Diagnostics

Partial Class WebApp_Pannelli_ListaProtocolli
    Inherits System.Web.UI.Page

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Protocollo.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then
            Dim ProtocolloDaEliminare As Protocollo = Protocollo.CaricaDaId(e.CommandArgument.ToString())
            ProtocolloDaEliminare.Elimina()


            Listview1.DataBind()
            'Else
        ElseIf String.Equals(e.CommandName, "Certificato") Then
            ' Stampa attestato di conformità
            Dim Prot As Protocollo = Protocollo.CaricaDaId(e.CommandArgument.ToString())
            If Not Prot.Conforme Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Protocollo non conforme.', 'Errore');", True)
                Exit Sub
            Else
                Dim Document As PdfDocument = New PdfDocument
                Dim Document2 As PdfDocument = New PdfDocument

                Document.Info.Title = "Attestato"

                Dim strTemplateStampa As String
                Dim strTemplateStampa2 As String
                Dim riga As Integer
                Dim Righe As Integer
                Dim Colonna As Integer
                Dim Pagina As Integer
                Dim ListaMarche As List(Of String) = Prot.ListaMarche
                Dim ListaModelli As List(Of String) = Prot.ListaModelli
                Dim ListaPannelli As List(Of String) = Prot.ListaPannelli
                Dim NrAttestato As String = Prot.NrAttestato
                Dim IdProd As Integer = Prot.GetIdProduttore
                Dim Produttore As Produttore = Produttore.Carica(IdProd)
                Dim myProd As Produttore = Produttore.Carica(IdProd)

                strTemplateStampa = Server.MapPath("~/Template/AttestatoConformita_New.pdf")
                strTemplateStampa2 = Server.MapPath("~/Template/Carta.pdf")

                If NrAttestato = "" Then
                    Dim NrAttestati As Integer = Prot.NrAttestatiProduttore(IdProd)
                    NrAttestati += 1
                    NrAttestato = "AD" + Day(Today).ToString("00") + Month(Today).ToString + Year(Today).ToString + "EO" + Produttore.Codice.ToString + NrAttestati.ToString("00")
                    Prot.IdProduttore = IdProd
                    Prot.NrAttestato = NrAttestato
                    Prot.DataAttestato = Today
                    Prot.Save()
                End If

                Document = PdfReader.Open(strTemplateStampa)
                Document2 = PdfReader.Open(strTemplateStampa2)
                Dim image As XImage = XImage.FromFile(Server.MapPath("~/Template/Logo.png"))

                Dim page As PdfPage = Document.Pages(0)
                Dim Page2 As PdfPage = Document2.Pages(0)
                Dim Page3 As PdfPage
                Dim gfx As XGraphics = XGraphics.FromPdfPage(page)

                'Dim pen As XPen = New XPen(XColor.FromArgb(255, 0, 0))
                Dim font As XFont = New XFont("Verdana", 9, XFontStyle.Bold)

                gfx.DrawString(NrAttestato, font, XBrushes.Black, New XRect(125, 140, 0, 0), XStringFormats.TopLeft)
                gfx.DrawString(Prot.Protocollo, font, XBrushes.Black, New XRect(125, 635, 0, 0), XStringFormats.TopLeft)
                gfx.DrawString(Prot.GetProduttore + " C.F. " + myProd.CodiceFiscale, font, XBrushes.Black, New XRect(0, 0, 600, 660), XStringFormats.Center)

                riga = 433
                Colonna = 60
                For Each MarcaProt As String In ListaMarche
                    Righe += 1
                    If Righe > 23 Then
                        Righe = 1
                        Colonna = 190
                        riga = 433
                    End If
                    gfx.DrawString(MarcaProt, New XFont("Verdana", 8, XFontStyle.Regular), XBrushes.Black, New XRect(Colonna, riga, 0, 0), XStringFormats.TopLeft)
                    riga += 8
                Next

                riga = 433
                Colonna = 300
                Righe = 0
                For Each ModelloProt As String In ListaModelli
                    Righe += 1
                    If Righe > 23 And Colonna < 410 Then
                        Righe = 1
                        Colonna = 410
                        riga = 433
                    ElseIf Righe > 23 Then
                        Righe = 1
                        Colonna = 500
                        riga = 433
                    End If
                    gfx.DrawString(ModelloProt, New XFont("Verdana", 8, XFontStyle.Regular), XBrushes.Black, New XRect(Colonna, riga, 0, 0), XStringFormats.TopLeft)
                    riga += 8
                Next

                ' Secondo page
                Page3 = Document.Pages(1)
                page = Document.Pages(1)
                gfx = XGraphics.FromPdfPage(page)

                riga = 170
                Colonna = 60
                Pagina = 1
                For Each PannelloProt In ListaPannelli
                    If Righe > 70 And Colonna = 60 Then
                        riga = 170
                        Colonna = 180
                        Righe = 0
                    ElseIf Righe > 70 And Colonna = 180 Then
                        riga = 170
                        Colonna = 300
                        Righe = 0
                    ElseIf Righe > 70 And Colonna = 300 Then
                        riga = 170
                        Colonna = 420
                        Righe = 0
                    ElseIf Righe > 70 And Colonna = 420 Then
                        'gfx.Dispose()                        
                        Pagina += 1
                        ' Aggiunge il numero di pagina
                        gfx.DrawString("pag. " & Pagina, New XFont("Verdana", 8, XFontStyle.Regular), XBrushes.Black, New XRect(520, 750, 0, 0), XStringFormats.TopLeft)
                        Document.AddPage()
                        page = Document.Pages(Pagina)
                        ''gfx.DrawRectangle(Pen, 60, 170, 500, 600)
                        gfx = XGraphics.FromPdfPage(page)
                        gfx.DrawImage(image, 270, 55, 70, 100)
                        riga = 170
                        Colonna = 60
                        Righe = 0

                    End If

                    'If Colonna < 460 Then
                    gfx.DrawString(PannelloProt, New XFont("Verdana", 8, XFontStyle.Regular), XBrushes.Black, New XRect(Colonna, riga, 0, 0), XStringFormats.TopLeft)
                        riga += 8
                        Righe += 1
                    '                    End If

                Next

                ' Aggiunge il numero di pagina
                gfx.DrawString("pag. " & Pagina + 1, New XFont("Verdana", 8, XFontStyle.Regular), XBrushes.Black, New XRect(520, 750, 0, 0), XStringFormats.TopLeft)

                Dim stream As MemoryStream = New MemoryStream()
                document.Save(stream, False)

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("Content-Disposition", "attachment; filename=Attestato.pdf")
                Response.BinaryWrite(stream.ToArray())
                Response.Flush()
                stream.Close()
                Response.[End]()

            End If
        End If


    End Sub

    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()
        dt.Columns.Remove("Id")
        dt.Columns("NrFattura").ColumnName = "Nr. fattura"
        dt.Columns("NrPannelli").ColumnName = "Nr. pannelli"
        dt.Columns("CostoServizio").ColumnName = "Costo servizio"
        dt.Columns("DataFattura").ColumnName = "Data fattura"
        dt.Columns("NrProforma").ColumnName = "Nr. proforma"
        dt.Columns("DataProforma").ColumnName = "Data proforma"
        dt.Columns("RagioneSociale").ColumnName = "Produttore"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaProtocolli")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:L1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("D1:D1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:L1048576")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaProtocolli.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        txtProtocollo.Text = String.Empty
    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound
        If (e.Item.ItemType = ListViewItemType.DataItem) Then
            If (Page.User.IsInRole("Operatore")) Then
                Dim myLinkDeleteButton As LinkButton = CType(e.Item.FindControl("DeleteButton"), LinkButton)
                myLinkDeleteButton.Visible = False
            ElseIf Page.User.IsInRole("Produttore") Then
                Dim myLinkDeleteButton As LinkButton = CType(e.Item.FindControl("DeleteButton"), LinkButton)
                Dim myLinkEditButton As LinkButton = CType(e.Item.FindControl("EditButton"), LinkButton)
                myLinkDeleteButton.Visible = False
                myLinkEditButton.Visible = False
            End If
        End If

    End Sub

    Private Sub WebApp_Pannelli_ListaProtocolli_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Not Page.IsPostBack Then
            If Page.User.IsInRole("Produttore") Then
                Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
                Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(userGuid)
                ddlProduttori.SelectedValue = UtenteProduttore.IdProduttore

                cmdEsporta.Visible = False

            End If
        End If
    End Sub

    Private Sub SqlDatasource1_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDatasource1.Selecting

        If Page.User.IsInRole("Produttore") Then
            Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(userGuid)
            e.Command.Parameters("@IdProduttore").Value = UtenteProduttore.IdProduttore
        Else
            e.Command.Parameters("@IdProduttore").Value = -1
        End If

    End Sub
End Class
