Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data

Namespace ASPNET.StarterKit.BusinessLogicLayer
    Public Class RigaAutocertificazioneRaggruppate

        Private _Raggruppamento As String
        Private _Importo As Decimal

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal Raggruppamento As String, ByVal Importo As Decimal)

            Me._Raggruppamento = Raggruppamento
            Me._Importo = Importo

        End Sub

        Public Shared Function Lista(ByVal IdCertificazione As Integer) As List(Of RigaAutocertificazioneRaggruppate)

            Return DataAccessHelper.GetDataAccess().ListaRigheCertificazioneRaggruppate(IdCertificazione)

        End Function

        Public Property Raggruppamento() As String
            Get
                Return _Raggruppamento
            End Get
            Set(ByVal value As String)
                _Raggruppamento = value
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


    End Class
End Namespace