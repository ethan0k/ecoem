Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data
Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class Categoria_Produttore

        Private _Id As Integer
        Private _IdCategoria As Integer
        Private _IdProduttore As Integer
        Private _Costo As Decimal
        Private _Peso As Decimal
        Private _Professionale As Boolean

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal IdCategoria As Integer, ByVal IdProduttore As Integer, ByVal Costo As Decimal, Peso As Decimal, ByVal Professionale As Boolean)

            Me._Id = Id
            Me._IdCategoria = IdCategoria
            Me._IdProduttore = IdProduttore
            Me._Costo = Costo
            Me._Peso = Peso
            Me._Professionale = Professionale

        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As Categoria_Produttore

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaCategoriaProduttore(Id)

        End Function

        Public Shared Function Lista(ByVal IdProduttore As Integer, ByVal Professionale As Boolean) As List(Of Categoria_Produttore)

            If IdProduttore <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().ListaCategoriaProduttore(IdProduttore, Professionale)

        End Function

        Public Shared Function Carica(ByVal IdCategoria As Integer, ByVal IdProduttore As Integer, ByVal Professionale As Boolean) As Categoria_Produttore

            If IdCategoria <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            If IdProduttore <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaCategoriaProduttore(IdCategoria, IdProduttore, Professionale)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaCategoriaProduttore(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaCategoriaProduttore(Me)
            End If

        End Function

        Public Shared Function Verifica(ByVal IdCategoria As Integer) As Boolean

            If IdCategoria <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().VerificaCategoriaProduttore(IdCategoria)

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaCategoriaProduttore(Me)

        End Function

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property IdCategoria() As Integer
            Get
                Return _IdCategoria
            End Get
            Set(ByVal value As Integer)
                _IdCategoria = value
            End Set

        End Property

        Public Property IdProduttore() As Integer
            Get
                Return _IdProduttore
            End Get
            Set(ByVal value As Integer)
                _IdProduttore = value
            End Set

        End Property

        Public Property Costo() As Decimal
            Get
                Return _Costo
            End Get
            Set(ByVal value As Decimal)
                _Costo = value
            End Set

        End Property

        Public Property Peso() As Decimal
            Get
                Return _Peso
            End Get
            Set(ByVal value As Decimal)
                _Peso = value
            End Set

        End Property

        Public Property Professionale() As Boolean
            Get
                Return _Professionale
            End Get
            Set(ByVal value As Boolean)
                _Professionale = value
            End Set

        End Property

    End Class
End Namespace

