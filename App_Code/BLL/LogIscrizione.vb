Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class LogIscrizione

        Private _Id As Integer
        Private _IdProduttore As Integer
        Private _DataIscrizione As Date
        Private _DataDisiscrizione As Date

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal IdProduttore As Integer, ByVal DataIscrizione As Date, ByVal DataDisiscrizione As Date)

            Me._Id = Id
            Me._IdProduttore = IdProduttore
            Me._DataIscrizione = DataIscrizione
            Me._DataDisiscrizione = DataDisiscrizione

        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As LogIscrizione

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaLogIscrizione(Id)

        End Function

        Public Shared Function CaricaDaProduttore(ByVal IdProduttore As Integer) As LogIscrizione

            Return DataAccessHelper.GetDataAccess().CaricaLogIscrizione2(IdProduttore)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoLogIscrizione(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaLogIscrizione(Me)
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

        Public Property DataIscrizione() As Date
            Get
                Return _DataIscrizione
            End Get
            Set(ByVal value As Date)
                _DataIscrizione = value
            End Set
        End Property

        Public Property DataDisiscrizione() As Date
            Get
                Return _DataDisiscrizione
            End Get
            Set(ByVal value As Date)
                _DataDisiscrizione = value
            End Set
        End Property

    End Class

End Namespace
