Imports System
Imports System.Configuration

Namespace ASPNET.StarterKit.DataAccessLayer

    Public Class DataAccessHelper

        Public Shared Function GetDataAccess() As DataAccess

            Dim dataAccessStringType As String = ConfigurationManager.AppSettings("aspnet_staterKits_TimeTracker")

            If String.IsNullOrEmpty(dataAccessStringType) Then

                Throw New NullReferenceException("ConnectionString configuration is missing from you web.config. It should contain  <connectionStrings> <add key=""aspnet_staterKits_TimeTracker"" value=""Server=(local);Integrated Security=True;Database=Issue_Tracker"" </connectionStrings>")

            Else

                Dim dataAccessType As Type = Type.GetType(dataAccessStringType)
                If dataAccessType Is Nothing Then
                    Throw New NullReferenceException("DataAccessType can not be found")
                End If

                Dim tp As Type = Type.GetType("ASPNET.StarterKit.DataAccessLayer.DataAccess")
                If (Not tp.IsAssignableFrom(dataAccessType)) Then
                    Throw (New ArgumentException("DataAccessType does not inherits from ASPNET.StarterKit.DataAccessLayer.DataAccess "))
                End If

                Dim dc As DataAccess = CType(Activator.CreateInstance(dataAccessType), DataAccess)
                Return dc

            End If

        End Function

    End Class
  
End Namespace
