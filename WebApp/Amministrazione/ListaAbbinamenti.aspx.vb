
Imports ASPNET.StarterKit.BusinessLogicLayer
Partial Class WebApp_Amministrazione_ListaAbbinamenti
    Inherits System.Web.UI.Page

    Private CurrentProduttore As Produttore

    Private Sub WebApp_Amministrazione_ListaAbbinamenti_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaProduttori.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_ListaAbbinamenti_Load(sender As Object, e As EventArgs) Handles Me.Load

        GetCurrentProduttore()

        If Not Page.IsPostBack Then

            If CurrentProduttore IsNot Nothing Then
                Titolo.Text = "Abbinamenti Tipologie celle / fasce di peso per il produttore " & CurrentProduttore.RagioneSociale
            End If

        End If

    End Sub

    Private Sub GetCurrentProduttore()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentProduttore = Produttore.Carica(IdFromQueryString)
        End If

    End Sub

    Private Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Abbinamento.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then

            Dim Abbinamento As AbbinamentoTipologiaFascia = AbbinamentoTipologiaFascia.Carica(CInt(e.CommandArgument.ToString()))

            Abbinamento.Elimina()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Abbinamento eliminato!', 'Messaggio errore');", True)
            Listview1.DataBind()
        End If

    End Sub

    Private Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Dim IdFromQueryString As Integer

        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            Response.Redirect("Abbinamento.aspx?IdProduttore=" & IdFromQueryString)
        End If
    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Dim IdFromQueryString As Integer

        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            Response.Redirect("Produttore.aspx?Id=" & IdFromQueryString)
        End If
    End Sub
End Class
