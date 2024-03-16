Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class Impianto

        Private _Id As Integer
        Private _Codice As String
        Private _Descrizione As String
        Private _Indirizzo As String
        Private _Cap As String
        Private _Città As String
        Private _Provincia As String
        Private _Latitudine As String
        Private _Longitudine As String
        Private _IdCliente As Integer
        Private _DataCreazione As DateTime
        Private _Responsabile As String
        Private _NrPraticaGSE As String
        Private _Regione As String
        Private _ContoEnergia As String
        Private _DataEntrataInEsercizio As DateTime
        Private _Attestato As String
        Private _DataAttestato As DateTime
        Private _NrAttestato As Integer
        Private _NomeProduttore As String

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, Codice As String, Descrizione As String, ByVal Indirizzo As String, Cap As String, Città As String, Provincia As String, Latitudine As String, _
                        ByVal Longitudine As String, ByVal IdCliente As Integer, ByVal DataCreazione As Date, ByVal Responsabile As String, ByVal NrPraticaGSE As String, ByVal Regione As String, ByVal ContoEnergia As String, _
                        DataEntrataInEsercizio As DateTime, ByVal Attestato As String, ByVal DataAttestato As DateTime, NrAttestato As Integer, _
                       ByVal NomeProduttore As String)

            Me._Id = Id
            Me._Codice = Codice
            Me._Descrizione = Descrizione
            Me._Indirizzo = Indirizzo
            Me._Cap = Cap
            Me._Città = Città
            Me._Provincia = Provincia
            Me._Latitudine = Latitudine
            Me._Longitudine = Longitudine
            Me._IdCliente = IdCliente
            Me._DataCreazione = DataCreazione
            Me._Responsabile = Responsabile
            Me._NrPraticaGSE = NrPraticaGSE
            Me._Regione = Regione
            Me._ContoEnergia = ContoEnergia
            Me._DataEntrataInEsercizio = DataEntrataInEsercizio
            Me._Attestato = Attestato
            Me._DataAttestato = DataAttestato
            Me._NrAttestato = NrAttestato
            Me._NomeProduttore = NomeProduttore

        End Sub

        Public Shared Function Carica(ByVal IdImpianto As Integer) As Impianto

            If IdImpianto <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaImpianto(IdImpianto)

        End Function

        Public Shared Function CaricaDaIdCliente(ByVal IdCliente As Integer) As List(Of Impianto)

            If IdCliente <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaImpiantoDaIdCliente(IdCliente)

        End Function

        Public Shared Function TotaleImpianti() As Integer

            Return DataAccessHelper.GetDataAccess().TotaleImpianti()

        End Function

        Public Shared Function TotaleImpianti(IdCliente As Integer) As Integer

            Return DataAccessHelper.GetDataAccess().TotaleImpianti(IdCliente)

        End Function

        Public Function Verifica() As Boolean ' Verifica la presenza di pannelli non certificati

            Return DataAccessHelper.GetDataAccess().VerificaImpianto(Me)

        End Function

        Public Function Controlla() As Boolean ' Verifica la presenza di pannelli 

            Return DataAccessHelper.GetDataAccess().ControllaImpianto(Me)

        End Function

        Public Function GeneraAttestato() As Boolean

            Return DataAccessHelper.GetDataAccess().GeneraAttestato(Me)

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaImpianto(Me)

        End Function

        Public Function Disabbina() As Boolean

            Return DataAccessHelper.GetDataAccess().DisabbinaImpianto(Me)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoImpianto(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaImpianto(Me)
            End If

        End Function

        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

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

        Public Property Descrizione() As String

            Get
                If String.IsNullOrEmpty(Me._Descrizione) Then
                    Return String.Empty
                Else
                    Return Me._Descrizione
                End If
            End Get

            Set(ByVal value As String)
                Me._Descrizione = value
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

        Public Property Cap() As String

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

        Public Property Città() As String

            Get
                If String.IsNullOrEmpty(Me._Città) Then
                    Return String.Empty
                Else
                    Return Me._Città
                End If
            End Get

            Set(ByVal value As String)
                Me._Città = value
            End Set

        End Property

        Public Property Provincia() As String

            Get
                If String.IsNullOrEmpty(Me._Provincia) Then
                    Return String.Empty
                Else
                    Return Me._Provincia
                End If
            End Get

            Set(ByVal value As String)
                Me._Provincia = value
            End Set

        End Property

        Public Property Latitudine() As String

            Get
                If String.IsNullOrEmpty(Me._Latitudine) Then
                    Return String.Empty
                Else
                    Return Me._Latitudine
                End If
            End Get

            Set(ByVal value As String)
                Me._Latitudine = value
            End Set

        End Property

        Public Property Longitudine() As String

            Get
                If String.IsNullOrEmpty(Me._Longitudine) Then
                    Return String.Empty
                Else
                    Return Me._Longitudine
                End If
            End Get

            Set(ByVal value As String)
                Me._Longitudine = value
            End Set

        End Property

        Public Property DataCreazione() As DateTime
            Get
                Return _DataCreazione
            End Get
            Set(ByVal value As DateTime)
                _DataCreazione = value
            End Set
        End Property

        Public Property IdCliente() As Integer
            Get
                Return _IdCliente
            End Get
            Set(ByVal value As Integer)
                _IdCliente = value
            End Set
        End Property

        Public Property Responsabile() As String

            Get
                If String.IsNullOrEmpty(Me._Responsabile) Then
                    Return String.Empty
                Else
                    Return Me._Responsabile
                End If
            End Get

            Set(ByVal value As String)
                Me._Responsabile = value
            End Set

        End Property

        Public Property NrPraticaGSE() As String

            Get
                If String.IsNullOrEmpty(Me._NrPraticaGSE) Then
                    Return String.Empty
                Else
                    Return Me._NrPraticaGSE
                End If
            End Get

            Set(ByVal value As String)
                Me._NrPraticaGSE = value
            End Set

        End Property

        Public Property Regione() As String

            Get
                If String.IsNullOrEmpty(Me._Regione) Then
                    Return String.Empty
                Else
                    Return Me._Regione
                End If
            End Get

            Set(ByVal value As String)
                Me._Regione = value
            End Set

        End Property

        Public Property ContoEnergia() As String

            Get
                If String.IsNullOrEmpty(Me._ContoEnergia) Then
                    Return String.Empty
                Else
                    Return Me._ContoEnergia
                End If
            End Get

            Set(ByVal value As String)
                Me._ContoEnergia = value
            End Set

        End Property

        Public Property DataEntrataInEsercizio() As DateTime
            Get
                Return _DataEntrataInEsercizio
            End Get
            Set(ByVal value As DateTime)
                _DataEntrataInEsercizio = value
            End Set
        End Property

        Public Property Attestato() As String

            Get
                If String.IsNullOrEmpty(Me._Attestato) Then
                    Return String.Empty
                Else
                    Return Me._Attestato
                End If
            End Get

            Set(ByVal value As String)
                Me._Attestato = value
            End Set

        End Property

        Public Property DataAttestato() As DateTime
            Get
                Return _DataAttestato
            End Get
            Set(ByVal value As DateTime)
                _DataAttestato = value
            End Set
        End Property

        Public Property NrAttestato() As Integer
            Get
                Return _NrAttestato
            End Get
            Set(ByVal value As Integer)
                _NrAttestato = value
            End Set
        End Property

         Public Property NomeProduttore() As String
            Get
                Return _NomeProduttore
            End Get
            Set(ByVal value As String)
                _NomeProduttore = value
            End Set
        End Property
    End Class
End Namespace
