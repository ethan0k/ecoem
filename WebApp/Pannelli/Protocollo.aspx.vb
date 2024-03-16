Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Pannelli_Protocollo
    Inherits System.Web.UI.Page

    Dim CurrentProtocollo As Protocollo

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Pannelli/ListaProtocolli.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            GetCurrentProtocollo()

            If Not CurrentProtocollo Is Nothing Then
                txtProtocollo.Text = CurrentProtocollo.Protocollo
                txtData.Text = CurrentProtocollo.Data
                txtNrFattura.Text = CurrentProtocollo.NrFattura
                txtNrProforma.Text = CurrentProtocollo.NrProforma
                If CurrentProtocollo.DataFattura > #01/01/2001# Then
                    txtDataFattura.Text = CurrentProtocollo.DataFattura
                End If
                If CurrentProtocollo.DataProforma > #01/01/2001# Then
                    txtDataProforma.Text = CurrentProtocollo.DataProforma
                End If
                txtCCT.Text = CurrentProtocollo.CCT
                txtUsername.Text = CurrentProtocollo.UserName
                txtNrAttestato.Text = CurrentProtocollo.NrAttestato
                If CurrentProtocollo.DataAttestato > DefaultValues.GetDateTimeMinValue() Then
                    txtDataAttestato.Text = CurrentProtocollo.DataAttestato
                End If
                lblCostoServizio.Text = CurrentProtocollo.CostoServizio
            End If

        End If

        If Page.User.IsInRole("Operatore") Then
            cmdSalva.Visible = False
        End If

    End Sub

    Private Sub GetCurrentProtocollo()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentProtocollo = Protocollo.CaricaDaId(IdFromQueryString)
        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaProtocolli.aspx")
    End Sub

   
    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovoProtocollo As Protocollo

        GetCurrentProtocollo()

        If Not CurrentProtocollo Is Nothing Then
            nuovoProtocollo = Protocollo.CaricaDaId(CurrentProtocollo.Id)
        Else
            nuovoProtocollo = New Protocollo
        End If

        With nuovoProtocollo
            .NrFattura = Left(txtNrFattura.Text, 20)
            .CCT = Left(txtCCT.Text, 20)
            .NrAttestato = txtNrAttestato.Text
            If txtDataAttestato.Text <> "" Then
                .DataAttestato = txtDataAttestato.Text
            Else
                .DataAttestato = DefaultValues.GetDateTimeMinValue
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
End Class
