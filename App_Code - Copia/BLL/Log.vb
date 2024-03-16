Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class Log

        Private _Id As Integer
        Private _Data As Date
        Private _Ora As DateTime
        Private _Origine As String
        Private _Descrizione As String
        Private _Utente As String
        Private _Letto As Boolean

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal Data As Date, ByVal Ora As DateTime, ByVal Origine as String, ByVal Descrizione As String, ByVal Utente As String, ByVal Letto As Boolean)

            Me._Id = Id
            Me._Data = Data
            Me._Ora = Ora
			Me._Origine = Origine
            Me._Descrizione = Descrizione
            Me._Utente = Utente
            Me._Letto = Letto

        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As Log

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaLog(Id)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoLog(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaLog(Me)
            End If

        End Function

        Public Shared Function NonLetti() As Integer

            Return DataAccessHelper.GetDataAccess().CaricaLogNonLetti()

        End Function

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property Data() As Date
            Get
                Return _Data
            End Get
            Set(ByVal value As Date)
                _Data = value
            End Set
        End Property

        Public Property Ora() As DateTime
            Get
                Return _Ora
            End Get
            Set(ByVal value As DateTime)
                _Ora = value
            End Set
        End Property

        Public Property Origine() As String

            Get
                If String.IsNullOrEmpty(Me._Origine) Then
                    Return String.Empty
                Else
                    Return Me._Origine
                End If
            End Get

            Set(ByVal value As String)
                Me._Origine = value
            End Set

        End Property

        Public Property Descrizione() As String

            Get
                If String.IsNullOrEmpty(Me._Descrizione) Then
                    Return String.Empty
                Else
                    Return Me._Descrizione
                End If
            End Get

            Set(ByVal value As String)
                Me._Descrizione = value
            End Set

        End Property

        Public Property Utente() As String

            Get
                If String.IsNullOrEmpty(Me._Utente) Then
                    Return String.Empty
                Else
                    Return Me._Utente
                End If
            End Get

            Set(ByVal value As String)
                Me._Utente = value
            End Set

        End Property

        Public Property Letto() As Boolean
            Get
                Return _Letto
            End Get
            Set(ByVal value As Boolean)
                _Letto = value
            End Set
        End Property

    End Class
End Namespace
