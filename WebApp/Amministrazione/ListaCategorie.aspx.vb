Imports System.Threading
Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_ListaCategorie
    Inherits System.Web.UI.Page

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("Categoria.aspx")
    End Sub

    Private Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Categoria.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then
            If Not Categoria_Produttore.Verifica(CInt(e.CommandArgument.ToString())) Then

                Dim Categoria As Categoria = Categoria.Carica(CInt(e.CommandArgument.ToString()))

                Categoria.Elimina()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Categoria eliminata!', 'Informazione');", True)
                Listview1.DataBind()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Categoria abbinata a cliente. Impossibile procedere'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

        End If

    End Sub
    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        ddlMacroCategoria.SelectedIndex = 0
        txtFiltroCategoria.Text = ""
        Listview1.DataBind()
    End Sub

    Private Sub SqlDatasource1_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDatasource1.Selecting

        If e.Command.Parameters("@Categoria").Value Is Nothing Or e.Command.Parameters("@Categoria").Value = "Search" Then
            e.Command.Parameters("@Categoria").Value = ""
        End If

    End Sub

    Private Sub cmdAggiorna_Click(sender As Object, e As EventArgs) Handles cmdAggiorna.Click

        Dim ListaProduttori As List(Of Produttore)
        Dim CatRiferimento As CategoriaNew
        Dim ListaCatAbbinate As List(Of Categoria_ProduttoreNew)
        Dim Contatore As Integer

        ListaProduttori = Produttore.Lista

        For Each Produttore In ListaProduttori
            If Not Produttore.EscludiMassivo Then
                ListaCatAbbinate = Categoria_ProduttoreNew.Lista(Produttore.Id, False)
                If Not ListaCatAbbinate Is Nothing Then
                    For Each CatAbbinata In ListaCatAbbinate
                        Contatore += 1
                        CatRiferimento = CategoriaNew.Carica(CatAbbinata.IdCategoria)
                        If CatRiferimento.Valore > 0 Then
                            CatAbbinata.Costo = CatRiferimento.Valore
                            CatAbbinata.Save()
                        End If
                    Next
                End If
            End If
        Next

        If Contatore = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessuna modifica!', 'Conferma');", True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Operazione completata!', 'Conferma');", True)
        End If

    End Sub

    <Services.WebMethod> _
    Public Shared Function GetText() As String
        For i As Integer = 0 To 9
            Thread.Sleep(1000)
        Next i
        Return "All finished!"
    End Function

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        If (e.Item.ItemType = ListViewItemType.DataItem) Then

            Dim myLinkDeleteButton As LinkButton = CType(e.Item.FindControl("DeleteButton"), LinkButton)
            If Not myLinkDeleteButton Is Nothing Then
                myLinkDeleteButton.Visible = False
            End If
        End If

    End Sub
End Class
