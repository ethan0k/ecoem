Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class RigaAutocertificazione

        Private _Id As Integer
        Private _IdCertificazione As Integer
        Private _IdCategoria As Integer
        Private _TipoDiDato As String
        Private _Pezzi As Integer
        Private _Kg As Decimal
        Private _PezziRettifica As Integer
        Private _KgRettifica As Decimal
        Private _DifferenzaPezzi As Integer
        Private _DifferenzaKg As Decimal
        Private _KgCertificazione As Decimal
        Private _KgDiffCertificazione As Decimal
        Private _KgCertSoci As Decimal
        Private _CostoUnitario As Decimal
        Private _Importo As Decimal
        Private _UtenteAggiornamento As String
        Private _DataAggiornamento As DateTime

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Id As Integer, ByVal IdCertificazione As Integer, ByVal IdCategoria As Integer, ByVal TipoDiDato As String, _
                       ByVal Pezzi As Integer, ByVal Kg As Decimal,ByVal PezziRettifica As Integer, ByVal KgRettifica As Decimal, _
                       ByVal DifferenzaPezzi As Integer, ByVal DifferenzaKg As Decimal, ByVal KgCertificazione As Decimal, ByVal KgDiffCertificazione As Decimal, _
                       ByVal CostoUnitario As Decimal,  ByVal KgCertSoci As Decimal, ByVal Importo As Decimal, ByVal UtenteAggiornamento as String, ByVal DataAggiornamento as DateTime)
            
            Me._Id = Id
            Me._IdCertificazione = IdCertificazione
            Me._IdCategoria = IdCategoria
            Me._TipoDiDato = TipoDiDato
            Me._Pezzi = Pezzi
            Me._Kg = Kg
            Me._PezziRettifica = PezziRettifica
            Me._KgRettifica = KgRettifica
            Me._KgCertificazione = KgCertificazione
            Me._DifferenzaPezzi = DifferenzaPezzi
            Me._DifferenzaKg = DifferenzaKg
            Me._KgCertificazione = KgCertificazione
            Me._KgDiffCertificazione = KgDiffCertificazione
            Me._KgCertSoci = KgCertSoci
            Me._CostoUnitario = CostoUnitario
            Me._Importo = Importo
            Me._UtenteAggiornamento = UtenteAggiornamento
            Me._DataAggiornamento = DataAggiornamento


        End Sub

        Public Shared Function Carica(ByVal Id As Integer) As RigaAutoCertificazione

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Return (Nothing)
            End If

            Return DataAccessHelper.GetDataAccess().CaricaRigaCertificazione(Id)

        End Function

        Public Shared Function Lista(ByVal IdAutocertificazione As Integer) As List(Of RigaAutoCertificazione)

            Return DataAccessHelper.GetDataAccess().ListaRigheCertificazione(IdAutocertificazione)

        End Function

        Public Function Save() As Boolean

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Dim TempId As Integer = DataAccessHelper.GetDataAccess().CreaNuovaRigaCertificazione(Me)
                If TempId > DefaultValues.GetCategoryIdMinValue() Then
                    Me._Id = TempId
                    Return True
                Else
                    Return False
                End If
            Else
                Return DataAccessHelper.GetDataAccess().AggiornaRigaCertificazione(Me)
            End If

        End Function


        Public ReadOnly Property Id() As Integer

            Get
                Return Me._Id
            End Get

        End Property

        Public Property IdCertificazione() As Integer
            Get
                Return _IdCertificazione
            End Get
            Set(ByVal value As Integer)
                _IdCertificazione = value
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

         Public Property PezziRettifica() As Integer
            Get
                Return _PezziRettifica
            End Get
            Set(ByVal value As Integer)
                _PezziRettifica = value
            End Set

        End Property
        Public Property KgRettifica() As Decimal
            Get
                Return _KgRettifica
            End Get
            Set(ByVal value As Decimal)
                _KgRettifica = value
            End Set

        End Property

          Public Property DifferenzaPezzi() As Integer
            Get
                Return _DifferenzaPezzi
            End Get
            Set(ByVal value As Integer)
                _DifferenzaPezzi = value
            End Set

        End Property
        Public Property DifferenzaKg() As Decimal
            Get
                Return _DifferenzaKg
            End Get
            Set(ByVal value As Decimal)
                _DifferenzaKg = value
            End Set

        End Property

        Public Property KgCertificazione() As Decimal
            Get
                Return _KgCertificazione
            End Get
            Set(ByVal value As Decimal)
                _KgCertificazione = value
            End Set

        End Property

         Public Property KgDiffCertificazione() As Decimal
            Get
                Return _KgDiffCertificazione
            End Get
            Set(ByVal value As Decimal)
                _KgDiffCertificazione = value
            End Set

        End Property

        Public Property KgCertSoci() As Decimal
            Get
                Return _KgCertSoci
            End Get
            Set(ByVal value As Decimal)
                _KgCertSoci = value
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