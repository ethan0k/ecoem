Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_Produttore
    Inherits System.Web.UI.Page

    Private CurrentProduttore As Produttore

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaProduttori.aspx")

    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoProduttore As Produttore

        If chkAttivo.Checked And ddlPeriodicita.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un valore nel campo Periodicità', 'Conferma');", True)
            Exit Sub
        End If

        If Not chkDomestico.Checked And Not chkProfessionale.Checked Then
            divError.Visible = True
            lblError.Text = "Selezionare almeno una scelta tra DOMESTICO e PROFESSIONALE"
            Exit Sub
        End If

        GetCurrentProduttore()

        If Not CurrentProduttore Is Nothing Then
            nuovoProduttore = Produttore.Carica(CurrentProduttore.Id)
        Else
            nuovoProduttore = New Produttore
        End If


        With nuovoProduttore
            .Codice = Left(txtCodice.Text, 10)
            .RagioneSociale = Left(txtRagioneSociale.Text, 50)
            .Email = Left(txtEmail.Text, 256)
            .Periodicita = ddlPeriodicita.SelectedValue
            .Attivo = chkAttivo.Checked
            .Rappresentante = Left(txtRappresentante.Text, 50)
            .Indirizzo = Left(txtIndirizzo.Text, 50)
            .CAP = Left(txtCap.Text, 10)
            .Citta = Left(txtCitta.Text, 50)
            .Note = txtNote.Text
            .EmailNotifiche = Left(txtEmailNotifiche.Text, 100)
            .CodiceFiscale = Left(txtCodiceFiscale.Text, 20)
            .Domestico = chkDomestico.Checked
            .EscludiMassivo = ChkEscludi.Checked
            If IsNumeric(txtSconto.Text) Then
                .Sconto = CDec(txtSconto.Text)
            Else
                .Sconto = 0
            End If
            If IsNumeric(txtServizioRappresentanza.Text) Then
                .ServizioDiRappresentanza = CDec(txtServizioRappresentanza.Text)
            Else
                .ServizioDiRappresentanza = 0
            End If
            .ServizioDiRappresentanza = CDec(txtServizioRappresentanza.Text)
            'If CDec(txtCostoMatricola.Text) < 0 Then
            '    .CostoMatricola = 0
            'Else
            '    .CostoMatricola = CDec(txtCostoMatricola.Text)
            'End If

            If chkDomestico.Checked Then

            Else
            End If
            .Professionale = chkProfessionale.Checked
            If chkProfessionale.Checked Then

            Else

            End If
            .MeseAdesione = ddlMeseAdesione.SelectedValue
            If IsNumeric(txtQuota.Text) Then
                .QuotaConsortile = txtQuota.Text
            Else
                .QuotaConsortile = 0
            End If
            .PEC = Left(txtPEC.Text, 250)
            .CodiceSDI = Left(txtCodiceSDI.Text, 10)
            .IBAN = Left(txtIBAN.Text, 20)
            .PartitaIVA = Left(txtPartitaIVA.Text, 20)
            .Telefono = Left(txtTelefono.Text, 15)
            .RegistroAEE = Left(txtRegistroAEE.Text, 20)
            .RegistroPile = Left(txtRegistroPile.Text, 20)
            Try
                If .Save Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il record è stato salvato', 'Conferma');", True)
                    Response.Redirect("ListaProduttori.aspx")
                Else

                End If
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
            End Try
            
        End With

    End Sub

    Private Sub GetCurrentProduttore()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentProduttore = Produttore.Carica(IdFromQueryString)
        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaProduttori.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentProduttore()

            divError.Visible = False

            If Not CurrentProduttore Is Nothing Then
                txtCodice.Text = CurrentProduttore.Codice
                txtRagioneSociale.Text = CurrentProduttore.RagioneSociale
                txtEmail.Text = CurrentProduttore.Email
                ddlPeriodicita.SelectedValue = CurrentProduttore.Periodicita
                chkAttivo.Checked = CurrentProduttore.Attivo
                txtIndirizzo.Text = CurrentProduttore.Indirizzo
                txtCap.Text = CurrentProduttore.CAP
                txtCitta.Text = CurrentProduttore.Citta
                txtRappresentante.Text = CurrentProduttore.Rappresentante
                txtNote.Text = CurrentProduttore.Note
                txtEmailNotifiche.Text = CurrentProduttore.EmailNotifiche
                txtCodiceFiscale.Text = CurrentProduttore.CodiceFiscale
                chkDomestico.Checked = CurrentProduttore.Domestico
                chkProfessionale.Checked = CurrentProduttore.Professionale
                ChkEscludi.Checked = CurrentProduttore.EscludiMassivo
                ddlMeseAdesione.SelectedValue = CurrentProduttore.MeseAdesione
                txtQuota.Text = CurrentProduttore.QuotaConsortile
                txtCodiceSDI.Text = CurrentProduttore.CodiceSDI
                txtPartitaIVA.Text = CurrentProduttore.PartitaIVA
                txtPEC.Text = CurrentProduttore.PEC
                txtIBAN.Text = CurrentProduttore.IBAN
                txtTelefono.Text = CurrentProduttore.Telefono
                txtRegistroPile.Text = CurrentProduttore.RegistroPile
                txtRegistroAEE.Text = CurrentProduttore.RegistroAEE
                txtSconto.Text = CurrentProduttore.Sconto
                txtServizioRappresentanza.Text = CurrentProduttore.ServizioDiRappresentanza
                If Not Page.User.IsInRole("Amministratore") Then
                    txtNote.Visible = False
                End If
            End If
        End If

        If Page.User.IsInRole("Operatore") Then
            cmdSalva.Visible = False
        End If

    End Sub

    Protected Sub Confirm(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Protected Sub cmdCategorie_Click(sender As Object, e As EventArgs) Handles cmdCategorie.Click
        Response.Redirect("CategorieAbbinate.aspx?Id=" & CStr(Request.QueryString("Id")))
    End Sub

    Protected Sub cmdCategorieAbbinate_Click(sender As Object, e As EventArgs) Handles cmdCategorieAbbinate.Click
        Response.Redirect("CategorieAbbinate.aspx?Id=" & CStr(Request.QueryString("Id")) & "&ReadOnly=1")
    End Sub

    Protected Sub cmdCatProf_Click(sender As Object, e As EventArgs) Handles cmdCatProf.Click
        Response.Redirect("CatProfAbbinate.aspx?Id=" & CStr(Request.QueryString("Id")))
    End Sub

    Protected Sub cmdCatProfAbbinate_Click(sender As Object, e As EventArgs) Handles cmdCatProfAbbinate.Click
        Response.Redirect("CatProfAbbinate.aspx?Id=" & CStr(Request.QueryString("Id")) & "&ReadOnly=1")
    End Sub

    'Private Sub cmdCostoMatricola_Click(sender As Object, e As EventArgs) Handles cmdCostoMatricola.Click
    '    GetCurrentProduttore()
    '    If Not IsNothing(CurrentProduttore) Then
    '        Response.Redirect("CostoMatricola.aspx?Id=" & CurrentProduttore.Id)
    '    Else
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Produttore non salvato', 'Errore');", True)
    '    End If
    'End Sub

    Private Sub cmdAbbinamenti_Click(sender As Object, e As EventArgs) Handles cmdAbbinamenti.Click
        Response.Redirect("ListaAbbinamenti.aspx?Id=" & CStr(Request.QueryString("Id")))
    End Sub
End Class
