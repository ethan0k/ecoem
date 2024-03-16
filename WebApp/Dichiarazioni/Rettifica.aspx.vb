
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data

Partial Class WebApp_Amministrazione_Rettifica
    Inherits System.Web.UI.Page

    Dim CurrentAutocertificazione As Autocertificazione
    Dim CurrentUser As MembershipUser

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Dichiarazioni/ListaAutocertificazioni.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_Dichiarazione_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.User.IsInRole("Amministratore") And Not Page.User.IsInRole("Produttore") Then
            Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
        End If

        GetCurrentAutocertificazione

        If Not CurrentAutocertificazione Is Nothing Then

            If not CurrentAutocertificazione.RigheGenerate Then
                If CurrentAutocertificazione.Anno >= 2020 Then
                    GeneraRighe(CurrentAutocertificazione.Anno)                    
                Else
                    'cmdConferma.Visible = False
                    'cmdEsporta.Visible = True
                End If
            Else
                'cmdConferma.Visible = not CurrentAutocertificazione.Confermata
                'cmdEsporta.Visible = True
            End If

            ddlProduttori.Enabled = False
            CurrentUser = Membership.GetUser()
            ' Verifica identità
            If Page.User.IsInRole("Produttore") Then
                Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)

                If (UtenteProduttore.IdProduttore <> CurrentAutocertificazione.IdProduttore) Then
                    Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
                End If
            End If
            
            if CurrentAutocertificazione.DataConferma <= DefaultValues.GetDateTimeMinValue then 
                txtDataConferma.Text = ""
            Else
                txtData.Text = CurrentAutocertificazione.DataConferma
            End if

            txtData.Text = CurrentAutocertificazione.Data
            lblanno.Text = CurrentAutocertificazione.Anno
            ddlProduttori.SelectedValue = CurrentAutocertificazione.IdProduttore
            txtImporto.Text = String.Format("{0:#,##0.00}", CurrentAutocertificazione.Importo)            

            titleConfermata.Visible = False
            If CurrentAutocertificazione.Rettificata Then
                lblRettificata.Text = "Sì"
            End If

            If CurrentAutocertificazione.Confermata Then
                lblConfermata.Text = "Sì"
            End If
        End If

    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaAutocertificazioni.aspx")
    End Sub

    Private Sub GetCurrentAutocertificazione()
        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentAutocertificazione = Autocertificazione.Carica(IdFromQueryString)            
        End If
    End Sub

    Private Sub GetCurrentUser()

        If Not Request.QueryString("Id") Is Nothing Then
            Dim userkey As Guid = New Guid(Page.User.Identity.Name)
            CurrentUser = Membership.GetUser()
        End If

    End Sub
    Protected Sub txtPezziRettifica_TextChanged(sender As Object, e As EventArgs)

        Dim CatProduttore As Categoria_ProduttoreNew
        Dim RigaAutocertificazione As RigaAutocertificazione
        Dim txtPezziRettifica As TextBox = DirectCast(sender, TextBox)
        Dim item As ListViewItem = DirectCast(txtPezziRettifica.NamingContainer, ListViewItem)
        Dim dataItem As ListViewDataItem = DirectCast(item, ListViewDataItem)
        Dim IdKey As String = Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString()
        Dim Autocertificazione As Autocertificazione
        Dim lblImporto As Label = CType(Listview1.Items(dataItem.DisplayIndex).FindControl("lblImporto"), Label)
        Dim txtKgrettifica As TextBox= CType(Listview1.Items(dataItem.DisplayIndex).FindControl("txtKgRettifica"), TextBox)

        If CInt(txtPezziRettifica.Text) < 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Il valore digitato non può essere negativo'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        GetCurrentAutocertificazione

        RigaAutocertificazione = RigaAutocertificazione.Carica(CInt(IdKey))
        CatProduttore = Categoria_ProduttoreNew.Carica(RigaAutocertificazione.IdCategoria, ddlProduttori.SelectedValue, False)
        RigaAutocertificazione.PezziRettifica = txtPezziRettifica.Text
        RigaAutocertificazione.KgCertificazione = CatProduttore.Peso * CInt(txtPezziRettifica.Text)
        RigaAutocertificazione.KgRettifica = CatProduttore.Peso * CInt(txtPezziRettifica.Text)
        RigaAutocertificazione.DifferenzaPezzi = RigaAutocertificazione.PezziRettifica-RigaAutocertificazione.Pezzi 
        RigaAutocertificazione.KgDiffCertificazione =  RigaAutocertificazione.DifferenzaPezzi * CatProduttore.Peso
        RigaAutocertificazione.Importo = RigaAutocertificazione.PezziRettifica * RigaAutocertificazione.CostoUnitario
        
        RigaAutocertificazione.Save()

        lblImporto.Text = String.Format("{0:c2}", RigaAutocertificazione.Importo)
        txtKgrettifica.Text = String.Format("{0:n2}", RigaAutocertificazione.KgRettifica)

        Autocertificazione = Autocertificazione.Carica(CInt(Request.QueryString("Id")))

        Autocertificazione.Rettificata = Autocertificazione.isRettificata
        Autocertificazione.Save
                    
        If Autocertificazione.Rettificata Then
            lblRettificata.Text = "Sì"
        Else
            lblRettificata.Text = "No"
        End If

        txtImporto.Text = String.Format("{0:c2}", Autocertificazione.Importo)

    End Sub

    Protected Sub txtKgRettifica_TextChanged(sender As Object, e As EventArgs)

        Dim Categoria As CategoriaNew
        Dim RigaAutocertificazione As RigaAutocertificazione
        Dim txtKgRettifica As TextBox = DirectCast(sender, TextBox)
        Dim item As ListViewItem = DirectCast(txtKgRettifica.NamingContainer, ListViewItem)
        Dim dataItem As ListViewDataItem = DirectCast(item, ListViewDataItem)
        Dim IdKey As String = Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString()
        Dim Autocertificazione As Autocertificazione
        Dim lblImporto As Label = CType(Listview1.Items(dataItem.DisplayIndex).FindControl("lblImporto"), Label)        
        
        
        RigaAutocertificazione = RigaAutocertificazione.Carica(CInt(IdKey))
        Categoria = CategoriaNew.Carica(RigaAutocertificazione.IdCategoria)
        
        RigaAutocertificazione.KgRettifica = cdec(txtKgRettifica.Text)
        RigaAutocertificazione.KgCertificazione = cdec(txtKgRettifica.Text)
        RigaAutocertificazione.DifferenzaKg = RigaAutocertificazione.KgRettifica-RigaAutocertificazione.Kg 
        RigaAutocertificazione.KgDiffCertificazione = RigaAutocertificazione.KgRettifica-RigaAutocertificazione.Kg
        If Categoria.TipoDiDato = "Valore" Then
            RigaAutocertificazione.Importo = CStr(RigaAutocertificazione.KgRettifica * RigaAutocertificazione.CostoUnitario).ToString()
        End If

        RigaAutocertificazione.UtenteAggiornamento = Page.User.Identity.Name.ToString
        RigaAutocertificazione.DataAggiornamento = Today
        RigaAutocertificazione.Save()

        lblImporto.Text = String.Format("{0:c2}", RigaAutocertificazione.Importo)

        Autocertificazione = Autocertificazione.Carica(RigaAutocertificazione.IdCertificazione)        
        Autocertificazione.Rettificata = Autocertificazione.isRettificata
        Autocertificazione.Save

        If Autocertificazione.Rettificata Then
            lblRettificata.Text = "Sì"
        Else
            lblRettificata.Text = "No"
        End If
        txtImporto.Text = String.Format("{0:c2}", Autocertificazione.Importo)

    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        If (e.Item.ItemType = ListViewItemType.DataItem) Then

            Dim item As ListViewDataItem = DirectCast(e.Item, ListViewDataItem)
            Dim KeyId As String = Listview1.DataKeys(item.DisplayIndex)("Id").ToString()
            Dim RigaCertificazione As RigaAutocertificazione

            RigaCertificazione = RigaAutocertificazione.Carica(CInt(KeyId.ToString))

            If RigaCertificazione.TipoDiDato = "Valore" Then
                Dim myTextbox As TextBox = CType(e.Item.FindControl("txtPezziRettifica"), TextBox)
                Dim myLabel2 As Label = CType(e.Item.FindControl("txtPezzi"), Label)
                myTextbox.BackColor = Drawing.Color.LightGray
                myTextbox.Enabled = False
                myTextbox.ToolTip = "Campo disabilitato"
                myTextbox.Visible = False
                myLabel2.Visible = False
                If CurrentAutocertificazione.Confermata Then
                    Dim myTextkg As TextBox = CType(e.Item.FindControl("txtKgRettifica"), TextBox)
                    myTextkg.Enabled = False
                    myTextkg.ToolTip = "Campo disabilitato"
                End If
            Else
                Dim myTextkgRettifica As TextBox = CType(e.Item.FindControl("txtKgRettifica"), TextBox)
                Dim myLabelkg As Label = CType(e.Item.FindControl("txtKg"), Label)
                myLabelkg.Enabled = False
                myLabelkg.BackColor = Drawing.Color.LightGray
                myLabelkg.Visible = False
                If CurrentAutocertificazione.Confermata Then
                    Dim myTextbox As TextBox = CType(e.Item.FindControl("txtPezziRettifica"), TextBox)
                    myTextbox.Enabled = False
                    myTextbox.ToolTip = "Campo disabilitato"
                    myTextkgRettifica.Enabled = False
                End If

            End If

            If Not Page.User.IsInRole("Amministratore") Then
                Dim tc As HtmlTableCell = DirectCast(e.Item.FindControl("colToHide"), HtmlTableCell)
                Dim tc2 As HtmlTableCell = DirectCast(e.Item.FindControl("colToHide2"), HtmlTableCell)
                tc.Visible = False
                tc2.Visible = False
            End If

        End If

    End Sub

    Private sub GeneraRighe(Anno As integer)

        Dim ListaRighe As List(Of RigaAutocertificazione)
        Dim ListaCategorie As List(Of Categoria_ProduttoreNew)

        GetCurrentAutocertificazione

        ListaCategorie = Categoria_ProduttoreNew.Lista(CurrentAutocertificazione.IdProduttore, False)
        If ListaCategorie Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Anagrafica Produttore senza categorie.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If Not CurrentAutocertificazione Is Nothing Then

            ListaRighe = CurrentAutocertificazione.GeneraRighe

             For Each CategoriaInLista In ListaCategorie
                Dim NuovaRiga As New RigaAutocertificazione
                Dim Categoria As CategoriaNew = CategoriaNew.Carica(CategoriaInLista.IdCategoria)
                Dim CatProd As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(CategoriaInLista.IdCategoria, CurrentAutocertificazione.IdProduttore, False)
                Dim RigaCalcolata As RigaAutocertificazione=ListaRighe.Find(Function(r As RigaAutocertificazione) r.IdCategoria=Categoria.Id)

                NuovaRiga.IdCategoria = CategoriaInLista.IdCategoria
                NuovaRiga.IdCertificazione = CurrentAutocertificazione.Id
                NuovaRiga.TipoDiDato = Categoria.TipoDiDato

                If Not RigaCalcolata Is Nothing Then
                    NuovaRiga.Pezzi = RigaCalcolata.Pezzi
                    NuovaRiga.Kg = RigaCalcolata.Kg
                    NuovaRiga.KgRettifica = RigaCalcolata.Kgcertificazione
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
            Next

        End If

    End Sub

    Private Sub cmdVaiACertificazione_Click(sender As Object, e As EventArgs) Handles cmdVaiACertificazione.Click

        Response.Redirect("~/WebApp/Dichiarazioni/Autocertificazione.aspx?Id=" & Request.QueryString("Id"))

    End Sub

    Private Sub Listview1_LayoutCreated(sender As Object, e As EventArgs) Handles Listview1.LayoutCreated

        If Not Page.User.IsInRole("Amministratore") Then
            Dim ctr As Control = Listview1.FindControl("colToHide")
            ctr.Visible = False
        End If
    End Sub


End Class
