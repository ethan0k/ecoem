Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_TipologiaCella
    Inherits System.Web.UI.Page

    Private CurrentTipologia As TipologiaCella

    Private Sub WebApp_Amministrazione_TipologiaCella_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaTipologie.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_TipologiaCella_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            GetCurrentTipologia()

            If Not CurrentTipologia Is Nothing Then
                txtDescrizioneTipologia.Text = CurrentTipologia.Descrizione
                txtDataModifica.Text = CurrentTipologia.DataUltModifica

            End If

        End If

        If Page.User.IsInRole("Operatore") Then
            cmdSalva.Visible = False
        End If

    End Sub

    Private Sub GetCurrentTipologia()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentTipologia = TipologiaCella.Carica(IdFromQueryString)
        End If

    End Sub

    Private Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim nuovaTipologia As TipologiaCella

        divError.Visible = False

        If txtDescrizioneTipologia.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Compilare il campo Descrizione.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        GetCurrentTipologia()

        If Not CurrentTipologia Is Nothing Then
            nuovaTipologia = TipologiaCella.Carica(CurrentTipologia.Id)
        Else
            nuovaTipologia = New TipologiaCella
        End If

        With nuovaTipologia
            .Descrizione = Left(txtDescrizioneTipologia.Text, 100)
            .DataUltModifica = Today

            Try
                If .Save Then
                    Response.Redirect("ListaTipologie.aspx")
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
