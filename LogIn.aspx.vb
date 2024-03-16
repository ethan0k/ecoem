Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class LogIn
    Inherits System.Web.UI.Page

    Protected Sub adminLogin_LoggedIn(sender As Object, e As EventArgs) Handles adminLogin.LoggedIn

        If Roles.IsUserInRole(adminLogin.UserName, "Produttore") Then
            Dim UtenteCorrente As MembershipUser = Membership.GetUser(adminLogin.UserName)
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(UtenteCorrente.ProviderUserKey)
            Dim myProduttore As Produttore = Produttore.Carica(UtenteProduttore.IdProduttore)
            If myProduttore.Professionale And Not Roles.IsUserInRole(adminLogin.UserName, "Professionale") Then
                Roles.AddUserToRole(adminLogin.UserName, "Professionale")
            ElseIf Not myProduttore.Professionale And Roles.IsUserInRole(adminLogin.UserName, "Professionale") Then
                Roles.RemoveUserFromRole(adminLogin.UserName, "Professionale")
            End If
        End If

        Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
    End Sub

   
End Class
