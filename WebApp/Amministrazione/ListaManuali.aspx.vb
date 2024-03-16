Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.IO
Imports System.Net

Partial Class WebApp_Amministrazione_ListaManuali
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        LoadFiles()

        If Page.User.IsInRole("Operatore") Then
            cmdNuovo.Visible = False
        End If

    End Sub

    Private Sub LoadFiles()

        Dim ListaManuali As New List(Of Files)

        Listview1.Items.Clear()
        Dim FolderPath As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("ManualiPath"))
        Dim FilePath As String
        For Each FilePath In IO.Directory.GetFiles(FolderPath)
            Dim info As New FileInfo(FilePath)
            Dim Manuale As New Files
            Manuale.FileName = FilePath.Substring(FilePath.LastIndexOf("\") + 1)
            Manuale.FilePath = FilePath
            Manuale.Dimension = info.Length
            Manuale.Data = File.GetLastWriteTime(FilePath).ToShortDateString() & " " & File.GetLastWriteTime(FilePath).ToLongTimeString()
            ListaManuali.Add(Manuale)
        Next

        Listview1.DataSource = ListaManuali
        Listview1.DataBind()

    End Sub

    Private Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Elimina") Then

            File.Delete(e.CommandArgument)

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('File eliminato!', 'Informazione');", True)
            LoadFiles()

        ElseIf String.Equals(e.CommandName, "Download") Then
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + e.CommandArgument)
            Response.TransmitFile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("ManualiPath").ToString & "\" & e.CommandArgument))
            Response.End()

        End If

    End Sub

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("UploadManuale.aspx")
    End Sub

    Protected Function GenerateBrochureLink(BrochurePath As Object) As String

        If Convert.IsDBNull(BrochurePath) Then
            Return "No Brochure Available"
        Else
            Return String.Format("<a href='{0}'", ResolveUrl(BrochurePath.ToString()))
        End If

    End Function


End Class
