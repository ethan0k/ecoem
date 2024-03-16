Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class UtenteCliente

        Private _Id As Integer
        Private _UserId As Guid
        Private _IdCliente As Integer


        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, UserId As Guid, ByVal IdCliente As Integer)

            Me._Id = Id
            Me._UserId = UserId
            Me._IdCliente = IdCliente

        End Sub

        Public Shared Function Carica(ByVal UserId As Guid) As UtenteCliente

            Return DataAccessHelper.GetDataAccess().CaricaUtenteCliente(UserId)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoUtenteCliente(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaUtenteCliente(Me)
            End If

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaUtenteCliente(Me)

        End Function

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property UserId() As Guid

            Get
                    Return Me._UserId

            End Get

            Set(ByVal value As Guid)
                Me._UserId = value
            End Set

        End Property

        Public Property IdCliente() As Integer

            Get
                
                Return Me._IdCliente

            End Get

            Set(ByVal value As Integer)
                Me._IdCliente = value
            End Set

        End Property


    End Class

End Namespace
