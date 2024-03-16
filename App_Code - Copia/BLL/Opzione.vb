Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class Opzione

        Private _Id As Integer
        Private _Oggetto As String
        Private _TestoMail As String
        Private _Smtp As String
        Private _NomeUtente As String
        Private _Password As String
        Private _SSL As Boolean
        Private _Porta As Integer
        Private _Mittente As String
        Private _DestinatarioTest As String
        Private _OggettoAutocertificazione As String
        Private _TestoMailAutocertificazione As String


        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal Oggetto As String, ByVal TestoMail As String, ByVal Smtp As String, ByVal NomeUtente As String, ByVal Password As String, _
                       ByVal SSL As Boolean, ByVal Porta As Integer, ByVal Mittente As String, ByVal DestinatarioTest As String, ByVal OggettoAutocertificazione As String, _
					   ByVal TestoMailAutocertificazione As String)

            Me._Id = Id
            Me._Oggetto = Oggetto
            Me._TestoMail = TestoMail
            Me._Smtp = Smtp
            Me._NomeUtente = NomeUtente
            Me._Password = Password
            Me._SSL = SSL
            Me._Porta = Porta
            Me._Mittente = Mittente
            Me._DestinatarioTest = DestinatarioTest
            Me._OggettoAutocertificazione = OggettoAutocertificazione
            Me._TestoMailAutocertificazione = TestoMailAutocertificazione

        End Sub

        Public Shared Function Carica() As Opzione

            Return DataAccessHelper.GetDataAccess().CaricaOpzione()

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaOpzione(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaOpzione(Me)
            End If

        End Function

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property Oggetto() As String

            Get
                If String.IsNullOrEmpty(Me._Oggetto) Then
                    Return String.Empty
                Else
                    Return Me._Oggetto
                End If
            End Get

            Set(ByVal value As String)
                Me._Oggetto = value
            End Set

        End Property

        Public Property TestoMail() As String

            Get
                If String.IsNullOrEmpty(Me._Oggetto) Then
                    Return String.Empty
                Else
                    Return Me._TestoMail
                End If
            End Get

            Set(ByVal value As String)
                Me._TestoMail = value
            End Set

        End Property

        Public Property Smtp() As String

            Get
                If String.IsNullOrEmpty(Me._Smtp) Then
                    Return String.Empty
                Else
                    Return Me._Smtp
                End If
            End Get

            Set(ByVal value As String)
                Me._Smtp = value
            End Set

        End Property

        Public Property NomeUtente() As String

            Get
                If String.IsNullOrEmpty(Me._NomeUtente) Then
                    Return String.Empty
                Else
                    Return Me._NomeUtente
                End If
            End Get

            Set(ByVal value As String)
                Me._NomeUtente = value
            End Set

        End Property

        Public Property Password() As String

            Get
                If String.IsNullOrEmpty(Me._Password) Then
                    Return String.Empty
                Else
                    Return Me._Password
                End If
            End Get

            Set(ByVal value As String)
                Me._Password = value
            End Set

        End Property

        Public Property SSL() As Boolean

            Get

                Return Me._SSL

            End Get

            Set(ByVal value As Boolean)
                Me._SSL = value
            End Set

        End Property

        Public Property Porta() As Integer

            Get

                Return Me._Porta

            End Get

            Set(ByVal value As Integer)
                Me._Porta = value
            End Set

        End Property

        Public Property Mittente() As String

            Get
                If String.IsNullOrEmpty(Me._Mittente) Then
                    Return String.Empty
                Else
                    Return Me._Mittente
                End If
            End Get

            Set(ByVal value As String)
                Me._Mittente = value
            End Set

        End Property

        Public Property DestinatarioTest() As String

            Get
                If String.IsNullOrEmpty(Me._DestinatarioTest) Then
                    Return String.Empty
                Else
                    Return Me._DestinatarioTest
                End If
            End Get

            Set(ByVal value As String)
                Me._DestinatarioTest = value
            End Set

        End Property

        Public Property OggettoAutocertificazione() As String

            Get
                If String.IsNullOrEmpty(Me._OggettoAutocertificazione) Then
                    Return String.Empty
                Else
                    Return Me._OggettoAutocertificazione
                End If
            End Get

            Set(ByVal value As String)
                Me._OggettoAutocertificazione = value
            End Set

        End Property

        Public Property TestoMailAutocertificazione() As String

            Get
                If String.IsNullOrEmpty(Me._Oggetto) Then
                    Return String.Empty
                Else
                    Return Me._TestoMailAutocertificazione
                End If
            End Get

            Set(ByVal value As String)
                Me._TestoMailAutocertificazione = value
            End Set

        End Property

    End Class
End Namespace
