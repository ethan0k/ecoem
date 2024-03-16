
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected logoutText As String = "<span class='exit-icon'></span><span class='desc'>Logout</span>"

    Private overrideUrl As String = String.Empty


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        adminLoginStatus.LogoutText = logoutText
    End Sub

    Protected Function CheckChildSelect(siteMapNode As SiteMapNode) As Boolean
        Dim childNodes As SiteMapNodeCollection = siteMapNode.ChildNodes
        Dim enumerator As IEnumerator = childNodes.GetEnumerator()
        Dim url As String = Request.Url.AbsolutePath

        url = If(String.IsNullOrEmpty(url), "~/Default.aspx", url)

        While enumerator.MoveNext()
            Dim childSiteNode As SiteMapNode = CType(enumerator.Current, SiteMapNode)
            If (String.IsNullOrEmpty(overrideUrl) And url.StartsWith(childSiteNode.Url)) Or (Not String.IsNullOrEmpty(overrideUrl) And childSiteNode.Url.StartsWith(overrideUrl)) Then
                Return True
            End If
            If (childSiteNode.HasChildNodes) Then
                If (CheckChildSelect(childSiteNode)) Then
                    Return True
                End If
            End If
        End While

        Return False

    End Function

    Protected Function CheckSelect(siteMapNode As SiteMapNode) As Boolean
        Dim url As String = Request.Url.AbsolutePath
        url = If(String.IsNullOrEmpty(url), "~/Default.aspx", url)

        Return (String.IsNullOrEmpty(overrideUrl) And url.StartsWith(siteMapNode.Url)) Or (Not String.IsNullOrEmpty(overrideUrl) And siteMapNode.Url.StartsWith(overrideUrl))
    End Function

    Public Sub OverrideMenuUrl(url As String)
        overrideUrl = url
    End Sub

End Class

