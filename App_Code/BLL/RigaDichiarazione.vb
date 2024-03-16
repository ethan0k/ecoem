Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class RigaDichiarazione

        Private _Id As Integer
        Private _IdDichiarazione As Integer
        Private _IdCategoria As Integer
        Private _TipoDiDato As String
        Private _Pezzi As Integer
        Private _Kg As Decimal
        Private _KgDichiarazione As Decimal
        Private _CostoUnitario As Decimal
        Private _Importo As Decimal
        Private _UtenteAggiornamento As String
        Private _DataAggiornamento As DateTime

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal IdDichiarazione As Integer, ByVal IdCategoria As Integer, ByVal TipoDiDato As String, ByVal Pezzi As Integer, _
                        ByVal Kg As Decimal, ByVal KgDichiarazione As Decimal, ByVal CostoUnitario As Decimal, ByVal Importo As Decimal, _
						ByVal UtenteAggiornamento as String, ByVal DataAggiornamento as DateTime)

            Me._Id = Id
            Me._IdDichiarazione = IdDichiarazione
            Me._IdCategoria = IdCategoria
            Me._TipoDiDato = TipoDiDato
            Me._Pezzi = Pezzi
            Me._Kg = Kg
            Me._KgDichiarazione = KgDichiarazione
            Me._CostoUnitario = CostoUnitario
            Me._Importo = Importo
            Me._UtenteAggiornamento = UtenteAggiornamento
            Me._DataAggiornamento = DataAggiornamento

        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As RigaDichiarazione

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaRigaDichiarazione(Id)

        End Function

        Public Shared Function Lista(ByVal IdDichiarazione As Integer) As List(Of RigaDichiarazione)

            Return DataAccessHelper.GetDataAccess().ListaRigheDichiarazioni(IdDichiarazione)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaRigaDichiarazione(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaRigaDichiarazione(Me)
            End If

        End Function


        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property IdDichiarazione() As Integer
            Get
                Return _IdDichiarazione
            End Get
            Set(ByVal value As Integer)
                _IdDichiarazione = value
            End Set

        End Property

        Public Property IdCategoria() As Integer
            Get
                Return _IdCategoria
            End Get
            Set(ByVal value As Integer)
                _IdCategoria = value
            End Set

        End Property

        Public Property TipoDiDato() As String

            Get
                If String.IsNullOrEmpty(Me._TipoDiDato) Then
                    Return String.Empty
                Else
                    Return Me._TipoDiDato
                End If
            End Get

            Set(ByVal value As String)
                Me._TipoDiDato = value
            End Set

        End Property

        Public Property Pezzi() As Integer
            Get
                Return _Pezzi
            End Get
            Set(ByVal value As Integer)
                _Pezzi = value
            End Set

        End Property
        Public Property Kg() As Decimal
            Get
                Return _Kg
            End Get
            Set(ByVal value As Decimal)
                _Kg = value
            End Set

        End Property
        Public Property KgDichiarazione() As Decimal
            Get
                Return _KgDichiarazione
            End Get
            Set(ByVal value As Decimal)
                _KgDichiarazione = value
            End Set

        End Property
        Public Property CostoUnitario() As Decimal
            Get
                Return _CostoUnitario
            End Get
            Set(ByVal value As Decimal)
                _CostoUnitario = value
            End Set

        End Property

        Public Property Importo() As Decimal
            Get
                Return _Importo
            End Get
            Set(ByVal value As Decimal)
                _Importo = value
            End Set

        End Property

        Public Property UtenteAggiornamento() As String
            Get
                Return _UtenteAggiornamento
            End Get
            Set(ByVal value As String)
                _UtenteAggiornamento = value
            End Set

        End Property

        Public Property DataAggiornamento() As DateTime
            Get
                Return _DataAggiornamento
            End Get
            Set(ByVal value As DateTime)
                _DataAggiornamento = value
            End Set

        End Property

    End Class
End Namespace