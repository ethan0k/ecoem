Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer

    Public Class Cliente

        Private _IdCliente As Integer
        Private _RagioneSociale As String
        Private _Indirizzo As String
        Private _Cap As String
        Private _Città As String
        Private _Provincia As String
        Private _Email As String
        Private _Telefono As String
        Private _Fax As String
        Private _PartitaIva As String
        Private _Contatto As String
        Private _Cognome As String
        Private _Nome As String
        Private _CodiceFiscale As String
        Private _Periodicita As Integer ' 1 Mensile, 2 Bimestrale, 3 Trimestrale
        Private _Attivo As Boolean
        Private _Note As String


        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal IdCliente As Integer, ByVal RagioneSociale As String, ByVal Indirizzo As String, ByVal Cap As String, _
                       ByVal Città As String, ByVal Provincia As String, ByVal Email As String, ByVal Telefono As String, _
                       ByVal Fax As String, ByVal PartitaIva As String, ByVal Contatto As String, ByVal Cognome As String, _
                       ByVal Nome As String, ByVal CodiceFiscale As String, ByVal _Periodicita As Integer, ByVal Attivo As Boolean, _
                       ByVal Note As String)

            Me._IdCliente = IdCliente
            Me._RagioneSociale = RagioneSociale
            Me._Indirizzo = Indirizzo
            Me._Cap = Cap
            Me._Città = Città
            Me._Provincia = Provincia
            Me._Email = Email
            Me._Telefono = Telefono
            Me._Fax = Fax
            Me._PartitaIva = PartitaIva
            Me._Contatto = Contatto
            Me._CodiceFiscale = CodiceFiscale
            Me._Cognome = Cognome
            Me._Nome = Nome
            Me._Periodicita = _Periodicita
            Me._Attivo = Attivo
            Me._Note = Note

        End Sub

        Public Shared Function Carica(ByVal IdCliente As Integer) As Cliente

            If IdCliente <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaCliente(IdCliente)

        End Function

        Public Function Save() As Boolean

            If IdCliente <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoCliente(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._IdCliente = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaCliente(Me)
            End If

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaCliente(Me)

        End Function

        Public Function VerificaImpianti() As Boolean

            Return DataAccessHelper.GetDataAccess().VerificaImpianti(Me)

        End Function

        Public ReadOnly Property IdCliente() As Integer

            Get
                Return Me._IdCliente
            End Get

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

        Public Property Telefono() As String

            Get
                If String.IsNullOrEmpty(Me._Telefono) Then
                    Return String.Empty
                Else
                    Return Me._Telefono
                End If
            End Get

            Set(ByVal value As String)
                Me._Telefono = value
            End Set

        End Property

        Public Property Fax() As String

            Get
                If String.IsNullOrEmpty(Me._Fax) Then
                    Return String.Empty
                Else
                    Return Me._Fax
                End If
            End Get

            Set(ByVal value As String)
                Me._Fax = value
            End Set

        End Property

        Public Property PartitaIva() As String

            Get
                If String.IsNullOrEmpty(Me._PartitaIva) Then
                    Return String.Empty
                Else
                    Return Me._PartitaIva
                End If
            End Get

            Set(ByVal value As String)
                Me._PartitaIva = value
            End Set

        End Property

        Public Property Contatto() As String

            Get
                If String.IsNullOrEmpty(Me._Contatto) Then
                    Return String.Empty
                Else
                    Return Me._Contatto
                End If
            End Get

            Set(ByVal value As String)
                Me._Contatto = value
            End Set

        End Property

        Public Property Cognome() As String

            Get
                If String.IsNullOrEmpty(Me._Cognome) Then
                    Return String.Empty
                Else
                    Return Me._Cognome
                End If
            End Get

            Set(ByVal value As String)
                Me._Cognome = value
            End Set

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

    End Class

End Namespace
