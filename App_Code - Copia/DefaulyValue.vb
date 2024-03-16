Imports System
Imports System.Data.SqlTypes

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class DefaultValues

        Private Sub New()
        End Sub

        Public Shared Function GetCategoryIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetDateTimeMinValue() As DateTime

            Return CDate(SqlDateTime.MinValue).AddYears(30)

        End Function

        Public Shared Function GetDurationMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetCustomFieldIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetIssueIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetIssueCommentIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetIssueNotificationIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetMilestoneIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetPriorityIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetProjectIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetProjectDurationMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetStatusIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetTimeEntryIdMinValue() As Integer

            Return 0

        End Function

        Public Shared Function GetUserIdMinValue() As Integer

            Return 0

        End Function

    End Class

End Namespace

