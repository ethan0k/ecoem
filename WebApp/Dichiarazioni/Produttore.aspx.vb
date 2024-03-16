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

            If Not CurrentProduttore Is Nothing Then
                txtCodice.Text = CurrentProduttore.Codice
                txtRagioneSociale.Text = CurrentProduttore.RagioneSociale
                txtEmail.Text = CurrentProduttore.Email
                ddlPeriodicita.SelectedValue = CurrentProduttore.Periodicita
                chkAttivo.Checked = CurrentProduttore.Attivo
                txtIndirizzo.Text = CurrentProduttore.Citta
                txtCap.Text = CurrentProduttore.CAP
                txtCitta.Text = CurrentProduttore.Citta
                txtRappresentante.Text = CurrentProduttore.Rappresentante
                txtNote.Text = CurrentProduttore.Note
                txtEmailNotifiche.Text = CurrentProduttore.EmailNotifiche
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

End Class
