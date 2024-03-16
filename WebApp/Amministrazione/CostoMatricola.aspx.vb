Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_CostoMatricola
    Inherits System.Web.UI.Page

    Private CurrentProduttore As Produttore

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentProduttore()

            If Not CurrentProduttore Is Nothing Then

                txtCostoMatricola.Text = CurrentProduttore.CostoMatricola.ToString("#,##0.00")

            End If
        End If

    End Sub

    Private Sub GetCurrentProduttore()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentProduttore = Produttore.Carica(IdFromQueryString)
            lblTitolo.Text = "Costo matricola produttore " & CurrentProduttore.RagioneSociale
        End If

    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoProduttore As Produttore

        If Not IsNumeric(txtCostoMatricola.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Indicare un valore numerico per il campo [Costo matricola]', 'Conferma');", True)
            Exit Sub
        End If

        GetCurrentProduttore()

        If Not CurrentProduttore Is Nothing Then
            GetCurrentProduttore()
        Else
            Response.Redirect("ListaProduttori.aspx")
        End If

        With CurrentProduttore
            If CDec(txtCostoMatricola.Text) < 0 Then
                .CostoMatricola = 0
            Else
                .CostoMatricola = CDec(txtCostoMatricola.Text)
            End If

            Try
                If .Save Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il record è stato salvato', 'Conferma');", True)
                Else

                End If
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
            End Try

        End With


    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        GetCurrentProduttore()
        If Not IsNothing(CurrentProduttore) Then
            Response.Redirect("Produttore.aspx?id=" & CurrentProduttore.Id)
        Else
            Response.Redirect("ListaProduttori.aspx")
        End If
    End Sub
End Class
