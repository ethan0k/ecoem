Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Public Class Utilities

   
    Public Shared Function MembershipToDelete(ByVal UserId As Guid) As Boolean

        Return DataAccessHelper.GetDataAccess().MembershipDelete(UserId)

    End Function



End Class
