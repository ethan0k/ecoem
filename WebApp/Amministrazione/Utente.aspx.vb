Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_Utente
    Inherits System.Web.UI.Page

    Dim CurrentUser As MembershipUser

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaUtenti.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        GetCurrentUser()

        If (Not Page.IsPostBack) Then

            If Not CurrentUser Is Nothing Then

                txtUserName.Text = CurrentUser.UserName
                txtNominativo.Text = CurrentUser.Comment
                chkApprovato.Checked = CurrentUser.IsApproved
                txtEmail.Text = CurrentUser.Email
                chkBloccato.Checked = CurrentUser.IsLockedOut

                If Roles.IsUserInRole(CurrentUser.UserName, "Amministratore") Then
                    ddlRuoli.SelectedValue = "Amministratore"
                ElseIf Roles.IsUserInRole(CurrentUser.UserName, "Produttore") Then
                    divProduttore.Visible = True
                    ddlRuoli.SelectedValue = "Produttore"
                    Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)
                    ddlProduttori.SelectedValue = UtenteProduttore.IdProduttore
                ElseIf Roles.IsUserInRole(CurrentUser.UserName, "Utente") Then
                    divCliente.Visible = True
                    ddlRuoli.SelectedValue = "Utente"
                    Dim UtenteCliente As UtenteCliente = UtenteCliente.Carica(CurrentUser.ProviderUserKey)
                    ddlClienti.SelectedValue = UtenteCliente.IdCliente
                ElseIf Roles.IsUserInRole(CurrentUser.UserName, "Monitor") Then
                    ddlRuoli.SelectedValue = "Monitor"
                ElseIf Roles.IsUserInRole(CurrentUser.UserName, "Operatore") Then
                    ddlRuoli.SelectedValue = "Operatore"
                End If

            End If
        End If

    End Sub

    Private Sub GetCurrentUser()

        If Not Request.QueryString("Id") Is Nothing Then
            Dim userkey As Guid = New Guid(Request.QueryString("Id").ToString())
            CurrentUser = Membership.GetUser(userkey)
        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaUtenti.aspx")
    End Sub

    Protected Sub ddlRuoli_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRuoli.SelectedIndexChanged

        If ddlRuoli.SelectedValue = "Amministratore" Then
            divCliente.Visible = False
            ddlClienti.SelectedIndex = 0
            divProduttore.Visible = False
            ddlProduttori.SelectedIndex = 0
        ElseIf ddlRuoli.SelectedValue = "Produttore" Then
            divCliente.Visible = False
            ddlClienti.SelectedIndex = 0
            divProduttore.Visible = True
            ddlProduttori.SelectedIndex = 0
        ElseIf ddlRuoli.SelectedValue = "Utente" Then
            divCliente.Visible = True
            ddlClienti.SelectedIndex = 0
            divProduttore.Visible = False
            ddlProduttori.SelectedIndex = 0
        End If

    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim utenteDaAggiornare As MembershipUser

        divError.Visible = False

        If ddlRuoli.SelectedValue = "Utente" And ddlClienti.SelectedIndex = 0 Then
            divError.Visible = True
            lblError.Text = "Selezionare un cliente da abbinare all'utente."
            Exit Sub

        ElseIf ddlRuoli.SelectedValue = "Produttore" And ddlProduttori.SelectedIndex = 0 Then
            divError.Visible = True
            lblError.Text = "Selezionare un produttore da abbinare all'utente."
            Exit Sub

        End If

        GetCurrentUser()

        If Not CurrentUser Is Nothing Then
            utenteDaAggiornare = Membership.GetUser(CurrentUser.ProviderUserKey)

            ' Se faceva parte dei clienti
            If Roles.IsUserInRole(utenteDaAggiornare.UserName, "Utente") And ddlRuoli.SelectedValue <> "Utente" Then
                ' Rimuove
                Dim UtenteClienteDaEliminare As UtenteCliente = UtenteCliente.Carica(utenteDaAggiornare.ProviderUserKey)
                UtenteClienteDaEliminare.Elimina()
            ElseIf Roles.IsUserInRole(utenteDaAggiornare.UserName, "Produttore") And ddlRuoli.SelectedValue <> "Produttore" Then
                ' Rimuove
                Dim UtenteProduttoreDaEliminare As UtenteProduttore = UtenteProduttore.Carica(utenteDaAggiornare.ProviderUserKey)
                UtenteProduttoreDaEliminare.Elimina()
            End If

            ' Aggiorna le info
            utenteDaAggiornare.Comment = txtNominativo.Text
            utenteDaAggiornare.Email = txtEmail.Text
            utenteDaAggiornare.IsApproved = chkApprovato.Checked

            ' Sblocca l'utente
            If utenteDaAggiornare.IsLockedOut And Not chkBloccato.Checked Then
                utenteDaAggiornare.UnlockUser()
            End If

            Try
                If Not Roles.IsUserInRole(utenteDaAggiornare.UserName, ddlRuoli.SelectedValue) Then
                    Dim RuoliDaRimuovere(5) As String
                    RuoliDaRimuovere = Roles.GetRolesForUser(utenteDaAggiornare.UserName)
                    If RuoliDaRimuovere.Length > 0 Then
                        Roles.RemoveUserFromRoles(utenteDaAggiornare.UserName, RuoliDaRimuovere)
                    End If
                    Roles.AddUserToRole(utenteDaAggiornare.UserName, ddlRuoli.SelectedValue)
                End If

                Membership.UpdateUser(utenteDaAggiornare)

                ' Se Ruolo cliente lo aggiunge
                If ddlRuoli.SelectedValue = "Utente" Then
                    ' Se Ruolo cliente aggiunge l'abbinamento o lo aggiorna

                    Dim UtenteCliente As UtenteCliente = UtenteCliente.Carica(utenteDaAggiornare.ProviderUserKey)
                    If UtenteCliente Is Nothing Then
                        UtenteCliente = New UtenteCliente
                    End If

                    UtenteCliente.UserId = utenteDaAggiornare.ProviderUserKey
                    UtenteCliente.IdCliente = ddlClienti.SelectedValue
                    UtenteCliente.Save()

                ElseIf ddlRuoli.SelectedValue = "Produttore" Then
                    ' Lo aggiunge ai produttori
                    Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(utenteDaAggiornare.ProviderUserKey)
                    If UtenteProduttore Is Nothing Then
                        UtenteProduttore = New UtenteProduttore
                    End If

                    UtenteProduttore.UserId = utenteDaAggiornare.ProviderUserKey
                    UtenteProduttore.IdProduttore = ddlProduttori.SelectedValue
                    UtenteProduttore.Save()
                End If

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il record è stato salvato', 'Conferma');", True)
            Catch ex As Exception
                lblError.Text = ex.Message
                divError.Visible = True
                Exit Sub
            End Try

        Else
            lblError.Text = "Errore. Impossibile salvare"
            divError.Visible = True
            Exit Sub
        End If

    End Sub

End Class
