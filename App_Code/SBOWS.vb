Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports ASPNET.StarterKit.BusinessLogicLayer


' Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://ecoem.bclab.it/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class SBOWS
    Inherits System.Web.Services.WebService

    Public Credenziali As myHeader

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function ListaProduttori() As List(Of Produttore)

        Dim MyLog As New Log

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS ListaProduttori"
            MyLog.Descrizione = ""
            MyLog.Save()

            Return Produttore.Lista

        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If
    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function ListaProtocolli(FromDate As Date, ToDate As Date) As List(Of Protocollo)

        Dim MyLog As New Log

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS ListaProtocolli"
            MyLog.Descrizione = "Dadata: " & FromDate.ToString & " AData: " & ToDate.ToString
            MyLog.Save()

            Return Protocollo.Lista(FromDate, ToDate)

        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If
    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function AggiornaProtocollo(NrProtocollo As String, NrFattura As String, DataFattura As String, NrProforma As String, DataProforma As String) As Boolean

        Dim MyLog As New Log

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS AggiornaProtocollo"
            MyLog.Descrizione = "NrProtocollo: " & NrProtocollo & " - NrFattura: " & NrFattura & " - DataFattura: " & DataFattura.ToString
            MyLog.Save()

            If DataFattura <> "" Then
                If Not IsDate(DataFattura) Then
                    Throw New SoapException("Formato Data fattura errato", SoapException.ClientFaultCode)
                End If

            End If

            If DataProforma <> "" Then
                If Not IsDate(DataProforma) Then
                    Throw New SoapException("Formato Data proforma errato", SoapException.ClientFaultCode)
                End If
            End If

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
            Else
                Throw New SoapException("Protocollo " & NrProtocollo & " non trovato", SoapException.ClientFaultCode)
            End If

            Return True
        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If
    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function ListaDichiarazioni(FromDate As Date, ToDate As Date) As List(Of Dichiarazione)

        Dim MyLog As New Log

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS ListaDichiarazioni"
            MyLog.Descrizione = "Dadata: " & FromDate.ToString & " AData: " & ToDate.ToString
            MyLog.Save()

            Return Dichiarazione.Lista(FromDate, ToDate)

        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If

    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function ListaRigheDichiarazioni(idDichiarazione As Integer) As List(Of RigaDichiarazioneRaggruppate)

        Dim MyLog As New Log

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS ListaRigheDichiarazioni"
            MyLog.Descrizione = "IdDichiarazione: " & idDichiarazione.ToString
            MyLog.Save()

            Return RigaDichiarazioneRaggruppate.Lista(idDichiarazione)

        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If
    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function AggiornaDichiarazione(Id As Integer, NrFattura As String, DataFattura As String) As Boolean

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            Dim MyLog As New Log

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS AggiornaDichiarazione"
            MyLog.Descrizione = "IdDichiarazione: " & Id.ToString & " - NrFattura: " & NrFattura & " - DataFattura: " & DataFattura.ToString
            MyLog.Save()

            If DataFattura <> "" Then
                If Not IsDate(DataFattura) Then
                    Throw New SoapException("Formato Data fattura errato", SoapException.ClientFaultCode)
                End If

            End If

            Dim MyDichiarazione As Dichiarazione = Dichiarazione.Carica(Id)
            If Not IsNothing(MyDichiarazione) Then
                MyDichiarazione.NrFattura = NrFattura
                If DataFattura <> "" Then
                    MyDichiarazione.DataFattura = CDate(DataFattura)
                End If

                If NrFattura <> String.Empty Then
                    MyDichiarazione.Fatturata = True
                End If
                MyDichiarazione.Save()
            Else
                Throw New SoapException("Dichiarazione " & Id & " non trovata", SoapException.ClientFaultCode)
            End If

            Return True
        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If
    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function ListaAutocertificazioni(FromDate As Date, ToDate As Date) As List(Of Autocertificazione)

        Dim MyLog As New Log

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS ListaAutocertificazioni"
            MyLog.Descrizione = "Dadata: " & FromDate.ToString & " AData: " & ToDate.ToString
            MyLog.Save()

            Return Autocertificazione.Lista(FromDate, ToDate)

        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If

    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function AggiornaAutocertificazione(Id As Integer, NrFattura As String, DataFattura As String) As Boolean

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            Dim MyLog As New Log

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS AggiornaAutocertificazione"
            MyLog.Descrizione = "IdCertificazione: " & Id.ToString & " - NrFattura: " & NrFattura & " - DataFattura: " & DataFattura.ToString
            MyLog.Save()

            If DataFattura <> "" Then
                If Not IsDate(DataFattura) Then
                    Throw New SoapException("Formato Data fattura errato", SoapException.ClientFaultCode)
                End If

            End If

            Dim MyCertificazione As Autocertificazione = Autocertificazione.Carica(Id)
            If Not IsNothing(MyCertificazione) Then
                MyCertificazione.NrFattura = NrFattura
                If DataFattura <> "" Then
                    MyCertificazione.DataFattura = CDate(DataFattura)
                End If

                MyCertificazione.Save()
            Else
                Throw New SoapException("Dichiarazione " & Id & " non trovata", SoapException.ClientFaultCode)
            End If

            Return True
        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If
    End Function

    <WebMethod(True)> _
    <SoapHeader("Credenziali", Direction:=SoapHeaderDirection.InOut)> _
    Public Function ListaRigheAutocertificazioni(idCertificazione As Integer) As List(Of RigaAutocertificazioneRaggruppate)

        Dim MyLog As New Log

        If Membership.ValidateUser(Credenziali.Username, Credenziali.Password) Then

            MyLog.Data = Today
            MyLog.Ora = Now
            MyLog.Utente = Credenziali.Username
            MyLog.Origine = "WS ListaRigheAutocertificazioni"
            MyLog.Descrizione = "IdCertificazione: " & idCertificazione.ToString
            MyLog.Save()

            Return RigaAutocertificazioneRaggruppate.Lista(idCertificazione)

        Else
            Throw New SoapException(Credenziali.Username & " - " & Credenziali.Password, SoapException.ClientFaultCode)
        End If
    End Function

End Class

Public Class myHeader

    Inherits SoapHeader

    Public Username As String
    Public Password As String

End Class
