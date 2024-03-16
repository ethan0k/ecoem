Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class Files

        Private _FileName As String
        Private _FilePath As String
        Private _Description As String
        Private _Dimension As Decimal
        Private _Data As DateTime

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal FileName As String, ByVal FilePath As String, ByVal Description As String, ByVal Dimension As Decimal, ByVal Data As DateTime)

            Me._FileName = FileName
            Me._FilePath = FilePath
            Me._Description = Description
            Me._Dimension = Dimension
            Me._Data = Data

        End Sub

        Public Property FileName() As String

            Get
                If String.IsNullOrEmpty(Me._FileName) Then
                    Return String.Empty
                Else
                    Return Me._FileName
                End If
            End Get

            Set(ByVal value As String)
                Me._FileName = value
            End Set

        End Property

        Public Property FilePath() As String

            Get
                If String.IsNullOrEmpty(Me._FilePath) Then
                    Return String.Empty
                Else
                    Return Me._FilePath
                End If
            End Get

            Set(ByVal value As String)
                Me._FilePath = value
            End Set

        End Property

        Public Property Description() As String

            Get
                If String.IsNullOrEmpty(Me._Description) Then
                    Return String.Empty
                Else
                    Return Me._Description
                End If
            End Get

            Set(ByVal value As String)
                Me._Description = value
            End Set

        End Property

        Public Property Dimension() As Decimal
            Get
                Return _Dimension
            End Get
            Set(ByVal value As Decimal)
                _Dimension = value
            End Set

        End Property

        Public Property Data() As DateTime

            Get
                Return Me._Data
            End Get

            Set(ByVal value As DateTime)
                Me._Data = value
            End Set

        End Property


    End Class

End Namespace
