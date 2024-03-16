
Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_Categoria
    Inherits System.Web.UI.Page

    Private CurrentCategoria As CategoriaNew

    Private Sub WebApp_Amministrazione_Categoria_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaCategorie.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_Categoria_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentCategoria()

            If Not CurrentCategoria Is Nothing Then
                txtNome.Text = CurrentCategoria.Nome
                ddlMacroCategoria.SelectedValue = CurrentCategoria.IdMacrocategoria
                ddlTipoDiDato.SelectedValue = CurrentCategoria.TipoDiDato
                txtDataModifica.Text = CurrentCategoria.DataModifica
                txtCodifica.Text = CurrentCategoria.Codifica
                txtRaggruppamento.Text = CurrentCategoria.Raggruppamento
                txtCosto.Text = CurrentCategoria.Valore
                txtPesoPerUnita.Text = CurrentCategoria.PesoPerUnita
            End If

        End If

        If Page.User.IsInRole("Operatore") Then
            cmdSalva.Visible = False
        End If

    End Sub

    Private Sub GetCurrentCategoria()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentCategoria = CategoriaNew.Carica(IdFromQueryString)
        End If

    End Sub
    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaCategorie.aspx")
    End Sub
    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoCategoria As CategoriaNew

        divError.Visible = False

        If ddlMacroCategoria.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Macrocategoria.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If txtNome.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Nome.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If ddlTipoDiDato.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Tipo di dato.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If Not IsNumeric(txtCosto.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Costo deve essere numerico.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If Not IsNumeric(txtPesoPerUnita.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo [Peso per unità] deve essere numerico.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        GetCurrentCategoria()

        If Not CurrentCategoria Is Nothing Then
            nuovoCategoria = CategoriaNew.Carica(CurrentCategoria.Id)
        Else
            nuovoCategoria = New CategoriaNew
        End If

        With nuovoCategoria
            .Nome = Left(txtNome.Text, 255)
            .IdMacrocategoria = ddlMacroCategoria.SelectedValue
            .TipoDiDato = ddlTipoDiDato.SelectedValue
            .Valore = txtCosto.Text
            .Codifica = Left(txtCodifica.Text, 10)
            .Raggruppamento = Left(txtRaggruppamento.Text, 10)
            .Valore = CDec(txtCosto.Text)
            .DataModifica = Today
            .PesoPerUnita = txtPesoPerUnita.Text
            Try
                If .Save Then
                    Response.Redirect("ListaCategorie.aspx")
                End If
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
            End Try
        End With
    End Sub
End Class
