Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class AbbinamentoTipologiaFascia

        ' Descrive l'abbinamento per produttore tra Tipologia cella e Fascia di peso
        Private _Id As Integer
        Private _IdProduttore As Integer
        Private _IdTipologiaCella As Integer
        Private _IdFasciaDiPeso As Integer
        Private _CostoMatricola As Decimal

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal IdProduttore As Integer, ByVal IdTipologiaCella As Integer, ByVal IdFasciaDiPeso As Integer, ByVal CostoMatricola As Decimal)

            Me._Id = Id
            Me._IdProduttore = IdProduttore
            Me._IdTipologiaCella = IdTipologiaCella
            Me._IdFasciaDiPeso = IdFasciaDiPeso
            Me._CostoMatricola = CostoMatricola
        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As AbbinamentoTipologiaFascia

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaAbbinamento(Id)

        End Function

        Public Shared Function Carica(ByVal IdProduttore As Integer, ByVal IdTipologia As Integer, ByVal IdFascia As Integer) As AbbinamentoTipologiaFascia

            If IdTipologia <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaAbbinamento(IdProduttore, IdTipologia, IdFascia)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoAbbinamento(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaAbbinamento(Me)
            End If

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaAbbinamento(Me)

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

        Public Property IdTipologiaCella() As Integer
            Get
                Return _IdTipologiaCella
            End Get
            Set(ByVal value As Integer)
                _IdTipologiaCella = value
            End Set

        End Property

        Public Property IdFasciaDiPeso() As Integer
            Get
                Return _IdFasciaDiPeso
            End Get
            Set(ByVal value As Integer)
                _IdFasciaDiPeso = value
            End Set

        End Property

        Public Property CostoMatricola() As Decimal
            Get
                Return _CostoMatricola
            End Get
            Set(ByVal value As Decimal)
                _CostoMatricola = value
            End Set

        End Property

    End Class

End Namespace