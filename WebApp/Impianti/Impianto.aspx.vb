Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Impianti_Impianto
    Inherits System.Web.UI.Page

    Private CurrentImpianto As Impianto

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Impianti/ListaImpianti.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            GetCurrentImpianto()

            If Page.User.IsInRole("Utente") Then
                Dim UtenteCliente As UtenteCliente = UtenteCliente.Carica(Membership.GetUser(Page.User.Identity.Name).ProviderUserKey)


                ddlCliente.SelectedValue = UtenteCliente.IdCliente

                divCliente.Visible = False

                If Not CurrentImpianto Is Nothing Then

                    If CurrentImpianto.IdCliente <> UtenteCliente.IdCliente Then
                        Response.Redirect("ListaImpianti.aspx")
                        Exit Sub
                    End If

                    ddlCliente.Visible = False
                    lblCliente.Visible = False
                    cmdSalva.Visible = False
                    ddlCliente.SelectedValue = UtenteCliente.IdCliente
                    txtDataEntrataInEsercizio.ReadOnly = True
                    txtDescrizione.ReadOnly = True
                    txtIndirizzo.ReadOnly = True
                    txtCap.ReadOnly = True
                    txtCittà.ReadOnly = True
                    ddlProvincia.Enabled = False
                    txtLatitudine.ReadOnly = True
                    txtLongitudine.ReadOnly = True
                    ddlRegione.Enabled = False
                    txtNrPraticaGSE.ReadOnly = True
                    ddlContoEnergia.Enabled = False
                    groupCosto.Visible = False
                End If

            ElseIf Page.User.IsInRole("Monitor") Then

                ddlCliente.Visible = False
                lblCliente.Visible = False
                cmdSalva.Visible = False
                txtDataEntrataInEsercizio.ReadOnly = True
                txtDescrizione.ReadOnly = True
                txtIndirizzo.ReadOnly = True
                txtCap.ReadOnly = True
                txtCittà.ReadOnly = True
                ddlProvincia.Enabled = False
                txtLatitudine.ReadOnly = True
                txtLongitudine.ReadOnly = True
                ddlRegione.Enabled = False
                txtNrPraticaGSE.ReadOnly = True
                ddlContoEnergia.Enabled = False
                groupCosto.Visible = False
            ElseIf Page.User.IsInRole("Amministratore") then
                divNomeProd.Visible=True
                cmdDisabbina.Visible = True
                groupCosto.Visible = True
            End If

            If Not CurrentImpianto Is Nothing Then
                txtDataEntrataInEsercizio.Text = CurrentImpianto.DataEntrataInEsercizio
                txtDescrizione.Text = CurrentImpianto.Descrizione
                txtIndirizzo.Text = CurrentImpianto.Indirizzo
                txtCap.Text = CurrentImpianto.Cap
                txtCittà.Text = CurrentImpianto.Citta
                ddlProvincia.SelectedValue = CurrentImpianto.Provincia
                txtLatitudine.Text = CurrentImpianto.Latitudine
                txtLongitudine.Text = CurrentImpianto.Longitudine
                txtResponsabile.Text = CurrentImpianto.Responsabile
                ddlRegione.SelectedValue = CurrentImpianto.Regione
                ddlCliente.SelectedValue = CurrentImpianto.IdCliente
                txtNrPraticaGSE.Text = CurrentImpianto.NrPraticaGSE
                ddlContoEnergia.SelectedValue = Trim(CurrentImpianto.ContoEnergia)
                txtNomeProduttore.Text = CurrentImpianto.NomeProduttore
                txtCosto.Text = Format(CurrentImpianto.Valore, "0.00")

            End If

        End If

    End Sub

    Private Sub GetCurrentImpianto()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentImpianto = Impianto.Carica(IdFromQueryString)
        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaImpianti.aspx")
    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoImpianto As Impianto

        If Not Page.User.IsInRole("Utente") Then
            If ddlCliente.SelectedIndex = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un cliente.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If
        End If

        If ddlRegione.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare una regione.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If ddlProvincia.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare una provincia.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If Not IsDate(txtDataEntrataInEsercizio.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Data Entrata in esercizio è errato.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If ddlContoEnergia.SelectedValue <> "N" And txtNrPraticaGSE.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Nr. pratica GSE non può essere vuoto.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        GetCurrentImpianto()

        If Not CurrentImpianto Is Nothing Then
            nuovoImpianto = Impianto.Carica(CurrentImpianto.Id)
        Else
            nuovoImpianto = New Impianto
        End If

        With nuovoImpianto
            .Codice = ""
            .Descrizione = Left(txtDescrizione.Text, 50)
            .Indirizzo = Left(txtIndirizzo.Text, 50)
            .Cap = Left(txtCap.Text, 5)
            .Citta = Left(txtCittà.Text, 50)
            .Provincia = ddlProvincia.SelectedValue
            .Latitudine = UCase(Left(txtLatitudine.Text, 20))
            .Longitudine = UCase(Left(txtLongitudine.Text, 20))
            If Page.User.IsInRole("Utente") Then
                Dim MioUtenteCliente As UtenteCliente = UtenteCliente.Carica(Membership.GetUser(Page.User.Identity.Name).ProviderUserKey)
                .IdCliente = MioUtenteCliente.IdCliente
            Else
                .IdCliente = ddlCliente.SelectedValue
            End If

            .DataAttestato = DefaultValues.GetDateTimeMinValue
            .DataEntrataInEsercizio = CDate(txtDataEntrataInEsercizio.Text)
            If .DataCreazione <= DefaultValues.GetDateTimeMinValue Then
                .DataCreazione = Today
            End If
            .Regione = ddlRegione.SelectedValue
            .Responsabile = Left(txtResponsabile.Text, 50)
            .NrPraticaGSE = UCase(Left(txtNrPraticaGSE.Text, 20))
            .ContoEnergia = UCase(Left(ddlContoEnergia.SelectedValue, 10))
            .NomeProduttore = Left(txtNomeProduttore.Text,100)
            Try
                If .Save Then
                    Response.Redirect("ListaImpianti.aspx")

                Else

                End If
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
            End Try

        End With

    End Sub

    Private Sub cmdDisabbina_Click(sender As Object, e As EventArgs) Handles cmdDisabbina.Click

        GetCurrentImpianto

        If CurrentImpianto IsNot Nothing Then

            Try
                CurrentImpianto.Disabbina
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Operazione completata', 'Conferma');", True)            
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & "', 'Errore');", True)            
                Exit Sub
            End Try

        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impianto inesistente', 'Errore');", True)            
        End If 

    End Sub
End Class
