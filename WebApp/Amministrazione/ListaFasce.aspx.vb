Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_ListaFasce
    Inherits System.Web.UI.Page

    Private Sub WebApp_Amministrazione_ListaFasce_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaFasce.aspx")
    End Sub

    Private Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("FasciaDiPeso.aspx")
    End Sub

    Private Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("FasciaDiPeso.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then
            If Not FasciaDiPeso.Verifica(CInt(e.CommandArgument.ToString())) Then

                Dim MyfasciaDiPeso As FasciaDiPeso = FasciaDiPeso.Carica(CInt(e.CommandArgument.ToString()))

                If FasciaDiPeso.Verifica(MyfasciaDiPeso.Id) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Fascia di peso presente nei pannelli!', 'Informazione');", True)
                    Exit Sub
                End If

                If MyfasciaDiPeso.Verifica() Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Fascia di peso presente in abbinamenti!', 'Informazione');", True)
                    Exit Sub
                End If

                MyfasciaDiPeso.Elimina()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Fascia di peso eliminata!', 'Informazione');", True)
                Listview1.DataBind()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Fascia di peso abbinata a produttore o pannello. Impossibile procedere'" & ", 'Messaggio errore');", True)
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
