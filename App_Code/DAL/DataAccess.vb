Imports System
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports ASPNET.StarterKit.BusinessLogicLayer

Namespace ASPNET.StarterKit.DataAccessLayer

    Public MustInherit Class DataAccess

        Protected ReadOnly Property ConnectionString() As String

            Get

                Dim connStr As String = ""

                If Not ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker") Is Nothing Then
                    connStr = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
                End If

                If String.IsNullOrEmpty(connStr) Then
                    Throw New NullReferenceException("ConnectionString configuration is missing from you web.config. It should contain  <connectionStrings> <add key=""aspnet_staterKits_TimeTracker"" value=""Server=(local);Integrated Security=True;Database=Issue_Tracker"" </connectionStrings>")
                Else
                    Return connStr
                End If

            End Get

        End Property

        'Produttore
        Public MustOverride Function CreaNuovoProduttore(ByVal Entity As Produttore) As Integer
        Public MustOverride Function CaricaProduttore(ByVal IdProduttore As Integer) As Produttore
        Public MustOverride Function AggiornaProduttore(ByVal Entity As Produttore) As Boolean
        Public MustOverride Function TotaleProduttori() As Integer
        Public MustOverride Function EliminaProduttore(ByVal Entity As Produttore) As Boolean
        Public MustOverride Function VerificaPannelli(ByVal Entity As Produttore) As Boolean
        Public MustOverride Function EsisteAEE(ByVal IdProduttore As Integer) As Boolean
        Public MustOverride Function EsistePILE(ByVal IdProduttore As Integer) As Boolean
        Public MustOverride Function EsisteINDUSTRIAL(ByVal IdProduttore As Integer) As Boolean
        Public MustOverride Function EsisteVEICOLI(ByVal IdProduttore As Integer) As Boolean
        Public MustOverride Function GetLastNrComunicazione(ByVal Entity As Produttore, Anno As Integer) As Integer
        Public MustOverride Function ListaProduttore() As List(Of Produttore)
        Public MustOverride Function VerificaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Boolean ' Se ci sono ancora dichiarazioni non certificate
        Public MustOverride Function ContaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Integer ' Conta le certificazioni in un periodo
        Public MustOverride Function CertificaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Integer ' Certifica tutte le dichiarazioni di un dato periodo
        Public MustOverride Function SoloProfessionale(ByVal Entity As Produttore) As Boolean

        'Pannello
        Public MustOverride Function CreaNuovoPannello(ByVal Entity As Pannello) As Integer
        Public MustOverride Function CaricaPannello(ByVal IdProduttore As Integer) As Pannello
        Public MustOverride Function AggiornaPannello(ByVal Entity As Pannello) As Boolean
        Public MustOverride Function MatricolaExists(ByVal IdMarca As Integer, Matricola As String) As Boolean
        Public MustOverride Function TotalePannelli() As Integer
        Public MustOverride Function TotaleAbbinati() As Integer
        Public MustOverride Function TotaleConformi() As Integer
        Public MustOverride Function TotaleDismessi() As Integer
        Public MustOverride Function TotalePannelliProduttore(IdProduttore As Integer) As Integer
        Public MustOverride Function TotalePannelliCliente(IdCliente As Integer) As Integer
        Public MustOverride Function CaricaPannelloDaMatricola(ByVal Matricola As String, CodiceProduttore As String) As Pannello
        Public MustOverride Function ListaPannelli(ByVal IdImpianto As Integer) As List(Of Pannello)
        Public MustOverride Function ListaPannelli(ByVal Matricola As String) As List(Of Pannello)
        Public MustOverride Function EliminaPannello(ByVal entity As Pannello) As Boolean

        'Clienti
        Public MustOverride Function CreaNuovoCliente(ByVal Entity As Cliente) As Integer
        Public MustOverride Function CaricaCliente(ByVal IdCliente As Integer) As Cliente
        Public MustOverride Function AggiornaCliente(ByVal Entity As Cliente) As Boolean
        Public MustOverride Function EliminaCliente(ByVal Entity As Cliente) As Boolean
        Public MustOverride Function VerificaImpianti(ByVal Entity As Cliente) As Boolean

        'Impianti
        Public MustOverride Function CreaNuovoImpianto(ByVal Entity As Impianto) As Integer
        Public MustOverride Function CaricaImpianto(ByVal IdProduttore As Integer) As Impianto
        Public MustOverride Function AggiornaImpianto(ByVal Entity As Impianto) As Boolean
        Public MustOverride Function TotaleImpianti() As Integer
        Public MustOverride Function TotaleImpianti(IdCliente As Integer) As Integer
        Public MustOverride Function VerificaImpianto(ByVal Entity As Impianto) As Boolean
        Public MustOverride Function ControllaImpianto(ByVal Entity As Impianto) As Boolean
        Public MustOverride Function GeneraAttestato(ByVal Entity As Impianto) As Boolean
        Public MustOverride Function CaricaImpiantoDaIdCliente(ByVal IdCliente As Integer) As List(Of Impianto)
        Public MustOverride Function EliminaImpianto(ByVal Entity As Impianto) As Boolean
        Public MustOverride Function DisabbinaImpianto(ByVal Entity As Impianto) As Boolean
        Public MustOverride Function ValoreImpianto(ByVal Entity As Impianto) As Decimal
        Public MustOverride Function ListaProtocolliImpianto(ByVal Entity As Impianto) As List(Of String)
        Public MustOverride Function ListaProduttoriImpianto(ByVal Entity As Impianto) As List(Of String)
        Public MustOverride Function ListaImpianti(ByVal IdCliente As Integer) As List(Of Impianto)
        Public MustOverride Function TotaleMatricole(ByVal Entity As Impianto) As Integer

        'UtentiClienti
        Public MustOverride Function CreaNuovoUtenteCliente(ByVal Entity As UtenteCliente) As Integer
        Public MustOverride Function CaricaUtenteCliente(ByVal UserId As Guid) As UtenteCliente
        Public MustOverride Function AggiornaUtenteCliente(ByVal Entity As UtenteCliente) As Boolean
        Public MustOverride Function EliminaUtenteCliente(ByVal Entity As UtenteCliente) As Boolean

        'UtentiProduttori
        Public MustOverride Function CreaNuovoUtenteProduttore(ByVal Entity As UtenteProduttore) As Integer
        Public MustOverride Function CaricaUtenteProduttore(ByVal UserId As Guid) As UtenteProduttore
        Public MustOverride Function AggiornaUtenteProduttore(ByVal Entity As UtenteProduttore) As Boolean
        Public MustOverride Function EliminaUtenteProduttore(ByVal Entity As UtenteProduttore) As Boolean

        'ErroriImportazione
        Public MustOverride Function CreaNuovoErrore(ByVal Entity As ErroreImportazione) As Integer
        Public MustOverride Function CaricaErroreImportazione(ByVal IdProduttore As Integer) As ErroreImportazione
        Public MustOverride Function CaricaErroreImportazione(ByVal Matricola As String, Username As String) As ErroreImportazione
        Public MustOverride Function AggiornaErroreImportazione(ByVal Entity As ErroreImportazione) As Boolean
        Public MustOverride Function IsInError(ByVal Username As String, ByVal TipoImportazione As Integer) As Boolean
        Public MustOverride Function GetByIdImportazione(ByVal UserName As String, ByVal TipoImportazione As Integer) As List(Of ErroreImportazione)
        Public MustOverride Function EliminaErrore(ByVal entity As ErroreImportazione) As Boolean
        Public MustOverride Function EliminaErroriTutti(ByVal UserName As String) As Boolean
        Public MustOverride Function EliminaErroriTuttiDismessi(ByVal UserName As String) As Boolean
        Public MustOverride Function EliminaErroriTuttiImpianto(ByVal UserName As String) As Boolean
        Public MustOverride Function EliminaErroriTuttiVerifica(ByVal UserName As String) As Boolean
        Public MustOverride Function ErroreExists(ByVal Matricola As String, ByVal UserName As String, ByVal Id As Integer) As Boolean

        'Utilities
        Public MustOverride Function MembershipDelete(ByVal UserId As Guid) As Boolean

        'Protocollo
        Public MustOverride Function CreaNuovoProtocollo(ByVal Entity As Protocollo) As Integer
        Public MustOverride Function CaricaProtocolloDaId(ByVal Id As Integer) As Protocollo
        Public MustOverride Function CaricaProtocollo(ByVal Protocollo As String) As Protocollo
        Public MustOverride Function AggiornaProtocollo(ByVal Entity As Protocollo) As Boolean
        Public MustOverride Function EliminaProtocollo(ByVal Entity As Protocollo) As Boolean
        Public MustOverride Function Conforme(ByVal Entity As Protocollo) As Boolean
        Public MustOverride Function ListaModelli(ByVal Entity As Protocollo) As List(Of String)
        Public MustOverride Function ListaMarche(ByVal Entity As Protocollo) As List(Of String)
        Public MustOverride Function ListaPannelli(ByVal Entity As Protocollo) As List(Of String)
        Public MustOverride Function GetProduttore(ByVal Entity As Protocollo) As String
        Public MustOverride Function GetIdProduttore(ByVal Entity As Protocollo) As Integer
        Public MustOverride Function NrAttestatiProduttore(ByVal Entity As Protocollo, IdProduttore As Integer) As Integer
        Public MustOverride Function ListaProtocolli(ByVal FromDate As Date, ToDate As Date) As List(Of Protocollo)
        Public MustOverride Function ConteggioPannelli(ByVal Protocollo As String) As Integer

        'Categoria
        Public MustOverride Function CreaNuovaCategoria(ByVal Entity As Categoria) As Integer
        Public MustOverride Function CaricaCategoria(ByVal Id As Integer) As Categoria
        Public MustOverride Function AggiornaCategoria(ByVal Entity As Categoria) As Boolean
        Public MustOverride Function EliminaCategoria(ByVal Entity As Categoria) As Boolean

        'CategoriaProduttore
        Public MustOverride Function CreaNuovaCategoriaProduttore(ByVal Entity As Categoria_Produttore) As Integer
        Public MustOverride Function CaricaCategoriaProduttore(ByVal Id As Integer) As Categoria_Produttore
        Public MustOverride Function AggiornaCategoriaProduttore(ByVal Entity As Categoria_Produttore) As Boolean
        Public MustOverride Function EliminaCategoriaProduttore(ByVal Entity As Categoria_Produttore) As Boolean
        Public MustOverride Function CaricaCategoriaProduttore(ByVal IdCategoria As Integer, ByVal IdProduttore As Integer, ByVal Professionale As Boolean) As Categoria_Produttore
        Public MustOverride Function VerificaCategoriaProduttore(ByVal IdCategoria As Integer) As Boolean
        Public MustOverride Function ListaCategoriaProduttore(ByVal Id As Integer, ByVal Professionale As Boolean) As List(Of Categoria_Produttore)

        'Dichiarazione
        Public MustOverride Function CreaNuovaDichiarazione(ByVal Entity As Dichiarazione) As Integer
        Public MustOverride Function CaricaDichiarazione(ByVal Id As Integer) As Dichiarazione
        Public MustOverride Function CaricaDichiarazione2(ByVal IdProduttore As Integer, ByVal Datadichiarazione As Date) As Dichiarazione
        Public MustOverride Function CaricaDichiarazione3(ByVal IdProduttore As Integer, ByVal Datadichiarazione As Date, ByVal Professionale As Boolean) As Dichiarazione
        Public MustOverride Function AggiornaDichiarazione(ByVal Entity As Dichiarazione) As Boolean
        Public MustOverride Function EliminaDichiarazione(ByVal Entity As Dichiarazione) As Boolean
        Public MustOverride Function TotaleKgCategoria(ByVal Codice As String, ByVal Anno As Integer) As Decimal
        Public MustOverride Function TotaleKgCategoriaNew(ByVal Codice As String, ByVal Anno As Integer) As Decimal
        Public MustOverride Function ListaDichiarazioni(ByVal idProduttore As Integer, ByVal SoloProfessionale As Boolean) As List(Of Dichiarazione)
        Public MustOverride Function ListaDichiarazioni(ByVal FromDate As Date, ByVal ToDate As Date) As List(Of Dichiarazione)
        Public MustOverride Function ListaAperte() As List(Of Dichiarazione)

        'Riga Dichiarazione
        Public MustOverride Function CreaNuovaRigaDichiarazione(ByVal Entity As RigaDichiarazione) As Integer
        Public MustOverride Function CaricaRigaDichiarazione(ByVal Id As Integer) As RigaDichiarazione
        Public MustOverride Function AggiornaRigaDichiarazione(ByVal Entity As RigaDichiarazione) As Boolean
        Public MustOverride Function ListaRigheDichiarazioni(ByVal IdDichiarazione As Integer) As List(Of RigaDichiarazione)

        'Autocertificazione
        Public MustOverride Function CreaNuovaAutocertificazione(ByVal Entity As Autocertificazione) As Integer
        Public MustOverride Function CaricaAutocertificazione(ByVal Id As Integer) As Autocertificazione
        Public MustOverride Function AggiornaAutocertificazione(ByVal Entity As Autocertificazione) As Boolean
        Public MustOverride Function ImportoAutocertificazione(ByVal Entity As Autocertificazione) As Decimal
        Public MustOverride Function RigheAutocertificazioneEsiste(ByVal Entity As Autocertificazione) As Boolean
        Public MustOverride Function AutocertificazioneRettificata(ByVal Entity As Autocertificazione) As Boolean
        Public MustOverride Function ListaAutocertificazioni(ByVal FromDate As Date, ByVal ToDate As Date) As List(Of Autocertificazione)
        Public MustOverride Function CaricaAutocertificazione(ByVal IdProduttore As Integer, ByVal Anno As Integer) As Autocertificazione
        Public MustOverride Function EliminaAutocertificazione(ByVal Entity As Autocertificazione, ByVal Utente As String) As Boolean
        Public MustOverride Function ListaAutocertificazioniAperte() As List(Of Autocertificazione)

        'Opzione
        Public MustOverride Function CreaNuovaOpzione(ByVal Entity As Opzione) As Integer
        Public MustOverride Function CaricaOpzione() As Opzione
        Public MustOverride Function AggiornaOpzione(ByVal Entity As Opzione) As Boolean

        'Log
        Public MustOverride Function CreaNuovoLog(ByVal Entity As Log) As Integer
        Public MustOverride Function CaricaLog(Id As Integer) As Log
        Public MustOverride Function CaricaLogNonLetti() As Integer
        Public MustOverride Function AggiornaLog(ByVal Entity As Log) As Boolean

        'CategoriaNew
        Public MustOverride Function CreaNuovaCategoriaNew(ByVal Entity As CategoriaNew) As Integer
        Public MustOverride Function CaricaCategoriaNew(ByVal Id As Integer) As CategoriaNew
        Public MustOverride Function AggiornaCategoriaNew(ByVal Entity As CategoriaNew) As Boolean
        Public MustOverride Function EliminaCategoriaNew(ByVal Entity As CategoriaNew) As Boolean
        Public MustOverride Function IsPileEnabled(IdProduttore As Integer) As Boolean
        Public MustOverride Function IsAEEEnabled(IdProduttore As Integer) As Boolean
        Public MustOverride Function IsVeicoliEnabled(IdProduttore As Integer) As Boolean
        Public MustOverride Function IsIndustrialiEnabled(IdProduttore As Integer) As Boolean
        Public MustOverride Function ListaCategorieNew() As List(Of CategoriaNew)

        'CategoriaProduttoreNew
        Public MustOverride Function CreaNuovaCategoriaProduttoreNew(ByVal Entity As Categoria_ProduttoreNew) As Integer
        Public MustOverride Function CaricaCategoriaProduttoreNew(ByVal Id As Integer) As Categoria_ProduttoreNew
        Public MustOverride Function AggiornaCategoriaProduttoreNew(ByVal Entity As Categoria_ProduttoreNew) As Boolean
        Public MustOverride Function EliminaCategoriaProduttoreNew(ByVal Entity As Categoria_ProduttoreNew) As Boolean
        Public MustOverride Function CaricaCategoriaProduttoreNew(ByVal IdCategoria As Integer, ByVal IdProduttore As Integer, ByVal Professionale As Boolean) As Categoria_ProduttoreNew
        Public MustOverride Function VerificaCategoriaProduttoreNew(ByVal IdCategoria As Integer) As Boolean
        Public MustOverride Function ListaCategoriaProduttoreNew(ByVal Id As Integer, ByVal Professionale As Boolean) As List(Of Categoria_ProduttoreNew)
        Public MustOverride Function AggiornaCategorieProduttori(ByVal IdCategoria As Integer, ByVal Disattiva As Integer) As Boolean

        'Riga Autocertificazione
        Public MustOverride Function CreaNuovaRigaCertificazione(ByVal Entity As RigaAutocertificazione) As Integer
        Public MustOverride Function CaricaRigaCertificazione(ByVal Id As Integer) As RigaAutocertificazione
        Public MustOverride Function AggiornaRigaCertificazione(ByVal Entity As RigaAutocertificazione) As Boolean
        Public MustOverride Function ListaRigheCertificazione(ByVal IdAutocertificazione As Integer) As List(Of RigaAutocertificazione)
        Public MustOverride Function GeneraRigheCertificazione(ByVal Entity As Autocertificazione) As List(Of RigaAutocertificazione)

        'Fascia di peso
        Public MustOverride Function CreaNuovaFasciaDiPeso(ByVal Entity As FasciaDiPeso) As Integer
        Public MustOverride Function CaricaFasciaDiPeso(ByVal Id As Integer) As FasciaDiPeso
        Public MustOverride Function AggiornaFasciaDiPeso(ByVal Entity As FasciaDiPeso) As Boolean
        Public MustOverride Function EliminaFasciaDipeso(ByVal Entity As FasciaDiPeso) As Boolean
        Public MustOverride Function VerificaFasciaDipeso(ByVal Idfascia As Integer) As Boolean
        Public MustOverride Function VerificaFasciaDipeso(ByVal Entity As FasciaDiPeso) As Boolean


        'Tipologia cella
        Public MustOverride Function CreaNuovaTipologiaCella(ByVal Entity As TipologiaCella) As Integer
        Public MustOverride Function CaricaTipologiaCella(ByVal Id As Integer) As TipologiaCella
        Public MustOverride Function AggiornaTipologiaCella(ByVal Entity As TipologiaCella) As Boolean
        Public MustOverride Function EliminaTipologiaCella(ByVal Entity As TipologiaCella) As Boolean
        Public MustOverride Function VerificaTipologiaCella(ByVal IdTipologia As Integer) As Boolean
        Public MustOverride Function VerificaTipologiaCella(ByVal Entity As TipologiaCella) As Boolean

        'Abbinamenti Tipologia cella / Fascia di peso per produttore
        Public MustOverride Function CreaNuovoAbbinamento(ByVal Entity As AbbinamentoTipologiaFascia) As Integer
        Public MustOverride Function CaricaAbbinamento(ByVal Id As Integer) As AbbinamentoTipologiaFascia
        Public MustOverride Function CaricaAbbinamento(ByVal IdProduttore As Integer, ByVal IdTipologia As Integer, ByVal IdFascia As Integer) As AbbinamentoTipologiaFascia
        Public MustOverride Function AggiornaAbbinamento(ByVal Entity As AbbinamentoTipologiaFascia) As Boolean
        Public MustOverride Function EliminaAbbinamento(ByVal Entity As AbbinamentoTipologiaFascia) As Boolean

        'Riga Dichiarazione raggruppate        
        Public MustOverride Function ListaRigheDicRaggruppate(ByVal IdDichiarazione As Integer) As List(Of RigaDichiarazioneRaggruppate)

        'Riga Certificazione raggruppate        
        Public MustOverride Function ListaRigheCertificazioneRaggruppate(ByVal IdCertificazione As Integer) As List(Of RigaAutocertificazioneRaggruppate)

        ' Certificati adesione
        Public MustOverride Function CreaNuovoCertificato(ByVal Entity As Certificato) As Integer
        Public MustOverride Function CaricaCertificato(ByVal IdCertificato As Integer) As Certificato
        Public MustOverride Function CaricaCertificato2(ByVal IdProduttore As Integer, ByVal Anno As Integer) As Certificato
        Public MustOverride Function AggiornaCertificato(ByVal Entity As Certificato) As Boolean

        ' Log iscrizione categoria 4.14
        Public MustOverride Function CreaNuovoLogIscrizione(ByVal Entity As LogIscrizione) As Integer
        Public MustOverride Function CaricaLogIscrizione(ByVal Id As Integer) As LogIscrizione
        Public MustOverride Function CaricaLogIscrizione2(ByVal IdProduttore As Integer) As LogIscrizione
        Public MustOverride Function AggiornaLogIscrizione(ByVal Entity As LogIscrizione) As Boolean


    End Class

End Namespace
