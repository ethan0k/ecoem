Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class ErroreImportazione

        Private _Id As Integer
        Private _Matricola As String
        Private _Errore As String
        Private _Importabile As Boolean
        Private _Modello As String
        Private _Peso As Decimal
        Private _DataInizioGaranzia As Date
        Private _IdMarca As Integer ' Consorziato
        Private _Produttore As String
        Private _IdImpianto As Integer
        Private _DataCaricamento As Date
        Private _UserName As String
        Private _InErrore As Boolean
        Private _AnnoCaricamento As Integer
        Private _IdProduttore As Integer
        Private _TipoImportazione As Integer ' 1 = Pannelli, 2 = Dismessi, 3 = Impianto, 4 = Verifica
        Private _Protocollo As String
        Private _Stato As String
        Private _Impianto As String
        Private _IdTipologia As Integer
        Private _IdFascia As Integer
        Private _CostoMatricola As Decimal
        Private _DataRitiro As Date
        Private _NumeroFIR As String


        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, Matricola As String, ByVal Errore As String, ByVal Importabile As Boolean, ByVal Modello As String, Peso As Decimal, DataInizioGaranzia As Date, IdMarca As Integer, Produttore As String, ByVal IdImpianto As Integer, _
                       ByVal DataCaricamento As Date, ByVal UserName As String, ByVal InErrore As Boolean, ByVal AnnoCaricamento As Integer, ByVal IdProduttore As Integer, ByVal TipoImportazione As Integer, _
                       ByVal Protocollo As String,byval Stato As String, ByVal Impianto As String, ByVal IdTipologia as Integer, ByVal Idfascia as integer, byval CostoMatricola as decimal, byval DataRitiro as Date, byval NumeroFIR as String)

            Me._Id = Id
            Me._Matricola = Matricola
            Me._Errore = Errore
            Me._Importabile = Importabile
            Me._Modello = Modello
            Me._Peso = Peso
            Me._DataInizioGaranzia = DataInizioGaranzia
            Me._IdMarca = IdMarca
            Me._Produttore = Produttore
            Me._IdImpianto = IdImpianto
            Me._DataCaricamento = DataCaricamento
            Me._UserName = UserName
            Me._InErrore = InErrore
            Me._AnnoCaricamento = AnnoCaricamento
            Me._IdProduttore = IdProduttore
            Me._TipoImportazione = TipoImportazione
            Me._Protocollo = Protocollo
            Me._Stato = Stato
            Me._Impianto = Impianto
            Me._IdTipologia = IdTipologia
            Me._IdFascia = Idfascia
            Me._CostoMatricola = CostoMatricola
            Me._DataRitiro = DataRitiro
            Me._NumeroFIR = NumeroFIR

        End Sub

        Public Shared Function IsInError(ByVal UserName As String, ByVal TipoImportazione As Integer) As Boolean

            Return DataAccessHelper.GetDataAccess().IsInError(UserName, TipoImportazione)

        End Function

        Public Shared Function Carica(ByVal Id As Integer) As ErroreImportazione

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaErroreImportazione(Id)

        End Function

        Public Shared Function Carica(ByVal Matricola As String, UserName As String) As ErroreImportazione

            Return DataAccessHelper.GetDataAccess().CaricaErroreImportazione(Matricola, UserName)

        End Function

        Public Shared Function GetByIdImportazione(ByVal UserName As String, ByVal TipoImportazione As Integer) As List(Of ErroreImportazione)

            Return DataAccessHelper.GetDataAccess().GetByIdImportazione(UserName, TipoImportazione)

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaErrore(Me)

        End Function

        Public Shared Function EliminaTutti(ByVal UserName As String) As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaErroriTutti(UserName)

        End Function

        Public Shared Function EliminaTuttiDismessi(ByVal UserName As String) As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaErroriTuttiDismessi(UserName)

        End Function

        Public Shared Function EliminaTuttiImpianto(ByVal UserName As String) As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaErroriTuttiImpianto(UserName)

        End Function

        Public Shared Function EliminaTuttiVerifica(ByVal UserName As String) As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaErroriTuttiVerifica(UserName)

        End Function

        Public Function Exists(Matricola As String, ByVal UserName As String, ByVal Id As Integer) As Boolean

            Return DataAccessHelper.GetDataAccess().ErroreExists(Matricola, UserName, Id)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoErrore(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaErroreImportazione(Me)
            End If

        End Function

        Public ReadOnly Property Id() As Integer
            Get
                Return Me._Id
            End Get

        End Property

        Public Property Matricola() As String
            Get
                If String.IsNullOrEmpty(Me._Matricola) Then
                    Return String.Empty
                Else
                    Return Me._Matricola
                End If
            End Get

            Set(ByVal value As String)
                Me._Matricola = value
            End Set

        End Property

        Public Property Errore() As String
            Get
                If String.IsNullOrEmpty(Me._Errore) Then
                    Return String.Empty
                Else
                    Return Me._Errore
                End If
            End Get

            Set(ByVal value As String)
                Me._Errore = value
            End Set

        End Property

        Public Property Importabile() As Boolean
            Get
                Return Me._Importabile
            End Get

            Set(ByVal value As Boolean)
                Me._Importabile = value
            End Set

        End Property

        Public Property Modello() As String
            Get
                If String.IsNullOrEmpty(Me._Modello) Then
                    Return String.Empty
                Else
                    Return Me._Modello
                End If
            End Get

            Set(ByVal value As String)
                Me._Modello = value
            End Set

        End Property

        Public Property Peso() As Decimal
            Get
                Return Me._Peso
            End Get

            Set(ByVal value As Decimal)
                Me._Peso = value
            End Set

        End Property

        Public Property DataInizioGaranzia() As Date
            Get
                Return _DataInizioGaranzia
            End Get
            Set(ByVal value As Date)
                _DataInizioGaranzia = value
            End Set
        End Property

        Public Property IdMarca() As Integer
            Get
                Return _IdMarca
            End Get
            Set(ByVal value As Integer)
                _IdMarca = value
            End Set
        End Property

        Public Property Produttore() As String
            Get
                If String.IsNullOrEmpty(Me._Produttore) Then
                    Return String.Empty
                Else
                    Return Me._Produttore
                End If
            End Get

            Set(ByVal value As String)
                Me._Produttore = value
            End Set

        End Property

        Public Property IdImpianto() As Integer
            Get
                Return _IdImpianto
            End Get
            Set(ByVal value As Integer)
                _IdImpianto = value
            End Set
        End Property

        Public Property DataCaricamento() As Date
            Get
                Return _DataCaricamento
            End Get
            Set(ByVal value As Date)
                _DataCaricamento = value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return Me._UserName
            End Get

            Set(ByVal value As String)
                Me._UserName = value
            End Set

        End Property

        Public Property InErrore() As Boolean
            Get
                Return _InErrore
            End Get
            Set(ByVal value As Boolean)
                _InErrore = value
            End Set
        End Property

        Public Property AnnoCaricamento() As Integer
            Get
                Return _AnnoCaricamento
            End Get
            Set(ByVal value As Integer)
                _AnnoCaricamento = value
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

        Public Property TipoImportazione() As Integer
            Get
                Return _TipoImportazione
            End Get
            Set(ByVal value As Integer)
                _TipoImportazione = value
            End Set
        End Property

        Public Property Protocollo() As String
            Get
                Return Me._Protocollo
            End Get

            Set(ByVal value As String)
                Me._Protocollo = value
            End Set

        End Property

        Public Property Stato() As String
            Get
                Return Me._Stato
            End Get

            Set(ByVal value As String)
                Me._Stato = value
            End Set

        End Property

        Public Property Impianto() As String
            Get
                Return Me._Impianto
            End Get

            Set(ByVal value As String)
                Me._Impianto = value
            End Set

        End Property

        Public Property IdTipologia() As Integer
            Get
                Return Me._IdTipologia
            End Get

            Set(ByVal value As Integer)
                Me._IdTipologia = value
            End Set

        End Property

        Public Property IdFascia() As Integer
            Get
                Return Me._IdFascia
            End Get

            Set(ByVal value As Integer)
                Me._IdFascia = value
            End Set

        End Property

        Public Property CostoMatricola() As Decimal
            Get
                Return Me._CostoMatricola
            End Get

            Set(ByVal value As Decimal)
                Me._CostoMatricola = value
            End Set

        End Property

        Public Property DataRitiro() As Date
            Get
                Return _DataRitiro
            End Get
            Set(ByVal value As Date)
                _DataRitiro = value
            End Set
        End Property

        Public Property NumeroFIR() As String
            Get
                Return Me._NumeroFIR
            End Get

            Set(ByVal value As String)
                Me._NumeroFIR = value
            End Set

        End Property
    End Class


End Namespace
