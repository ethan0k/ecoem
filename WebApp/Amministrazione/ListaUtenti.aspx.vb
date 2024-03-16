Imports ASPNET.StarterKit.DataAccessLayer
Imports Utilities
Partial Class WebApp_Amministrazione_ListaUtenti
    Inherits System.Web.UI.Page

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        txtNominativo.Text = String.Empty
        txtUserName.Text = String.Empty

        Listview1.DataBind()
    End Sub

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("NuovoUtente.aspx")
    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Utente.aspx?id=" & e.CommandArgument.ToString())
        ElseIf String.Equals(e.CommandName, "ChangePwd") Then
            Response.Redirect("CambiaPassword.aspx?id=" & e.CommandArgument.ToString)

        ElseIf String.Equals(e.CommandName, "Elimina") Then
            Dim UserToDelete As MembershipUser = Membership.GetUser(e.CommandArgument.ToString)
            Roles.RemoveUserFromRoles(e.CommandArgument.ToString, Roles.GetRolesForUser(e.CommandArgument.ToString))

            If MembershipToDelete(UserToDelete.ProviderUserKey) Then
                Membership.DeleteUser(e.CommandArgument.ToString, True)
            End If

            Listview1.DataBind()
        End If



    End Sub

End Class
