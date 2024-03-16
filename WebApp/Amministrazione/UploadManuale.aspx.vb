Imports System.Data
Imports System.Data.OleDb
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net
Partial Class WebApp_Amministrazione_UploadManuale
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaManuali.aspx")
    End Sub

    Protected Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        If Not fileUpload.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("ManualiPath"))
        Dim fileName As String = fileUpload.FileName ' & DateTime.Now.ToString("ddMMyy_HHMMss")
        Dim PathFile As String = folder & "\" & fileName

        Try
            fileUpload.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Exit Sub

        End Try

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Caricamento completato!', 'Errore');", True)

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click

        Response.Redirect("ListaManuali.aspx")

    End Sub
End Class
