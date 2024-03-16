Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class Produttore

        Private _Id As Integer
        Private _Codice As String
        Private _RagioneSociale As String
        Private _Email As String
        Private _Periodicita As Integer ' 1 Mensile, 2 Bimestrale, 3 Trimestrale
        Private _Attivo As Boolean
        Private _Indirizzo As String
        Private _Cap As String
        Private _Citta As String
        Private _Rappresentante As String
        Dim _Note As String
        Private _EmailNotifiche As String
        Private _CodiceFiscale As String
        Private _Professionale As Boolean
        Private _Domestico As Boolean
        Private _EscludiMassivo As Boolean ' Esclude dalla modifica massiva delle categorie
        Private _MeseAdesione As Integer
        Private _QuotaConsortile As Decimal
        Private _CodiceSDI As String
        Private _PartitaIVA As String
        Private _PEC As String
        Private _IBAN As String
        Private _Telefono As String
        Private _RegistroAEE As String
        Private _RegistroPile As String
        Private _CostoMatricola As Decimal
        Private _ServizioDiRappresentanza As Decimal
        Private _Sconto As Decimal


        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, Codice As String, ByVal RagioneSociale As String, ByVal Email As String, ByVal Periodicita As Integer, ByVal Attivo As Boolean, ByVal Indirizzo As String, ByVal CAP As String, _
                       ByVal Citta As String, ByVal Rappresentante As String, ByVal Note As String, ByVal EmailNotifiche As String, _
                       ByVal CodiceFiscale As String, ByVal Domestico As Boolean, ByVal Professionale As Boolean, ByVal EscludiMassivo As Boolean, _
                       ByVal MeseAdesione As Integer, ByVal QuotaConsortile As Decimal, ByVal CodiceSDI As String, ByVal PartitaIVA As String, _
                       ByVal PEC As String, ByVal IBAN As String, ByVal Telefono As String, ByVal RegistroAEE As String, _
                       ByVal RegistroPile As String, ByVal CostoMatricola As Decimal, ByVal ServizioDiRappresentanza As Decimal, ByVal Sconto As Decimal)

            Me._Id = Id
            Me._RagioneSociale = RagioneSociale
            Me._Codice = Codice
            Me._Email = Email
            Me._Periodicita = Periodicita
            Me._Attivo = Attivo
            Me._Indirizzo = Indirizzo
            Me._Cap = CAP
            Me._Citta = Citta
            Me._Rappresentante = Rappresentante
            Me._Note = Note
            Me._EmailNotifiche = EmailNotifiche
            Me._CodiceFiscale = CodiceFiscale
            Me._Professionale = Professionale
            Me._Domestico = Domestico
            Me._EscludiMassivo = EscludiMassivo
            Me._MeseAdesione = MeseAdesione
            Me._QuotaConsortile = QuotaConsortile
            Me._CodiceSDI = CodiceSDI
            Me._PartitaIVA = PartitaIVA
            Me._PEC = PEC
            Me._IBAN = IBAN
            Me._Telefono = Telefono
            Me._RegistroAEE = RegistroAEE
            Me._RegistroPile = RegistroPile
            Me._CostoMatricola = CostoMatricola
            Me._ServizioDiRappresentanza = ServizioDiRappresentanza
            Me._Sconto = Sconto

        End Sub

        Public Shared Function Carica(ByVal IdCliente As Integer) As Produttore

            If IdCliente <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaProduttore(IdCliente)

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaProduttore(Me)

        End Function

        Public Function Verifica() As Boolean

            Return DataAccessHelper.GetDataAccess().VerificaPannelli(Me)

        End Function

        Public Function EsisteAEE(ByVal IdProduttore As Integer) As Boolean

            Return DataAccessHelper.GetDataAccess().EsisteAEE(IdProduttore)

        End Function

        Public Function EsistePILE(ByVal IdProduttore As Integer) As Boolean

            Return DataAccessHelper.GetDataAccess().EsistePILE(IdProduttore)

        End Function

        Public Function EsisteINDUSTRIAL(ByVal IdProduttore As Integer) As Boolean

            Return DataAccessHelper.GetDataAccess().EsisteINDUSTRIAL(IdProduttore)

        End Function

        Public Function EsisteVEICOLI(ByVal IdProduttore As Integer) As Boolean

            Return DataAccessHelper.GetDataAccess().EsisteVEICOLI(IdProduttore)

        End Function

        Public Function SoloProfessionale() As Boolean

            Return DataAccessHelper.GetDataAccess().SoloProfessionale(Me)

        End Function

        Public Function VerificaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Boolean

            Return DataAccessHelper.GetDataAccess().VerificaDichiarazioni(Anno, IdProduttore)

        End Function

        Public Function ContaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Integer

            Return DataAccessHelper.GetDataAccess().ContaDichiarazioni(Anno, IdProduttore)

        End Function

        Public Function DichiarazioniCertifica(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Integer

            Return DataAccessHelper.GetDataAccess().CertificaDichiarazioni(Anno, IdProduttore)

        End Function

        Public Shared Function TotaleProduttori() As Integer

            Return DataAccessHelper.GetDataAccess().TotaleProduttori()

        End Function

        Public Function GetLastNrComunicazione(Anno As Integer) As Integer

            Return DataAccessHelper.GetDataAccess().GetLastNrComunicazione(Me, Anno)

        End Function

        Public Shared Function Lista() As List(Of Produttore)

            Return DataAccessHelper.GetDataAccess().ListaProduttore()

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoProduttore(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaProduttore(Me)
            End If

        End Function


        Public Property Id() As Integer
            Get
                Return Me._Id
            End Get

            Set(ByVal value As Integer)
                '    Me._RagioneSociale = value
            End Set

        End Property

        Public Property RagioneSociale() As String
            Get
                If String.IsNullOrEmpty(Me._RagioneSociale) Then
                    Return String.Empty
                Else
                    Return Me._RagioneSociale
                End If
            End Get

            Set(ByVal value As String)
                Me._RagioneSociale = value
            End Set

        End Property

        Public Property Codice() As String
            Get
                If String.IsNullOrEmpty(Me._Codice) Then
                    Return String.Empty
                Else
                    Return Me._Codice
                End If
            End Get

            Set(ByVal value As String)
                Me._Codice = value
            End Set

        End Property

        Public Property Email() As String
            Get
                If String.IsNullOrEmpty(Me._Email) Then
                    Return String.Empty
                Else
                    Return Me._Email
                End If
            End Get

            Set(ByVal value As String)
                Me._Email = value
            End Set

        End Property

        Public Property Periodicita() As Integer
            Get
                Return _Periodicita
            End Get
            Set(ByVal value As Integer)
                _Periodicita = value
            End Set
        End Property

        Public Property Attivo() As Boolean
            Get
                Return _Attivo
            End Get
            Set(ByVal value As Boolean)
                _Attivo = value
            End Set
        End Property

        Public Property Indirizzo() As String
            Get
                If String.IsNullOrEmpty(Me._Indirizzo) Then
                    Return String.Empty
                Else
                    Return Me._Indirizzo
                End If
            End Get

            Set(ByVal value As String)
                Me._Indirizzo = value
            End Set

        End Property

        Public Property CAP() As String
            Get
                If String.IsNullOrEmpty(Me._Cap) Then
                    Return String.Empty
                Else
                    Return Me._Cap
                End If
            End Get

            Set(ByVal value As String)
                Me._Cap = value
            End Set

        End Property

        Public Property Citta() As String
            Get
                If String.IsNullOrEmpty(Me._Citta) Then
                    Return String.Empty
                Else
                    Return Me._Citta
                End If
            End Get

            Set(ByVal value As String)
                Me._Citta = value
            End Set

        End Property

        Public Property Rappresentante() As String
            Get
                If String.IsNullOrEmpty(Me._Rappresentante) Then
                    Return String.Empty
                Else
                    Return Me._Rappresentante
                End If
            End Get

            Set(ByVal value As String)
                Me._Rappresentante = value
            End Set

        End Property

        Public Property Note() As String
            Get
                If String.IsNullOrEmpty(Me._Note) Then
                    Return String.Empty
                Else
                    Return Me._Note
                End If
            End Get

            Set(ByVal value As String)
                Me._Note = value
            End Set

        End Property

        Public Property EmailNotifiche() As String
            Get
                If String.IsNullOrEmpty(Me._EmailNotifiche) Then
                    Return String.Empty
                Else
                    Return Me._EmailNotifiche
                End If
            End Get

            Set(ByVal value As String)
                Me._EmailNotifiche = value
            End Set

        End Property

        Public Property CodiceFiscale() As String
            Get
                If String.IsNullOrEmpty(Me._CodiceFiscale) Then
                    Return String.Empty
                Else
                    Return Me._CodiceFiscale
                End If
            End Get

            Set(ByVal value As String)
                Me._CodiceFiscale = value
            End Set

        End Property

        Public Property Professionale() As Integer
            Get
                Return _Professionale
            End Get
            Set(ByVal value As Integer)
                _Professionale = value
            End Set

        End Property

        Public Property Domestico() As Integer
            Get
                Return _Domestico
            End Get
            Set(ByVal value As Integer)
                _Domestico = value
            End Set

        End Property

        Public Property EscludiMassivo() As Integer
            Get
                Return _EscludiMassivo
            End Get
            Set(ByVal value As Integer)
                _EscludiMassivo = value
            End Set

        End Property

        Public Property MeseAdesione() As Integer
            Get
                Return _MeseAdesione
            End Get
            Set(ByVal value As Integer)
                _MeseAdesione = value
            End Set

        End Property

        Public Property QuotaConsortile() As Decimal
            Get
                Return _QuotaConsortile
            End Get
            Set(ByVal value As Decimal)
                _QuotaConsortile = value
            End Set

        End Property

        Public Property CodiceSDI() As String
            Get
                Return Me._CodiceSDI
            End Get

            Set(ByVal value As String)
                Me._CodiceSDI = value
            End Set

        End Property

        Public Property PartitaIVA() As String
            Get
                Return Me._PartitaIVA
            End Get

            Set(ByVal value As String)
                Me._PartitaIVA = value
            End Set

        End Property

        Public Property PEC() As String
            Get
                Return Me._PEC
            End Get

            Set(ByVal value As String)
                Me._PEC = value
            End Set

        End Property

        Public Property IBAN() As String
            Get
                Return Me._IBAN
            End Get

            Set(ByVal value As String)
                Me._IBAN = value
            End Set

        End Property

        Public Property Telefono() As String
            Get
                Return Me._Telefono
            End Get

            Set(ByVal value As String)
                Me._Telefono = value
            End Set

        End Property

        Public Property RegistroAEE() As String
            Get
                Return Me._RegistroAEE
            End Get

            Set(ByVal value As String)
                Me._RegistroAEE = value
            End Set

        End Property

        Public Property RegistroPile() As String
            Get
                Return Me._RegistroPile
            End Get

            Set(ByVal value As String)
                Me._RegistroPile = value
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

        Public Property ServizioDiRappresentanza() As Decimal
            Get
                Return Me._ServizioDiRappresentanza
            End Get

            Set(ByVal value As Decimal)
                Me._ServizioDiRappresentanza = value
            End Set
        End Property

        Public Property Sconto() As Decimal
            Get
                Return Me._Sconto
            End Get

            Set(ByVal value As Decimal)
                Me._Sconto = value
            End Set
        End Property

    End Class

End Namespace
