Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class Categoria

        Private _Id As Integer
        Private _Nome As String
        Private _TipoDiDato As String
        Private _Valore As Decimal
        Private _IdMacroCategoria As Integer
        Private _Codifica As String
        Private _Raggruppamento As String
        Private _DataModifica As DateTime

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal Nome As String, ByVal TipoDiDato As String, ByVal Valore As Decimal, ByVal IdMacroCategoria As Integer, ByVal Codifica As String, ByVal Raggruppamento As String, ByVal DataModifica As DateTime)

            Me._Id = Id
            Me._Nome = Nome
            Me._TipoDiDato = TipoDiDato
            Me._Valore = Valore
            Me._IdMacroCategoria = IdMacroCategoria
            Me._Codifica = Codifica
            Me._Raggruppamento = Raggruppamento
            Me._DataModifica = DataModifica

        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As Categoria

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaCategoria(Id)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaCategoria(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaCategoria(Me)
            End If

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaCategoria(Me)

        End Function       

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property Nome() As String

            Get
                If String.IsNullOrEmpty(Me._Nome) Then
                    Return String.Empty
                Else
                    Return Me._Nome
                End If
            End Get

            Set(ByVal value As String)
                Me._Nome = value
            End Set

        End Property

        Public Property TipoDiDato() As String

            Get
                If String.IsNullOrEmpty(Me._Nome) Then
                    Return String.Empty
                Else
                    Return Me._TipoDiDato
                End If
            End Get

            Set(ByVal value As String)
                Me._TipoDiDato = value
            End Set

        End Property

        Public Property Valore() As Decimal
            Get
                Return _Valore
            End Get
            Set(ByVal value As Decimal)
                _Valore = value
            End Set

        End Property

        Public Property IdMacrocategoria() As Integer
            Get
                Return _IdMacroCategoria
            End Get
            Set(ByVal value As Integer)
                _IdMacroCategoria = value
            End Set

        End Property

        Public Property Codifica() As String

            Get
                If String.IsNullOrEmpty(Me._Codifica) Then
                    Return String.Empty
                Else
                    Return Me._Codifica
                End If
            End Get

            Set(ByVal value As String)
                Me._Codifica = value
            End Set

        End Property

        Public Property Raggruppamento() As String

            Get
                If String.IsNullOrEmpty(Me._Raggruppamento) Then
                    Return String.Empty
                Else
                    Return Me._Raggruppamento
                End If
            End Get

            Set(ByVal value As String)
                Me._Raggruppamento = value
            End Set

        End Property

        Public Property DataModifica() As Date
            Get
                Return _DataModifica
            End Get
            Set(ByVal value As Date)
                _DataModifica = value
            End Set

        End Property
    End Class
End Namespace
