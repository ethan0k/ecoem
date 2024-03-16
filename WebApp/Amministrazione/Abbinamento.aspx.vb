Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_Abbinamento
    Inherits System.Web.UI.Page

    Private CurrentAbbinamento As AbbinamentoTipologiaFascia
    Private CurrentProduttore As Produttore

    Private Sub WebApp_Amministrazione_Abbinamento_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaProduttori.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_Abbinamento_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentAbbinamento()
            GetCurrentProduttore()

            If Not CurrentProduttore Is Nothing Then
                ddlProduttore.SelectedValue = CurrentProduttore.Id
            End If

            If Not CurrentAbbinamento Is Nothing Then
                    ddlProduttore.SelectedValue = CurrentAbbinamento.IdProduttore
                    ddlTipologia.SelectedValue = CurrentAbbinamento.IdTipologiaCella
                    ddlfascia.SelectedValue = CurrentAbbinamento.IdFasciaDiPeso
                    txtCostoMatricola.Text = CurrentAbbinamento.CostoMatricola
                End If

            End If

            If Page.User.IsInRole("Operatore") Then
            cmdSalva.Visible = False
        End If

    End Sub

    Private Sub GetCurrentAbbinamento()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentAbbinamento = AbbinamentoTipologiaFascia.Carica(IdFromQueryString)
        End If

    End Sub

    Private Sub GetCurrentProduttore()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("IdProduttore") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("IdProduttore")), IdFromQueryString) Then
            CurrentProduttore = Produttore.Carica(IdFromQueryString)
        End If

    End Sub
    Private Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoAbbinamento As AbbinamentoTipologiaFascia

        divError.Visible = False

        If ddlTipologia.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Tipologia di cella.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If ddlfascia.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Fascia di peso di cella.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If Not IsNumeric(txtCostoMatricola.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Costo matricola deve essere numerico.'" & ", 'Messaggio errore');", True)
            txtCostoMatricola.Focus()
            Exit Sub
        End If

        GetCurrentAbbinamento()

        If Not CurrentAbbinamento Is Nothing Then

            nuovoAbbinamento = AbbinamentoTipologiaFascia.Carica(CurrentAbbinamento.Id)
        Else
            Dim AbbinamentoEsistente As AbbinamentoTipologiaFascia = _
                AbbinamentoTipologiaFascia.Carica(ddlProduttore.SelectedValue, ddlTipologia.SelectedValue, _
                                                  ddlfascia.SelectedValue)
            If Not AbbinamentoEsistente Is Nothing Then
                divError.Visible = True
                lblError.Text = "Abbinamento esistente per il produttore selezionato"
                Exit Sub
            End If
            nuovoAbbinamento = New AbbinamentoTipologiaFascia
        End If

        With nuovoAbbinamento
            .IdProduttore = ddlProduttore.SelectedValue
            .IdTipologiaCella = ddlTipologia.SelectedValue
            .IdFasciaDiPeso = ddlfascia.SelectedValue
            .CostoMatricola = CDec(txtCostoMatricola.Text)

            Try
                If .Save Then
                    Response.Redirect("ListaAbbinamenti.aspx?Id=" & ddlProduttore.SelectedValue)
                End If
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
            End Try
        End With

    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Dim IdFromQueryString As Integer
        If Not Request.QueryString("IdProduttore") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("IdProduttore")), IdFromQueryString) Then
            Response.Redirect("ListaAbbinamenti.aspx?Id=" & IdFromQueryString)
        End If

    End Sub
End Class
