Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Data
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class WebApp_Dichiarazioni_Nuova
    Inherits System.Web.UI.Page

    Dim dt As New DataTable

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Dichiarazioni/ListaAutocertificazioni.aspx")
    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaAutoCertificazioni.aspx")
    End Sub

    Private Sub cmdEsegui_Click(sender As Object, e As EventArgs) Handles cmdEsegui.Click

        Dim AutoCertGenerate As Integer
        Dim RigheGenerate As Integer

        dt.Columns.Add("Ragione sociale")
        dt.Columns.Add("Autocertificazione prodotta")
        dt.Columns.Add("Dichiarazioni nel periodo")
        dt.Columns.Add("Dichiarazioni certificate")
        dt.Columns.Add("Dichiarazioni non confermate")

        If txtAnno.Text = String.Empty Then
            divError.Visible = True
            lblError.Text = "Compilare il campo ANNO"
        Else
            divError.Visible = False
            If ddlProduttore.SelectedIndex = 0 Then

                Dim ListaProduttori As List(Of Produttore)

                ListaProduttori = Produttore.Lista

                For Each produttore In ListaProduttori

                    ' Verifica se per il produttore è già presente autocertificazione nel periodo
                    Dim Certificazione As Autocertificazione = Autocertificazione.Carica(produttore.Id, CInt(txtAnno.Text))

                    If Certificazione Is Nothing Then

                        ' Verifica dichiarazioni nel periodo
                        Dim DichiarazioniNelPeriodo As Integer = produttore.ContaDichiarazioni(CInt(txtAnno.Text), produttore.Id)
                        If DichiarazioniNelPeriodo > 0 Then
                            If Not produttore.VerificaDichiarazioni(CInt(txtAnno.Text), produttore.Id) Then
                                Dim DichiarazioniCertificate As Integer = produttore.DichiarazioniCertifica(CInt(txtAnno.Text), produttore.Id)
                                If DichiarazioniCertificate > 0 Then

                                    Dim Nuova As New Autocertificazione

                                    Nuova.Anno = CInt(txtAnno.Text)
                                    Nuova.Data = Today
                                    Nuova.IdProduttore = produttore.Id
                                    Nuova.NrFattura = String.Empty
                                    Nuova.Save()

                                    AutoCertGenerate += 1

                                    Dim Dtr As DataRow
                                    Dtr = dt.NewRow
                                    Dtr("Ragione Sociale") = produttore.RagioneSociale
                                    Dtr("Autocertificazione prodotta") = "Si"
                                    Dtr("Dichiarazioni nel periodo") = DichiarazioniNelPeriodo
                                    Dtr("Dichiarazioni certificate") = DichiarazioniCertificate
                                    Dtr("Dichiarazioni non confermate") = "No"
                                    dt.Rows.Add(Dtr)

                                    If Not Nuova.RigheGenerate Then
                                        RigheGenerate += GeneraRighe(Nuova)

                                    End If
                                End If
                            Else
                                Dim Dtr As DataRow
                                Dtr = dt.NewRow
                                Dtr("Ragione Sociale") = produttore.RagioneSociale
                                Dtr("Autocertificazione prodotta") = "No"
                                Dtr("Dichiarazioni nel periodo") = DichiarazioniNelPeriodo
                                Dtr("Dichiarazioni certificate") = 0
                                Dtr("Dichiarazioni non confermate") = "Si"
                                dt.Rows.Add(Dtr)
                            End If
                        Else
                            Dim Dtr As DataRow
                            Dtr = dt.NewRow
                            Dtr("Ragione Sociale") = produttore.RagioneSociale
                            Dtr("Autocertificazione prodotta") = "No"
                            Dtr("Dichiarazioni nel periodo") = DichiarazioniNelPeriodo
                            Dtr("Dichiarazioni certificate") = 0
                            Dtr("Dichiarazioni non confermate") = "No"
                            dt.Rows.Add(Dtr)
                        End If

                    Else ' Verifica generazione righe per l'autocertificazione trovata
                        If Not Certificazione.RigheGenerate Then
                            RigheGenerate = GeneraRighe(Certificazione)

                        End If
                    End If
                Next

            Else
                ' Verifica certificazioni
                Dim Produttore As Produttore = Produttore.Carica(ddlProduttore.SelectedValue)

                Dim Certificazione As Autocertificazione = Autocertificazione.Carica(Produttore.Id, CInt(txtAnno.Text))

                If Certificazione Is Nothing Then

                    Dim DichiarazioniNelPeriodo As Integer = Produttore.ContaDichiarazioni(CInt(txtAnno.Text), Produttore.Id)
                    If DichiarazioniNelPeriodo > 0 Then ' Se ci sono dichiarazioni nel periodo 
                        If Not Produttore.VerificaDichiarazioni(CInt(txtAnno.Text), ddlProduttore.SelectedValue) Then ' Se non ce ne sono aperte
                            Dim DichiarazioniCertificate As Integer = Produttore.DichiarazioniCertifica(CInt(txtAnno.Text), Produttore.Id)
                            If DichiarazioniCertificate > 0 Then  ' Se almeno una dichiarazione risulta certificata
                                Dim Nuova As New Autocertificazione

                                Nuova.Anno = CInt(txtAnno.Text)
                                Nuova.Data = Today
                                Nuova.IdProduttore = ddlProduttore.SelectedValue
                                Nuova.NrFattura = String.Empty
                                Nuova.DataFattura = DefaultValues.GetDateTimeMinValue
                                Nuova.Save()

                                AutoCertGenerate += 1

                                Dim Dtr As DataRow
                                Dtr = dt.NewRow
                                Dtr("Ragione Sociale") = Produttore.RagioneSociale
                                Dtr("Autocertificazione prodotta") = "Si"
                                Dtr("Dichiarazioni nel periodo") = DichiarazioniNelPeriodo
                                Dtr("Dichiarazioni certificate") = DichiarazioniCertificate
                                Dtr("Dichiarazioni non confermate") = "No"
                                dt.Rows.Add(Dtr)
                            End If
                        Else
                            Dim Dtr As DataRow
                            Dtr = dt.NewRow
                            Dtr("Ragione Sociale") = Produttore.RagioneSociale
                            Dtr("Autocertificazione prodotta") = "No"
                            Dtr("Dichiarazioni nel periodo") = DichiarazioniNelPeriodo
                            Dtr("Dichiarazioni certificate") = 0
                            Dtr("Dichiarazioni non confermate") = "Si"
                            dt.Rows.Add(Dtr)
                        End If
                    Else
                        Dim Dtr As DataRow
                        Dtr = dt.NewRow
                        Dtr("Ragione Sociale") = Produttore.RagioneSociale
                        Dtr("Autocertificazione prodotta") = "No"
                        Dtr("Dichiarazioni nel periodo") = DichiarazioniNelPeriodo
                        Dtr("Dichiarazioni certificate") = 0
                        Dtr("Dichiarazioni non confermate") = "No"
                        dt.Rows.Add(Dtr)
                    End If

                Else ' Verifica generazione righe per l'autocertificazione trovata
                    If Not Certificazione.RigheGenerate Then
                        GeneraRighe(Certificazione)
                        RigheGenerate = GeneraRighe(Certificazione)
                    End If
                End If
            End If
        End If

        ListaErrori.DataSource = dt
        ListaErrori.DataBind()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Sono state generate " & AutoCertGenerate.ToString & " autocertificazioni e " & RigheGenerate & " righe.', 'Conferma');", True)


    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaDichiarazioni")
            Dim riga As Integer = 1

            ws.Cells("A1").Value = "Ragione sociale"
            ws.Cells("B1").Value = "Autocertificazione prodotta"
            ws.Cells("C1").Value = "Dichiarazioni nel periodo"
            ws.Cells("D1").Value = "Dichiarazioni certificate"
            ws.Cells("E1").Value = "Dichiarazioni non confermate"

            For Each item As ListViewItem In Me.ListaErrori.Items
                riga += 1
                Dim Ragione As Label = item.FindControl("lblRagione")
                Dim AutocertficazioneProdotta As Label = item.FindControl("lblAutProd")
                Dim DichiarazioniInPeriodo As Label = item.FindControl("lblDichInPeriodo")
                Dim DichiarazioniCertificate As Label = item.FindControl("lblDichCertificate")
                Dim DichiarazioniNonConfermate As Label = item.FindControl("lblDichNonConfermate")

                ws.Cells("A" + riga.ToString).Value = Ragione.Text
                ws.Cells("B" + riga.ToString).Value = AutocertficazioneProdotta.Text
            Next

            Using rng As ExcelRange = ws.Cells("A1:i1048576")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=Esito.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

    Private Sub WebApp_Dichiarazioni_Nuova_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.IsInRole("Amministratore") Then
            cmdEsegui.Visible = True
        Else
            cmdEsegui.Visible = False
        End If

    End Sub

    Private Function GeneraRighe(CurrentAutoCertificazione As Autocertificazione) As Integer

        Dim NrRigheGenerate As Integer
        Dim ListaRighe As List(Of RigaAutocertificazione)
        Dim ListaCategorie As List(Of Categoria_ProduttoreNew)

        ListaCategorie = Categoria_ProduttoreNew.Lista(CurrentAutoCertificazione.IdProduttore, False)
        If ListaCategorie Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Anagrafica Produttore senza categorie.'" & ", 'Messaggio errore');", True)
            Exit Function
        End If

        If Not CurrentAutoCertificazione Is Nothing Then

            ListaRighe = CurrentAutoCertificazione.GeneraRighe

            For Each CategoriaInLista In ListaCategorie
                Dim NuovaRiga As New RigaAutocertificazione
                Dim Categoria As CategoriaNew = CategoriaNew.Carica(CategoriaInLista.IdCategoria)
                Dim CatProd As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(CategoriaInLista.IdCategoria, CurrentAutoCertificazione.IdProduttore, False)
                Dim RigaCalcolata As RigaAutocertificazione = ListaRighe.Find(Function(r As RigaAutocertificazione) r.IdCategoria = Categoria.Id)

                NuovaRiga.IdCategoria = CategoriaInLista.IdCategoria
                NuovaRiga.IdCertificazione = CurrentAutoCertificazione.Id
                NuovaRiga.TipoDiDato = Categoria.TipoDiDato

                If Not RigaCalcolata Is Nothing Then
                    NuovaRiga.Pezzi = RigaCalcolata.Pezzi
                    NuovaRiga.Kg = RigaCalcolata.Kg
                    NuovaRiga.KgRettifica = RigaCalcolata.KgCertificazione
                    NuovaRiga.PezziRettifica = NuovaRiga.Pezzi
                    NuovaRiga.KgCertificazione = RigaCalcolata.KgCertificazione
                    NuovaRiga.KgCertSoci = RigaCalcolata.KgCertificazione

                Else
                    NuovaRiga.Pezzi = 0
                    NuovaRiga.Kg = 0
                    NuovaRiga.KgRettifica = 0
                    NuovaRiga.PezziRettifica = 0
                    NuovaRiga.KgCertificazione = 0
                    NuovaRiga.KgCertSoci = 0

                End If

                NuovaRiga.UtenteAggiornamento = Page.User.Identity.Name.ToString
                NuovaRiga.DataAggiornamento = Today
                NuovaRiga.DifferenzaKg = 0
                NuovaRiga.DifferenzaPezzi = 0
                NuovaRiga.CostoUnitario = CatProd.Costo
                If Categoria.TipoDiDato = "Valore" Then
                    NuovaRiga.Importo = NuovaRiga.KgCertificazione * CatProd.Costo
                Else
                    NuovaRiga.Importo = NuovaRiga.PezziRettifica * CatProd.Costo
                End If
                NuovaRiga.Save()

                NrRigheGenerate += 1
            Next

        End If

        Return NrRigheGenerate

    End Function
End Class
