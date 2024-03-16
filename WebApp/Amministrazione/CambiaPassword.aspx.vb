
Partial Class WebApp_Amministrazione_CambiaPassword
    Inherits System.Web.UI.Page

    Dim CurrentUser As MembershipUser

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaUtenti.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        GetCurrentUser()

        If Not CurrentUser Is Nothing Then
            txtUtente.Text = CurrentUser.UserName
        End If
    End Sub

    Private Sub GetCurrentUser()

        If Not Request.QueryString("Id") Is Nothing Then
            Dim userkey As Guid = New Guid(Request.QueryString("Id").ToString())
            CurrentUser = Membership.GetUser(userkey)
        End If

    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click
        Dim strOldPassword As String

        If txtPassword.Text <> txtConfermaPassword.Text Then
            divError.Visible = True
            lblError.Text = "Le password fornite non coincidono."
            Exit Sub
        End If

        GetCurrentUser()

        If Not CurrentUser Is Nothing Then

            strOldPassword = CurrentUser.ResetPassword()

            Try
                CurrentUser.ChangePassword(strOldPassword, txtPassword.Text)
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message

                Exit Sub
            End Try

            Response.Redirect("ListaUtenti.aspx")

        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaUtenti.aspx")
    End Sub
End Class
