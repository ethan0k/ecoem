Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.ComponentModel.DataAnnotations
Imports System.Net
Imports System.Net.Mail
Partial Class WebApp_Amministrazione_Opzioni
    Inherits System.Web.UI.Page

    Private CurrentOpzione As Opzione

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentOpzione()

            If Not CurrentOpzione Is Nothing Then

                freeTestoMail.Text = Server.HtmlDecode(CurrentOpzione.TestoMail)
                txtOggetto.Text = CurrentOpzione.Oggetto
                txtNomeUtente.Text = CurrentOpzione.NomeUtente
                txtSmtp.Text = CurrentOpzione.Smtp
                txtPassword.Text = CurrentOpzione.Password
                chkUsaSSL.Checked = CurrentOpzione.SSL
                txtPorta.Text = CurrentOpzione.Porta
                txtMittente.Text = CurrentOpzione.Mittente
                txtDestinatarioTest.Text = CurrentOpzione.DestinatarioTest
                txtOggettoAutocertificazione.Text = CurrentOpzione.OggettoAutocertificazione
                freeTestoMailAutocertificazione.Text = Server.HtmlDecode(CurrentOpzione.TestoMailAutocertificazione)

            End If
        End If

        If Page.User.IsInRole("Operatore") Then
            cmdSalva.Visible = False
        End If

    End Sub

    Private Sub GetCurrentOpzione()

        CurrentOpzione = Opzione.Carica()

    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovaOpzione As Opzione

        GetCurrentOpzione()

        If Not IsNumeric(txtPorta.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Porta deve essere numerico.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        If Not IsValidEmailAddress(txtMittente.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Mittente non è una mail valida.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        If Not IsValidEmailAddress(txtDestinatarioTest.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Destinatario test non è una mail valida.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        If Not CurrentOpzione Is Nothing Then
            nuovaOpzione = Opzione.Carica
        Else
            nuovaOpzione = New Opzione
        End If

        With nuovaOpzione
            .Oggetto = Left(txtOggetto.Text, 200)
            .OggettoAutocertificazione = Left(txtOggettoAutocertificazione.Text, 200)
            .TestoMail = Server.HtmlEncode(freeTestoMail.Text)
            .TestoMailAutocertificazione = Server.HtmlEncode(freeTestoMailAutocertificazione.Text)
            .Smtp = txtSmtp.Text
            .Password = txtPassword.Text
            .NomeUtente = txtNomeUtente.Text
            .Porta = txtPorta.Text
            .SSL = chkUsaSSL.Checked
            .Mittente = Left(txtMittente.Text, 255)
            .DestinatarioTest = Left(txtDestinatarioTest.Text, 255)

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

    Public Function IsValidEmailAddress(ByVal strEmail As String) As Boolean
        ' Check An eMail Address To Ensure That It Is Valid
        Const cValidEmail = "^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$"   ' 98% Of All Valid eMail Addresses
        IsValidEmailAddress = False
        ' Take Care Of Blanks, Nulls & EOLs
        strEmail = Replace(Replace(Trim$(strEmail & " "), vbCr, ""), vbLf, "")
        ' Blank eMail Is Invalid
        If strEmail = "" Then Exit Function
        ' RegEx Test The eMail Address
        Dim regEx As New System.Text.RegularExpressions.Regex(cValidEmail)
        IsValidEmailAddress = regEx.IsMatch(strEmail)
    End Function

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click

        Dim smtp As New SmtpClient

        GetCurrentOpzione()

        If Not IsNumeric(CurrentOpzione.Porta) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Porta deve essere numerico.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        If Not IsValidEmailAddress(CurrentOpzione.Mittente) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Mittente non è una mail valida.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        If Not IsValidEmailAddress(CurrentOpzione.DestinatarioTest) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Destinatario test non è una mail valida.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        smtp.Host = CurrentOpzione.Smtp
        smtp.Port = CurrentOpzione.Porta
        smtp.EnableSsl = CurrentOpzione.SSL

        smtp.UseDefaultCredentials = False
        smtp.Credentials = New NetworkCredential(CurrentOpzione.NomeUtente, CurrentOpzione.Password)
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

        Dim NuovoMessaggio As New MailMessage

        NuovoMessaggio.From = New MailAddress(CurrentOpzione.Mittente, "Gestore")
        NuovoMessaggio.To.Add(New MailAddress(CurrentOpzione.DestinatarioTest, ""))
        NuovoMessaggio.IsBodyHtml = True
        NuovoMessaggio.Subject = CurrentOpzione.Oggetto
        NuovoMessaggio.Body = Server.HtmlDecode(CurrentOpzione.TestoMail)
        Try
            smtp.Send(NuovoMessaggio)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Mail inviata.'" & ", 'Informazione');", True)

        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & "', 'Messaggio errore');", True)

        End Try

    End Sub

    Private Sub cmdTestAutocertificazione_Click(sender As Object, e As EventArgs) Handles cmdTestAutocertificazione.Click

        Dim smtp As New SmtpClient

        GetCurrentOpzione()

        If Not IsNumeric(CurrentOpzione.Porta) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Porta deve essere numerico.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        If Not IsValidEmailAddress(CurrentOpzione.Mittente) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Mittente non è una mail valida.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        If Not IsValidEmailAddress(CurrentOpzione.DestinatarioTest) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il campo Destinatario test non è una mail valida.'" & ", 'Messaggio errore');", True)
            txtPorta.Focus()
            Exit Sub
        End If

        smtp.Host = CurrentOpzione.Smtp
        smtp.Port = CurrentOpzione.Porta
        smtp.EnableSsl = CurrentOpzione.SSL

        smtp.UseDefaultCredentials = False
        smtp.Credentials = New NetworkCredential(CurrentOpzione.NomeUtente, CurrentOpzione.Password)
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

        Dim NuovoMessaggio As New MailMessage

        NuovoMessaggio.From = New MailAddress(CurrentOpzione.Mittente, "Gestore")
        NuovoMessaggio.To.Add(New MailAddress(CurrentOpzione.DestinatarioTest, ""))
        NuovoMessaggio.IsBodyHtml = True
        NuovoMessaggio.Subject = CurrentOpzione.OggettoAutocertificazione
        NuovoMessaggio.Body = Server.HtmlDecode(CurrentOpzione.TestoMailAutocertificazione)
        Try
            smtp.Send(NuovoMessaggio)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Mail inviata.'" & ", 'Informazione');", True)

        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message & "', 'Messaggio errore');", True)

        End Try


    End Sub
End Class
