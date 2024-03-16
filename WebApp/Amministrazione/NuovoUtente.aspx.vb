Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_NuovoUtente
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaUtenti.aspx")
    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaUtenti.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            txtNuovaPassword.Text = String.Empty
            txtEmail.Text = String.Empty
        End If


    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        ' Verifica le password
        If txtNuovaPassword.Text <> txtConfermaPassword.Text Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Le password specificate non corrispondono.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If ddlRuoli.SelectedValue = "Produttore" Then
            If ddlProduttori.SelectedIndex = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un produttore da abbinare al nuovo utente.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If
        End If

        ' Verifica le password
        If ddlRuoli.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un ruolo.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If ddlRuoli.SelectedValue = "Utente" Then
            If ddlClienti.SelectedIndex = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un Cliente da abbinare al nuovo utente.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If
        End If

        Try ' Crea il nuovo utente
            Dim newUser As MembershipUser = Membership.CreateUser(txtUserName.Text, txtNuovaPassword.Text, txtEmail.Text)

            ' Aggiorna i dettagli dell'utente
            newUser.Comment = txtNominativo.Text
            Membership.UpdateUser(newUser)
            Roles.AddUserToRole(txtUserName.Text, ddlRuoli.SelectedItem.Text)

            If ddlRuoli.SelectedValue = "Produttore" Then
                Dim nuovoUtenteProduttore As New UtenteProduttore
                nuovoUtenteProduttore.UserId = newUser.ProviderUserKey
                nuovoUtenteProduttore.IdProduttore = ddlProduttori.SelectedValue
                nuovoUtenteProduttore.Save()
            ElseIf ddlRuoli.SelectedValue = "Utente" Then
                Dim nuovoUtenteCliente As New UtenteCliente
                nuovoUtenteCliente.UserId = newUser.ProviderUserKey
                nuovoUtenteCliente.IdCliente = ddlClienti.SelectedValue
                nuovoUtenteCliente.Save()

            End If

            ' Torna alla pagina precedente
            Response.Redirect("ListaUtenti.aspx")

        Catch ex As MembershipCreateUserException
            Dim strErrore = GetErrorMessage(ex.StatusCode)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & strErrore & ", 'Messaggio errore');", True)
            Exit Sub
        Catch ex As HttpException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & ", 'Messaggio errore');", True)
            Exit Sub

        End Try


    End Sub

    Public Function GetErrorMessage(ByVal status As MembershipCreateStatus) As String

        Select Case status
            Case MembershipCreateStatus.DuplicateUserName
                Return "Username already exists. Please enter a different user name."

            Case MembershipCreateStatus.DuplicateEmail
                Return "A username for that e-mail address already exists. Please enter a different e-mail address."

            Case MembershipCreateStatus.InvalidPassword
                Return "The password provided is invalid. Please enter a valid password value."

            Case MembershipCreateStatus.InvalidEmail
                Return "The e-mail address provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.InvalidAnswer
                Return "The password retrieval answer provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.InvalidQuestion
                Return "The password retrieval question provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.InvalidUserName
                Return "The user name provided is invalid. Please check the value and try again."

            Case MembershipCreateStatus.ProviderError
                Return "The authentication provider Returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator."

            Case MembershipCreateStatus.UserRejected
                Return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator."

            Case Else
                Return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator."
        End Select
    End Function

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
End Class
