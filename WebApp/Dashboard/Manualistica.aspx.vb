Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.IO
Imports System.Net
Partial Class WebApp_Dashboard_Manualistica
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFiles()
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
            Manuale.FilePath = System.Configuration.ConfigurationManager.AppSettings("ManualiPath").ToString & "/" & Manuale.FileName
            Manuale.Dimension = info.Length
            Manuale.Data = File.GetLastWriteTime(FilePath).ToShortDateString() & " " & File.GetLastWriteTime(FilePath).ToLongTimeString()
            ListaManuali.Add(Manuale)
        Next

        Listview1.DataSource = ListaManuali
        Listview1.DataBind()

    End Sub

    
End Class
