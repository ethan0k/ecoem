
Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class TESTWS
    Inherits System.Web.UI.Page

    Private Sub cmdAggiornaProtocollo_Click(sender As Object, e As EventArgs) Handles cmdAggiornaProtocollo.Click

        Dim NrProtocollo As String = "4892022023"
        Dim NrFattura As String = "232600006"
        Dim NrProforma As String = "NrProforma"
        Dim DataFattura As String = "17-01-2023"
        Dim DataProforma As String = "17-01-2023"

        Dim ProtocolloDaMod As Protocollo = Protocollo.Carica(NrProtocollo)
        If Not IsNothing(ProtocolloDaMod) Then
            ProtocolloDaMod.NrFattura = NrFattura
            ProtocolloDaMod.NrProforma = NrProforma
            If DataFattura <> "" Then
                ProtocolloDaMod.DataFattura = CDate(DataFattura)
            End If
            If DataProforma <> "" Then
                ProtocolloDaMod.DataProforma = CDate(DataProforma)
            End If
            ProtocolloDaMod.DataProforma = DataProforma
            ProtocolloDaMod.Save()
        End If

    End Sub
End Class
