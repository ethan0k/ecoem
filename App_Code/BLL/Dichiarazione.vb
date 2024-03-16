Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class Dichiarazione

        Private _Id As Integer
        Private _Data As Date
        Private _IdProduttore As Integer
        Private _DataRegistrazione As Date
        Private _Utente As String ' (colui che ha compilata la dichiarazione o che l’ha modificata l’ultima volta)
        Private _Confermata As Boolean
        Private _DataConferma As Date
        Private _Importo As Decimal
        Private _AutocertificazioneProdotta As Boolean
        Private _Fatturata As Boolean
        Private _Professionale As Boolean
        Private _OldVersion As Boolean
        Private _NrFattura As String
        Private _DataFattura As Date
        Private _Autostimata As Boolean

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, Data As Date, ByVal IdProduttore As Integer, DataRegistrazione As Date, _
                       Utente As String, Confermata As Boolean, DataConferma As Date, ByVal Importo As Decimal, _
                       ByVal AutocertificazioneProdotta As Boolean, ByVal Fatturata As Boolean, ByVal Professionale As Boolean, _
                       ByVal OldVersion As Boolean, ByVal NrFattura as String, ByVal DataFattura as Date, ByVal Autostimata as Boolean)

            Me._Id = Id
            Me._Data = Data
            Me._IdProduttore = IdProduttore
            Me._DataRegistrazione = DataRegistrazione
            Me._Utente = Utente
            Me._Confermata = Confermata
            Me._DataConferma = DataConferma
            Me._Importo = Importo
            Me._AutocertificazioneProdotta = AutocertificazioneProdotta
            Me._Fatturata = Fatturata
            Me._Professionale = Professionale
            Me._OldVersion = OldVersion
			Me._NrFattura = NrFattura
            Me._DataFattura = DataFattura
            Me._Autostimata = Autostimata


        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As Dichiarazione

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaDichiarazione(Id)

        End Function

        Public Shared Function TotaleKgCategoria(ByVal Codice As String, Anno As Integer) As Decimal

            Return DataAccessHelper.GetDataAccess().TotaleKgCategoria(Codice, Anno)

        End Function

        Public Shared Function TotaleKgCategoriaNew(ByVal Codice As String, Anno As Integer) As Decimal

            Return DataAccessHelper.GetDataAccess().TotaleKgCategoriaNew(Codice, Anno)

        End Function

        Public Shared Function Carica(ByVal IdProduttore As Integer, DataDichiarazione As Date) As Dichiarazione

            If IdProduttore <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaDichiarazione2(IdProduttore, DataDichiarazione)

        End Function

        Public Shared Function Carica(ByVal IdProduttore As Integer, DataDichiarazione As Date, Professionale As Boolean) As Dichiarazione

            If IdProduttore <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaDichiarazione3(IdProduttore, DataDichiarazione, Professionale)

        End Function

        Public Shared Function Lista(ByVal IdProduttore As Integer, ByVal SoloProfessionale As Boolean) As List(Of Dichiarazione)

            Return DataAccessHelper.GetDataAccess().ListaDichiarazioni(IdProduttore, SoloProfessionale)

        End Function

        Public Shared Function Lista(ByVal FromDate As Date, ByVal ToDate As Date) As List(Of Dichiarazione)

            Return DataAccessHelper.GetDataAccess().ListaDichiarazioni(FromDate, ToDate)

        End Function

        Public Shared Function ListaAperte()

            Return DataAccessHelper.GetDataAccess().ListaAperte()

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaDichiarazione(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaDichiarazione(Me)
            End If

        End Function

        Public Function Elimina() As Boolean
            Return DataAccessHelper.GetDataAccess().EliminaDichiarazione(Me)
        End Function

        Public Property Id() As Integer

            Get
                Return Me._Id
            End Get

            Set(ByVal value As Integer)
                Me._Id = value
            End Set

        End Property

        Public Property Data() As Date

            Get
                Return Me._Data
            End Get

            Set(ByVal value As Date)
                Me._Data = value
            End Set

        End Property

        Public Property IdProduttore() As Integer

            Get
                Return Me._IdProduttore
            End Get

            Set(ByVal value As Integer)
                Me._IdProduttore = value
            End Set

        End Property

        Public Property DataRegistrazione() As Date

            Get
                Return Me._DataRegistrazione
            End Get

            Set(ByVal value As Date)
                Me._DataRegistrazione = value
            End Set

        End Property

        Public Property Utente() As String

            Get
                If String.IsNullOrEmpty(Me._Utente) Then
                    Return String.Empty
                Else
                    Return Me._Utente
                End If
            End Get

            Set(ByVal value As String)
                Me._Utente = value
            End Set

        End Property

        Public Property Confermata() As Boolean

            Get
                Return Me._Confermata
            End Get

            Set(ByVal value As Boolean)
                Me._Confermata = value
            End Set

        End Property

        Public Property DataConferma() As Date

            Get
                Return Me._DataConferma
            End Get

            Set(ByVal value As Date)
                Me._DataConferma = value
            End Set

        End Property

        Public ReadOnly Property Importo() As Decimal

            Get
                Return Me._Importo
            End Get

        End Property

        Public Property AutocertificazioneProdotta() As Boolean

            Get
                Return Me._AutocertificazioneProdotta
            End Get

            Set(ByVal value As Boolean)
                Me._AutocertificazioneProdotta = value
            End Set

        End Property

        Public Property Fatturata() As Boolean

            Get
                Return Me._Fatturata
            End Get

            Set(ByVal value As Boolean)
                Me._Fatturata = value
            End Set

        End Property

        Public Property Professionale() As Integer

            Get
                Return Me._Professionale
            End Get

            Set(ByVal value As Integer)
                Me._Professionale = value
            End Set

        End Property
        Public Property OldVersion() As Boolean

            Get
                Return Me._OldVersion
            End Get

            Set(ByVal value As Boolean)
                Me._OldVersion = value
            End Set

        End Property

        Public Property NrFattura() As String

            Get
                If String.IsNullOrEmpty(Me._NrFattura) Then
                    Return String.Empty
                Else
                    Return Me._NrFattura
                End If
            End Get

            Set(ByVal value As String)
                Me._NrFattura = value
            End Set

        End Property

        Public Property DataFattura() As Date

            Get
                Return Me._DataFattura
            End Get

            Set(ByVal value As Date)
                Me._DataFattura = value
            End Set

        End Property

        Public Property Autostimata() As Boolean

            Get
                Return Me._Autostimata
            End Get

            Set(ByVal value As Boolean)
                Me._Autostimata = value
            End Set

        End Property

    End Class
End Namespace