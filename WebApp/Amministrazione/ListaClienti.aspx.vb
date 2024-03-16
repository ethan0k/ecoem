Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_ListaClienti
    Inherits System.Web.UI.Page

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("Cliente.aspx")
    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Cliente.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then
            Dim Cliente As Cliente = Cliente.Carica(CInt(e.CommandArgument.ToString()))
            If Not Cliente.VerificaImpianti Then
                Cliente.Elimina()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Cliente eliminato!', 'Informazione');", True)
                Listview1.DataBind()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile eliminare. Esistono impianti abbinati!', 'Errore');", True)
            End If

        End If

    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        txtPartitaIVA.Text = String.Empty
        txtRagioneSociale.Text = String.Empty

        SqlDatasource1.SelectCommand = "SELECT IdCliente, RagioneSociale, Cognome + ' ' + Nome AS Nominativo, Contatto, Telefono FROM tbl_Clienti Order By RagioneSociale"

        Listview1.DataBind()
    End Sub

    Private Sub cmdCerca_Click(sender As Object, e As EventArgs) Handles cmdCerca.Click

        Dim IsIn As Boolean

        Dim StrSql As String = "SELECT IdCliente, RagioneSociale, Cognome + ' ' + Nome AS Nominativo, Contatto, Telefono FROM tbl_Clienti "

        If (txtRagioneSociale.Text <> "") And (txtRagioneSociale.Text <> "Search") Then
            StrSql = StrSql + " WHERE (RagioneSociale Like '%" + txtRagioneSociale.Text + "%' OR (Cognome + ' ' + Nome) Like  '%" + txtRagioneSociale.Text + "%')"
            IsIn = True

        End If

        If txtPartitaIVA.Text <> "" And txtPartitaIVA.Text <> "Search" Then
            If IsIn Then
                StrSql = StrSql + "AND PartitaIva Like '%" + txtPartitaIVA.Text + "%' "
            Else
                StrSql = StrSql + "WHERE PartitaIva Like '%" + txtPartitaIVA.Text + "%' "
            End If
        End If

        StrSql = StrSql + " Order By RagioneSociale"
        SqlDatasource1.SelectCommand = StrSql

    End Sub
End Class
