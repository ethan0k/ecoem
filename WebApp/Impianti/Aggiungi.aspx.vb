Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Impianti_Aggiungi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Page.User.IsInRole("Utente") Then

                Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
                Dim UtenteCliente As UtenteCliente = UtenteCliente.Carica(userGuid)
                'Assegna il valore predefinito
                ddlCliente.SelectedValue = UtenteCliente.IdCliente
                divClienti.Attributes.CssStyle.Add("Display", "None")
                'ddlImpianti.Items.Clear()
                sqlImpianti.SelectCommand = ""

                Dim ListaImpianti As List(Of Impianto) = Impianto.CaricaDaIdCliente(UtenteCliente.IdCliente)

                If Not ListaImpianti Is Nothing Then
                    For Each Impianto In ListaImpianti
                        ddlImpianti.Items.Add(New ListItem(Impianto.Descrizione, Impianto.Id))
                    Next

                End If


            End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

       
    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click

        If Not Page.User.IsInRole("Utente") Then
            ddlCliente.SelectedIndex = 0
            txtMatricola.Text = String.Empty
        End If

    End Sub

    Protected Sub cmdAggiungi_Click(sender As Object, e As EventArgs) Handles cmdAggiungi.Click

        If txtCodiceProduttore.Text = String.Empty Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Inserire il codice produttore!', 'Errore');", True)
            Exit Sub
        End If

        If ddlImpianti.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Selezionare un impianto!', 'Errore');", True)
            Exit Sub
        End If

        Dim PannelloDaCaricare As Pannello = Pannello.Carica(txtMatricola.Text, txtCodiceProduttore.Text)

        If PannelloDaCaricare Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun pannello trovato!', 'Errore');", True)
            Exit Sub
        End If

        If PannelloDaCaricare.IdImpianto <> 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il pannello ricercato risulta abbinata ad altro impianto. Contattare l-amministratore di sistema.', 'Errore');", True)
            Exit Sub
        End If

        PannelloDaCaricare.IdImpianto = ddlImpianti.SelectedValue
        PannelloDaCaricare.Save()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il pannello è stato aggiunto.', 'Informazione');", True)

        txtMatricola.Text = String.Empty
        ddlImpianti.SelectedIndex = 0
        txtCodiceProduttore.Text = String.Empty
        If Not Page.User.IsInRole("Utente") Then
            ddlCliente.SelectedIndex = 0
        End If

    End Sub

    Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged
        ddlImpianti.Items.Clear()
        ddlImpianti.Items.Add(New ListItem("Selezionare ..", "0"))
        ddlImpianti.DataBind()
    End Sub

    
    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
       
    End Sub
End Class
