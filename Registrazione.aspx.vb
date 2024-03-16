Imports System.Net.Mail
Imports System.Net

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click

        txtEmail.Text = String.Empty
        txtCognome.Text = String.Empty
        txtNome.Text = String.Empty
        txtCap.Text = String.Empty
        txtComune.Text = String.Empty
        txtPartitaIva.Text = String.Empty
        txtCodiceFiscale.Text = String.Empty
        chkAzienda.Checked = False
        txtTelefono.Text = String.Empty
        txtIndirizzo.Text = String.Empty

    End Sub

    Protected Sub cmdInvia_Click(sender As Object, e As EventArgs) Handles cmdInvia.Click

        Dim smtp As New SmtpClient
        Dim NuovoMessaggio As New MailMessage

        If txtCodiceFiscale.Text = String.Empty And chkAzienda.Checked = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Specificare il Codice Fiscale.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        If txtPartitaIva.Text = String.Empty And chkAzienda.Checked Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Specificare la Partita IVA.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        NuovoMessaggio.From = New MailAddress("registrazione@ecoem", "Gestore")
        NuovoMessaggio.To.Add(New MailAddress(txtEmail.Text, ""))
        NuovoMessaggio.To.Add(New MailAddress("corrado.lembo@gmail.com", "Gestore"))
        NuovoMessaggio.Subject = "Richiesta registrazione sito Ecoem."
        NuovoMessaggio.Body = "Dettagli richiesta: " & vbCrLf & _
                            "COGNOME: " & txtCognome.Text & vbCrLf & _
                            "NOME: " & txtNome.Text & vbCrLf & _
                            "INDIRIZZO: " & txtIndirizzo.Text & vbCrLf & _
                            "CAP: " & txtCap.Text & vbCrLf & _
                            "COMUNE: " & txtComune.Text & vbCrLf & _
                            "TELEFONO: " & txtTelefono.Text & vbCrLf & _
                            "AZIENDA: " & chkAzienda.Checked & vbCrLf & _
                            "CODICE FISCALE: " & txtCodiceFiscale.Text & vbCrLf & _
                            "PARTITA IVA: " & txtPartitaIva.Text & vbCrLf & _
                            "EMAIL: " & txtEmail.Text

        Try
            smtp.Host = System.Configuration.ConfigurationManager.AppSettings("MailService_SMTPHost")
            smtp.UseDefaultCredentials = False
            smtp.EnableSsl = False
            smtp.Credentials = New NetworkCredential("registrazione#ecoem.it", "reco123456")
            'smtp.Credentials = New NetworkCredential(System.Configuration.ConfigurationManager.AppSettings("MailService_user").ToString, System.Configuration.ConfigurationManager.AppSettings("MailService_password").ToString)

            smtp.Send(NuovoMessaggio)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Messaggio inviato.'" & ", 'Messaggio');", True)
            txtEmail.Text = String.Empty
            txtCognome.Text = String.Empty
            txtNome.Text = String.Empty
            txtCap.Text = String.Empty
            txtComune.Text = String.Empty
            txtPartitaIva.Text = String.Empty
            txtCodiceFiscale.Text = String.Empty
            chkAzienda.Checked = False
            txtTelefono.Text = String.Empty
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & " Impossibile inviare il messaggio.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End Try

    End Sub

End Class
