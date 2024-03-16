Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class Autocertificazione

        Private _Id As Integer
        Private _IdProduttore As Integer
        Private _Anno As String
        Private _Data As Date
        Private _PathFile As String
        Private _NomeFile As String
        Private _PathFilePile As String
        Private _NomeFilePile As String
        Private _PathFileVeicoli As String
        Private _NomeFileVeicoli As String
        Private _PathFileIndustriali As String
        Private _NomeFileIndustriali As String
        Private _UploadEseguito As Boolean
        Private _Confermata As Boolean
        Private _DataConferma As Date ' Nuovo campo
        Private _Rettificata As Boolean
        Private _NrFattura As String
        Private _DataFattura As String

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal IdProduttore As Integer, ByVal Anno As Integer, ByVal Data As Date, ByVal PathFile As String, _
                       ByVal NomeFile As String, ByVal Pathfilepile As String, ByVal nomefilepile As String, ByVal pathfileindustriali As String, _
                       ByVal NomeFileIndustriali As String, ByVal PathFileVeicoli As String, ByVal NomeFileveicoli As String, _
                       ByVal UploadEseguito As Boolean, ByVal confermata As String, ByVal DataConferma As Date, ByVal Rettificata As Boolean, _
                       ByVal NrFattura As String, ByVal DataFattura As Date)

            Me._Id = Id
            Me._IdProduttore = IdProduttore
            Me._Anno = Anno
            Me._Data = Data
            Me._PathFile = PathFile
            Me._NomeFile = NomeFile
            Me._PathFilePile = Pathfilepile
            Me._NomeFilePile = nomefilepile
            Me._PathFileIndustriali = pathfileindustriali
            Me._NomeFileIndustriali = NomeFileIndustriali
            Me._PathFileVeicoli = PathFileVeicoli
            Me._NomeFileVeicoli = NomeFileveicoli
            Me._UploadEseguito = UploadEseguito
            Me._Confermata = confermata
            Me._DataConferma = DataConferma
            Me._Rettificata = Rettificata
            Me._NrFattura = NrFattura
            Me._DataFattura = DataFattura

        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As Autocertificazione

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaAutocertificazione(Id)

        End Function

        Public Shared Function Carica(ByVal IdProduttore As Integer, ByVal Anno As Integer) As Autocertificazione

            If IdProduttore <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            If Anno <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaAutocertificazione(IdProduttore, Anno)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaAutocertificazione(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaAutocertificazione(Me)
            End If

        End Function

        Public Function Elimina(ByVal Utente As String) As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaAutocertificazione(Me, Utente)

        End Function

        Public Function GeneraRighe() As List(Of RigaAutocertificazione)
           
            Return DataAccessHelper.GetDataAccess().GeneraRigheCertificazione(Me)

        End Function
        Public Shared Function ListaAutocertificazioniAperte() As List(Of Autocertificazione)

            Return DataAccessHelper.GetDataAccess().ListaAutocertificazioniAperte()

        End Function
        Public Function Importo() As Decimal
             Return DataAccessHelper.GetDataAccess().ImportoAutocertificazione(Me)
        End Function

        Public Function RigheGenerate() As Boolean
             Return DataAccessHelper.GetDataAccess().RigheAutocertificazioneEsiste(Me)
        End Function

        Public Function IsRettificata() As Boolean
             Return DataAccessHelper.GetDataAccess().AutocertificazioneRettificata(Me)
        End Function

        Public Shared Function Lista(ByVal FromDate As Date, ByVal ToDate As Date) As List(Of Autocertificazione)

            Return DataAccessHelper.GetDataAccess().ListaAutocertificazioni(FromDate, ToDate)

        End Function

        Public Property Id() As Integer

            Get
                Return Me._Id
            End Get
            Set(ByVal value As Integer)

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

        Public Property Anno() As Integer
            Get
                Return _Anno
            End Get
            Set(ByVal value As Integer)
                _Anno = value
            End Set
        End Property

        Public Property Data() As Date
            Get
                Return _Data
            End Get
            Set(ByVal value As Date)
                _Data = value
            End Set
        End Property

        Public Property PathFile() As String

            Get
                If String.IsNullOrEmpty(Me._PathFile) Then
                    Return String.Empty
                Else
                    Return Me._PathFile
                End If
            End Get

            Set(ByVal value As String)
                Me._PathFile = value
            End Set

        End Property

        Public Property NomeFile() As String

            Get
                If String.IsNullOrEmpty(Me._NomeFile) Then
                    Return String.Empty
                Else
                    Return Me._NomeFile
                End If
            End Get

            Set(ByVal value As String)
                Me._NomeFile = value
            End Set

        End Property

        Public Property PathFilePile() As String

            Get
                If String.IsNullOrEmpty(Me._PathFilePile) Then
                    Return String.Empty
                Else
                    Return Me._PathFilePile
                End If
            End Get

            Set(ByVal value As String)
                Me._PathFilePile = value
            End Set

        End Property

        Public Property NomeFilePile() As String

            Get
                If String.IsNullOrEmpty(Me._NomeFilePile) Then
                    Return String.Empty
                Else
                    Return Me._NomeFilePile
                End If
            End Get

            Set(ByVal value As String)
                Me._NomeFilePile = value
            End Set

        End Property

        Public Property PathFileVeicoli() As String

            Get
                If String.IsNullOrEmpty(Me._PathFileVeicoli) Then
                    Return String.Empty
                Else
                    Return Me._PathFileVeicoli
                End If
            End Get

            Set(ByVal value As String)
                Me._PathFileVeicoli = value
            End Set

        End Property

        Public Property NomeFileVeicoli() As String

            Get
                If String.IsNullOrEmpty(Me._NomeFileVeicoli) Then
                    Return String.Empty
                Else
                    Return Me._NomeFileVeicoli
                End If
            End Get

            Set(ByVal value As String)
                Me._NomeFileVeicoli = value
            End Set

        End Property

        Public Property PathFileIndustriali() As String

            Get
                If String.IsNullOrEmpty(Me._PathFileIndustriali) Then
                    Return String.Empty
                Else
                    Return Me._PathFileIndustriali
                End If
            End Get

            Set(ByVal value As String)
                Me._PathFileIndustriali = value
            End Set

        End Property

        Public Property NomeFileIndustriali() As String

            Get
                If String.IsNullOrEmpty(Me._NomeFileIndustriali) Then
                    Return String.Empty
                Else
                    Return Me._NomeFileIndustriali
                End If
            End Get

            Set(ByVal value As String)
                Me._NomeFileIndustriali = value
            End Set

        End Property


        Public Property UploadEseguito() As Boolean
            Get
                Return _UploadEseguito
            End Get
            Set(ByVal value As Boolean)
                _UploadEseguito = value
            End Set
        End Property

         Public Property Confermata() As Boolean
            Get
                Return _Confermata
            End Get
            Set(ByVal value As Boolean)
                _Confermata = value
            End Set
        End Property

        Public Property DataConferma() As Date
            Get
                Return _DataConferma
            End Get
            Set(ByVal value As Date)
                _DataConferma = value
            End Set
        End Property
     
        Public Property Rettificata() As Boolean
            Get
                Return _Rettificata
            End Get
            Set(ByVal value As Boolean)
                _Rettificata = value
            End Set
        End Property

        Public Property NrFattura() As String
            Get
                Return _NrFattura
            End Get
            Set(ByVal value As String)
                _NrFattura = value
            End Set
        End Property

        Public Property DataFattura() As Date
            Get
                If _DataFattura = Nothing Then
                    _DataFattura = DefaultValues.GetDateTimeMinValue
                End If
                Return _DataFattura
            End Get
            Set(ByVal value As Date)
                _DataFattura = value
            End Set
        End Property

    End Class
End Namespace

