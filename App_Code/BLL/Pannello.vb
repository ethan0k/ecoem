Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class Pannello

        Private _Id As Integer
        Private _Matricola As String
        Private _Modello As String
        Private _Peso As Decimal
        Private _DataInizioGaranzia As Date
        Private _IdMarca As Integer ' Consorziato
        Private _Produttore As String
        Private _IdImpianto As Integer
        Private _DataCaricamento As Date
        Private _Conforme As Boolean
        Private _Protocollo As String
        Private _NrComunicazione As Integer
        Private _Anno As Integer
        Private _DataConformita As Date
        Private _Dismesso As Boolean
        Private _IdFasciaDiPeso As Integer
        Private _IdTipologiaCella As Integer
        Private _CostoMatricola As Decimal
        Private _DataRitiro As Date
        Private _NumeroFIR As String


        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, Matricola As String, ByVal Modello As String, Peso As Decimal, DataInizioGaranzia As Date, IdMarca As Integer, Produttore As String, ByVal IdImpianto As Integer, _
						ByVal DataCaricamento As Date, ByVal Conforme As Boolean, ByVal Protocollo As String, ByVal NrComunicazione As Integer, ByVal Anno As Integer, ByVal DataConformita As Date, _
						ByVal Dismesso As Boolean, ByVal IdfasciaDiPeso As Integer, ByVal IdTipologiaCella As Integer, ByVal CostoMatricola As Decimal, ByVal DataRitiro As Date, ByVal NumeroFIR As String)

            Me._Id = Id
            Me._Matricola = Matricola
            Me._Modello = Modello
            Me._Peso = Peso
            Me._DataInizioGaranzia = DataInizioGaranzia
            Me._IdMarca = IdMarca
            Me._Produttore = Produttore
            Me._IdImpianto = IdImpianto
            Me._DataCaricamento = DataCaricamento
            Me._Conforme = Conforme
            Me._Protocollo = Protocollo
            Me._NrComunicazione = NrComunicazione
            Me._Anno = Anno
            Me._DataConformita = DataConformita
            Me._Dismesso = Dismesso
            Me._IdFasciaDiPeso = IdfasciaDiPeso
            Me._IdTipologiaCella = IdTipologiaCella
            Me._CostoMatricola = CostoMatricola
            Me._DataRitiro = DataRitiro
            Me._NumeroFIR = NumeroFIR

        End Sub

        Public Shared Function Carica(ByVal IdPannello As Integer) As Pannello

            If IdPannello <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaPannello(IdPannello)

        End Function

        Public Shared Function Carica(ByVal Matricola As String, CodiceProduttore As String) As Pannello

            Return DataAccessHelper.GetDataAccess().CaricaPannelloDaMatricola(Matricola, CodiceProduttore)

        End Function

        Public Shared Function Lista(ByVal IdImpianto As Integer) As List(Of Pannello)

            Return DataAccessHelper.GetDataAccess().ListaPannelli(IdImpianto)

        End Function

        Public Shared Function Lista(ByVal Matricola As String) As List(Of Pannello)

            Return DataAccessHelper.GetDataAccess().ListaPannelli(Matricola)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoPannello(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaPannello(Me)
            End If

        End Function

        Public Function Elimina() As Boolean
            Return DataAccessHelper.GetDataAccess().EliminaPannello(Me)
        End Function

        Public Shared Function Exists(ByVal IdMarca As Integer, Matricola As String) As Boolean

            If IdMarca <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().MatricolaExists(IdMarca, Matricola)

        End Function

        Public Shared Function TotaleAbbinati() As Integer

            Return DataAccessHelper.GetDataAccess().TotaleAbbinati()

        End Function

        Public Shared Function TotalePannelli() As Integer

            Return DataAccessHelper.GetDataAccess().TotalePannelli()

        End Function

        Public Shared Function TotaleConformi() As Integer

            Return DataAccessHelper.GetDataAccess().TotaleConformi()

        End Function
        Public Shared Function TotaleDismessi() As Integer

            Return DataAccessHelper.GetDataAccess().TotaleDismessi()

        End Function

        Public Shared Function TotalePannelliProduttore(IdProduttore As Integer) As Integer

            Return DataAccessHelper.GetDataAccess().TotalePannelliProduttore(IdProduttore)

        End Function

        Public Shared Function TotalePannelliCliente(IdCliente As Integer) As Integer

            Return DataAccessHelper.GetDataAccess().TotalePannelliCliente(IdCliente)

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

        Public Property Conforme() As Boolean
            Get
                Return _Conforme
            End Get
            Set(ByVal value As Boolean)
                _Conforme = value
            End Set
        End Property

        Public Property Protocollo() As String

            Get
                If String.IsNullOrEmpty(Me._Protocollo) Then
                    Return String.Empty
                Else
                    Return Me._Protocollo
                End If
            End Get

            Set(ByVal value As String)
                Me._Protocollo = value
            End Set

        End Property

        Public Property NrComunicazione() As Integer
            Get
                Return _NrComunicazione
            End Get
            Set(ByVal value As Integer)
                _NrComunicazione = value
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

        Public Property DataCaricamento() As Date
            Get
                Return _DataCaricamento
            End Get
            Set(ByVal value As Date)
                _DataCaricamento = value
            End Set
        End Property

        Public Property DataConformita() As Date
            Get
                Return _DataConformita
            End Get
            Set(ByVal value As Date)
                _DataConformita = value
            End Set
        End Property

        Public Property Dismesso() As Integer
            Get
                Return _Dismesso
            End Get
            Set(ByVal value As Integer)
                _Dismesso = value
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

        Public Property IdTipologiaCella() As Integer
            Get
                Return _IdTipologiaCella
            End Get
            Set(ByVal value As Integer)
                _IdTipologiaCella = value
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
                If String.IsNullOrEmpty(Me._NumeroFIR) Then
                    Return String.Empty
                Else
                    Return Me._NumeroFIR
                End If
            End Get

            Set(ByVal value As String)
                Me._NumeroFIR = value
            End Set

        End Property

    End Class
End Namespace
