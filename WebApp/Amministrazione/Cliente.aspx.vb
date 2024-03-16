Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_Cliente
    Inherits System.Web.UI.Page

    Private CurrentCliente As Cliente

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaClienti.aspx")
    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaClienti.aspx")
    End Sub

    Private Sub GetCurrentCliente()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentCliente = Cliente.Carica(IdFromQueryString)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentCliente()

            If Not CurrentCliente Is Nothing Then
                txtRagioneSociale.Text = CurrentCliente.RagioneSociale
                txtIndirizzo.Text = CurrentCliente.Indirizzo
                txtCap.Text = CurrentCliente.Cap
                txtContatto.Text = CurrentCliente.Contatto
                txtProvincia.Text = CurrentCliente.Provincia
                txtCittà.Text = CurrentCliente.Città
                txtFax.Text = CurrentCliente.Fax
                txtEmail.Text = CurrentCliente.Email
                txtCognome.Text = CurrentCliente.Cognome
                txtNome.Text = CurrentCliente.Nome
                txtCodiceFiscale.Text = CurrentCliente.CodiceFiscale
                txtPartitaIva.Text = CurrentCliente.PartitaIva
                ddlPeriodicita.SelectedValue = CurrentCliente.Periodicita
                chkAttivo.Checked = CurrentCliente.Attivo
                txtTelefono.Text = CurrentCliente.Telefono
                txtFax.Text = CurrentCliente.Fax
                txtNote.Text = CurrentCliente.Note
            End If

            If Not Page.User.IsInRole("Amministratore") Then
                txtNote.Visible = False
            End If
        End If

    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoCliente As Cliente
        Dim CreaAbbinamenti As Boolean

        divError.Visible = False

        If txtCognome.Text = "" And txtRagioneSociale.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare i campi Ragione Sociale o Cognome e Nome.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If (txtRagioneSociale.Text <> "") And (txtPartitaIva.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Partita IVA.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If (txtCognome.Text <> "") And (txtNome.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo NOME.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If (txtNome.Text <> "") And (txtCognome.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo COGNOME.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If (txtCognome.Text <> "") And (txtCodiceFiscale.Text = "") Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo CODICE FISCALE.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        GetCurrentCliente()

        If Not CurrentCliente Is Nothing Then
            nuovoCliente = Cliente.Carica(CurrentCliente.IdCliente)
        Else
            nuovoCliente = New Cliente
            CreaAbbinamenti = True
        End If

        With nuovoCliente
            .RagioneSociale = Left(txtRagioneSociale.Text, 50)
            .Indirizzo = Left(txtIndirizzo.Text, 50)
            .Cap = Left(txtCap.Text, 10)
            .Città = Left(txtCittà.Text, 50)
            .Provincia = Left(txtProvincia.Text, 50)
            .Telefono = Left(txtTelefono.Text, 15)
            .Fax = Left(txtFax.Text, 15)
            .PartitaIva = Left(txtPartitaIva.Text, 11)
            .Contatto = Left(txtContatto.Text, 50)
            .Email = Left(txtEmail.Text, 200)
            .Cognome = Left(txtCognome.Text, 20)
            .Nome = Left(txtNome.Text, 20)
            .CodiceFiscale = Left(txtCodiceFiscale.Text, 20)
            .Periodicita = ddlPeriodicita.SelectedValue
            .Attivo = chkAttivo.Checked
            .Note = txtNote.Text
            Try
                If .Save Then
                    Response.Redirect("ListaClienti.aspx")

                Else

                End If
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
            End Try


        End With



    End Sub



End Class
