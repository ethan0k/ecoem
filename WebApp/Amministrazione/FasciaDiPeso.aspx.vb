Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_FasciaDiPeso
    Inherits System.Web.UI.Page

    Private CurrentFascia As FasciaDiPeso
    Private Sub WebApp_Amministrazione_FasciaDiPeso_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaFasce.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_FasciaDiPeso_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentFascia()

            If Not CurrentFascia Is Nothing Then
                txtDescrizione.Text = CurrentFascia.Descrizione
                txtDataModifica.Text = CurrentFascia.DataUltModifica

            End If

        End If

        If Page.User.IsInRole("Operatore") Then
            cmdSalva.Visible = False
        End If

    End Sub

    Private Sub GetCurrentFascia()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentFascia = FasciaDiPeso.Carica(IdFromQueryString)
        End If

    End Sub

    Private Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovaFascia As FasciaDiPeso

        divError.Visible = False

        If txtDescrizione.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Descrizione.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        GetCurrentFascia()

        If Not CurrentFascia Is Nothing Then
            nuovaFascia = FasciaDiPeso.Carica(CurrentFascia.Id)
        Else
            nuovaFascia = New FasciaDiPeso
        End If

        With nuovaFascia
            .Descrizione = Left(txtDescrizione.Text, 100)
            .DataUltModifica = Today

            Try
                If .Save Then
                    Response.Redirect("Listafasce.aspx")
                End If
            Catch ex As Exception
                divError.Visible = True
                lblError.Text = ex.Message
            End Try
        End With

    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("Listafasce.aspx")
    End Sub
End Class
