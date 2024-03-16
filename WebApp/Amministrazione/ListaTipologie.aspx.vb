Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_ListaTipologie
    Inherits System.Web.UI.Page

    Private Sub WebApp_Amministrazione_ListaTipologie_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaTipologie.aspx")
    End Sub

    Private Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("TipologiaCella.aspx")
    End Sub

    Private Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("TipologiaCella.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then
            If Not TipologiaCella.Verifica(CInt(e.CommandArgument.ToString())) Then

                Dim MytipologiaCella As TipologiaCella = TipologiaCella.Carica(CInt(e.CommandArgument.ToString()))

                If TipologiaCella.Verifica(MytipologiaCella.Id) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipologia di cella presente nei pannelli!', 'Informazione');", True)
                    Exit Sub
                End If

                If MytipologiaCella.Verifica() Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipologia di cella presente in abbinamenti!', 'Informazione');", True)
                    Exit Sub
                End If

                MytipologiaCella.Elimina()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipologia cella eliminata!', 'Informazione');", True)
                Listview1.DataBind()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Tipologia cella abbinata a produttore o pannello. Impossibile procedere'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

        End If

    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        txtFiltroDescrizione.Text = String.Empty
        Listview1.DataBind()
    End Sub

    Private Sub SqlDatasource1_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDatasource1.Selecting
        If e.Command.Parameters("@Descrizione").Value Is Nothing Or e.Command.Parameters("@Descrizione").Value = "Search" Then
            e.Command.Parameters("@Descrizione").Value = ""
        End If
    End Sub
End Class
