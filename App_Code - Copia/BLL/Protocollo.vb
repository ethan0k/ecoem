Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class Protocollo

        Private _Id As Integer
        Private _Protocollo As String
        Private _Data As Date
        Private _NrFattura As String
        Private _CCT As String
        Private _UserName As String
        Private _DataAttestato As Date
        Private _NrAttestato As String
        Private _IdProduttore As Integer
        Private _CostoServizio As Decimal
        Private _DataFattura As Date
        Private _NrProforma As String
        Private _DataProforma As Date
        Private _PannelliAbbinati As Integer

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal Protocollo As String, ByVal Data As Date, ByVal NrFattura As String, ByVal CCT As String, ByVal UserName As String, _
                       ByVal DataAttestato As Date, ByVal NrAttestato As String, ByVal IdProduttore As Integer, _
                       ByVal CostoServizio As Decimal, ByVal DataFattura As Date, ByVal NrProforma As String, ByVal DataProforma As Date)

            Me._Id = Id
            Me._Protocollo = Protocollo
            Me._Data = Data
            Me._NrFattura = NrFattura
            Me._CCT = CCT
            Me._UserName = UserName
            Me._DataAttestato = DataAttestato
            Me._NrAttestato = NrAttestato
            Me._IdProduttore = IdProduttore
            Me._CostoServizio = CostoServizio
            Me._DataFattura = DataFattura
            Me._NrProforma = NrProforma
            Me._DataProforma = DataProforma
        End Sub

        Public Shared Function Carica(ByVal Protocollo As String) As Protocollo

            If Protocollo = "" Or Protocollo Is DBNull.Value Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaProtocollo(Protocollo)

        End Function

        Public Shared Function CaricaDaId(ByVal Id As Integer) As Protocollo

            If Id = 0 Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaProtocolloDaId(Id)

        End Function

        Public Function Conforme() As Boolean

            Return DataAccessHelper.GetDataAccess().Conforme(Me)

        End Function

        Public Function ListaModelli() As List(Of String)

            Return DataAccessHelper.GetDataAccess().ListaModelli(Me)

        End Function

        Public Function ListaMarche() As List(Of String)

            Return DataAccessHelper.GetDataAccess().ListaMarche(Me)

        End Function

        Public Function ListaPannelli() As List(Of String)

            Return DataAccessHelper.GetDataAccess().ListaPannelli(Me)

        End Function
        Public Shared Function Lista(ByVal FromDate As Date, ByVal ToDate As Date) As List(Of Protocollo)

            Return DataAccessHelper.GetDataAccess().ListaProtocolli(FromDate, ToDate)

        End Function

        Public Function GetProduttore() As String

            Return DataAccessHelper.GetDataAccess().GetProduttore(Me)

        End Function

        Public Function GetIdProduttore() As Integer

            Return DataAccessHelper.GetDataAccess().GetIdProduttore(Me)

        End Function

        Public Function NrAttestatiProduttore(IdProduttore As Integer) As Integer

            Return DataAccessHelper.GetDataAccess().NrAttestatiProduttore(Me, IdProduttore)

        End Function

        Public Function Elimina() As Boolean

            Return DataAccessHelper.GetDataAccess().EliminaProtocollo(Me)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovoProtocollo(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaProtocollo(Me)
            End If

        End Function

        Public Property Id() As Integer
            Get
                Return Me._Id
            End Get

            Set(ByVal value As Integer)
                'Me._Protocollo = value
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

        Public Property Data() As Date
            Get
                Return _Data
            End Get
            Set(ByVal value As Date)
                _Data = value
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

        Public Property CCT() As String
            Get
                If String.IsNullOrEmpty(Me._CCT) Then
                    Return String.Empty
                Else
                    Return Me._CCT
                End If
            End Get

            Set(ByVal value As String)
                Me._CCT = value
            End Set

        End Property

        Public Property UserName() As String

            Get
                If String.IsNullOrEmpty(Me._UserName) Then
                    Return String.Empty
                Else
                    Return Me._UserName
                End If
            End Get

            Set(ByVal value As String)
                Me._UserName = value
            End Set

        End Property

        Public Property DataAttestato() As Date
            Get
                Return _DataAttestato
            End Get
            Set(ByVal value As Date)
                _DataAttestato = value
            End Set
        End Property

        Public Property NrAttestato() As String
            Get
                If String.IsNullOrEmpty(Me._NrAttestato) Then
                    Return String.Empty
                Else
                    Return Me._NrAttestato
                End If
            End Get

            Set(ByVal value As String)
                Me._NrAttestato = value
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

        Public Property CostoServizio() As Decimal
            Get
                Return _CostoServizio
            End Get
            Set(ByVal value As Decimal)
                _CostoServizio = value
            End Set
        End Property

        Public Property DataFattura() As Date
            Get
                Return _DataFattura
            End Get
            Set(ByVal value As Date)
                _DataFattura = value
            End Set
        End Property

        Public Property NrProforma() As String
            Get
                If String.IsNullOrEmpty(Me._NrProforma) Then
                    Return String.Empty
                Else
                    Return Me._NrProforma
                End If
            End Get

            Set(ByVal value As String)
                Me._NrProforma = value
            End Set

        End Property

        Public Property DataProforma() As Date
            Get
                Return _DataProforma
            End Get
            Set(ByVal value As Date)
                _DataProforma = value
            End Set
        End Property

        Public Property PannelliAbbinati() As Integer
            Get
                Return DataAccessHelper.GetDataAccess().ConteggioPannelli(Protocollo)
            End Get
            Set(ByVal value As Integer)
                '_DataProforma = value
            End Set
        End Property

    End Class

End Namespace
