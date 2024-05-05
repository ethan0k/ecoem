Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Pannelli_Pannello
    Inherits System.Web.UI.Page

    Private CurrentPannello As Pannello

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Pannelli/ListaPannelli.aspx")
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then


            GetCurrentPannello()

            If Not CurrentPannello Is Nothing Then

                txtMatricola.Text = CurrentPannello.Matricola
                txtModello.Text = CurrentPannello.Modello
                txtPeso.Text = CurrentPannello.Peso
                txtProduttore.Text = CurrentPannello.Produttore
                txtDataInizioGaranzia.Text = CurrentPannello.DataInizioGaranzia
                txtDataCaricamento.Text = CurrentPannello.DataCaricamento
                txtProtocollo.Text = CurrentPannello.Protocollo
                txtMarca.Text = CurrentPannello.Produttore
                txtCostoMatricola.Text = CurrentPannello.CostoMatricola.ToString("#,0.00")
                If CurrentPannello.DataConformita > DefaultValues.GetDateTimeMinValue Then
                    txtDataConformità.Text = CurrentPannello.DataConformita
                End If
                If CurrentPannello.DataRitiro > DefaultValues.GetDateTimeMinValue Then
                    txtDataritiro.Text = CurrentPannello.DataRitiro
                End If
                txtNumeroFIR.Text = CurrentPannello.NumeroFIR
                chkConforme.Checked = CurrentPannello.Conforme
                chkDismesso.Checked = CurrentPannello.Dismesso
                Try
                    ddlProduttore.SelectedValue = CurrentPannello.IdMarca
                Catch ex As Exception

                End Try

                'DivConsorziato.Visible = False
                divImpianto.Visible = True
                divProduttore.Visible = False
                If CurrentPannello.IdImpianto <> 0 Then
                    Dim Impianto As Impianto = Impianto.Carica(CurrentPannello.IdImpianto)
                    If Not Impianto Is Nothing Then
                        txtImpianto.Text = Impianto.Descrizione
                    Else
                        txtImpianto.Text = "Impianto eliminato.."
                        txtImpianto.ForeColor = Drawing.Color.Red
                        cmdDisabbina.Visible = True
                    End If
                Else
                    Dim Utente As UtenteProduttore = UtenteProduttore.Carica(Membership.GetUser(Page.User.Identity.Name).ProviderUserKey)
                    If Not Utente Is Nothing Then
                        ddlProduttore.SelectedValue = Utente.IdProduttore
                    End If
                End If

                If Page.User.IsInRole("Amministratore") Then
                    chkDismesso.Enabled = True
                End If

                If Not Page.User.IsInRole("Amministratore") Then
                    txtMatricola.ReadOnly = True
                    txtModello.ReadOnly = True
                    txtPeso.ReadOnly = True
                    txtDataInizioGaranzia.ReadOnly = True
                    txtDataCaricamento.ReadOnly = True
                    txtProtocollo.ReadOnly = True
                    chkConforme.Enabled = False
                    cmdSalva.Visible = False
                End If

            Else
                    divError.Visible = False
            End If

        End If
    End Sub

    Private Sub GetCurrentPannello()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentPannello = Pannello.Carica(IdFromQueryString)
        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaPannelli.aspx")
    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoPannello As Pannello

        If Not IsNumeric(txtPeso.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il valore del peso deve essere un numero.', 'Errore');", True)
            Exit Sub
        End If

        If Not IsDate(txtDataInizioGaranzia.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un consorziato.', 'Errore');", True)
            Exit Sub
        End If

        If ddlProduttore.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un consorziato.', 'Errore');", True)
            Exit Sub
        End If

        GetCurrentPannello()

        If Not CurrentPannello Is Nothing Then
            nuovoPannello = Pannello.Carica(CurrentPannello.Id)
        Else
            nuovoPannello = New Pannello
        End If

        With nuovoPannello
            .Matricola = Left(txtMatricola.Text, 20)
            .Modello = Left(txtModello.Text, 30)
            .Peso = CDec(txtPeso.Text)
            '.Produttore = Left(txtProduttore.Text, 50)
            '.IdImpianto=ddlImpianti.SelectedValue
            If IsDate(txtDataInizioGaranzia.Text) Then
                .DataInizioGaranzia = CDate(txtDataInizioGaranzia.Text)
            End If
            If IsDate(txtDataritiro.Text) Then
                .DataRitiro = CDate(txtDataritiro.Text)
            End If
            .NumeroFIR = Left(txtNumeroFIR.Text, 30)
            .Protocollo = Left(txtProtocollo.Text, 20)
            '.Conforme = chkConforme.Checked
            .Dismesso = chkDismesso.Checked
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

    Protected Sub cmdDisabbina_Click(sender As Object, e As EventArgs) Handles cmdDisabbina.Click

        GetCurrentPannello()

        CurrentPannello.IdImpianto = 0

        CurrentPannello.Save()

        cmdDisabbina.Visible = False

        txtImpianto.Text = ""

    End Sub
End Class
