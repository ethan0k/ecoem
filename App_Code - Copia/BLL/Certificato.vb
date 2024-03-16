Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class Certificato

        Private _Id As Integer
        Private _IdProduttore As Integer
        Private _Anno As Integer
        Private _Protocollo As String

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal IdProduttore As Integer, ByVal Anno As Integer, ByVal Protocollo As String)

            Me._Id = Id
            Me._IdProduttore = IdProduttore
            Me._Anno = Anno
            Me._Protocollo = Protocollo
            Me._Protocollo = Protocollo

        End Sub

        Public Shared Function Carica(ByVal IdCertificato As Integer) As Certificato

            If IdCertificato <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaCertificato(IdCertificato)

        End Function

        Public Shared Function Carica(ByVal IdProduttore As Integer, ByVal Anno As Integer) As Certificato

            Return DataAccessHelper.GetDataAccess().CaricaCertificato2(IdProduttore, Anno)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoCertificato(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaCertificato(Me)
            End If

        End Function

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property IdProduttore() As Integer
            Get
                Return _IdProduttore
            End Get
            Set(ByVal value As Integer)
                _IdProduttore = value
            End Set
        End Property

        Public Property Anno() As Integer
            Get
                Return _Anno
            End Get
            Set(ByVal value As Integer)
                _Anno = value
            End Set
        End Property

        Public Property Protocollo() As String
            Get
                Return _Protocollo
            End Get
            Set(ByVal value As String)
                _Protocollo = value
            End Set
        End Property

    End Class

End Namespace
