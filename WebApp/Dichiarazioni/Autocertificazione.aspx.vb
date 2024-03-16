Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Font = iTextSharp.text.Font
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mime

Partial Class WebApp_Dichiarazioni_Autocertificazione
    Inherits System.Web.UI.Page

    Private CurrentAutocertificazione As Autocertificazione

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Dichiarazioni/ListaAutocertificazioni.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentAuto()

            If Not CurrentAutocertificazione Is Nothing Then

                txtAnno.Text = CurrentAutocertificazione.Anno
                ddlProduttore.SelectedValue = CurrentAutocertificazione.IdProduttore
                txtData.Text = CurrentAutocertificazione.Data
                chkCaricato.Checked = CurrentAutocertificazione.UploadEseguito 
                chkConfermata.Checked = CurrentAutocertificazione.Confermata
                chkRettificata.Checked =CurrentAutocertificazione.Rettificata

                Dim MyProduttore As Produttore
                MyProduttore = Produttore.Carica(CurrentAutocertificazione.IdProduttore)

                If Not MyProduttore.EsisteAEE(CurrentAutocertificazione.IdProduttore) Then
                    ctrlAEE.Visible = False                    
                    divFileUpload.Visible = False
                Else
                    liCaricaAee.Visible = Not CurrentAutocertificazione.Confermata
                    divUploadAee.Visible = Not CurrentAutocertificazione.Confermata
                End If

                If Not MyProduttore.EsistePILE(CurrentAutocertificazione.IdProduttore) Then
                    ctrlPILE.Visible = False
                    divPile.Visible = False
                Else
                    licaricaPile.Visible = Not CurrentAutocertificazione.Confermata
                    uploadPile.Visible = Not CurrentAutocertificazione.Confermata
                End If

                If Not MyProduttore.EsisteINDUSTRIAL(CurrentAutocertificazione.IdProduttore) Then
                    ctrlIndustrial.Visible = False
                    divIndustriali.Visible = False
                Else
                    upIndustria.Visible = Not CurrentAutocertificazione.Confermata
                    liCaricaIndustriali.Visible = Not CurrentAutocertificazione.Confermata
                End If

                If Not MyProduttore.EsisteVEICOLI(CurrentAutocertificazione.IdProduttore) Then
                    ctrlVeicoli.Visible = False
                    divVeicoli.Visible = False
                Else
                    divVeicoliUp.Visible = Not CurrentAutocertificazione.Confermata
                End If

                liScaricaAEE.Visible = CurrentAutocertificazione.NomeFile <> ""
                liScaricaIndustriali.Visible = CurrentAutocertificazione.NomeFileIndustriali <> ""
                liScaricaPile.Visible = CurrentAutocertificazione.NomeFilePile <> ""
                liScaricaVeicoli.Visible = CurrentAutocertificazione.NomeFileVeicoli <> ""
                chkRettificata.Checked = CurrentAutocertificazione.Rettificata
                cmdConferma.Visible = Not CurrentAutocertificazione.Confermata

                If Page.User.IsInRole("Amministratore") Then
                        cmdRiapri.Visible = CurrentAutocertificazione.Confermata
                    Else
                        cmdRiapri.Visible = False
                    End If
                End If
            End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaAutocertificazioni.aspx")
    End Sub

    Private Sub GetCurrentAuto()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentAutocertificazione = Autocertificazione.Carica(IdFromQueryString)
        End If

    End Sub

    Private Sub cmdAutocertificazione_Click(sender As Object, e As ImageClickEventArgs) Handles cmdAutocertificazione.Click

        GetCurrentAuto

        If CurrentAutocertificazione.Anno <= 2020 Then
            PrintAEE_OLD
        Else

            If Not CurrentAutocertificazione.RigheGenerate Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Accedere alla sezione rettifica prima di procedere.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

            Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
            Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
            Dim conn As New SqlConnection
            Dim strsql As String
            Dim Valore, ValoreR, ValoreS As Decimal
            Dim A, B, C, D, E_, E1, G, F As Decimal
            Dim Ar, Br, Cr, E_r, E1_r, Dr, Gr, Fr As Decimal
            Dim A_s, Bs, Cs, Es, E1_s, Ds, Gs, Fs As Decimal
            strsql = "select Raggruppamento, r.KgCertificazione, r.KgDiffCertificazione, r.KgCertSoci FROM tbl_Autocertificazioni a INNER JOIN " & _
                    "tbl_RigheAutocertificazione r ON a.Id = r.IdCertificazione INNER JOIN tbl_CategorieNew c ON c.Id = r.IdCategoria " & _
                    "WHERE Anno = " & txtAnno.Text & " And a.IdProduttore = " & ddlProduttore.SelectedValue & _
                    " And Raggruppamento Not Like 'Z%'"

            conn.ConnectionString = connString
            Dim dt As DataTable = New DataTable()
            Dim cmd As New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strsql

            Dim Adapter As New SqlDataAdapter(cmd)

            Adapter.Fill(dt)

            Response.Charset = "utf-8"
            Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
            Response.CacheControl = "public"
            Response.AddHeader("Content-Disposition", "attachment; filename=Autocert_AEE_" & Prod.RagioneSociale & "_" & CurrentAutocertificazione.Anno & ".pdf")
            Response.ContentType = "application/pdf"

            Dim outputPdfStream As Stream = Response.OutputStream
            Dim strTemplateStampa As String

            strTemplateStampa = Server.MapPath("~/Template/Last_Autocert_AEE.pdf")

            Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
            Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)

            Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
            Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibri.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)            

            Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)
            Dim TotaleKg, TotaleKgR,TotaleKgS As Decimal

            WriteToPdf(cb, bf, 10, 197, 218, Prod.RagioneSociale)
            WriteToPdf(cb, bf, 10, 145, 237, Prod.Citta)
            WriteToPdf(cb, bf, 10, 163, 255, Prod.Indirizzo)
            WriteToPdf(cb, bf, 10, 140, 275, Prod.CAP)
            WriteToPdf(cb, bf, 10, 230, 295, Prod.Rappresentante)

            WriteToPdf(cb, bf, 10, 156, 353, txtAnno.Text)

            For Each row As DataRow In dt.Rows

                Valore = Math.Round(CDec(row(1).ToString), 0)
                ValoreR = Math.Round(CDec(row(2).ToString), 0)
                ValoreS = Math.Round(CDec(row(3).ToString), 0)

                TotaleKg += Valore ' CDec(row(1).ToString)
                TotaleKgR += ValoreR
                TotaleKgS += ValoreS
                Select Case row(0)

                    Case "A"
                        If CDec(row(1).ToString) <> 0 Then
                            A += CDec(Valore.ToString)
                        End If

                        If CDec(row(2).ToString) <> 0 Then
                            Ar += CDec(ValoreR.ToString)
                        End If

                         If CDec(row(3).ToString) <> 0 Then
                            A_s += CDec(ValoreS.ToString)
                        End If

                    Case "B"
                        If CDec(row(1).ToString) <> 0 Then
                            B += CDec(Valore.ToString)
                        End If

                        If CDec(row(2).ToString) <> 0 Then
                            Br += CDec(ValoreR.ToString)
                        End If

                        If CDec(row(3).ToString) <> 0 Then
                            Bs += CDec(ValoreS.ToString)
                        End If

                    Case "C"
                        If CDec(row(1).ToString) <> 0 Then
                            C += CDec(Valore.ToString)
                        End If

                        If CDec(row(2).ToString) <> 0 Then
                            Cr += CDec(ValoreR.ToString)
                        End If

                         If CDec(row(3).ToString) <> 0 Then
                            Cs += CDec(ValoreS.ToString)
                        End If
                    Case "D"
                        If CDec(row(1).ToString) <> 0 Then
                            D += CDec(Valore.ToString)
                        End If

                        If CDec(row(2).ToString) <> 0 Then
                            Dr += CDec(ValoreR.ToString)
                        End If

                        If CDec(row(3).ToString) <> 0 Then
                            Ds += CDec(ValoreS.ToString)
                        End If
                    Case "E"
                        If CDec(row(1).ToString) <> 0 Then
                            E_ += CDec(Valore.ToString)
                        End If

                        If CDec(row(2).ToString) <> 0 Then
                            E_r += CDec(ValoreR.ToString)
                        End If

                        If CDec(row(3).ToString) <> 0 Then
                            Es += CDec(ValoreS.ToString)
                        End If
                    Case "E1"
                        If CDec(row(1).ToString) <> 0 Then
                            E1 += CDec(Valore.ToString)
                        End If

                        If CDec(row(2).ToString) <> 0 Then
                            E1_r += CDec(ValoreR.ToString)
                        End If

                        If CDec(row(3).ToString) <> 0 Then
                            E1_s += CDec(ValoreS.ToString)
                        End If
                    Case "F"
                        If CDec(row(1).ToString) <> 0 Then
                            F += CDec(Valore.ToString)
                        End If

                        If CDec(row(2).ToString) <> 0 Then
                            Fr += CDec(ValoreR.ToString)
                        End If

                        If CDec(row(3).ToString) <> 0 Then
                            Fs += CDec(ValoreS.ToString)
                        End If
                    Case Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Raggruppamento errato!', 'Errore');", True)
            Exit Sub

                End Select
            Next

            WriteToPdf(cb, bf, 8, 440, 402, B.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 380, 402, Br.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 320, 402, Bs.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 440, 420, A.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 380, 420, Ar.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 320, 420, A_s.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 440, 437, C.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 380, 437, Cr.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 320, 437, Cs.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 440, 455, D.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 380, 455, Dr.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 320, 455, Ds.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 440, 473, e_.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 380, 473, E_r.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 320, 473, Es.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 440, 490, E1.ToString("#,##0")) ' R4_pv
            WriteToPdf(cb, bf, 8, 380, 490, E1_r.ToString("#,##0")) ' R4_pv
            WriteToPdf(cb, bf, 8, 320, 490, E1_s.ToString("#,##0")) ' R4_pv

            WriteToPdf(cb, bf, 8, 440, 506, F.ToString("#,##0")) 'R5
            WriteToPdf(cb, bf, 8, 380, 506, Fr.ToString("#,##0")) 'R5
            WriteToPdf(cb, bf, 8, 320, 506, Fs.ToString("#,##0")) 'R5

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibrib.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

            WriteToPdf(cb, bf, 8, 440, 522, TotaleKg.ToString("#,##0")) ' TOTALE
            WriteToPdf(cb, bf, 8, 380, 522, TotaleKgR.ToString("#,##0")) ' TOTALE Rettifica
            WriteToPdf(cb, bf, 8, 320, 522, TotaleKgS.ToString("#,##0")) ' TOTALE dichiarato soci

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

            cb.SetFontAndSize(bf, 2)
            WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
            ' ct.Go()
            stamper.Close()

            Response.End()

        End If
    End Sub

    Private Sub WriteToPdf(cb As PdfContentByte, bf As BaseFont, fSize As Integer, x As Single, y As Single, text As String)
        cb.BeginText()
        cb.SetFontAndSize(bf, fSize)
        cb.SetTextMatrix(x, (PageSize.A4.Height - (y)))
        cb.ShowText(text)
        cb.EndText()
    End Sub

    Private Sub cmdAutoPile_Click(sender As Object, e As ImageClickEventArgs) Handles cmdAutoPile.Click

        GetCurrentAuto

        If CurrentAutocertificazione.Anno < 2020 Then
            PrintPile_old
        Else
            If Not CurrentAutocertificazione.RigheGenerate Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Accedere alla sezione rettifica prima di procedere.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

            Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
            Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
            Dim conn As New SqlConnection
            Dim strsql As String
            Dim Valore, ValoreR, ValoreS As Decimal
            Dim Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10, Z11 As Decimal
            Dim Zr1, Zr2, Zr3, Zr4, Zr5, Zr6, Zr7, Zr8, Zr9, Zr10, Zr11 As Decimal
            Dim Zs1, Zs2, Zs3, Zs4, Zs5, Zs6, Zs7, Zs8, Zs9, Zs10, Zs11 As Decimal

             strsql = "select Raggruppamento, r.KgCertificazione, r.KgDiffCertificazione, r.KgCertSoci FROM tbl_Autocertificazioni a INNER JOIN " & _
                        "tbl_RigheAutocertificazione r ON a.Id = r.IdCertificazione INNER JOIN tbl_CategorieNew c ON c.Id = r.IdCategoria " & _
                        "WHERE Anno = " & txtAnno.Text & " And a.IdProduttore = " & ddlProduttore.SelectedValue & _
                        "And Raggruppamento Like 'Z%' "

            conn.ConnectionString = connString
            Dim dt As DataTable = New DataTable()
            Dim cmd As New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strsql

            Dim Adapter As New SqlDataAdapter(cmd)

            Adapter.Fill(dt)

            Response.Charset = "utf-8"
            Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
            Response.CacheControl = "public"
            Response.AddHeader("Content-Disposition", "attachment; filename=AutocertificazionePile_" & Prod.RagioneSociale & "_" & CurrentAutocertificazione.Anno & ".pdf")
            Response.ContentType = "application/pdf"

            Dim outputPdfStream As Stream = Response.OutputStream
            Dim strTemplateStampa As String
            Dim TotaleKg, TotalerKg, TotalesKg As Decimal

            strTemplateStampa = Server.MapPath("~/Template/Last_Autocert_Pile.pdf")

            Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
            Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)
            Dim Totale As Decimal

            Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
            Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Arial.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
            Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)

            WriteToPdf(cb, bf, 10, 90, 322, txtAnno.Text)

            WriteToPdf(cb, bf, 10, 197, 219, Prod.RagioneSociale)
            WriteToPdf(cb, bf, 10, 146, 237, Prod.Citta)
            WriteToPdf(cb, bf, 10, 165, 257, Prod.Indirizzo)
            WriteToPdf(cb, bf, 10, 145, 276, Prod.CAP)
            WriteToPdf(cb, bf, 10, 230, 295, Prod.Rappresentante)

            For Each row As DataRow In dt.Rows
                Valore = Math.Round(CDec(row(1).ToString), 0)
                ValoreR = Math.Round(CDec(row(2).ToString), 0)
                ValoreS = Math.Round(CDec(row(3).ToString), 0)

                Select Case row(0)

                    Case "Z1"
                        Z1 += CDec(row(1))
                        TotaleKg += Valore 

                        Zr1 += CDec(row(2))
                        TotalerKg += ValoreR

                        Zs1 += CDec(row(3))
                        TotalesKg += ValoreS

                    Case "Z2"
                        Z2 += CDec(row(1))
                        TotaleKg += Valore 

                        Zr2 += CDec(row(2))
                        TotalerKg += ValoreR

                        Zs2 += CDec(row(3))
                        TotalesKg += Valores 
                    Case "Z3"
                        Z3 += CDec(row(1))
                        TotaleKg += Valore 

                        Zr3 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs3 += CDec(row(3))
                        TotalesKg += Valores

                    Case "Z4"
                        Z4 += CDec(row(1))
                        TotaleKg += Valore

                        Zr4 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs4 += CDec(row(3))
                        TotalesKg += ValoreS

                    Case "Z5"
                        Z5 += CDec(row(1))
                        TotaleKg += Valore

                        Zr5 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs5 += CDec(row(3))
                        TotalesKg += ValoreS

                    Case "Z6"
                        Z6 += CDec(row(1))
                        TotaleKg += Valore

                        Zr6 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs6 += CDec(row(3))
                        TotalesKg += Valores 
                    Case "Z7"
                        Z7 += CDec(row(1))
                        TotaleKg += Valore

                        Zr7 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs7 += CDec(row(3))
                        TotalesKg += ValoreS 

                    Case "Z8"
                        Z8 += CDec(row(1))
                        TotaleKg += Valore

                        Zr8 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs8 += CDec(row(3))
                        TotalesKg += ValoreS 
                    Case "Z9"
                        Z9 += CDec(row(1))
                        TotaleKg += Valore

                        Zr9 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs9 += CDec(row(3))
                        TotalesKg += ValoreS
                    Case "Z10"
                        Z10 += CDec(row(1))
                        TotaleKg += Valore

                        Zr10 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs10 += CDec(row(3))
                        TotalesKg += ValoreS
                    Case "Z11"
                        Z11 += CDec(row(1))
                        TotaleKg += Valore

                        Zr11 += CDec(row(2))
                        TotalerKg += ValoreR 

                        Zs11 += CDec(row(3))
                        TotalesKg += ValoreS 
                End Select

            Next

            WriteToPdf(cb, bf, 8, 396, 373, Zs1.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 373, Zr1.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 373, Z1.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 390, Zs2.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 390, Zr2.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 390, Z2.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 407, Zs3.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 407, Zr3.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 407, Z3.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 424, Zs4.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 424, Zr4.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 424, Z4.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 439, Zs5.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 439, Zr5.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 439, Z5.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 456, Zs6.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 456, Zr6.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 456, Z6.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 471, Zs7.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 471, Zr7.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 471, Z7.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 488, Zs8.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 488, Zr8.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 488, Z8.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 505, Zs9.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 505, Zr9.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 505, Z9.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 520, Zs10.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 520, Zr10.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 520, Z10.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 396, 538, Zs11.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 460, 538, Zr11.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 510, 538, Z11.ToString("#,##0"))

            bf= iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibrib.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)        

            WriteToPdf(cb, bf, 8, 396, 553, TotalesKg.ToString("#,##0")) ' TOTALE
            WriteToPdf(cb, bf, 8, 460, 553, TotalerKg.ToString("#,##0")) ' TOTALE
            WriteToPdf(cb, bf, 8, 510, 553, TotaleKg.ToString("#,##0")) ' TOTALE

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

            cb.SetFontAndSize(bf, 2)
            WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
            ' ct.Go()
            stamper.Close()

            Response.End()
        End If

    End Sub

    Private Function GetExtension(FileName As String) As String

        Dim RetVal As String
        Dim Posizione As Integer = FileName.LastIndexOf(".")

        RetVal = FileName.Substring(Posizione, Len(FileName) - Posizione)

        Return RetVal

    End Function

    Private Sub cmdVeicoli_Click(sender As Object, e As ImageClickEventArgs) Handles cmdVeicoli.Click

        GetCurrentAuto

        If CurrentAutocertificazione.Anno < 2020 Then
            PrintVeicoli_old
        Else
            If Not CurrentAutocertificazione.RigheGenerate Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Accedere alla sezione rettifica prima di procedere.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

            Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
            Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
            Dim conn As New SqlConnection
            Dim strsql As String
            Dim Valore, ValoreS, ValoreR As Decimal
            Dim Z26, Z27, Z28, Z29, Z30 As Decimal     
            Dim ZS26, ZS27, ZS28, ZS29, ZS30 As Decimal     
            Dim ZR26, ZR27, ZR28, ZR29, ZR30 As Decimal     

            strsql = "select Raggruppamento, r.KgCertificazione, r.KgDiffCertificazione, r.KgCertSoci FROM tbl_Autocertificazioni a INNER JOIN " & _
                        "tbl_RigheAutocertificazione r ON a.Id = r.IdCertificazione INNER JOIN tbl_CategorieNew c ON c.Id = r.IdCategoria " & _
                        "WHERE Anno = " & txtAnno.Text & " And a.IdProduttore = " & ddlProduttore.SelectedValue & _
                        "And Raggruppamento Like 'Z%' "

            conn.ConnectionString = connString
            Dim dt As DataTable = New DataTable()
            Dim cmd As New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strsql

            Dim Adapter As New SqlDataAdapter(cmd)

            Adapter.Fill(dt)

            Response.Charset = "utf-8"
            Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
            Response.CacheControl = "public"
            Response.AddHeader("Content-Disposition", "attachment; filename=AutoCert_Veicoli_" & Prod.RagioneSociale & "_" & CurrentAutocertificazione.Anno & ".pdf")
            Response.ContentType = "application/pdf"

            Dim outputPdfStream As Stream = Response.OutputStream
            Dim strTemplateStampa As String
            Dim TotaleKg, TotaleSKg, TotaleRkg As Decimal

            strTemplateStampa = Server.MapPath("~/Template/Last_AutoCert_Veicoli.pdf")

            Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
            Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)

            Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
            Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibri.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
            Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)

            WriteToPdf(cb, bf, 10, 110, 354, txtAnno.Text)

            WriteToPdf(cb, bf, 10, 200, 218, Prod.RagioneSociale)
            WriteToPdf(cb, bf, 10, 150, 237, Prod.Citta + " - citta")
            WriteToPdf(cb, bf, 10, 159, 256, Prod.Indirizzo + " - indirizzo")
            'WriteToPdf(cb, bf, 8, 200, 295, Prod.CAP)
            WriteToPdf(cb, bf, 10, 228, 294, Prod.Rappresentante)

            For Each row As DataRow In dt.Rows
                Valore = Math.Round(CDec(row(1).ToString), 0)
                ValoreR = Math.Round(CDec(row(2).ToString), 0)
                ValoreS = Math.Round(CDec(row(3).ToString), 0)


                If (row(0).ToString = "Z26") Or (row(0).ToString = "Z27") Or (row(0).ToString = "Z28") Or (row(0).ToString = "Z29") Or (row(0).ToString = "Z30") Then
                    TotaleKg += Valore
                    TotaleSKg += ValoreS
                    TotaleRkg += ValoreR
                End If


                Select Case row(0)

                    Case "Z26" ' Acc. per Veicoli al Piombo
                        Z26 += CDec(row(1))
                        ZR26 += CDec(row(2))
                        ZS26 += CDec(row(3))

                    Case "Z27" ' Acc. per Veicoli al Nichel-Cadmio
                        Z27 += CDec(row(1))
                        ZR27 += CDec(row(2))
                        ZS27 += CDec(row(3))

                    Case "Z28" ' Acc. per Veicoli al Litio
                        Z28 += CDec(row(1))
                        Zr28 += CDec(row(2))
                        ZS28 += CDec(row(3))
                    Case "Z29" ' Acc. per Veicoli Ni-Mh
                        Z29 += CDec(row(1))
                        ZR29 += CDec(row(2))
                        ZS29 += CDec(row(3))

                    Case "Z30" ' Altri Acc. per Veicoli
                        Z30 += CDec(row(1))
                        ZR30 += CDec(row(2))
                        ZS30 += CDec(row(3))

                End Select

            Next

            WriteToPdf(cb, bf, 8, 340, 404, Zs26.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 416, 404, Zr26.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 499, 404, Z26.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 340, 424, Zs27.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 416, 424, ZR27.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 499, 424, Z27.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 340, 440, ZS28.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 416, 440, ZR28.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 499, 440, Z28.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 340, 459, ZS29.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 416, 459, ZR29.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 499, 459, Z29.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 340, 477, ZS30.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 416, 477, ZR30.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 499, 477, Z30.ToString("#,##0"))
                
            bf= iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibrib.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)        

            WriteToPdf(cb, bf, 8, 340, 495, TotalesKg.ToString("#,##0")) ' TOTALE SOCI
            WriteToPdf(cb, bf, 8, 416, 495, TotalerKg.ToString("#,##0")) ' TOTALE RETTIFICA
            WriteToPdf(cb, bf, 8, 499, 495, TotaleKg.ToString("#,##0")) ' TOTALE

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

            cb.SetFontAndSize(bf, 2)
            WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
            ' ct.Go()
            stamper.Close()

            Response.End()

        End If
    End Sub

    Private Sub cmdIndustriali_Click(sender As Object, e As ImageClickEventArgs) Handles cmdIndustriali.Click

        GetCurrentAuto

        If CurrentAutocertificazione.Anno < 2020 Then
            PrintIndustriali_old
        Else
            If Not CurrentAutocertificazione.RigheGenerate Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Accedere alla sezione rettifica prima di procedere.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

            Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
            Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
            Dim conn As New SqlConnection
            Dim strsql As String
            Dim Valore, ValoreR, ValoreS As Decimal
            Dim Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20, Z21, Z22, Z23, Z24, Z25 As Decimal
            Dim ZS12, ZS13, ZS14, ZS15, ZS16, ZS17, ZS18, ZS19, ZS20, ZS21, ZS22, ZS23, ZS24, ZS25 As Decimal
            Dim ZR12, ZR13, ZR14, ZR15, ZR16, ZR17, ZR18, ZR19, ZR20, ZR21, ZR22, ZR23, ZR24, ZR25 As Decimal
        
            strsql = "select Raggruppamento, r.KgCertificazione, r.KgDiffCertificazione, r.KgCertSoci FROM tbl_Autocertificazioni a INNER JOIN " & _
                        "tbl_RigheAutocertificazione r ON a.Id = r.IdCertificazione INNER JOIN tbl_CategorieNew c ON c.Id = r.IdCategoria " & _
                        "WHERE Anno = " & txtAnno.Text & " And a.IdProduttore = " & ddlProduttore.SelectedValue & _
                        "And Raggruppamento Like 'Z%' "


            conn.ConnectionString = connString
            Dim dt As DataTable = New DataTable()
            Dim cmd As New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strsql

            Dim Adapter As New SqlDataAdapter(cmd)

            Adapter.Fill(dt)

            Response.Charset = "utf-8"
            Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
            Response.CacheControl = "public"
            Response.AddHeader("Content-Disposition", "attachment; filename=Autocert_Industriali_" & Prod.RagioneSociale & "_" & CurrentAutocertificazione.Anno & ".pdf")
            Response.ContentType = "application/pdf"

            Dim outputPdfStream As Stream = Response.OutputStream
            Dim strTemplateStampa As String
            Dim TotaleKg, TotaleSKg,TotaleRKg As Decimal

            strTemplateStampa = Server.MapPath("~/Template/Last_Autocert_Industriali.pdf")

            Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
            Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)

            Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
            Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Arial.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
            Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)

            WriteToPdf(cb, bf, 10, 110, 328, txtAnno.Text)

            WriteToPdf(cb, bf, 10, 200, 218, Prod.RagioneSociale)
            WriteToPdf(cb, bf, 10, 147, 238, Prod.Indirizzo + " - Indirizzo")
            WriteToPdf(cb, bf, 10, 160, 258, Prod.Citta+ " - Citta")
            WriteToPdf(cb, bf, 10, 229, 296, Prod.Rappresentante)

            For Each row As DataRow In dt.Rows
                Valore = Math.Round(CDec(row(1).ToString), 0)
                ValoreR = Math.Round(CDec(row(2).ToString), 0)
                ValoreS = Math.Round(CDec(row(3).ToString), 0)
            
                Select Case row(0)

                    Case "Z12" ' Acc. Industriali al Piombo
                        Z12 += CDec(row(1))
                        Zr12 += CDec(row(2))
                        Zs12 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 

                    Case "Z13" ' Acc. Industriali al Nichel-Cadmio
                        Z13 += CDec(row(1))
                        Zr13 += CDec(row(2))
                        Zs13 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 

                    Case "Z14" ' Acc. Industriali al Litio
                        Z14 += CDec(row(1))
                        Zr14 += CDec(row(2))
                        ZS14 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z15" ' Acc. Industriali Ni-Mh
                        Z15 += CDec(row(1))
                        ZR15 += CDec(row(2))
                        ZS15 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z16" ' Altri Acc. Industriali
                        Z16 += CDec(row(1))
                        ZR16 += CDec(row(2))
                        ZS16 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z17" ' Acc. per Veicoli al Piombo
                        Z17 += CDec(row(1))
                        ZR17 += CDec(row(2))
                        ZS17 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z18" ' Acc. per Veicoli al Nichel-Cadmio
                        Z18 += CDec(row(1))
                        ZR18 += CDec(row(2))
                        ZS18 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z19" ' Acc. per Veicoli al Litio
                        Z19 += CDec(row(1))
                        ZR19 += CDec(row(2))
                        ZS19 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z20" ' Acc. per Veicoli Ni-Mh
                        Z20 += CDec(row(1))
                        ZR20 += CDec(row(2))
                        ZS20 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z21" ' Altri Acc. per Veicoli
                        Z21 += CDec(row(1))
                        ZR21 += CDec(row(2))
                        ZS21 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z22" ' Altri Acc. per Veicoli
                        Z22 += CDec(row(1))
                        ZR22 += CDec(row(2))
                        ZS22 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z23" ' Altri Acc. per Veicoli
                        Z23 += CDec(row(1))
                        ZR23 += CDec(row(2))
                        ZS23 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z24" ' Altri Acc. per Veicoli
                        Z24 += CDec(row(1))
                        ZR24 += CDec(row(2))
                        ZS24 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                    Case "Z25" ' Altri Acc. per Veicoli
                        Z25 += CDec(row(1))
                        ZR25 += CDec(row(2))
                        ZS25 += CDec(row(3))
                        TotaleKg += Valore 
                        TotaleSKg += ValoreS
                        TotaleRKg += ValoreR 
                End Select

            Next

            WriteToPdf(cb, bf, 8, 400, 382, Zs12.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 382, Zr12.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 382, Z12.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 402, ZS13.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 402, Zr13.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 402, Z13.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 428, ZS14.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 428, ZR14.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 428, Z14.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 447, ZS15.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 447, ZR15.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 447, Z15.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 463, ZS16.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 463, ZR16.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 463, Z16.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 482, ZS17.ToString("#,##0")) ' Piombo
            WriteToPdf(cb, bf, 8, 470, 482, ZR17.ToString("#,##0")) ' Piombo
            WriteToPdf(cb, bf, 8, 528, 482, Z17.ToString("#,##0")) ' Piombo

            WriteToPdf(cb, bf, 8, 400, 506, ZS18.ToString("#,##0")) ' Nichel
            WriteToPdf(cb, bf, 8, 470, 506, ZR18.ToString("#,##0")) ' Nichel
            WriteToPdf(cb, bf, 8, 528, 506, Z18.ToString("#,##0")) ' Nichel

            WriteToPdf(cb, bf, 8, 400, 524, ZS19.ToString("#,##0")) ' Litio
            WriteToPdf(cb, bf, 8, 470, 524, ZR19.ToString("#,##0")) ' Litio
            WriteToPdf(cb, bf, 8, 528, 524, Z19.ToString("#,##0")) ' Litio

            WriteToPdf(cb, bf, 8, 400, 552, ZS20.ToString("#,##0")) ' NI
            WriteToPdf(cb, bf, 8, 470, 552, ZR20.ToString("#,##0")) ' NI
            WriteToPdf(cb, bf, 8, 528, 552, Z20.ToString("#,##0")) ' NI

            WriteToPdf(cb, bf, 8, 400, 580, ZS21.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 580, ZR21.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 580, Z21.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 594, ZS22.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 594, ZR22.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 594, Z22.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 623, ZS23.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 623, ZR23.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 623, Z23.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 640, ZS24.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 640, ZR24.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 640, Z24.ToString("#,##0"))

            WriteToPdf(cb, bf, 8, 400, 659, ZS25.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 470, 659, ZR25.ToString("#,##0"))
            WriteToPdf(cb, bf, 8, 528, 659, Z25.ToString("#,##0"))

            bf= iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Calibrib.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)        

            WriteToPdf(cb, bf, 8, 400, 676, TotaleSKg.ToString("#,##0")) ' TOTALE soci
            WriteToPdf(cb, bf, 8, 470, 676, TotaleRKg.ToString("#,##0")) ' TOTALE rett
            WriteToPdf(cb, bf, 8, 528, 676, TotaleKg.ToString("#,##0")) ' TOTALE

            bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

            cb.SetFontAndSize(bf, 2)
            WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
            ' ct.Go()
            stamper.Close()

            Response.End()

         End If

    End Sub

    Protected Sub cmdCarica_Click(sender As Object, e As EventArgs) Handles cmdCarica.Click

        Dim fileName As String
        Dim Ext As String
        Dim myProduttore As Produttore
        Dim UploadCompletato As Boolean = True
        Dim size As Decimal

        If Not fileUpload.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        size = Math.Round((CDec(fileUpload.PostedFile.ContentLength) / CDec(1024)), 2)

        If size > 5000 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dimensione file eccessiva! Kb:" & size.ToString & "', 'Errore');", True)
            Exit Sub
        End If

        Ext = GetExtension(fileUpload.FileName)

        Select Case Ext

            Case ".pdf", ".jpg", ".PDF", ".JPG"

            Case Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipo file non permesso!', 'Errore');", True)
                Exit Sub

        End Select

        GetCurrentAuto()

        myProduttore = Produttore.Carica(CurrentAutocertificazione.IdProduttore)

        fileName = "Autocertificazione_" & myProduttore.RagioneSociale & "_" & CurrentAutocertificazione.Anno

        fileName = fileName.Replace(".", "_")
        fileName = fileName.Replace(",", "_")
        fileName = fileName.Replace(")", " ")
        fileName = fileName.Replace("(", " ")

        fileName = fileName + Ext

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath"))

        If Not Directory.Exists(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0")) Then
            Directory.CreateDirectory(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0"))
        End If

        Dim PathFile As String = folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName


        Try
            fileUpload.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Exit Sub

        End Try

        CurrentAutocertificazione.NomeFile = fileName
        CurrentAutocertificazione.PathFile = System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath") & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName
        CurrentAutocertificazione.Save()

        If myProduttore.EsisteAEE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsistePILE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFilePile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteVEICOLI(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileVeicoli <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteINDUSTRIAL(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileIndustriali <> "") Then
            UploadCompletato = False
        End If

        CurrentAutocertificazione.UploadEseguito = UploadCompletato
        CurrentAutocertificazione.Save()

        chkCaricato.Checked = True
        cmdScaricaAEE.Visible = True

        Response.Redirect(Request.RawUrl, True)

    End Sub

    Private Sub cmdCaricaPile_Click(sender As Object, e As EventArgs) Handles cmdCaricaPile.Click

        Dim fileName As String
        Dim Ext As String
        Dim myProduttore As Produttore
        Dim UploadCompletato As Boolean = True
        Dim size As Decimal

        If Not fileUploadPile.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        size = Math.Round((CDec(fileUploadPile.PostedFile.ContentLength) / CDec(1024)), 2)

        If size > 5000 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dimensione file eccessiva! Kb:" & size.ToString & "', 'Errore');", True)
            Exit Sub
        End If

        Ext = GetExtension(fileUploadPile.FileName)

        Select Case Ext

            Case ".pdf", ".jpg", ".PDF", ".JPG"

            Case Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipo file non permesso!', 'Errore');", True)
                Exit Sub

        End Select

        GetCurrentAuto()

        myProduttore = Produttore.Carica(CurrentAutocertificazione.IdProduttore)

        fileName = "Autocertificazione_Pile" & "_" & CurrentAutocertificazione.Anno & Ext

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath"))

        If Not Directory.Exists(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0")) Then
            Directory.CreateDirectory(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0"))
        End If

        Dim PathFile As String = folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName

        Try
            fileUploadPile.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & "', 'Errore');", True)
            Exit Sub

        End Try

        CurrentAutocertificazione.NomeFilePile = fileName
        CurrentAutocertificazione.PathFilePile = System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath") & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName
        CurrentAutocertificazione.Save()

        If myProduttore.EsisteAEE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsistePILE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFilePile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteVEICOLI(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileVeicoli <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteINDUSTRIAL(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileIndustriali <> "") Then
            UploadCompletato = False
        End If

        CurrentAutocertificazione.UploadEseguito = UploadCompletato
        CurrentAutocertificazione.Save()

        chkCaricato.Checked = True
        cmdScaricaPIle.Visible=True

        Response.Redirect(Request.RawUrl,True)

    End Sub

    Private Sub cmdCaricaVeicoli_Click(sender As Object, e As EventArgs) Handles cmdCaricaVeicoli.Click

        Dim fileName As String
        Dim Ext As String
        Dim myProduttore As Produttore
        Dim UploadCompletato As Boolean = True
        Dim size As Decimal

        If Not fileUploadVeicoli.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        size = Math.Round((CDec(fileUploadVeicoli.PostedFile.ContentLength) / CDec(1024)), 2)

        If size > 5000 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dimensione file eccessiva! Kb:" & size.ToString & "', 'Errore');", True)
            Exit Sub
        End If

        Ext = GetExtension(fileUploadVeicoli.FileName)

        Select Case Ext

            Case ".pdf", ".jpg", ".PDF", ".JPG"

            Case Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipo file non permesso!', 'Errore');", True)
                Exit Sub

        End Select

        GetCurrentAuto()

        myProduttore = Produttore.Carica(CurrentAutocertificazione.IdProduttore)

        fileName = "Autocertificazione_Veicoli" & "_" & CurrentAutocertificazione.Anno & Ext

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath"))

        If Not Directory.Exists(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0")) Then
            Directory.CreateDirectory(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0"))
        End If

        Dim PathFile As String = folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName

        Try
            fileUploadVeicoli.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & "', 'Errore');", True)
            Exit Sub

        End Try

        CurrentAutocertificazione.NomeFileVeicoli = fileName
        CurrentAutocertificazione.PathFileVeicoli = System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath") & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName
        CurrentAutocertificazione.Save()

        If myProduttore.EsisteAEE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsistePILE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFilePile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteVEICOLI(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileVeicoli <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteINDUSTRIAL(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileIndustriali <> "") Then
            UploadCompletato = False
        End If

        CurrentAutocertificazione.UploadEseguito = UploadCompletato
        CurrentAutocertificazione.Save()

        chkCaricato.Checked = True
        cmdCaricaVeicoli.Visible=True

    End Sub

    Private Sub cmdcaricaIndustriali_Click(sender As Object, e As EventArgs) Handles cmdcaricaIndustriali.Click

        Dim fileName As String
        Dim Ext As String
        Dim myProduttore As Produttore
        Dim UploadCompletato As Boolean = True
        Dim size As Decimal

        If Not fileUploadIndustriali.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        size = Math.Round((CDec(fileUploadIndustriali.PostedFile.ContentLength) / CDec(1024)), 2)

        If size > 5000 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dimensione file eccessiva! Kb:" & size.ToString & "', 'Errore');", True)
            Exit Sub
        End If

        Ext = GetExtension(fileUploadIndustriali.FileName)

        Select Case Ext

            Case ".pdf", ".jpg", ".PDF", ".JPG"

            Case Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipo file non permesso!', 'Errore');", True)
                Exit Sub

        End Select

        GetCurrentAuto()

        myProduttore = Produttore.Carica(CurrentAutocertificazione.IdProduttore)

        fileName = "Autocertificazione_Industriali" & "_" & CurrentAutocertificazione.Anno & Ext

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath"))

        If Not Directory.Exists(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0")) Then
            Directory.CreateDirectory(folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0"))
        End If

        Dim PathFile As String = folder & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName

        Try
            fileUploadIndustriali.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & "', 'Errore');", True)
            Exit Sub

        End Try

        CurrentAutocertificazione.NomeFileIndustriali = fileName
        CurrentAutocertificazione.PathFileIndustriali = System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath") & "\" & myProduttore.Id.ToString.PadLeft(5, "0") & "\" & fileName
        CurrentAutocertificazione.Save()

        If myProduttore.EsisteAEE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsistePILE(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFilePile <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteVEICOLI(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileVeicoli <> "") Then
            UploadCompletato = False
        End If

        If myProduttore.EsisteINDUSTRIAL(CurrentAutocertificazione.IdProduttore) And (Not CurrentAutocertificazione.NomeFileIndustriali <> "") Then
            UploadCompletato = False
        End If

        CurrentAutocertificazione.UploadEseguito = UploadCompletato
        CurrentAutocertificazione.Save()

        chkCaricato.Checked = True
        cmdScaricaIndustriali.Visible=True
        
        Response.Redirect(Request.RawUrl,True)
    End Sub

    Private Sub cmdScaricaAEE_Click(sender As Object, e As EventArgs) Handles cmdScaricaAEE.Click

        GetCurrentAuto()

        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim fileToDownload = Server.MapPath(CurrentAutocertificazione.PathFile)
        Dim NewFileName As String
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        NewFileName = "R_Autocert_AEE_" & Replace(Prod.RagioneSociale, ",", "") & "_" & CurrentAutocertificazione.Anno & Path.GetExtension(fileToDownload)
        response.ClearContent()
        response.Clear()
        response.ContentType = "text/plain"
        response.AddHeader("Content-Disposition", "attachment; filename=" & NewFileName & ";")
        response.TransmitFile(fileToDownload)
        response.Flush()
        response.[End]()


    End Sub

    Private Sub cmdScaricaIndustriali_Click(sender As Object, e As EventArgs) Handles cmdScaricaIndustriali.Click

        GetCurrentAuto()

        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim fileToDownload = Server.MapPath(CurrentAutocertificazione.PathFileIndustriali)
        Dim NewFileName As String
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        NewFileName = "R_Autocert_Industriali_" & Replace(Prod.RagioneSociale, ",", "") & "_" & CurrentAutocertificazione.Anno & Path.GetExtension(fileToDownload)
        response.ClearContent()
        response.Clear()
        response.ContentType = "text/plain"
        response.AddHeader("Content-Disposition", "attachment; filename=" & NewFileName & ";")
        response.TransmitFile(fileToDownload)
        response.Flush()
        response.[End]()

    End Sub

    Private Sub cmdScaricaPIle_Click(sender As Object, e As EventArgs) Handles cmdScaricaPIle.Click

        GetCurrentAuto()

        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim fileToDownload = Server.MapPath(CurrentAutocertificazione.PathFilePile)
        Dim NewFileName As String
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        NewFileName = "R_Autocert_Pile_" & Replace(Prod.RagioneSociale, ",", "") & "_" & CurrentAutocertificazione.Anno & Path.GetExtension(fileToDownload)
        response.ClearContent()
        response.Clear()
        response.ContentType = "text/plain"
        response.AddHeader("Content-Disposition", "attachment; filename=" & NewFileName & ";")
        response.TransmitFile(fileToDownload)
        response.Flush()
        response.[End]()


    End Sub

    Private Sub cmdScaricaVeicoli_Click(sender As Object, e As EventArgs) Handles cmdScaricaVeicoli.Click

        GetCurrentAuto()


        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim fileToDownload = Server.MapPath(CurrentAutocertificazione.PathFileVeicoli)
        Dim NewFileName As String
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        NewFileName = "R_Autocert_Veicoli_" & Replace(Prod.RagioneSociale, ",", "") & "_" & CurrentAutocertificazione.Anno & Path.GetExtension(fileToDownload)
        response.ClearContent()
        response.Clear()
        response.ContentType = "text/plain"
        response.AddHeader("Content-Disposition", "attachment; filename=" & NewFileName & ";")
        response.TransmitFile(fileToDownload)
        response.Flush()
        response.[End]()


    End Sub

    Private Sub PrintAEE_old    

        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim conn As New SqlConnection
        Dim strsql As String
        Dim Valore As Decimal
        Dim A, B, C, D, G, F As Decimal

        strsql = "Select [Raggruppamento] ,SUM (tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [tbl_Categorie] " & _
                "INNER  JOIN tbl_RigheDichiarazioni On tbl_Categorie.id = tbl_RigheDichiarazioni.IdCategoria " & _
                "LEFT JOIN tbl_Dichiarazioni On tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 " & _
                "And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & " And " & _
                "Raggruppamento Not Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 1  GROUP By Raggruppamento UNION " & _
                "SELECT [Raggruppamento] ,SUM (tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [tbl_CategorieNew] " & _
                "INNER  JOIN tbl_RigheDichiarazioni ON tbl_CategorieNew.id = tbl_RigheDichiarazioni.IdCategoria LEFT JOIN " & _
                "tbl_Dichiarazioni ON tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 " & _
                "And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & _
                " And Raggruppamento Not Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 0  GROUP By Raggruppamento ORDER By Raggruppamento"

        conn.ConnectionString = connString
        Dim dt As DataTable = New DataTable()
        Dim cmd As New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = strsql

        Dim Adapter As New SqlDataAdapter(cmd)

        Adapter.Fill(dt)

        Response.Charset = "utf-8"
        Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
        Response.CacheControl = "public"
        Response.AddHeader("Content-Disposition", "attachment; filename=Autocertificazione.pdf")
        Response.ContentType = "application/pdf"

        Dim outputPdfStream As Stream = Response.OutputStream
        Dim strTemplateStampa As String

        strTemplateStampa = Server.MapPath("~/Template/Autocertificazione.pdf")

        Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
        Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)

        Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
        Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Arial.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)
        Dim TotaleKg As Decimal

        WriteToPdf(cb, bf, 8, 197, 226, Prod.RagioneSociale)
        WriteToPdf(cb, bf, 8, 150, 243, Prod.Citta)
        WriteToPdf(cb, bf, 8, 167, 260, Prod.Indirizzo)
        WriteToPdf(cb, bf, 8, 145, 276, Prod.CAP)
        WriteToPdf(cb, bf, 8, 230, 292, Prod.Rappresentante)

        WriteToPdf(cb, bf, 8, 156, 353, txtAnno.Text)

        For Each row As DataRow In dt.Rows

            Valore = Math.Round(CDec(row(1).ToString), 0)

            TotaleKg += Valore ' CDec(row(1).ToString)

            Select Case row(0)

                Case "A"
                    If CDec(row(1).ToString) <> 0 Then
                        A += CDec(Valore.ToString)
                    End If

                Case "B"
                    If CDec(row(1).ToString) <> 0 Then
                        B += CDec(Valore.ToString)
                    End If

                Case "C"
                    If CDec(row(1).ToString) <> 0 Then
                        C += CDec(Valore.ToString)
                    End If

                Case "D"
                    If CDec(row(1).ToString) <> 0 Then
                        D += CDec(Valore.ToString)
                    End If

                Case "E"
                    If CDec(row(1).ToString) <> 0 Then
                        G += CDec(Valore.ToString)
                    End If

                Case "F"
                    If CDec(row(1).ToString) <> 0 Then
                        F += CDec(Valore.ToString)
                    End If

            End Select
        Next

        WriteToPdf(cb, bf, 8, 360, 423, A.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 360, 409, B.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 360, 437, C.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 360, 450, D.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 360, 465, G.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 360, 480, F.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 360, 491, TotaleKg.ToString("#,##0")) ' TOTALE

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

        cb.SetFontAndSize(bf, 2)
        WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
        ' ct.Go()
        stamper.Close()

        Response.End()


    End Sub

    Private Sub PrintVeicoli_old    

        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim conn As New SqlConnection
        Dim strsql As String
        Dim Valore As Decimal
        Dim Z26, Z27, Z28, Z29, Z30 As Decimal

        strsql = "Select [Raggruppamento] , SUM(tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [Ecoem].[dbo].[tbl_Categorie] INNER  JOIN tbl_RigheDichiarazioni On tbl_Categorie.id = tbl_RigheDichiarazioni.IdCategoria LEFT JOIN tbl_Dichiarazioni On tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & " And Raggruppamento Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 1 GROUP By Raggruppamento " & _
                "UNION Select [Raggruppamento] , SUM(tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [Ecoem].[dbo].[tbl_CategorieNew] INNER  JOIN tbl_RigheDichiarazioni On tbl_CategorieNew.id = tbl_RigheDichiarazioni.IdCategoria LEFT JOIN tbl_Dichiarazioni On tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & " And Raggruppamento Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 0 GROUP By Raggruppamento " & _
                "ORDER By Raggruppamento"

        conn.ConnectionString = connString
        Dim dt As DataTable = New DataTable()
        Dim cmd As New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = strsql

        Dim Adapter As New SqlDataAdapter(cmd)

        Adapter.Fill(dt)

        Response.Charset = "utf-8"
        Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
        Response.CacheControl = "public"
        Response.AddHeader("Content-Disposition", "attachment; filename=AutoCert_Veicoli.pdf")
        Response.ContentType = "application/pdf"

        Dim outputPdfStream As Stream = Response.OutputStream
        Dim strTemplateStampa As String
        Dim TotaleKg As Decimal

        strTemplateStampa = Server.MapPath("~/Template/AutoCert_Veicoli.pdf")

        Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
        Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)
        Dim Totale As Decimal

        Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
        Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Arial.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)

        WriteToPdf(cb, bf, 8, 110, 355, txtAnno.Text)

        WriteToPdf(cb, bf, 8, 200, 235, Prod.RagioneSociale)
        WriteToPdf(cb, bf, 8, 200, 295, Prod.Citta)
        WriteToPdf(cb, bf, 8, 200, 265, Prod.Indirizzo)
        'WriteToPdf(cb, bf, 8, 200, 295, Prod.CAP)
        WriteToPdf(cb, bf, 8, 200, 325, Prod.Rappresentante)

        For Each row As DataRow In dt.Rows
            Valore = Math.Round(CDec(row(1).ToString), 0)
            TotaleKg += Valore ' CDec(row(1).ToString)

            Select Case row(0)

                Case "Z26" ' Acc. Industriali al Piombo
                    Z26 += CDec(row(1))

                Case "Z27" ' Acc. Industriali al Nichel-Cadmio
                    Z27 += CDec(row(1))

                Case "Z28" ' Acc. Industriali al Litio
                    Z28 += CDec(row(1))

                Case "Z29" ' Acc. Industriali Ni-Mh
                    Z29 += CDec(row(1))

                Case "Z30" ' Altri Acc. Industriali
                    Z30 += CDec(row(1))

            End Select

        Next

        WriteToPdf(cb, bf, 8, 435, 414, Z26.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 427, Z27.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 440, Z28.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 452, Z29.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 464, Z30.ToString("#,##0"))

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

        cb.SetFontAndSize(bf, 2)
        WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
        ' ct.Go()
        stamper.Close()

        Response.End()


     End Sub

    Private Sub PrintIndustriali_old    

        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim conn As New SqlConnection
        Dim strsql As String
        Dim Valore As Decimal
        Dim Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20, Z21, Z22, Z23, Z24, Z25 As Decimal

        strsql = "Select [Raggruppamento] , SUM(tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [Ecoem].[dbo].[tbl_Categorie] INNER  JOIN tbl_RigheDichiarazioni On tbl_Categorie.id = tbl_RigheDichiarazioni.IdCategoria LEFT JOIN tbl_Dichiarazioni On tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & " And Raggruppamento Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 1 GROUP By Raggruppamento " & _
                "UNION Select [Raggruppamento] , SUM(tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [Ecoem].[dbo].[tbl_CategorieNew] INNER  JOIN tbl_RigheDichiarazioni On tbl_CategorieNew.id = tbl_RigheDichiarazioni.IdCategoria LEFT JOIN tbl_Dichiarazioni On tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & " And Raggruppamento Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 0 GROUP By Raggruppamento " & _
                "ORDER By Raggruppamento"

        conn.ConnectionString = connString
        Dim dt As DataTable = New DataTable()
        Dim cmd As New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = strsql

        Dim Adapter As New SqlDataAdapter(cmd)

        Adapter.Fill(dt)

        Response.Charset = "utf-8"
        Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
        Response.CacheControl = "public"
        Response.AddHeader("Content-Disposition", "attachment; filename=Autocert_Industriali.pdf")
        Response.ContentType = "application/pdf"

        Dim outputPdfStream As Stream = Response.OutputStream
        Dim strTemplateStampa As String
        Dim TotaleKg As Decimal

        strTemplateStampa = Server.MapPath("~/Template/Autocert_Industriali.pdf")

        Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
        Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)
        Dim Totale As Decimal

        Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
        Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Arial.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)

        WriteToPdf(cb, bf, 8, 110, 340, txtAnno.Text)

        WriteToPdf(cb, bf, 8, 200, 220, Prod.RagioneSociale)
        WriteToPdf(cb, bf, 8, 200, 251, Prod.Indirizzo)
        WriteToPdf(cb, bf, 8, 200, 282, Prod.Citta)
        WriteToPdf(cb, bf, 8, 200, 313, Prod.Rappresentante)

        For Each row As DataRow In dt.Rows
            Valore = Math.Round(CDec(row(1).ToString), 0)
            TotaleKg += Valore ' CDec(row(1).ToString)

            Select Case row(0)

                Case "Z12" ' Acc. Industriali al Piombo
                    Z12 += CDec(row(1))

                Case "Z13" ' Acc. Industriali al Nichel-Cadmio
                    Z13 += CDec(row(1))

                Case "Z14" ' Acc. Industriali al Litio
                    Z14 += CDec(row(1))

                Case "Z15" ' Acc. Industriali Ni-Mh
                    Z15 += CDec(row(1))

                Case "Z16" ' Altri Acc. Industriali
                    Z16 += CDec(row(1))

                Case "Z17" ' Acc. per Veicoli al Piombo
                    Z17 += CDec(row(1))

                Case "Z18" ' Acc. per Veicoli al Nichel-Cadmio
                    Z18 += CDec(row(1))

                Case "Z19" ' Acc. per Veicoli al Litio
                    Z19 += CDec(row(1))

                Case "Z20" ' Acc. per Veicoli Ni-Mh
                    Z20 += CDec(row(1))

                Case "Z21" ' Altri Acc. per Veicoli
                    Z21 += CDec(row(1))

                Case "Z22" ' Altri Acc. per Veicoli
                    Z22 += CDec(row(1))

                Case "Z23" ' Altri Acc. per Veicoli
                    Z23 += CDec(row(1))

                Case "Z24" ' Altri Acc. per Veicoli
                    Z24 += CDec(row(1))

                Case "Z25" ' Altri Acc. per Veicoli
                    Z25 += CDec(row(1))
            End Select

        Next

        WriteToPdf(cb, bf, 8, 435, 382, Z12.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 402, Z13.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 422, Z14.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 445, Z15.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 465, Z16.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 480, Z17.ToString("#,##0")) ' Piombo
        WriteToPdf(cb, bf, 8, 435, 500, Z18.ToString("#,##0")) ' Nichel
        WriteToPdf(cb, bf, 8, 435, 520, Z19.ToString("#,##0")) ' Litio
        WriteToPdf(cb, bf, 8, 435, 540, Z20.ToString("#,##0")) ' NI
        WriteToPdf(cb, bf, 8, 435, 569, Z21.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 591, Z22.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 620, Z23.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 639, Z24.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 435, 656, Z25.ToString("#,##0"))


        'WriteToPdf(cb, bf, 8, 420, 514, TotaleKg.ToString("#,##0")) ' TOTALE

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

        cb.SetFontAndSize(bf, 2)
        WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
        ' ct.Go()
        stamper.Close()

        Response.End()


    End Sub

    Private Sub PrintPile_old    

        Dim Prod As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim conn As New SqlConnection
        Dim strsql As String
        Dim Valore As Decimal
        Dim Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10, Z11 As Decimal

        strsql = "Select [Raggruppamento] , SUM(tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [Ecoem].[dbo].[tbl_Categorie] INNER  JOIN tbl_RigheDichiarazioni On tbl_Categorie.id = tbl_RigheDichiarazioni.IdCategoria LEFT JOIN tbl_Dichiarazioni On tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & " And Raggruppamento Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 1 GROUP By Raggruppamento " & _
                "UNION Select [Raggruppamento] , SUM(tbl_RigheDichiarazioni.KgDichiarazione) As TotaleKg FROM [Ecoem].[dbo].[tbl_CategorieNew] INNER  JOIN tbl_RigheDichiarazioni On tbl_CategorieNew.id = tbl_RigheDichiarazioni.IdCategoria LEFT JOIN tbl_Dichiarazioni On tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id WHERE tbl_Dichiarazioni.AutocertificazioneProdotta = 1 And year(tbl_Dichiarazioni.Data) = " & txtAnno.Text & " And tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue & " And Raggruppamento Like 'Z%' AND tbl_Dichiarazioni.OldVersion = 0 GROUP By Raggruppamento " & _
                "ORDER By Raggruppamento"

        conn.ConnectionString = connString
        Dim dt As DataTable = New DataTable()
        Dim cmd As New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = strsql

        Dim Adapter As New SqlDataAdapter(cmd)

        Adapter.Fill(dt)

        Response.Charset = "utf-8"
        Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"))
        Response.CacheControl = "public"
        Response.AddHeader("Content-Disposition", "attachment; filename=AutocertificazionePile.pdf")
        Response.ContentType = "application/pdf"

        Dim outputPdfStream As Stream = Response.OutputStream
        Dim strTemplateStampa As String
        Dim TotaleKg As Decimal

        strTemplateStampa = Server.MapPath("~/Template/AutocertificazionePile.pdf")

        Dim reader As New iTextSharp.text.pdf.PdfReader(strTemplateStampa)
        Dim mb As iTextSharp.text.Rectangle = reader.GetPageSize(1)
        Dim Totale As Decimal

        Dim stamper As New iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream)
        Dim bf As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\Arial.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        Dim cb As iTextSharp.text.pdf.PdfContentByte = stamper.GetOverContent(1)

        WriteToPdf(cb, bf, 8, 90, 322, txtAnno.Text)

        WriteToPdf(cb, bf, 8, 197, 226, Prod.RagioneSociale)
        WriteToPdf(cb, bf, 8, 150, 243, Prod.Citta)
        WriteToPdf(cb, bf, 8, 167, 260, Prod.Indirizzo)
        WriteToPdf(cb, bf, 8, 145, 276, Prod.CAP)
        WriteToPdf(cb, bf, 8, 230, 292, Prod.Rappresentante)

        For Each row As DataRow In dt.Rows
            Valore = Math.Round(CDec(row(1).ToString), 0)
            'TotaleKg += Valore ' CDec(row(1).ToString)

            Select Case row(0)

                Case "Z1"
                    Z1 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)

                Case "Z2"
                    Z2 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z3"
                    Z3 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z4"
                    Z4 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z5"
                    Z5 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z6"
                    Z6 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z7"
                    Z7 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z8"
                    Z8 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z9"
                    Z9 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z10"
                    Z10 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
                Case "Z11"
                    Z11 += CDec(row(1))
                    TotaleKg += Valore ' CDec(row(1).ToString)
            End Select

        Next

        WriteToPdf(cb, bf, 8, 420, 373, Z1.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 386, Z2.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 399, Z3.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 412, Z4.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 425, Z5.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 438, Z6.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 451, Z7.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 464, Z8.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 476, Z9.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 489, Z10.ToString("#,##0"))
        WriteToPdf(cb, bf, 8, 420, 502, Z11.ToString("#,##0"))

        WriteToPdf(cb, bf, 8, 420, 514, TotaleKg.ToString("#,##0")) ' TOTALE

        bf = iTextSharp.text.pdf.BaseFont.CreateFont(Request.PhysicalApplicationPath + "Fonts" + "\IDAutomationHC39M.ttf", iTextSharp.text.pdf.BaseFont.WINANSI, iTextSharp.text.pdf.BaseFont.EMBEDDED)

        cb.SetFontAndSize(bf, 2)
        WriteToPdf(cb, bf, 10, 450, 60, "*" & Prod.RagioneSociale & "*")
        ' ct.Go()
        stamper.Close()

        Response.End()


    End Sub

    Private Sub cmdConferma_Click(sender As Object, e As EventArgs) Handles cmdConferma.Click

        GetCurrentAuto()

        If Not CurrentAutocertificazione Is Nothing Then
            If Page.User.IsInRole("Amministratore") Then
                CurrentAutocertificazione.Confermata = True
                CurrentAutocertificazione.DataConferma = Today.ToShortDateString

                CurrentAutocertificazione.Save()

                cmdConferma.Visible = False
                chkConfermata.Checked = True
                txtDataConferma.Text = CurrentAutocertificazione.DataConferma
                txtData.Text = CurrentAutocertificazione.Data
                cmdRiapri.Visible = True
            ElseIf CurrentAutocertificazione.UploadEseguito Then
                CurrentAutocertificazione.Confermata = True
                CurrentAutocertificazione.DataConferma = Today.ToShortDateString

                CurrentAutocertificazione.Save()

                cmdConferma.Visible = False
                chkConfermata.Checked = True
                txtDataConferma.Text = CurrentAutocertificazione.DataConferma
                txtData.Text = CurrentAutocertificazione.Data
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Caricare tutti i documenti'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If
        End If


    End Sub

    Private Sub cmdRiapri_Click(sender As Object, e As EventArgs) Handles cmdRiapri.Click

        GetCurrentAuto

        CurrentAutocertificazione.Confermata = False
        CurrentAutocertificazione.DataConferma = DefaultValues.GetDateTimeMinValue
        CurrentAutocertificazione.Save

        chkConfermata.Checked=False
        txtDataConferma.Text = String.Empty

    End Sub

End Class
