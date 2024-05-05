Imports System
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Web.UI.WebControls
Imports ASPNET.StarterKit.BusinessLogicLayer

Namespace ASPNET.StarterKit.DataAccessLayer

    Public Class SQLDataAccess
        Inherits DataAccess

        Private Delegate Sub TGenerateListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef tempList As List(Of T))

        ' BASE CLASS IMPLEMENTATION

        ' PRODUTTORE
        Private Const SP_PRODUTTORE_CREA As String = "sp_Produttore_Crea"
        Private Const SP_PRODUTTORE_CARICADAID As String = "sp_Produttore_Carica"
        Private Const SP_PRODUTTORE_AGGIORNA As String = "sp_Produttore_Aggiorna"
        Private Const SP_PRODUTTORE_TOTALI As String = "sp_Produttore_Totali"
        Private Const SP_PRODUTTORE_ELIMINA As String = "sp_Produttore_Elimina"
        Private Const SP_PRODUTTORE_VERIFICA As String = "sp_Produttore_VerificaPannelli"
        Private Const SP_PRODUTTORE_GETLASTNRCOMUNICAZIONE As String = "sp_Produttore_GetLastNrComunicazione"
        Private Const SP_PRODUTTORE_LISTA As String = "sp_Produttore_Lista"
        Private Const SP_PRODUTTORE_VERIFICADICHIARAZIONI As String = "sp_Dichiarazione_Verifica"
        Private Const SP_PRODUTTORE_CONTADICHIARAZIONI As String = "sp_Dichiarazione_Conta"
        Private Const SP_PRODUTTORE_CERTIFICADICHIARAZIONI As String = "sp_Dichiarazione_Certifica"
        Private Const SP_PRODUTTORE_SOLOPROFESSIONALE As String = "sp_Produttore_SoloProfessionale"
        Private Const SP_PRODUTTORE_ESISTEAEE As String = "sp_Produttore_EsisteAEE"
        Private Const SP_PRODUTTORE_ESISTEPILE As String = "sp_Produttore_EsistePILE"
        Private Const SP_PRODUTTORE_ESISTEINDUSTRIAL As String = "sp_Produttore_EsisteINDUSTRIAL"
        Private Const SP_PRODUTTORE_ESISTEVEICOLI As String = "sp_Produttore_EsisteVEICOLI"
        Private Const SP_PRODUTTORE_CONTADICHIARAZIONIMANCANTI As String = "sp_Produttore_ContaDichiarazioniMancanti"

        Public Overrides Function CreaNuovoProduttore(ByVal newEntity As Produttore) As Integer

            If newEntity Is Nothing Then
                Throw New ArgumentNullException("newEntity")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Codice", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.Codice)
            AddParamToSQLCmd(sqlCmd, "@RagioneSociale", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.RagioneSociale)
            AddParamToSQLCmd(sqlCmd, "@Email", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.Email)
            AddParamToSQLCmd(sqlCmd, "@Periodicita", SqlDbType.Int, 0, ParameterDirection.Input, newEntity.Periodicita)
            AddParamToSQLCmd(sqlCmd, "@Attivo", SqlDbType.Bit, 0, ParameterDirection.Input, newEntity.Attivo)
            AddParamToSQLCmd(sqlCmd, "@Indirizzo", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.Indirizzo)
            AddParamToSQLCmd(sqlCmd, "@Cap", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.CAP)
            AddParamToSQLCmd(sqlCmd, "@Citta", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.Citta)
            AddParamToSQLCmd(sqlCmd, "@Rappresentante", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.Rappresentante)
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 65000, ParameterDirection.Input, newEntity.Note)
            AddParamToSQLCmd(sqlCmd, "@EmailNotifiche", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.EmailNotifiche)
            AddParamToSQLCmd(sqlCmd, "@CodiceFiscale", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.CodiceFiscale)
            AddParamToSQLCmd(sqlCmd, "@Domestico", SqlDbType.Bit, 0, ParameterDirection.Input, newEntity.Domestico)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, newEntity.Professionale)
            AddParamToSQLCmd(sqlCmd, "@EscludiMassivo", SqlDbType.Bit, 0, ParameterDirection.Input, newEntity.EscludiMassivo)
            AddParamToSQLCmd(sqlCmd, "@MeseAdesione", SqlDbType.Int, 0, ParameterDirection.Input, newEntity.MeseAdesione)
            AddParamToSQLCmd(sqlCmd, "@QuotaConsortile", SqlDbType.Decimal, 0, ParameterDirection.Input, newEntity.QuotaConsortile)
            AddParamToSQLCmd(sqlCmd, "@CodiceSDI", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.CodiceSDI)
            AddParamToSQLCmd(sqlCmd, "@PartitaIVA", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.PartitaIVA)
            AddParamToSQLCmd(sqlCmd, "@PEC", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.PEC)
            AddParamToSQLCmd(sqlCmd, "@IBAN", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.IBAN)
            AddParamToSQLCmd(sqlCmd, "@Telefono", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.Telefono)
            AddParamToSQLCmd(sqlCmd, "@RegistroAEE", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.RegistroAEE)
            AddParamToSQLCmd(sqlCmd, "@RegistroPile", SqlDbType.NVarChar, 255, ParameterDirection.Input, newEntity.RegistroPile)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Decimal, 255, ParameterDirection.Input, newEntity.CostoMatricola)
            AddParamToSQLCmd(sqlCmd, "@ServizioRappresentanza", SqlDbType.Decimal, 255, ParameterDirection.Input, newEntity.ServizioDiRappresentanza)
            AddParamToSQLCmd(sqlCmd, "@Sconto", SqlDbType.Decimal, 255, ParameterDirection.Input, newEntity.Sconto)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)


        End Function

        Public Overrides Function CaricaProduttore(ByVal IdProduttore As Integer) As Produttore

            If IdProduttore <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("IdProduttore")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_CARICADAID)

            Dim ProduttoreList As New List(Of Produttore)()
            TExecuteReaderCmd(Of Produttore)(sqlCmd, AddressOf TGenerateProduttoreListFromReader(Of Produttore), ProduttoreList)

            If ProduttoreList.Count > 0 Then
                Return ProduttoreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaProduttore(ByVal Entity As Produttore) As Boolean

            If Entity Is Nothing Then
                Throw New ArgumentNullException("Entity")
            End If

            If Entity.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("Entity.Id")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Codice", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.Codice)
            AddParamToSQLCmd(sqlCmd, "@RagioneSociale", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.RagioneSociale)
            AddParamToSQLCmd(sqlCmd, "@EMail", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.Email)
            AddParamToSQLCmd(sqlCmd, "@Periodicita", SqlDbType.Int, 0, ParameterDirection.Input, Entity.Periodicita)
            AddParamToSQLCmd(sqlCmd, "@Attivo", SqlDbType.Bit, 0, ParameterDirection.Input, Entity.Attivo)
            AddParamToSQLCmd(sqlCmd, "@Indirizzo", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.Indirizzo)
            AddParamToSQLCmd(sqlCmd, "@Cap", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.CAP)
            AddParamToSQLCmd(sqlCmd, "@Citta", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.Citta)
            AddParamToSQLCmd(sqlCmd, "@Rappresentante", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.Rappresentante)
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 65000, ParameterDirection.Input, Entity.Note)
            AddParamToSQLCmd(sqlCmd, "@EmailNotifiche", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.EmailNotifiche)
            AddParamToSQLCmd(sqlCmd, "@CodiceFiscale", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.CodiceFiscale)
            AddParamToSQLCmd(sqlCmd, "@Domestico", SqlDbType.Bit, 0, ParameterDirection.Input, Entity.Domestico)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, Entity.Professionale)
            AddParamToSQLCmd(sqlCmd, "@EscludiMassivo", SqlDbType.Bit, 0, ParameterDirection.Input, Entity.EscludiMassivo)
            AddParamToSQLCmd(sqlCmd, "@MeseAdesione", SqlDbType.Int, 0, ParameterDirection.Input, Entity.MeseAdesione)
            AddParamToSQLCmd(sqlCmd, "@QuotaConsortile", SqlDbType.Decimal, 0, ParameterDirection.Input, Entity.QuotaConsortile)
            AddParamToSQLCmd(sqlCmd, "@CodiceSDI", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.CodiceSDI)
            AddParamToSQLCmd(sqlCmd, "@PartitaIVA", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.PartitaIVA)
            AddParamToSQLCmd(sqlCmd, "@PEC", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.PEC)
            AddParamToSQLCmd(sqlCmd, "@IBAN", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.IBAN)
            AddParamToSQLCmd(sqlCmd, "@Telefono", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.Telefono)
            AddParamToSQLCmd(sqlCmd, "@RegistroAEE", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.RegistroAEE)
            AddParamToSQLCmd(sqlCmd, "@RegistroPile", SqlDbType.NVarChar, 255, ParameterDirection.Input, Entity.RegistroPile)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Decimal, 0, ParameterDirection.Input, Entity.CostoMatricola)
            AddParamToSQLCmd(sqlCmd, "@servizioRappresentanza", SqlDbType.Decimal, 0, ParameterDirection.Input, Entity.ServizioDiRappresentanza)
            AddParamToSQLCmd(sqlCmd, "@Sconto", SqlDbType.Decimal, 0, ParameterDirection.Input, Entity.Sconto)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Entity.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function TotaleProduttori() As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_TOTALI)

            Dim ProduttoreList As New List(Of Produttore)()
            TExecuteReaderCmd(Of Produttore)(sqlCmd, AddressOf TGenerateProduttoreListFromReader(Of Produttore), ProduttoreList)

            If ProduttoreList.Count > 0 Then
                Return ProduttoreList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function EliminaProduttore(ByVal produttoreDaEliminare As Produttore) As Boolean

            If produttoreDaEliminare Is Nothing Then
                Throw New ArgumentNullException("ProduttoreDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, produttoreDaEliminare.Id)


            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function VerificaPannelli(ByVal produttoreDaVerificare As Produttore) As Boolean

            If produttoreDaVerificare Is Nothing Then
                Throw New ArgumentNullException("produttoreDaVerificare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, produttoreDaVerificare.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_VERIFICA)

            Dim pannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), pannelloList)

            If pannelloList.Count > 0 Then
                Return True
            Else
                Return False
            End If

            Return True

        End Function

        Public Overrides Function VerificaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Boolean

            If Anno = 0 Then
                Throw New ArgumentNullException("Anno errato")
            End If

            If IdProduttore = 0 Then
                Throw New ArgumentNullException("Codice produttore assente")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_VERIFICADICHIARAZIONI)

            Dim dichiarazioniList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), dichiarazioniList)

            If dichiarazioniList.Count > 0 Then
                Return True
            Else
                Return False
            End If

            Return True

        End Function

        Public Overrides Function CertificaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Integer

            If Anno = 0 Then
                Throw New ArgumentNullException("Anno errato")
            End If

            If IdProduttore = 0 Then
                Throw New ArgumentNullException("Codice produttore assente")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@RowCount", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_CERTIFICADICHIARAZIONI)
            ExecuteScalarCmd(sqlCmd)

            Return CInt(sqlCmd.Parameters("@RowCount").Value)

        End Function

        Public Overrides Function ContaDichiarazioni(ByVal Anno As Integer, ByVal IdProduttore As Integer) As Integer

            If Anno = 0 Then
                Throw New ArgumentNullException("Anno errato")
            End If

            If IdProduttore = 0 Then
                Throw New ArgumentNullException("Codice produttore assente")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_CONTADICHIARAZIONI)

            Dim dichiarazioniList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), dichiarazioniList)

            If dichiarazioniList.Count > 0 Then
                Return dichiarazioniList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function GetLastNrComunicazione(ByVal Produttore As Produttore, Anno As Integer) As Integer

            Dim LastId As Integer
            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, Produttore.Id)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_GETLASTNRCOMUNICAZIONE)

            LastId = ExecuteScalarCmd2(sqlCmd)

            If LastId > 0 Then
                Return LastId
            Else
                Return 0
            End If

        End Function

        Public Overrides Function ListaProduttore() As List(Of Produttore)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_LISTA)

            Dim ProduttoreList As New List(Of Produttore)()
            TExecuteReaderCmd(Of Produttore)(sqlCmd, AddressOf TGenerateProduttoreListFromReader(Of Produttore), ProduttoreList)

            If ProduttoreList.Count > 0 Then
                Return ProduttoreList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function SoloProfessionale(ByVal MyProduttore As Produttore) As Boolean

            If MyProduttore Is Nothing Then
                Throw New ArgumentNullException("Produttore")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
          
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, MyProduttore.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_SOLOPROFESSIONALE)

            Dim CategorieProfList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), CategorieProfList)

            If CategorieProfList.Count > 0 Then
                Return False
            Else
                Return True
            End If

            Return True

        End Function

        Public Overrides Function EsisteAEE(ByVal IdProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim righePresenti As Integer

            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_ESISTEAEE)

            Dim categorieProduttoreList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categorieProduttoreList)

            righePresenti = categorieProduttoreList.Count

            If righePresenti > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function EsistePILE(ByVal IdProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim righePresenti As Integer

            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_ESISTEPILE)

            Dim categorieProduttoreList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categorieProduttoreList)

            righePresenti = categorieProduttoreList.Count

            If righePresenti > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function EsisteINDUSTRIAL(ByVal IdProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim righePresenti As Integer

            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_ESISTEINDUSTRIAL)

            Dim categorieProduttoreList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categorieProduttoreList)

            righePresenti = categorieProduttoreList.Count

            If righePresenti > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function EsisteVEICOLI(ByVal IdProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim righePresenti As Integer

            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PRODUTTORE_ESISTEVEICOLI)

            Dim categorieProduttoreList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categorieProduttoreList)

            righePresenti = categorieProduttoreList.Count

            If righePresenti > 0 Then
                Return True
            Else
                Return False
            End If

        End Function


        ' PANNELLO
        Private Const SP_PANNELLO_CREA As String = "sp_Pannello_Crea"
        Private Const SP_PANNELLO_CARICADAID As String = "sp_Pannello_Carica"
        Private Const SP_PANNELLO_AGGIORNA As String = "sp_Pannello_Aggiorna"
        Private Const SP_PANNELLO_MATRICOLAEXISTS As String = "sp_Pannello_MatricolaExists"
        Private Const SP_PANNELLO_TOTALE As String = "sp_Pannello_Totale"
        Private Const SP_PANNELLO_TOTALEABBINATI As String = "sp_Pannello_TotaleAbbinati"
        Private Const SP_PANNELLO_TOTALECONFORMI As String = "sp_Pannello_TotaleConformi"
        Private Const SP_PANNELLO_TOTALEDISMESSI As String = "sp_Pannello_TotaleDismessi"
        Private Const SP_PANNELLO_TOTALEPRODUTTORE As String = "sp_Pannello_TotaleProduttore"
        Private Const SP_PANNELLO_TOTALECLIENTE As String = "sp_Pannello_TotaleCliente"
        Private Const SP_PANNELLO_CARICADAMATRICOLA As String = "sp_Pannello_CaricaDaMatricola"
        Private Const SP_PANNELLO_LISTA As String = "sp_Pannello_Lista"
        Private Const SP_PANNELLO_LISTADAMATRICOLA As String = "sp_Pannello_ListaDaMatricola"
        Private Const SP_PANNELLO_ELIMINA As String = "sp_Pannello_Elimina"

        Public Overrides Function CreaNuovoPannello(ByVal nuovoPannello As Pannello) As Integer

            If nuovoPannello Is Nothing Then
                Throw New ArgumentNullException("nuovoPannello")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoPannello.Matricola)
            AddParamToSQLCmd(sqlCmd, "@Modello", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoPannello.Modello)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Float, 0, ParameterDirection.Input, nuovoPannello.Peso)
            AddParamToSQLCmd(sqlCmd, "@DataInizioGaranzia", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoPannello.DataInizioGaranzia)
            AddParamToSQLCmd(sqlCmd, "@IdMarca", SqlDbType.Int, 0, ParameterDirection.Input, nuovoPannello.IdMarca)
            AddParamToSQLCmd(sqlCmd, "@Produttore", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoPannello.Produttore)
            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, nuovoPannello.IdImpianto)
            AddParamToSQLCmd(sqlCmd, "@DataCaricamento", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoPannello.DataCaricamento)
            AddParamToSQLCmd(sqlCmd, "@Conforme", SqlDbType.Bit, 0, ParameterDirection.Input, nuovoPannello.Conforme)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovoPannello.Protocollo)
            AddParamToSQLCmd(sqlCmd, "@NrComunicazione", SqlDbType.Int, 0, ParameterDirection.Input, nuovoPannello.NrComunicazione)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, nuovoPannello.Anno)
            AddParamToSQLCmd(sqlCmd, "@DataConformita", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoPannello.DataConformita)
            AddParamToSQLCmd(sqlCmd, "@Dismesso", SqlDbType.Bit, 0, ParameterDirection.Input, nuovoPannello.Dismesso)
            AddParamToSQLCmd(sqlCmd, "@IdFasciaDiPeso", SqlDbType.Int, 0, ParameterDirection.Input, nuovoPannello.IdFasciaDiPeso)
            AddParamToSQLCmd(sqlCmd, "@IdTipologiaCella", SqlDbType.Int, 0, ParameterDirection.Input, nuovoPannello.IdTipologiaCella)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Money, 0, ParameterDirection.Input, nuovoPannello.CostoMatricola)
            AddParamToSQLCmd(sqlCmd, "@DataRitiro", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoPannello.DataRitiro)
            AddParamToSQLCmd(sqlCmd, "@NumeroFIR", SqlDbType.Text, 255, ParameterDirection.Input, nuovoPannello.NumeroFIR)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)


        End Function

        Public Overrides Function CaricaPannello(ByVal IdPannello As Integer) As Pannello

            If IdPannello <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("IdPannello")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, IdPannello)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_CARICADAID)

            Dim PannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function EliminaPannello(ByVal pannelloToDelete As Pannello) As Boolean

            If pannelloToDelete Is Nothing Then
                Throw New ArgumentNullException("pannelloToDelete")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, pannelloToDelete.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function AggiornaPannello(ByVal pannelloDaAggiornare As Pannello) As Boolean

            If pannelloDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("pannelloDaAggiornare")
            End If

            If pannelloDaAggiornare.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("pannelloDaAggiornare.Id")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 255, ParameterDirection.Input, pannelloDaAggiornare.Matricola)
            AddParamToSQLCmd(sqlCmd, "@Modello", SqlDbType.NVarChar, 255, ParameterDirection.Input, pannelloDaAggiornare.Modello)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Float, 0, ParameterDirection.Input, pannelloDaAggiornare.Peso)
            AddParamToSQLCmd(sqlCmd, "@DataInizioGaranzia", SqlDbType.DateTime, 0, ParameterDirection.Input, pannelloDaAggiornare.DataInizioGaranzia)
            AddParamToSQLCmd(sqlCmd, "@IdMarca", SqlDbType.Int, 0, ParameterDirection.Input, pannelloDaAggiornare.IdMarca)
            AddParamToSQLCmd(sqlCmd, "@Produttore", SqlDbType.NVarChar, 255, ParameterDirection.Input, pannelloDaAggiornare.Produttore)
            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, pannelloDaAggiornare.IdImpianto)
            AddParamToSQLCmd(sqlCmd, "@DataCaricamento", SqlDbType.DateTime, 0, ParameterDirection.Input, pannelloDaAggiornare.DataCaricamento)
            AddParamToSQLCmd(sqlCmd, "@Conforme", SqlDbType.Bit, 0, ParameterDirection.Input, pannelloDaAggiornare.Conforme)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 256, ParameterDirection.Input, pannelloDaAggiornare.Protocollo)
            AddParamToSQLCmd(sqlCmd, "@NrComunicazione", SqlDbType.Int, 0, ParameterDirection.Input, pannelloDaAggiornare.NrComunicazione)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, pannelloDaAggiornare.Anno)
            AddParamToSQLCmd(sqlCmd, "@DataConformita", SqlDbType.DateTime, 0, ParameterDirection.Input, pannelloDaAggiornare.DataConformita)
            AddParamToSQLCmd(sqlCmd, "@Dismesso", SqlDbType.Bit, 0, ParameterDirection.Input, pannelloDaAggiornare.Dismesso)
            AddParamToSQLCmd(sqlCmd, "@IdFasciaDiPeso", SqlDbType.Int, 0, ParameterDirection.Input, pannelloDaAggiornare.IdFasciaDiPeso)
            AddParamToSQLCmd(sqlCmd, "@IdTipologiaCella", SqlDbType.Int, 0, ParameterDirection.Input, pannelloDaAggiornare.IdTipologiaCella)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Money, 0, ParameterDirection.Input, pannelloDaAggiornare.CostoMatricola)
            AddParamToSQLCmd(sqlCmd, "@DataRitiro", SqlDbType.DateTime, 0, ParameterDirection.Input, pannelloDaAggiornare.DataRitiro)
            AddParamToSQLCmd(sqlCmd, "@NumeroFIR", SqlDbType.Text, 255, ParameterDirection.Input, pannelloDaAggiornare.NumeroFIR)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, pannelloDaAggiornare.Id)


            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function

        Public Overrides Function MatricolaExists(ByVal IdMarca As Integer, Matricola As String) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdMarca", SqlDbType.Int, 0, ParameterDirection.Input, IdMarca)
            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 256, ParameterDirection.Input, Matricola)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_MATRICOLAEXISTS)

            Dim PannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function TotalePannelli() As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_TOTALE)

            Dim PannelloList As New List(Of Pannello)()
            'TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)
            Return ExecuteScalarCmd2(sqlCmd)

            'If PannelloList.Count > 0 Then
            '    Return PannelloList.Count
            'Else
            '    Return 0
            'End If

        End Function

        Public Overrides Function TotaleAbbinati() As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_TOTALEABBINATI)

            'Dim PannelloList As New List(Of Pannello)()
            'TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            Return ExecuteScalarCmd2(sqlCmd)
            'If PannelloList.Count > 0 Then
            '    Return PannelloList.Count
            'Else
            '    Return 0
            'End If

        End Function

        Public Overrides Function TotaleConformi() As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_TOTALECONFORMI)

            'Dim PannelloList As New List(Of Pannello)()
            'TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            Return ExecuteScalarCmd2(sqlCmd)
            'If PannelloList.Count > 0 Then
            '    Return PannelloList.Count
            'Else
            '    Return 0
            'End If

        End Function

        Public Overrides Function TotaleDismessi() As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_TOTALEDISMESSI)
            Return ExecuteScalarCmd2(sqlCmd)
            'Dim PannelloList As New List(Of Pannello)()
            'TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            'If PannelloList.Count > 0 Then
            '    Return PannelloList.Count
            'Else
            '    Return 0
            'End If

        End Function

        Public Overrides Function TotalePannelliProduttore(IdProduttore As Integer) As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_TOTALEPRODUTTORE)

            Dim PannelloList As New List(Of Pannello)()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function TotalePannelliCliente(IdCliente As Integer) As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_TOTALECLIENTE)

            Dim PannelloList As New List(Of Pannello)()
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, IdCliente)
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function CaricaPannelloDaMatricola(ByVal Matricola As String, CodiceProduttore As String) As Pannello


            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 256, ParameterDirection.Input, Matricola)
            AddParamToSQLCmd(sqlCmd, "@CodiceProduttore", SqlDbType.NVarChar, 256, ParameterDirection.Input, CodiceProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_CARICADAMATRICOLA)

            Dim PannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaPannelli(ByVal IdImpianto As Integer) As List(Of Pannello)

            If IdImpianto <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("IdImpianto")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, IdImpianto)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_LISTA)

            Dim PannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaPannelli(ByVal Matricola As String) As List(Of Pannello)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 255, ParameterDirection.Input, Matricola)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PANNELLO_LISTADAMATRICOLA)

            Dim PannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList
            Else
                Return Nothing
            End If

        End Function

        ' CLIENTE
        Private Const SP_CLIENTE_CREA As String = "sp_Cliente_Crea"
        Private Const SP_CLIENTE_CARICADA As String = "sp_Cliente_Carica"
        Private Const SP_CLIENTE_AGGIORNA As String = "sp_Cliente_Aggiorna"
        Private Const SP_CLIENTE_ELIMINA As String = "sp_Cliente_Elimina"
        Private Const SP_CLIENTE_VERIFICA As String = "sp_Cliente_VerificaImpianti"

        Public Overrides Function CreaNuovoCliente(ByVal nuovoCliente As Cliente) As Integer

            If nuovoCliente Is Nothing Then
                Throw New ArgumentNullException("nuovoCliente")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@RagioneSociale", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.RagioneSociale)
            AddParamToSQLCmd(sqlCmd, "@Indirizzo", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Indirizzo)
            AddParamToSQLCmd(sqlCmd, "@Cap", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Cap)
            AddParamToSQLCmd(sqlCmd, "@Citta", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Citta)
            AddParamToSQLCmd(sqlCmd, "@Provincia", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Provincia)
            AddParamToSQLCmd(sqlCmd, "@Telefono", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Telefono)
            AddParamToSQLCmd(sqlCmd, "@Fax", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Fax)
            AddParamToSQLCmd(sqlCmd, "@Email", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Email)
            AddParamToSQLCmd(sqlCmd, "@Contatto", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Contatto)
            AddParamToSQLCmd(sqlCmd, "@Cognome", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Cognome)
            AddParamToSQLCmd(sqlCmd, "@Nome", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.Nome)
            AddParamToSQLCmd(sqlCmd, "@CodiceFiscale", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoCliente.CodiceFiscale)
            AddParamToSQLCmd(sqlCmd, "@Periodicita", SqlDbType.Int, 0, ParameterDirection.Input, nuovoCliente.Periodicita)
            AddParamToSQLCmd(sqlCmd, "@Attivo", SqlDbType.Int, 0, ParameterDirection.Input, nuovoCliente.Attivo)
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 65000, ParameterDirection.Input, nuovoCliente.Note)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CLIENTE_CREA)
            ExecuteScalarCmd(sqlCmd)

            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaCliente(ByVal IdCliente As Integer) As Cliente

            If IdCliente <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("IdCliente")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, IdCliente)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CLIENTE_CARICADA)

            Dim ClienteList As New List(Of Cliente)()
            TExecuteReaderCmd(Of Cliente)(sqlCmd, AddressOf TGenerateClienteListFromReader(Of Cliente), ClienteList)

            If ClienteList.Count > 0 Then
                Return ClienteList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaCliente(ByVal ClienteDaAggiornare As Cliente) As Boolean

            If ClienteDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("ClienteDaAggiornare")
            End If

            If ClienteDaAggiornare.IdCliente <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("ClienteDaAggiornare.IdCliente")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@RagioneSociale", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.RagioneSociale)
            AddParamToSQLCmd(sqlCmd, "@Indirizzo", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Indirizzo)
            AddParamToSQLCmd(sqlCmd, "@Cap", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Cap)
            AddParamToSQLCmd(sqlCmd, "@Citta", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Citta)
            AddParamToSQLCmd(sqlCmd, "@Provincia", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Provincia)
            AddParamToSQLCmd(sqlCmd, "@Email", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Email)
            AddParamToSQLCmd(sqlCmd, "@Telefono", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Telefono)
            AddParamToSQLCmd(sqlCmd, "@Fax", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Fax)
            AddParamToSQLCmd(sqlCmd, "@PartitaIva", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.PartitaIva)
            AddParamToSQLCmd(sqlCmd, "@Contatto", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Contatto)
            AddParamToSQLCmd(sqlCmd, "@Cognome", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Cognome)
            AddParamToSQLCmd(sqlCmd, "@Nome", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.Nome)
            AddParamToSQLCmd(sqlCmd, "@CodiceFiscale", SqlDbType.NVarChar, 255, ParameterDirection.Input, ClienteDaAggiornare.CodiceFiscale)
            AddParamToSQLCmd(sqlCmd, "@Periodicita", SqlDbType.Int, 255, ParameterDirection.Input, ClienteDaAggiornare.Periodicita)
            AddParamToSQLCmd(sqlCmd, "@Attivo", SqlDbType.Int, 255, ParameterDirection.Input, ClienteDaAggiornare.Attivo)
            AddParamToSQLCmd(sqlCmd, "@Note", SqlDbType.NVarChar, 65000, ParameterDirection.Input, ClienteDaAggiornare.Note)

            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, ClienteDaAggiornare.IdCliente)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CLIENTE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaCliente(ByVal ClienteDaEliminare As Cliente) As Boolean

            If ClienteDaEliminare Is Nothing Then
                Throw New ArgumentNullException("ClienteDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, ClienteDaEliminare.IdCliente)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CLIENTE_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function VerificaImpianti(ByVal ClienteDaVerificare As Cliente) As Boolean

            If ClienteDaVerificare Is Nothing Then
                Throw New ArgumentNullException("ClienteDaVerificare")
            End If
            ' Questo

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, ClienteDaVerificare.IdCliente)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CLIENTE_VERIFICA)

            Dim impiantoList As New List(Of Impianto)()
            TExecuteReaderCmd(Of Impianto)(sqlCmd, AddressOf TGenerateImpiantoListFromReader(Of Pannello), impiantoList)

            If impiantoList.Count > 0 Then
                Return True
            Else
                Return False
            End If

            Return True

        End Function


        ' IMPIANTO
        Private Const SP_IMPIANTO_CREA As String = "sp_Impianto_Crea"
        Private Const SP_IMPIANTO_CARICADA As String = "sp_Impianto_Carica"
        Private Const SP_IMPIANTO_AGGIORNA As String = "sp_Impianto_Aggiorna"
        Private Const SP_UTENTECLIENTE_ELIMINA As String = "sp_UtenteCliente_Elimina"
        Private Const SP_IMPIANTO_TOTALE As String = "sp_Impianto_Totale"
        Private Const SP_IMPIANTO_TOTALECLIENTE As String = "sp_Impianto_TotaleCliente"
        Private Const SP_IMPIANTO_VERIFICA As String = "sp_Impianto_Verifica"
        Private Const SP_IMPIANTO_CONTROLLA As String = "sp_Impianto_Controlla"
        Private Const SP_IMPIANTO_CARICADAIDCLIENTE As String = "sp_Impianto_CaricaDaIdCliente"
        Private Const SP_IMPIANTO_ELIMINA As String = "sp_Impianto_Elimina"
        Private Const SP_IMPIANTO_DISABBINA As String = "sp_Impianto_Disabbina"
        Private Const SP_IMPIANTO_VALORE As String = "sp_Impianto_Valore"
        Private Const SP_IMPIANTO_LISTAPROTOCOLLI As String = "sp_Impianto_ListaProtocolli"
        Private Const SP_IMPIANTO_LISTAPRODUTTORI As String = "sp_Impianto_ListaProduttori"
        Private Const SP_IMPIANTO_TOTALEMATRICOLE As String = "sp_Impianto_TotaleMatricole"

        Public Overrides Function CreaNuovoImpianto(ByVal nuovoImpianto As Impianto) As Integer

            If nuovoImpianto Is Nothing Then
                Throw New ArgumentNullException("nuovoImpianto")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Codice", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Codice)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@Indirizzo", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Indirizzo)
            AddParamToSQLCmd(sqlCmd, "@Cap", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Cap)
            AddParamToSQLCmd(sqlCmd, "@Citta", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Citta)
            AddParamToSQLCmd(sqlCmd, "@Provincia", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Provincia)
            AddParamToSQLCmd(sqlCmd, "@Latitudine", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Latitudine)
            AddParamToSQLCmd(sqlCmd, "@Longitudine", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Longitudine)
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, nuovoImpianto.IdCliente)
            AddParamToSQLCmd(sqlCmd, "@DataCreazione", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoImpianto.DataCreazione)
            AddParamToSQLCmd(sqlCmd, "@Responsabile", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Responsabile)
            AddParamToSQLCmd(sqlCmd, "@NrPraticaGSE", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.NrPraticaGSE)
            AddParamToSQLCmd(sqlCmd, "@Regione", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Regione)
            AddParamToSQLCmd(sqlCmd, "@ContoEnergia", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.ContoEnergia)
            AddParamToSQLCmd(sqlCmd, "@DataEntrataInEsercizio", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoImpianto.DataEntrataInEsercizio)
            AddParamToSQLCmd(sqlCmd, "@Attestato", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.Attestato)
            AddParamToSQLCmd(sqlCmd, "@DataAttestato", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoImpianto.DataAttestato)
            AddParamToSQLCmd(sqlCmd, "@NrAttestato", SqlDbType.Int, 0, ParameterDirection.Input, nuovoImpianto.NrAttestato)
            AddParamToSQLCmd(sqlCmd, "@NomeProduttore", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoImpianto.NomeProduttore)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaImpianto(ByVal IdImpianto As Integer) As Impianto

            If IdImpianto <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("IdImpianto")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, IdImpianto)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_CARICADA)

            Dim impiantoList As New List(Of Impianto)()

            TExecuteReaderCmd(Of Impianto)(sqlCmd, AddressOf TGenerateImpiantoListFromReader(Of Impianto), impiantoList)

            If impiantoList.Count > 0 Then
                Return impiantoList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaImpianto(ByVal impiantoDaAggiornare As Impianto) As Boolean

            If impiantoDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("impiantoDaAggiornare")
            End If

            If impiantoDaAggiornare.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("impiantoDaAggiornare.ID")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Codice", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Codice)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@Indirizzo", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Indirizzo)
            AddParamToSQLCmd(sqlCmd, "@Cap", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Cap)
            AddParamToSQLCmd(sqlCmd, "@Citta", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Citta)
            AddParamToSQLCmd(sqlCmd, "@Provincia", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Provincia)
            AddParamToSQLCmd(sqlCmd, "@Latitudine", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Latitudine)
            AddParamToSQLCmd(sqlCmd, "@Longitudine", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Longitudine)
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, impiantoDaAggiornare.IdCliente)
            AddParamToSQLCmd(sqlCmd, "@DataCreazione", SqlDbType.DateTime, 255, ParameterDirection.Input, impiantoDaAggiornare.DataCreazione)
            AddParamToSQLCmd(sqlCmd, "@Responsabile", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Responsabile)
            AddParamToSQLCmd(sqlCmd, "@NrPraticaGSE", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.NrPraticaGSE)
            AddParamToSQLCmd(sqlCmd, "@Regione", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Regione)
            AddParamToSQLCmd(sqlCmd, "@ContoEnergia", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.ContoEnergia)
            AddParamToSQLCmd(sqlCmd, "@DataEntrataInEsercizio", SqlDbType.DateTime, 0, ParameterDirection.Input, impiantoDaAggiornare.DataEntrataInEsercizio)
            AddParamToSQLCmd(sqlCmd, "@Attestato", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.Attestato)
            AddParamToSQLCmd(sqlCmd, "@DataAttestato", SqlDbType.DateTime, 0, ParameterDirection.Input, impiantoDaAggiornare.DataAttestato)
            AddParamToSQLCmd(sqlCmd, "@NrAttestato", SqlDbType.Int, 0, ParameterDirection.Input, impiantoDaAggiornare.NrAttestato)
            AddParamToSQLCmd(sqlCmd, "@NomeProduttore", SqlDbType.NVarChar, 255, ParameterDirection.Input, impiantoDaAggiornare.NomeProduttore)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, impiantoDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function GeneraAttestato(ByVal impiantoDaAggiornare As Impianto) As Boolean

            Dim NrAttestatoOdierno As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.DateTime, 0, ParameterDirection.Input, DateTime.Today)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, "SP_UTILITY_ULTIMONRATTESTATO")
            NrAttestatoOdierno = ExecuteScalarCmd2(sqlCmd)

            NrAttestatoOdierno = NrAttestatoOdierno + 1

            impiantoDaAggiornare.DataAttestato = Today
            impiantoDaAggiornare.NrAttestato = NrAttestatoOdierno
            impiantoDaAggiornare.Attestato = impiantoDaAggiornare.DataAttestato.ToString("ddMMyyyy") + "-" + NrAttestatoOdierno.ToString.PadLeft(3, "0")
            impiantoDaAggiornare.Save()

            Return True

        End Function

        Public Overrides Function CaricaImpiantoDaIdCliente(ByVal IdCliente As Integer) As List(Of Impianto)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, IdCliente)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_CARICADAIDCLIENTE)

            Dim impiantoList As New List(Of Impianto)()

            TExecuteReaderCmd(Of Impianto)(sqlCmd, AddressOf TGenerateImpiantoListFromReader(Of Impianto), impiantoList)

            If impiantoList.Count > 0 Then
                Return impiantoList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function TotaleImpianti() As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_TOTALE)

            Dim ImpiantoList As New List(Of Impianto)()
            TExecuteReaderCmd(Of Impianto)(sqlCmd, AddressOf TGenerateImpiantoListFromReader(Of Impianto), ImpiantoList)

            If ImpiantoList.Count > 0 Then
                Return ImpiantoList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function TotaleImpianti(IdCliente As Integer) As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, IdCliente)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_TOTALECLIENTE)

            Dim ImpiantoList As New List(Of Impianto)()
            TExecuteReaderCmd(Of Impianto)(sqlCmd, AddressOf TGenerateImpiantoListFromReader(Of Impianto), ImpiantoList)

            If ImpiantoList.Count > 0 Then
                Return ImpiantoList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function VerificaImpianto(ByVal impiantoDaVerificare As Impianto) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, impiantoDaVerificare.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_VERIFICA)

            Dim pannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), pannelloList)

            If pannelloList.Count > 0 Then
                Return False
            Else
                Return True
            End If

        End Function

        Public Overrides Function ControllaImpianto(ByVal impiantoDaControllare As Impianto) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, impiantoDaControllare.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_CONTROLLA)

            Dim pannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), pannelloList)

            If pannelloList.Count > 0 Then
                Return False
            Else
                Return True
            End If

        End Function

        Public Overrides Function EliminaImpianto(ByVal ImpiantoDaEliminare As Impianto) As Boolean

            If ImpiantoDaEliminare Is Nothing Then
                Throw New ArgumentNullException("ImpiantoDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, ImpiantoDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function DisabbinaImpianto(ByVal ImpiantoDaDisabbinare As Impianto) As Boolean

            If ImpiantoDaDisabbinare Is Nothing Then
                Throw New ArgumentNullException("ImpiantoDaDisabbinare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, ImpiantoDaDisabbinare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_DISABBINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function ValoreImpianto(ByVal impianto As Impianto) As Decimal

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim Valore As Object

            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, impianto.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_Impianto_Valore")

            Valore = ExecuteScalarCmd2(sqlCmd)

            If Valore Is DBNull.Value Then
                Return 0
            Else
                Return CDec(Valore)
            End If

        End Function

        Public Overrides Function TotaleMatricole(ByVal impianto As Impianto) As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim Valore As Object

            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, impianto.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_Impianto_TotaleMatricole")

            Valore = ExecuteScalarCmd2(sqlCmd)

            If Valore Is DBNull.Value Then
                Return 0
            Else
                Return CInt(Valore)
            End If

        End Function

        Public Overrides Function ListaProtocolliImpianto(ByVal impianto As Impianto) As List(Of String)

            If impianto Is Nothing Then
                Throw New ArgumentNullException("Impianto")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, impianto.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_LISTAPROTOCOLLI)

            Dim ListaProtocolli As New List(Of String)()
            TExecuteReaderCmd(Of String)(sqlCmd, AddressOf TGenerateListOfProtocolliFromReader(Of String), ListaProtocolli)

            If ListaProtocolli.Count > 0 Then
                Return ListaProtocolli
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaProduttoriImpianto(ByVal impianto As Impianto) As List(Of String)

            If impianto Is Nothing Then
                Throw New ArgumentNullException("Impianto")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, impianto.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_LISTAPRODUTTORI)

            Dim ListaProduttori As New List(Of String)()
            TExecuteReaderCmd(Of String)(sqlCmd, AddressOf TGenerateListOfProduttoriFromReader(Of String), ListaProduttori)

            If ListaProduttori.Count > 0 Then
                Return ListaProduttori
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaImpianti(ByVal IdCliente As Integer) As List(Of Impianto)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, IdCliente)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_IMPIANTO_CARICADAIDCLIENTE)

            Dim impiantoList As New List(Of Impianto)()

            TExecuteReaderCmd(Of Impianto)(sqlCmd, AddressOf TGenerateImpiantoListFromReader(Of Impianto), impiantoList)

            If impiantoList.Count > 0 Then
                Return impiantoList
            Else
                Return Nothing
            End If

        End Function


        ' UTENTECLIENTE
        Private Const SP_UTENTECLIENTE_CREA As String = "sp_UtenteCliente_Crea"
        Private Const SP_UTENTECLIENTE_CARICADA As String = "sp_UtenteCliente_Carica"
        Private Const SP_UTENTECLIENTE_AGGIORNA As String = "sp_UtenteCliente_Aggiorna"

        Public Overrides Function CreanuovoUtenteCliente(ByVal nuovoUtenteCliente As UtenteCliente) As Integer

            If nuovoUtenteCliente Is Nothing Then
                Throw New ArgumentNullException("nuovoUtenteCliente")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.UniqueIdentifier, 255, ParameterDirection.Input, nuovoUtenteCliente.UserId)
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.Int, 0, ParameterDirection.Input, nuovoUtenteCliente.IdCliente)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTECLIENTE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaUtenteCliente(ByVal Userid As Guid) As UtenteCliente

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, Userid)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTECLIENTE_CARICADA)

            Dim UtenteClienteList As New List(Of UtenteCliente)()

            TExecuteReaderCmd(Of UtenteCliente)(sqlCmd, AddressOf TGenerateUtenteClienteListFromReader(Of UtenteCliente), UtenteClienteList)

            If UtenteClienteList.Count > 0 Then
                Return UtenteClienteList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaUtenteCliente(ByVal utenteclienteDaAggiornare As UtenteCliente) As Boolean

            If utenteclienteDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("utenteclienteDaAggiornare")
            End If

            If utenteclienteDaAggiornare.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("utenteclienteDaAggiornare.ID")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.UniqueIdentifier, 255, ParameterDirection.Input, utenteclienteDaAggiornare.UserId)
            AddParamToSQLCmd(sqlCmd, "@IdCliente", SqlDbType.NVarChar, 255, ParameterDirection.Input, utenteclienteDaAggiornare.IdCliente)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, utenteclienteDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTECLIENTE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaUtenteCliente(ByVal UtenteClienteDaEliminare As UtenteCliente) As Boolean

            If UtenteClienteDaEliminare Is Nothing Then
                Throw New ArgumentNullException("UtenteClienteDaEliminare")
            End If

            If UtenteClienteDaEliminare.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("UtenteClienteDaEliminare.Id")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, UtenteClienteDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTECLIENTE_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function


        ' UTENTEPRODUTTORE
        Private Const SP_UTENTEPRODUTTORE_CREA As String = "sp_UtenteProduttore_Crea"
        Private Const SP_UTENTEPRODUTTORE_CARICADA As String = "sp_UtenteProduttore_Carica"
        Private Const SP_UTENTEPRODUTTORE_AGGIORNA As String = "sp_UtenteProduttore_Aggiorna"
        Private Const SP_UTENTEPRODUTTORE_ELIMINA As String = "sp_UtenteProduttore_Elimina"

        Public Overrides Function CreanuovoUtenteProduttore(ByVal nuovoUtenteProduttore As UtenteProduttore) As Integer

            If nuovoUtenteProduttore Is Nothing Then
                Throw New ArgumentNullException("nuovoUtenteProduttore")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.UniqueIdentifier, 255, ParameterDirection.Input, nuovoUtenteProduttore.UserId)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovoUtenteProduttore.IdProduttore)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTEPRODUTTORE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaUtenteProduttore(ByVal Userid As Guid) As UtenteProduttore

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, Userid)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTEPRODUTTORE_CARICADA)

            Dim UtenteProduttoreList As New List(Of UtenteProduttore)()

            TExecuteReaderCmd(Of UtenteProduttore)(sqlCmd, AddressOf TGenerateUtenteProduttoreListFromReader(Of UtenteProduttore), UtenteProduttoreList)

            If UtenteProduttoreList.Count > 0 Then
                Return UtenteProduttoreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaUtenteProduttore(ByVal UtenteProduttoreDaAggiornare As UtenteProduttore) As Boolean

            If UtenteProduttoreDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("UtenteProduttoreDaAggiornare")
            End If

            If UtenteProduttoreDaAggiornare.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("UtenteProduttoreDaAggiornare.ID")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.UniqueIdentifier, 255, ParameterDirection.Input, UtenteProduttoreDaAggiornare.UserId)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.NVarChar, 255, ParameterDirection.Input, UtenteProduttoreDaAggiornare.IdProduttore)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, UtenteProduttoreDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTEPRODUTTORE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaUtenteProduttore(ByVal UtenteProduttoreDaEliminare As UtenteProduttore) As Boolean

            If UtenteProduttoreDaEliminare Is Nothing Then
                Throw New ArgumentNullException("UtenteProduttoreDaEliminare ")
            End If

            If UtenteProduttoreDaEliminare.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("UtenteProduttoreDaEliminare.Id")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, UtenteProduttoreDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_UTENTEPRODUTTORE_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        ' PROTOCOLLO
        Private Const SP_PROTOCOLLO_CREA As String = "sp_Protocollo_Crea"
        Private Const SP_PROTOCOLLO_CARICA As String = "sp_Protocollo_Carica"
        Private Const SP_PROTOCOLLO_CARICADAID As String = "sp_Protocollo_CaricaDaID"
        Private Const SP_PROTOCOLLO_AGGIORNA As String = "sp_Protocollo_Aggiorna"
        Private Const SP_PROTOCOLLO_ELIMINA As String = "sp_Protocollo_Elimina"
        Private Const SP_PROTOCOLLO_CONFORME As String = "sp_Protocollo_Conforme"
        Private Const SP_PROTOCOLLO_LISTAMODELLI As String = "sp_Protocollo_ListaModelli"
        Private Const SP_PROTOCOLLO_LISTAMARCHE As String = "sp_Protocollo_ListaMarche"
        Private Const SP_PROTOCOLLO_LISTAPANNELLI As String = "sp_Protocollo_ListaPannelli"
        Private Const SP_PROTOCOLLO_GETPRODUTTORE As String = "sp_Protocollo_GetProduttore"
        Private Const SP_PROTOCOLLO_NRATTESTATI As String = "sp_Protocollo_NrAttestati"
        Private Const SP_PROTOCOLLO_LISTA As String = "sp_Protocollo_Lista"
        Private Const SP_PROTOCOLLO_CONTEGGIOPANNELLI As String = "sp_Protocollo_ConteggioPannelli"


        Public Overrides Function CreanuovoProtocollo(ByVal nuovoProtocollo As Protocollo) As Integer

            If nuovoProtocollo Is Nothing Then
                Throw New ArgumentNullException("nuovoProtocollo")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoProtocollo.Protocollo)
            AddParamToSQLCmd(sqlCmd, "@data", SqlDbType.DateTime, 255, ParameterDirection.Input, nuovoProtocollo.Data)
            AddParamToSQLCmd(sqlCmd, "@NrFattura", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoProtocollo.NrFattura)
            AddParamToSQLCmd(sqlCmd, "@cct", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoProtocollo.CCT)
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoProtocollo.UserName)
            AddParamToSQLCmd(sqlCmd, "@DataAttestato", SqlDbType.DateTime, 255, ParameterDirection.Input, nuovoProtocollo.DataAttestato)
            AddParamToSQLCmd(sqlCmd, "@NrAttestato", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoProtocollo.NrAttestato)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovoProtocollo.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@CostoServizio", SqlDbType.Money, 0, ParameterDirection.Input, nuovoProtocollo.CostoServizio)
            AddParamToSQLCmd(sqlCmd, "@DataFattura", SqlDbType.Date, 0, ParameterDirection.Input, nuovoProtocollo.DataFattura)
            AddParamToSQLCmd(sqlCmd, "@NrProforma", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoProtocollo.NrProforma)
            AddParamToSQLCmd(sqlCmd, "@DataProforma", SqlDbType.Date, 0, ParameterDirection.Input, nuovoProtocollo.DataProforma)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaProtocolloDaId(ByVal Id As Integer) As Protocollo

            If Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("Id")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_CARICADAID)

            Dim ProtocolloList As New List(Of Protocollo)()
            TExecuteReaderCmd(Of Protocollo)(sqlCmd, AddressOf TGenerateProtocolloListFromReader(Of Protocollo), ProtocolloList)

            If ProtocolloList.Count > 0 Then
                Return ProtocolloList(0)
            Else
                Return Nothing
            End If


        End Function

        Public Overrides Function CaricaProtocollo(ByVal Protocollo As String) As Protocollo

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, Protocollo)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_CARICA)

            Dim ProtocolloList As New List(Of Protocollo)()
            TExecuteReaderCmd(Of Protocollo)(sqlCmd, AddressOf TGenerateProtocolloListFromReader(Of Protocollo), ProtocolloList)

            If ProtocolloList.Count > 0 Then
                Return ProtocolloList(0)
            Else
                Return Nothing
            End If


        End Function

        Public Overrides Function AggiornaProtocollo(ByVal protocolloDaAggiornare As Protocollo) As Boolean

            If protocolloDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("protocolloDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaAggiornare.Protocollo)
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.DateTime, 255, ParameterDirection.Input, protocolloDaAggiornare.Data)
            AddParamToSQLCmd(sqlCmd, "@NrFattura", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaAggiornare.NrFattura)
            AddParamToSQLCmd(sqlCmd, "@cct", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaAggiornare.CCT)
            AddParamToSQLCmd(sqlCmd, "@username", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaAggiornare.UserName)
            AddParamToSQLCmd(sqlCmd, "@DataAttestato", SqlDbType.DateTime, 255, ParameterDirection.Input, protocolloDaAggiornare.DataAttestato)
            AddParamToSQLCmd(sqlCmd, "@NrAttestato", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaAggiornare.NrAttestato)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, protocolloDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@CostoServizio", SqlDbType.Money, 0, ParameterDirection.Input, protocolloDaAggiornare.CostoServizio)
            AddParamToSQLCmd(sqlCmd, "@DataFattura", SqlDbType.Date, 0, ParameterDirection.Input, protocolloDaAggiornare.DataFattura)
            AddParamToSQLCmd(sqlCmd, "@NrProforma", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaAggiornare.NrProforma)
            AddParamToSQLCmd(sqlCmd, "@DataProforma", SqlDbType.Date, 0, ParameterDirection.Input, protocolloDaAggiornare.DataProforma)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, protocolloDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaProtocollo(ByVal protocolloDaEliminare As Protocollo) As Boolean

            If protocolloDaEliminare Is Nothing Then
                Throw New ArgumentNullException("protocolloDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaEliminare.Protocollo)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            ' Aggiorna i pannelli abbinati
            Dim sqlCmd2 As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd2, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocolloDaEliminare.Protocollo)
            SetCommandType(sqlCmd2, CommandType.StoredProcedure, "sp_Protocollo_EliminaPannelli")
            ExecuteScalarCmd(sqlCmd2)

            Return True

        End Function

        Public Overrides Function Conforme(ByVal protocollo As Protocollo) As Boolean

            If protocollo Is Nothing Then
                Throw New ArgumentNullException("protocollo")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocollo.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_CONFORME)
            Dim ProtocolloList As New List(Of Protocollo)()
            TExecuteReaderCmd(Of Protocollo)(sqlCmd, AddressOf TGenerateProtocolloListFromReader(Of Protocollo), ProtocolloList)

            If ProtocolloList.Count > 0 Then
                Return False
            Else
                Return True
            End If

        End Function

        Public Overrides Function ListaModelli(ByVal protocollo As Protocollo) As List(Of String)

            If protocollo Is Nothing Then
                Throw New ArgumentNullException("protocollo")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocollo.Protocollo)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_LISTAMODELLI)
            Dim ListaDeiModelli As New List(Of String)()
            TExecuteReaderCmd(Of String)(sqlCmd, AddressOf TGenerateListOfModelliFromReader(Of String), ListaDeiModelli)

            If ListaDeiModelli.Count > 0 Then
                Return ListaDeiModelli
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function GetProduttore(ByVal protocollo As Protocollo) As String

            If protocollo Is Nothing Then
                Throw New ArgumentNullException("protocollo")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocollo.Protocollo)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_GETPRODUTTORE)
            Dim ProduttoreList As New List(Of Produttore)()
            TExecuteReaderCmd(Of Produttore)(sqlCmd, AddressOf TGenerateProduttoreListFromReader(Of String), ProduttoreList)

            If ProduttoreList.Count > 0 Then
                Return ProduttoreList(0).RagioneSociale
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function GetIdProduttore(ByVal protocollo As Protocollo) As Integer

            If protocollo Is Nothing Then
                Throw New ArgumentNullException("protocollo")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocollo.Protocollo)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_GETPRODUTTORE)
            Dim ProduttoreList As New List(Of Produttore)()
            TExecuteReaderCmd(Of Produttore)(sqlCmd, AddressOf TGenerateProduttoreListFromReader(Of String), ProduttoreList)

            If ProduttoreList.Count > 0 Then
                Return ProduttoreList(0).Id
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ConteggioPannelli(ByVal Protocollo As String) As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, Protocollo)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_CONTEGGIOPANNELLI)

            Return CInt(ExecuteScalarCmd2(sqlCmd))

        End Function

        Public Overrides Function NrAttestatiProduttore(ByVal protocollo As Protocollo, IdProduttore As Integer) As Integer

            Dim NrAttestati As Integer
            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_NRATTESTATI)

            Dim erroreList As New List(Of ErroreImportazione)()
            'TExecuteReaderCmd(Of ErroreImportazione)(sqlCmd, AddressOf TGenerateErroreImportazioneListFromReader(Of ErroreImportazione), erroreList)

            NrAttestati = ExecuteScalarCmd2(sqlCmd)

            If NrAttestati > 0 Then
                Return NrAttestati
            Else
                Return 0
            End If

        End Function

        Public Overrides Function ListaMarche(ByVal protocollo As Protocollo) As List(Of String)

            If protocollo Is Nothing Then
                Throw New ArgumentNullException("protocollo")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocollo.Protocollo)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_LISTAMARCHE)
            Dim ListaDelleMarche As New List(Of String)()
            TExecuteReaderCmd(Of String)(sqlCmd, AddressOf TGenerateListOfMarcheFromReader(Of String), ListaDelleMarche)

            If ListaDelleMarche.Count > 0 Then
                Return ListaDelleMarche
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaPannelli(ByVal protocollo As Protocollo) As List(Of String)

            If protocollo Is Nothing Then
                Throw New ArgumentNullException("protocollo")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, protocollo.Protocollo)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_LISTAPANNELLI)
            Dim ListaDeiPannelli As New List(Of String)()
            TExecuteReaderCmd(Of String)(sqlCmd, AddressOf TGenerateListOfPannelliFromReader(Of String), ListaDeiPannelli)

            If ListaDeiPannelli.Count > 0 Then
                Return ListaDeiPannelli
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaProtocolli(ByVal FromDate As Date, ToDate As Date) As List(Of Protocollo)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@FromDate", SqlDbType.Date, 0, ParameterDirection.Input, FromDate)
            AddParamToSQLCmd(sqlCmd, "@ToDate", SqlDbType.Date, 0, ParameterDirection.Input, ToDate)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_PROTOCOLLO_LISTA)

            Dim ProtocolloList As New List(Of Protocollo)()
            TExecuteReaderCmd(Of Protocollo)(sqlCmd, AddressOf TGenerateProtocolloListFromReader(Of Protocollo), ProtocolloList)

            If ProtocolloList.Count > 0 Then
                Return ProtocolloList
            Else
                Return Nothing
            End If


        End Function

        ' ERRORE
        Private Const SP_ERRORE_CREA As String = "sp_Errore_Crea"
        Private Const SP_ERRORE_CARICADAID As String = "sp_Errore_Carica"
        Private Const SP_ERRORE_CARICADAID2 As String = "sp_Errore_Carica2"
        Private Const SP_ERRORE_AGGIORNA As String = "sp_Errore_Aggiorna"
        Private Const SP_ERRORE_ISINERROR As String = "sp_Errore_IsInError"
        Private Const SP_ERRORE_GETBYIDIMPORTAZIONE As String = "sp_Errore_GetByIdImportazione"
        Private Const SP_ERRORE_ELIMINA As String = "sp_Errore_Elimina"
        Private Const SP_ERRORE_ELIMINATUTTI As String = "sp_Errore_EliminaTutti"
        Private Const SP_ERRORE_ELIMINATUTTIDISMESSI As String = "sp_Errore_EliminaTuttiDismessi"
        Private Const SP_ERRORE_ELIMINATUTTIIMPIANTO As String = "sp_Errore_EliminaTuttiImpianto"
        Private Const SP_ERRORE_ELIMINATUTTIVERIFICA As String = "sp_Errore_EliminaTuttiVerifica"
        Private Const SP_ERRORE_EXIST As String = "sp_Errore_Exist"

        Public Overrides Function CreaNuovoErrore(ByVal nuovoErrore As ErroreImportazione) As Integer

            If nuovoErrore Is Nothing Then
                Throw New ArgumentNullException("nuovoErrore")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.Matricola)
            AddParamToSQLCmd(sqlCmd, "@Errore", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.Errore)
            AddParamToSQLCmd(sqlCmd, "@Importabile", SqlDbType.Bit, 0, ParameterDirection.Input, nuovoErrore.Importabile)
            AddParamToSQLCmd(sqlCmd, "@Modello", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.Modello)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Float, 0, ParameterDirection.Input, nuovoErrore.Peso)
            AddParamToSQLCmd(sqlCmd, "@DataInizioGaranzia", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoErrore.DataInizioGaranzia)
            AddParamToSQLCmd(sqlCmd, "@IdMarca", SqlDbType.Int, 0, ParameterDirection.Input, nuovoErrore.IdMarca)
            AddParamToSQLCmd(sqlCmd, "@Produttore", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.Produttore)
            AddParamToSQLCmd(sqlCmd, "@DataCaricamento", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoErrore.DataCaricamento)
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.UserName)
            AddParamToSQLCmd(sqlCmd, "@InErrore", SqlDbType.Bit, 0, ParameterDirection.Input, nuovoErrore.InErrore)
            AddParamToSQLCmd(sqlCmd, "@AnnoCaricamento", SqlDbType.Int, 0, ParameterDirection.Input, nuovoErrore.AnnoCaricamento)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovoErrore.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@idimpianto", SqlDbType.Int, 0, ParameterDirection.Input, nuovoErrore.IdImpianto)
            AddParamToSQLCmd(sqlCmd, "@TipoImportazione", SqlDbType.Int, 0, ParameterDirection.Input, nuovoErrore.TipoImportazione)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.Protocollo)
            AddParamToSQLCmd(sqlCmd, "@Stato", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.Stato)
            AddParamToSQLCmd(sqlCmd, "@Impianto", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.Impianto)
            AddParamToSQLCmd(sqlCmd, "@IdTipologia", SqlDbType.Int, 0, ParameterDirection.Input, nuovoErrore.IdTipologia)
            AddParamToSQLCmd(sqlCmd, "@IdFascia", SqlDbType.Int, 0, ParameterDirection.Input, nuovoErrore.IdFascia)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Money, 0, ParameterDirection.Input, nuovoErrore.CostoMatricola)
            AddParamToSQLCmd(sqlCmd, "@DataRitiro", SqlDbType.Date, 0, ParameterDirection.Input, nuovoErrore.DataRitiro)
            AddParamToSQLCmd(sqlCmd, "@NumeroFIR", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovoErrore.NumeroFIR)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)


        End Function

        Public Overrides Function CaricaErroreImportazione(ByVal IdErrore As Integer) As ErroreImportazione

            If IdErrore <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("IdErrore")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, IdErrore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_CARICADAID)

            Dim erroreList As New List(Of ErroreImportazione)()
            TExecuteReaderCmd(Of ErroreImportazione)(sqlCmd, AddressOf TGenerateErroreImportazioneListFromReader(Of ErroreImportazione), erroreList)

            If erroreList.Count > 0 Then
                Return erroreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function CaricaErroreImportazione(ByVal Matricola As String, Username As String) As ErroreImportazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 256, ParameterDirection.Input, Matricola)
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, Username)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_CARICADAID2)

            Dim erroreList As New List(Of ErroreImportazione)()
            TExecuteReaderCmd(Of ErroreImportazione)(sqlCmd, AddressOf TGenerateErroreImportazioneListFromReader(Of ErroreImportazione), erroreList)

            If erroreList.Count > 0 Then
                Return erroreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function IsInError(ByVal UserName As String, ByVal TipoImportazione As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, UserName)
            AddParamToSQLCmd(sqlCmd, "@TipoImportazione", SqlDbType.Int, 0, ParameterDirection.Input, TipoImportazione)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_ISINERROR)

            Dim erroreList As New List(Of ErroreImportazione)()
            TExecuteReaderCmd(Of ErroreImportazione)(sqlCmd, AddressOf TGenerateErroreImportazioneListFromReader(Of ErroreImportazione), erroreList)

            If erroreList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function AggiornaErroreImportazione(ByVal erroreDaAggiornare As ErroreImportazione) As Boolean

            If erroreDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("erroreDaAggiornare")
            End If

            If erroreDaAggiornare.Id <= DefaultValues.GetCategoryIdMinValue() Then
                Throw New ArgumentOutOfRangeException("erroreDaAggiornare.Id")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.Matricola)
            AddParamToSQLCmd(sqlCmd, "@Errore", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.Errore)
            AddParamToSQLCmd(sqlCmd, "@Importabile", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.Importabile)
            AddParamToSQLCmd(sqlCmd, "@Modello", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.Modello)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Float, 0, ParameterDirection.Input, erroreDaAggiornare.Peso)
            AddParamToSQLCmd(sqlCmd, "@DataInizioGaranzia", SqlDbType.DateTime, 0, ParameterDirection.Input, erroreDaAggiornare.DataInizioGaranzia)
            AddParamToSQLCmd(sqlCmd, "@IdMarca", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.IdMarca)
            AddParamToSQLCmd(sqlCmd, "@Produttore", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.Produttore)
            AddParamToSQLCmd(sqlCmd, "@IdImpianto", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.IdImpianto)
            AddParamToSQLCmd(sqlCmd, "@DataCaricamento", SqlDbType.DateTime, 0, ParameterDirection.Input, erroreDaAggiornare.DataCaricamento)
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.UserName)
            AddParamToSQLCmd(sqlCmd, "@InErrore", SqlDbType.Bit, 255, ParameterDirection.Input, erroreDaAggiornare.InErrore)
            AddParamToSQLCmd(sqlCmd, "@AnnoCaricamento", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.AnnoCaricamento)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@TipoImportazione", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.TipoImportazione)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.Protocollo)
            AddParamToSQLCmd(sqlCmd, "@Stato", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.Stato)
            AddParamToSQLCmd(sqlCmd, "@Impianto", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.Impianto)
            AddParamToSQLCmd(sqlCmd, "@IdTipologia", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.IdTipologia)
            AddParamToSQLCmd(sqlCmd, "@IdFascia", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.IdFascia)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Money, 0, ParameterDirection.Input, erroreDaAggiornare.CostoMatricola)
            AddParamToSQLCmd(sqlCmd, "@DataRitiro", SqlDbType.Date, 0, ParameterDirection.Input, erroreDaAggiornare.DataRitiro)
            AddParamToSQLCmd(sqlCmd, "@NumeroFIR", SqlDbType.NVarChar, 255, ParameterDirection.Input, erroreDaAggiornare.NumeroFIR)
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function GetByIdImportazione(ByVal Username As String, ByVal TipoImportazione As Integer) As List(Of ErroreImportazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_GETBYIDIMPORTAZIONE)

            Dim erroreList As New List(Of ErroreImportazione)()
            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NVarChar, 255, ParameterDirection.Input, Username)
            AddParamToSQLCmd(sqlCmd, "@TipoImportazione", SqlDbType.Int, 0, ParameterDirection.Input, TipoImportazione)
            TExecuteReaderCmd(Of ErroreImportazione)(sqlCmd, AddressOf TGenerateErroreImportazioneListFromReader(Of ErroreImportazione), erroreList)

            If erroreList.Count > 0 Then
                Return erroreList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function EliminaErrore(ByVal erroreDaEliminare As ErroreImportazione) As Boolean

            If erroreDaEliminare Is Nothing Then
                Throw New ArgumentNullException("erroreDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, erroreDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaErroriTutti(ByVal UserName As String) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, UserName)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_ELIMINATUTTI)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaErroriTuttiDismessi(ByVal UserName As String) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, UserName)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_ELIMINATUTTIDISMESSI)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaErroriTuttiImpianto(ByVal UserName As String) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, UserName)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_ELIMINATUTTIIMPIANTO)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaErroriTuttiVerifica(ByVal UserName As String) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, UserName)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_ELIMINATUTTIVERIFICA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function ErroreExists(Matricola As String, ByVal UserName As String, ByVal Id As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Matricola", SqlDbType.NVarChar, 255, ParameterDirection.Input, Matricola)
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 255, ParameterDirection.Input, UserName)
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ERRORE_EXIST)

            Dim erroreList As New List(Of ErroreImportazione)()
            TExecuteReaderCmd(Of ErroreImportazione)(sqlCmd, AddressOf TGenerateErroreImportazioneListFromReader(Of ErroreImportazione), erroreList)

            If erroreList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        ' UTILITIES
        Private Const SP_MEMBERSHIP_DELETE As String = "SP_Membership_Delete"

        Public Overrides Function MembershipDelete(ByVal UserId As Guid) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, UserId)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_MEMBERSHIP_DELETE)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        ' CATEGORIAPRODUTTORE
        Private Const SP_CATEGORIAPRO_CREA As String = "sp_CategoriaProduttore_Crea"
        Private Const SP_CATEGORIAPRO_CARICA As String = "sp_CategoriaProduttore_Carica"
        Private Const SP_CATEGORIAPRO_AGGIORNA As String = "sp_CategoriaProduttore_Aggiorna"
        Private Const SP_CATEGORIAPRO_ELIMINA As String = "sp_CategoriaProduttore_Elimina"
        Private Const SP_CATEGORIAPRO_CARICA2 As String = "sp_CategoriaProduttore_Carica2"
        Private Const SP_CATEGORIAPRO_VERIFICA As String = "sp_CategoriaProduttore_Verifica"
        Private Const SP_CATEGORIAPRO_LISTA As String = "sp_CategoriaProduttore_Lista"

        Public Overrides Function CreanuovaCategoriaProduttore(ByVal nuovaCategoriaPro As Categoria_Produttore) As Integer

            If nuovaCategoriaPro Is Nothing Then
                Throw New ArgumentNullException("nuovaCategoriaPro")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, nuovaCategoriaPro.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovaCategoriaPro.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Costo", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoriaPro.Costo)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoriaPro.Peso)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaCategoriaPro.Professionale)


            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRO_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaCategoriaProduttore(ByVal Id As Integer) As Categoria_Produttore

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRO_CARICA)

            Dim categoriaProduttoreList As New List(Of Categoria_Produttore)()
            TExecuteReaderCmd(Of Categoria_Produttore)(sqlCmd, AddressOf TGenerateCategoriaProduttoreListFromReader(Of Categoria_Produttore), categoriaProduttoreList)

            If categoriaProduttoreList.Count > 0 Then
                Return categoriaProduttoreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaCategoriaProduttore(ByVal categoriaProDaAggiornare As Categoria_Produttore) As Boolean

            If categoriaProDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("categoriaProDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaAggiornare.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Costo", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaProDaAggiornare.Costo)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaProDaAggiornare.Peso)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, categoriaProDaAggiornare.Professionale)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRO_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaCategoriaProduttore(ByVal categoriaProDaEliminare As Categoria_Produttore) As Boolean

            If categoriaProDaEliminare Is Nothing Then
                Throw New ArgumentNullException("categoriaProDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRO_ELIMINA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function

        Public Overrides Function CaricaCategoriaProduttore(ByVal IdCategoria As Integer, IdProduttore As Integer, ByVal Professionale As Boolean) As Categoria_Produttore

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, Professionale)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRO_CARICA2)

            Dim categoriaProduttoreList As New List(Of Categoria_Produttore)()
            TExecuteReaderCmd(Of Categoria_Produttore)(sqlCmd, AddressOf TGenerateCategoriaProduttoreListFromReader(Of Categoria_Produttore), categoriaProduttoreList)

            If categoriaProduttoreList.Count > 0 Then
                Return categoriaProduttoreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function VerificaCategoriaProduttore(ByVal IdCategoria As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, IdCategoria)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRO_VERIFICA)

            Dim categoriaClienteList As New List(Of Categoria_Produttore)()
            TExecuteReaderCmd(Of Categoria_Produttore)(sqlCmd, AddressOf TGenerateCategoriaProduttoreListFromReader(Of Categoria_Produttore), categoriaClienteList)

            If categoriaClienteList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function ListaCategoriaProduttore(ByVal IdProduttore As Integer, ByVal Professionale As Boolean) As List(Of Categoria_Produttore)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, Professionale)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRO_LISTA)

            Dim categoriaProduttoreList As New List(Of Categoria_Produttore)()
            TExecuteReaderCmd(Of Categoria_Produttore)(sqlCmd, AddressOf TGenerateCategoriaProduttoreListFromReader(Of Categoria_Produttore), categoriaProduttoreList)

            If categoriaProduttoreList.Count > 0 Then
                Return categoriaProduttoreList
            Else
                Return Nothing
            End If

        End Function

        ' CATEGORIA
        Private Const SP_CATEGORIA_CREA As String = "sp_Categoria_Crea"
        Private Const SP_CATEGORIA_CARICA As String = "sp_Categoria_Carica"
        Private Const SP_CATEGORIA_AGGIORNA As String = "sp_Categoria_Aggiorna"
        Private Const SP_CATEGORIA_ELIMINA As String = "sp_Categoria_Elimina"

        Public Overrides Function CreanuovaCategoria(ByVal nuovaCategoria As Categoria) As Integer

            If nuovaCategoria Is Nothing Then
                Throw New ArgumentNullException("nuovaCategoria")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Nome", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.Nome)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Valore", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoria.Valore)
            AddParamToSQLCmd(sqlCmd, "@IdMacrocategoria", SqlDbType.Int, 0, ParameterDirection.Input, nuovaCategoria.IdMacrocategoria)
            AddParamToSQLCmd(sqlCmd, "@Codifica", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.Codifica)
            AddParamToSQLCmd(sqlCmd, "@Raggruppamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.Raggruppamento)
            AddParamToSQLCmd(sqlCmd, "@DataModifica", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovaCategoria.DataModifica)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIA_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaCategoria(ByVal Id As Integer) As Categoria

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIA_CARICA)

            Dim categoriaList As New List(Of Categoria)()
            TExecuteReaderCmd(Of Categoria)(sqlCmd, AddressOf TGenerateCategoriaListFromReader(Of Categoria), categoriaList)

            If categoriaList.Count > 0 Then
                Return categoriaList(0)
            Else
                Return Nothing
            End If


        End Function

        Public Overrides Function AggiornaCategoria(ByVal categoriaDaAggiornare As Categoria) As Boolean

            If categoriaDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("categoriaDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Nome", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.Nome)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Valore", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaDaAggiornare.Valore)
            AddParamToSQLCmd(sqlCmd, "@IdMacrocategoria", SqlDbType.Int, 0, ParameterDirection.Input, categoriaDaAggiornare.IdMacrocategoria)
            AddParamToSQLCmd(sqlCmd, "@Codifica", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.Codifica)
            AddParamToSQLCmd(sqlCmd, "@Raggruppamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.Raggruppamento)
            AddParamToSQLCmd(sqlCmd, "@DataModifica", SqlDbType.DateTime, 0, ParameterDirection.Input, categoriaDaAggiornare.DataModifica)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIA_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaCategoria(ByVal categoriaDaEliminare As Categoria) As Boolean

            If categoriaDaEliminare Is Nothing Then
                Throw New ArgumentNullException("categoriaDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIA_ELIMINA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function


        ' DICHIARAZIONE
        Private Const SP_DICHIARAZIONE_CREA As String = "sp_Dichiarazione_Crea"
        Private Const SP_DICHIARAZIONE_CARICA As String = "sp_Dichiarazione_Carica"
        Private Const SP_DICHIARAZIONE_CARICA2 As String = "sp_Dichiarazione_Carica2"
        Private Const SP_DICHIARAZIONE_CARICA3 As String = "sp_Dichiarazione_Carica3"
        Private Const SP_DICHIARAZIONE_AGGIORNA As String = "sp_Dichiarazione_Aggiorna"
        Private Const SP_DICHIARAZIONE_ELIMINA As String = "sp_Dichiarazione_Elimina"
        Private Const SP_DICHIARAZIONE_ELIMINARIGHE As String = "sp_Dichiarazione_EliminaRighe"
        Private Const SP_DICHIARAZIONE_LISTA As String = "sp_Dichiarazione_Lista"
        Private Const SP_DICHIARAZIONE_LISTAPERDATA As String = "sp_Dichiarazione_ListaPerData"
        Private Const SP_DICHIARAZIONE_LISTAAPERTE As String = "sp_Dichiarazione_ListaAperte"

        Public Overrides Function CreaNuovaDichiarazione(ByVal nuovaDichiarazione As Dichiarazione) As Integer

            If nuovaDichiarazione Is Nothing Then
                Throw New ArgumentNullException("nuovaDichiarazione")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.Date, 0, ParameterDirection.Input, nuovaDichiarazione.Data)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovaDichiarazione.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@DataRegistrazione", SqlDbType.Date, 0, ParameterDirection.Input, nuovaDichiarazione.DataRegistrazione)
            AddParamToSQLCmd(sqlCmd, "@Utente", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaDichiarazione.Utente)
            AddParamToSQLCmd(sqlCmd, "@Confermata", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaDichiarazione.Confermata)
            AddParamToSQLCmd(sqlCmd, "@DataConferma", SqlDbType.Date, 0, ParameterDirection.Input, nuovaDichiarazione.DataConferma)
            AddParamToSQLCmd(sqlCmd, "@AutocertificazioneProdotta", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaDichiarazione.AutocertificazioneProdotta)
            AddParamToSQLCmd(sqlCmd, "@Fatturata", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaDichiarazione.Fatturata)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaDichiarazione.Professionale)
            AddParamToSQLCmd(sqlCmd, "@OldVersion", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaDichiarazione.OldVersion)
            AddParamToSQLCmd(sqlCmd, "@NrFattura", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaDichiarazione.NrFattura)
            AddParamToSQLCmd(sqlCmd, "@DataFattura", SqlDbType.Date, 0, ParameterDirection.Input, nuovaDichiarazione.DataFattura)
            AddParamToSQLCmd(sqlCmd, "@Autostimata", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaDichiarazione.Autostimata)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaDichiarazione(ByVal Id As Integer) As Dichiarazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_CARICA)

            Dim dichiarazioneList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), dichiarazioneList)

            If dichiarazioneList.Count > 0 Then
                Return dichiarazioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function CaricaDichiarazione2(ByVal IdProduttore As Integer, ByVal DataDichiarazione As Date) As Dichiarazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@DataDichiarazione", SqlDbType.Date, 0, ParameterDirection.Input, DataDichiarazione)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_CARICA2)

            Dim dichiarazioneList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), dichiarazioneList)

            If dichiarazioneList.Count > 0 Then
                Return dichiarazioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function CaricaDichiarazione3(ByVal IdProduttore As Integer, ByVal DataDichiarazione As Date, ByVal Professionale As Boolean) As Dichiarazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@DataDichiarazione", SqlDbType.Date, 0, ParameterDirection.Input, DataDichiarazione)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, Professionale)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_CARICA3)

            Dim dichiarazioneList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), dichiarazioneList)

            If dichiarazioneList.Count > 0 Then
                Return dichiarazioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaDichiarazione(ByVal dichiarazioneDaAggiornare As Dichiarazione) As Boolean

            If dichiarazioneDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("dichiarazioneDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.Date, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.Data)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@DataRegistrazione", SqlDbType.Date, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.DataRegistrazione)
            AddParamToSQLCmd(sqlCmd, "@Utente", SqlDbType.NVarChar, 256, ParameterDirection.Input, dichiarazioneDaAggiornare.Utente)
            AddParamToSQLCmd(sqlCmd, "@Confermata", SqlDbType.Bit, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.Confermata)
            AddParamToSQLCmd(sqlCmd, "@DataConferma", SqlDbType.Date, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.DataConferma)
            AddParamToSQLCmd(sqlCmd, "@AutocertificazioneProdotta", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.AutocertificazioneProdotta)
            AddParamToSQLCmd(sqlCmd, "@Fatturata", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.Fatturata)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.Professionale)
            AddParamToSQLCmd(sqlCmd, "@OldVersion", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.OldVersion)
            AddParamToSQLCmd(sqlCmd, "@NrFattura", SqlDbType.NVarChar, 256, ParameterDirection.Input, dichiarazioneDaAggiornare.NrFattura)
            AddParamToSQLCmd(sqlCmd, "@DataFattura", SqlDbType.Date, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.DataFattura)
            AddParamToSQLCmd(sqlCmd, "@Autostimata", SqlDbType.Bit, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.Autostimata)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaDichiarazione(ByVal dichiarazioneDaEliminare As Dichiarazione) As Boolean

            If dichiarazioneDaEliminare Is Nothing Then
                Throw New ArgumentNullException("dichiarazioneDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim sqlCmd2 As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaEliminare.Id)

            AddParamToSQLCmd(sqlCmd2, "@IdDichiarazione", SqlDbType.Int, 0, ParameterDirection.Input, dichiarazioneDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_ELIMINA)
            SetCommandType(sqlCmd2, CommandType.StoredProcedure, SP_DICHIARAZIONE_ELIMINARIGHE)

            ExecuteScalarCmd(sqlCmd)
            ExecuteScalarCmd(sqlCmd2)


            Return True

        End Function

        Public Overrides Function TotaleKgCategoria(ByVal Codice As String, ByVal Anno As Integer) As Decimal

            If Codice Is Nothing Then
                Throw New ArgumentNullException("Codice")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@CodiceRaggruppamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, Codice)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_Dichiarazione_Raggruppamento")
            Return CDec(ExecuteScalarCmd2(sqlCmd))

        End Function

        Public Overrides Function TotaleKgCategoriaNew(ByVal Codice As String, ByVal Anno As Integer) As Decimal

            If Codice Is Nothing Then
                Throw New ArgumentNullException("Codice")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@CodiceRaggruppamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, Codice)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_Dichiarazione_RaggruppamentoNew")
            Return CDec(ExecuteScalarCmd2(sqlCmd))

        End Function

        Public Overrides Function ListaDichiarazioni(ByVal IdProduttore As Integer, ByVal Professionale As Boolean) As List(Of Dichiarazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@SoloProfessionale", SqlDbType.Int, 0, ParameterDirection.Input, Professionale)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_LISTA)

            Dim DichiarazioneList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), DichiarazioneList)

            If DichiarazioneList.Count > 0 Then
                Return DichiarazioneList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaDichiarazioni(ByVal FromDate As Date, ByVal ToDate As Date) As List(Of Dichiarazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@FromDate", SqlDbType.Date, 0, ParameterDirection.Input, FromDate)
            AddParamToSQLCmd(sqlCmd, "@ToDate", SqlDbType.Date, 0, ParameterDirection.Input, ToDate)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_LISTAPERDATA)

            Dim DichiarazioneList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), DichiarazioneList)

            If DichiarazioneList.Count > 0 Then
                Return DichiarazioneList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function ListaAperte() As List(Of Dichiarazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_DICHIARAZIONE_LISTAAPERTE)

            Dim DichiarazioneList As New List(Of Dichiarazione)()
            TExecuteReaderCmd(Of Dichiarazione)(sqlCmd, AddressOf TGenerateDichiarazioneListFromReader(Of Dichiarazione), DichiarazioneList)

            If DichiarazioneList.Count > 0 Then
                Return DichiarazioneList
            Else
                Return Nothing
            End If

        End Function



        ' RIGA DICHIARAZIONE
        Private Const SP_RIGADICHIARAZIONE_CREA As String = "sp_RigaDichiarazione_Crea"
        Private Const SP_RIGADICHIARAZIONE_CARICA As String = "sp_RigaDichiarazione_Carica"
        Private Const SP_RIGADICHIARAZIONE_AGGIORNA As String = "sp_RigaDichiarazione_Aggiorna"
        Private Const SP_RIGADICHIARAZIONE_LISTA As String = "sp_RigaDichiarazioni_Lista"

        Public Overrides Function CreaNuovaRigaDichiarazione(ByVal nuovaRigaDichiarazione As RigaDichiarazione) As Integer

            If nuovaRigaDichiarazione Is Nothing Then
                Throw New ArgumentNullException("nuovaRigaDichiarazione")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdDichiarazione", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaDichiarazione.IdDichiarazione)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaDichiarazione.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaRigaDichiarazione.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Pezzi", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaDichiarazione.Pezzi)
            AddParamToSQLCmd(sqlCmd, "@Kg", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaDichiarazione.Kg)
            AddParamToSQLCmd(sqlCmd, "@KgDichiarazione", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaDichiarazione.KgDichiarazione)
            AddParamToSQLCmd(sqlCmd, "@CostoUnitario", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaDichiarazione.CostoUnitario)
            AddParamToSQLCmd(sqlCmd, "@Importo", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaDichiarazione.Importo)
            AddParamToSQLCmd(sqlCmd, "@UtenteAggiornamento", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaRigaDichiarazione.UtenteAggiornamento)
            AddParamToSQLCmd(sqlCmd, "@DataAggiornamento", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovaRigaDichiarazione.DataAggiornamento)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGADICHIARAZIONE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaRigaDichiarazione(ByVal Id As Integer) As RigaDichiarazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGADICHIARAZIONE_CARICA)

            Dim rigadichiarazioneList As New List(Of RigaDichiarazione)()
            TExecuteReaderCmd(Of RigaDichiarazione)(sqlCmd, AddressOf TGenerateRigaDichiarazioneListFromReader(Of RigaDichiarazione), rigadichiarazioneList)

            If rigadichiarazioneList.Count > 0 Then
                Return rigadichiarazioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaRigaDichiarazione(ByVal rigaDaAggiornare As RigaDichiarazione) As Boolean

            If rigaDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("rigaDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdDichiarazione", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.IdDichiarazione)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 256, ParameterDirection.Input, rigaDaAggiornare.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Pezzi", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.Pezzi)
            AddParamToSQLCmd(sqlCmd, "@Kg", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.Kg)
            AddParamToSQLCmd(sqlCmd, "@KgDichiarazione", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.KgDichiarazione)
            AddParamToSQLCmd(sqlCmd, "@CostoUnitario", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.CostoUnitario)
            AddParamToSQLCmd(sqlCmd, "@Importo", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.Importo)
            AddParamToSQLCmd(sqlCmd, "@UtenteAggiornamento", SqlDbType.NVarChar, 256, ParameterDirection.Input, rigaDaAggiornare.UtenteAggiornamento)
            AddParamToSQLCmd(sqlCmd, "@DataAggiornamento", SqlDbType.DateTime, 0, ParameterDirection.Input, rigaDaAggiornare.DataAggiornamento)
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGADICHIARAZIONE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function ListaRigheDichiarazioni(ByVal IdDichiaraazione As Integer) As List(Of RigaDichiarazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdDichiarazione", SqlDbType.Int, 0, ParameterDirection.Input, IdDichiaraazione)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGADICHIARAZIONE_LISTA)

            Dim RigheDichiarazioneList As New List(Of RigaDichiarazione)()
            TExecuteReaderCmd(Of RigaDichiarazione)(sqlCmd, AddressOf TGenerateRigaDichiarazioneListFromReader(Of RigaDichiarazione), RigheDichiarazioneList)

            If RigheDichiarazioneList.Count > 0 Then
                Return RigheDichiarazioneList
            Else
                Return Nothing
            End If

        End Function

        ' RIGA DICHIARAZIONE RAGGRUPPATE        
        Private Const SP_RIGADICHIARAZIONERAGGRUPATE_LISTA As String = "sp_RigaDichiarazioniRaggruppate_Lista"

        Public Overrides Function ListaRigheDicRaggruppate(ByVal IdDichiarazione As Integer) As List(Of RigaDichiarazioneRaggruppate)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdDichiarazione", SqlDbType.Int, 0, ParameterDirection.Input, IdDichiarazione)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGADICHIARAZIONERAGGRUPATE_LISTA)

            Dim RigheDichiarazioneList As New List(Of RigaDichiarazioneRaggruppate)()
            TExecuteReaderCmd(Of RigaDichiarazioneRaggruppate)(sqlCmd, AddressOf TGenerateRigaDichiarazioneRaggListFromReader(Of RigaDichiarazioneRaggruppate), RigheDichiarazioneList)

            If RigheDichiarazioneList.Count > 0 Then
                Return RigheDichiarazioneList
            Else
                Return Nothing
            End If

        End Function

        ' AUTOCERTIFICAZIONE
        Private Const SP_AUTOCERTIFICAZIONE_CREA As String = "sp_Autocertificazione_Crea"
        Private Const SP_AUTOCERTIFICAZIONE_CARICA As String = "sp_Autocertificazione_Carica"
        Private Const SP_AUTOCERTIFICAZIONE_AGGIORNA As String = "sp_Autocertificazione_Aggiorna"
        Private Const SP_AUTOCERTIFICAZIONE_IMPORTO As String = "sp_Autocertificazione_Importo"
        Private Const SP_AUTOCERTIFICAZIONE_RIGHEESISTE As String = "sp_Autocertificazione_RighEsiste"
        Private Const SP_AUTOCERTIFICAZIONE_RETTIFICATA As String = "sp_Autocertificazione_Rettificata"
        Private Const SP_AUTOCERTIFICAZIONE_LISTAPERDATA As String = "sp_Autocertificazione_ListaPerData"
        Private Const SP_AUTOCERTIFICAZIONE_ELIMINA As String = "sp_Autocertificazione_Elimina"
        Private Const SP_AUTOCERTIFICAZIONE_CARICADAANNO As String = "sp_Autocertificazione_CaricaDaAnno"
        Private Const SP_AUTOCERTIFICAZIONE_LISTAAPERTE As String = "sp_Autocertificazione_ListaAperte"

        Public Overrides Function CreaNuovaAutocertificazione(ByVal nuovaAutocertificazione As Autocertificazione) As Integer

            If nuovaAutocertificazione Is Nothing Then
                Throw New ArgumentNullException("nuovaAutocertificazione")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovaAutocertificazione.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, nuovaAutocertificazione.Anno)
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.Date, 0, ParameterDirection.Input, nuovaAutocertificazione.Data)
            AddParamToSQLCmd(sqlCmd, "@PathFile", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.PathFile)
            AddParamToSQLCmd(sqlCmd, "@NomeFile", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.NomeFile)
            AddParamToSQLCmd(sqlCmd, "@PathFilePile", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.PathFilePile)
            AddParamToSQLCmd(sqlCmd, "@NomeFilePile", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.NomeFilePile)
            AddParamToSQLCmd(sqlCmd, "@PathFileIndustriali", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.PathFileIndustriali)
            AddParamToSQLCmd(sqlCmd, "@NomeFileIndustriali", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.NomeFileIndustriali)
            AddParamToSQLCmd(sqlCmd, "@PathFileVeicoli", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.PathFileVeicoli)
            AddParamToSQLCmd(sqlCmd, "@NomeFileVeicoli", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.NomeFileVeicoli)
            AddParamToSQLCmd(sqlCmd, "@UploadEseguito", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaAutocertificazione.UploadEseguito)
            AddParamToSQLCmd(sqlCmd, "@Confermata", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaAutocertificazione.Confermata)
            AddParamToSQLCmd(sqlCmd, "@DataConferma", SqlDbType.Date, 0, ParameterDirection.Input, nuovaAutocertificazione.DataConferma)
            AddParamToSQLCmd(sqlCmd, "@Rettificata", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaAutocertificazione.Rettificata)
            AddParamToSQLCmd(sqlCmd, "@NrFattura", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaAutocertificazione.NrFattura)
            AddParamToSQLCmd(sqlCmd, "@DataFattura", SqlDbType.Date, 0, ParameterDirection.Input, nuovaAutocertificazione.DataFattura)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaAutocertificazione(ByVal Id As Integer) As Autocertificazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_CARICA)

            Dim autocertificazioneList As New List(Of Autocertificazione)()
            TExecuteReaderCmd(Of Autocertificazione)(sqlCmd, AddressOf TGenerateAutocertificazioneListFromReader(Of Autocertificazione), autocertificazioneList)

            If autocertificazioneList.Count > 0 Then
                Return autocertificazioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function CaricaAutocertificazione(ByVal IdProduttore As Integer, ByVal Anno As Integer) As Autocertificazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_CARICADAANNO)

            Dim autocertificazioneList As New List(Of Autocertificazione)()
            TExecuteReaderCmd(Of Autocertificazione)(sqlCmd, AddressOf TGenerateAutocertificazioneListFromReader(Of Autocertificazione), autocertificazioneList)

            If autocertificazioneList.Count > 0 Then
                Return autocertificazioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaAutocertificazione(ByVal autocertificazioneDaAggiornare As Autocertificazione) As Boolean

            If autocertificazioneDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("autocertificazioneDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.Anno)
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.Date, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.Data)
            AddParamToSQLCmd(sqlCmd, "@PathFile", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.PathFile)
            AddParamToSQLCmd(sqlCmd, "@NomeFile", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.NomeFile)
            AddParamToSQLCmd(sqlCmd, "@PathFilePile", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.PathFilePile)
            AddParamToSQLCmd(sqlCmd, "@NomeFilePile", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.NomeFilePile)
            AddParamToSQLCmd(sqlCmd, "@PathFileIndustriali", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.PathFileIndustriali)
            AddParamToSQLCmd(sqlCmd, "@NomeFileIndustriali", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.NomeFileIndustriali)
            AddParamToSQLCmd(sqlCmd, "@PathFileVeicoli", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.PathFileVeicoli)
            AddParamToSQLCmd(sqlCmd, "@NomeFileVeicoli", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.NomeFileVeicoli)
            AddParamToSQLCmd(sqlCmd, "@UploadEseguito", SqlDbType.Bit, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.UploadEseguito)
            AddParamToSQLCmd(sqlCmd, "@Confermata", SqlDbType.Bit, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.Confermata)
            AddParamToSQLCmd(sqlCmd, "@DataConferma", SqlDbType.Date, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.DataConferma)
            AddParamToSQLCmd(sqlCmd, "@Rettificata", SqlDbType.Bit, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.Rettificata)
            AddParamToSQLCmd(sqlCmd, "@NrFattura", SqlDbType.NVarChar, 256, ParameterDirection.Input, autocertificazioneDaAggiornare.NrFattura)
            AddParamToSQLCmd(sqlCmd, "@DataFattura", SqlDbType.Date, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.DataFattura)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, autocertificazioneDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function ImportoAutocertificazione(ByVal autocert As Autocertificazione) As Decimal

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim Importo As Decimal?

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, autocert.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_IMPORTO)
            Try
                Importo = ExecuteScalarCmd2(sqlCmd)
            Catch ex As Exception
                Importo = 0
            End Try
                        
            Return Importo
            

        End Function

        Public Overrides Function RigheAutocertificazioneEsiste(ByVal autocert As Autocertificazione) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, autocert.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_RIGHEESISTE)
            Return Cbool(ExecuteScalarCmd2(sqlCmd))

        End Function

        Public Overrides Function AutocertificazioneRettificata(ByVal autocert As Autocertificazione) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim DiffKg As Decimal

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, autocert.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_RETTIFICATA)
            Try
                DiffKg = ExecuteScalarCmd2(sqlCmd)
            Catch ex As Exception
                DiffKg = 0
            End Try

            If DiffKg <> 0 Then
                Return True
            Else
                Return False
            End If                        
            
            

        End Function

        Public Overrides Function ListaAutocertificazioni(ByVal FromDate As Date, ByVal ToDate As Date) As List(Of Autocertificazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@FromDate", SqlDbType.Date, 0, ParameterDirection.Input, FromDate)
            AddParamToSQLCmd(sqlCmd, "@ToDate", SqlDbType.Date, 0, ParameterDirection.Input, ToDate)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_LISTAPERDATA)

            Dim AutocertificazioneList As New List(Of Autocertificazione)()
            TExecuteReaderCmd(Of Autocertificazione)(sqlCmd, AddressOf TGenerateAutocertificazioneListFromReader(Of Autocertificazione), AutocertificazioneList)

            If AutocertificazioneList.Count > 0 Then
                Return AutocertificazioneList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function EliminaAutocertificazione(ByVal autocertificazioneDaEliminare As Autocertificazione, ByVal Utente As String) As Boolean

            If autocertificazioneDaEliminare Is Nothing Then
                Throw New ArgumentNullException("autocertificazioneDaEliminare")
            End If

            ' Registra il Log
            Dim NewLog As New Log
            Dim Produttore As Produttore = Produttore.Carica(autocertificazioneDaEliminare.IdProduttore)

            NewLog.Utente = Utente
            NewLog.Data = Today
            NewLog.Ora = Now
            NewLog.Origine = "Lista autocertificazioni"
            NewLog.Descrizione = "Eliminata autocertificazione ID = " & autocertificazioneDaEliminare.Id & " Produttore " & Produttore.RagioneSociale
            NewLog.Save()



            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim DaData, AData As String
            Dim StrSql As String = "DELETE FROM tbl_RigheAutocertificazione WHERE IdCertificazione = " & autocertificazioneDaEliminare.Id

            DaData = autocertificazioneDaEliminare.Anno & "-01-01"
            AData = autocertificazioneDaEliminare.Anno & "-12-31"

            ' Elimina le righe dell'autocertificazione
            SetCommandType(sqlCmd, CommandType.Text, StrSql)
            ExecuteScalarCmd(sqlCmd)

            ' Elimina la testata dell'autocertificazione
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, autocertificazioneDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_ELIMINA)
            ExecuteScalarCmd(sqlCmd)

            ' Aggiorna le dichiarazioni
            StrSql = "UPDATE tbl_Dichiarazioni Set  AutocertificazioneProdotta = 0 WHERE Data Between '" & DaData & "' And '" & AData & "'"
            SetCommandType(sqlCmd, CommandType.Text, StrSql)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function ListaAutocertificazioniAperte() As List(Of Autocertificazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_AUTOCERTIFICAZIONE_LISTAAPERTE)

            Dim AutocertificazioneList As New List(Of Autocertificazione)()
            TExecuteReaderCmd(Of Autocertificazione)(sqlCmd, AddressOf TGenerateAutocertificazioneListFromReader(Of Autocertificazione), AutocertificazioneList)

            If AutocertificazioneList.Count > 0 Then
                Return AutocertificazioneList
            Else
                Return Nothing
            End If

        End Function




        ' RIGA AUTOCERTIFICAZIONE
        Private Const SP_RIGACERTIFICAZIONE_CREA As String = "sp_RigaCertificazione_Crea"
        Private Const SP_RIGACERTIFICAZIONE_CARICA As String = "sp_RigaCertificazione_Carica"
        Private Const SP_RIGACERTIFICAZIONE_AGGIORNA As String = "sp_RigaCertificazione_Aggiorna"
        Private Const SP_RIGACERTIFICAZIONE_LISTA As String = "sp_RigaCertificazione_Lista"
        Private Const SP_RIGACERTIFICAZIONE_GENERA As String = "sp_RigaCertificazione_Genera"

        Public Overrides Function CreaNuovaRigaCertificazione(ByVal nuovaRigaCertificazione As RigaAutocertificazione) As Integer

            If nuovaRigaCertificazione Is Nothing Then
                Throw New ArgumentNullException("nuovaRigaCertificazione")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdCertificazione", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaCertificazione.IdCertificazione)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaCertificazione.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaRigaCertificazione.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Pezzi", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaCertificazione.Pezzi)
            AddParamToSQLCmd(sqlCmd, "@Kg", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.Kg)
            AddParamToSQLCmd(sqlCmd, "@PezziRettifica", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaCertificazione.PezziRettifica)
            AddParamToSQLCmd(sqlCmd, "@KgRettifica", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.KgRettifica)
            AddParamToSQLCmd(sqlCmd, "@DifferenzaPezzi", SqlDbType.Int, 0, ParameterDirection.Input, nuovaRigaCertificazione.DifferenzaPezzi)
            AddParamToSQLCmd(sqlCmd, "@DifferenzaKg", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.DifferenzaKg)
            AddParamToSQLCmd(sqlCmd, "@KgCertificazione", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.KgCertificazione)
            AddParamToSQLCmd(sqlCmd, "@KgDiffCertificazione", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.KgDiffCertificazione)
            AddParamToSQLCmd(sqlCmd, "@KgCertSoci", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.KgCertSoci)
            AddParamToSQLCmd(sqlCmd, "@CostoUnitario", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.CostoUnitario)
            AddParamToSQLCmd(sqlCmd, "@Importo", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaRigaCertificazione.Importo)
            AddParamToSQLCmd(sqlCmd, "@UtenteAggiornamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaRigaCertificazione.UtenteAggiornamento)
            AddParamToSQLCmd(sqlCmd, "@DataAggiornamento", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovaRigaCertificazione.DataAggiornamento)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGACERTIFICAZIONE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaRigaCertificazione(ByVal Id As Integer) As RigaAutocertificazione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGACERTIFICAZIONE_CARICA)

            Dim rigaCertificazioneList As New List(Of RigaAutocertificazione)()
            TExecuteReaderCmd(Of RigaAutocertificazione)(sqlCmd, AddressOf TGenerateRigaCertificazioneListFromReader(Of RigaAutocertificazione), rigaCertificazioneList)

            If rigaCertificazioneList.Count > 0 Then
                Return rigaCertificazioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaRigaCertificazione(ByVal rigaDaAggiornare As RigaAutocertificazione) As Boolean

            If rigaDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("rigaDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdCertificazione", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.IdCertificazione)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 256, ParameterDirection.Input, rigaDaAggiornare.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Pezzi", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.Pezzi)
            AddParamToSQLCmd(sqlCmd, "@Kg", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.Kg)
            AddParamToSQLCmd(sqlCmd, "@PezziRettifica", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.PezziRettifica)
            AddParamToSQLCmd(sqlCmd, "@KgRettifica", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.KgRettifica)
            AddParamToSQLCmd(sqlCmd, "@DifferenzaPezzi", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.DifferenzaPezzi)
            AddParamToSQLCmd(sqlCmd, "@DifferenzaKg", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.DifferenzaKg)
            AddParamToSQLCmd(sqlCmd, "@KgCertificazione", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.KgCertificazione)
            AddParamToSQLCmd(sqlCmd, "@KgDiffCertificazione", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.KgDiffCertificazione)
            AddParamToSQLCmd(sqlCmd, "@KgCertSoci", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.KgCertSoci)
            AddParamToSQLCmd(sqlCmd, "@CostoUnitario", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.CostoUnitario)
            AddParamToSQLCmd(sqlCmd, "@Importo", SqlDbType.Decimal, 0, ParameterDirection.Input, rigaDaAggiornare.Importo)
            AddParamToSQLCmd(sqlCmd, "@UtenteAggiornamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, rigaDaAggiornare.UtenteAggiornamento)
            AddParamToSQLCmd(sqlCmd, "@DataAggiornamento", SqlDbType.DateTime, 0, ParameterDirection.Input, rigaDaAggiornare.DataAggiornamento)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, rigaDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGAcertificazione_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function ListaRigheCertificazione(ByVal IdCertificazione As Integer) As List(Of RigaAutocertificazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCertificazione", SqlDbType.Int, 0, ParameterDirection.Input, IdCertificazione)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGAcertificazione_LISTA)

            Dim RigheCertificazioneList As New List(Of RigaAutocertificazione)()
            TExecuteReaderCmd(Of RigaAutocertificazione)(sqlCmd, AddressOf TGenerateRigaCertificazioneListFromReader(Of RigaAutocertificazione), RigheCertificazioneList)

            If RigheCertificazioneList.Count > 0 Then
                Return RigheCertificazioneList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function GeneraRigheCertificazione(ByVal AutocertificazioneDaCreare As Autocertificazione) As List(Of RigaAutocertificazione)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            Dim DaData As Date = new Date(AutocertificazioneDaCreare.Anno,1,1)
            Dim AData As Date = New Date(AutocertificazioneDaCreare.Anno,12,31)

            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, AutocertificazioneDaCreare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@DataInizio", SqlDbType.Date, 0, ParameterDirection.Input, DaData)
            AddParamToSQLCmd(sqlCmd, "@DataFine", SqlDbType.Date, 0, ParameterDirection.Input, AData)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGACERTIFICAZIONE_GENERA)

            Dim RigheCertificazioneList As New List(Of RigaAutocertificazione)()
            TExecuteReaderCmd(Of RigaAutocertificazione)(sqlCmd, AddressOf TGenerateRigaCertificazioneListFromReader(Of RigaAutocertificazione), RigheCertificazioneList)

            If RigheCertificazioneList.Count > 0 Then
                Return RigheCertificazioneList
            Else
                Return Nothing
            End If

        End Function


        ' RIGA DICHIARAZIONE RAGGRUPPATE        
        Private Const SP_RIGACERTIFICAZIONERAGGRUPATE_LISTA As String = "sp_CertificazioneRaggruppate_Lista"

        Public Overrides Function ListaRigheCertificazioneRaggruppate(ByVal IdCertificazione As Integer) As List(Of RigaAutocertificazioneRaggruppate)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCertificazione", SqlDbType.Int, 0, ParameterDirection.Input, IdCertificazione)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_RIGACERTIFICAZIONERAGGRUPATE_LISTA)

            Dim RigheCertificazioneList As New List(Of RigaAutocertificazioneRaggruppate)()
            TExecuteReaderCmd(Of RigaAutocertificazioneRaggruppate)(sqlCmd, AddressOf TGenerateRigaCertificazioneRaggListFromReader(Of RigaAutocertificazioneRaggruppate), RigheCertificazioneList)

            If RigheCertificazioneList.Count > 0 Then
                Return RigheCertificazioneList
            Else
                Return Nothing
            End If

        End Function

        ' OPZIONE
        Private Const SP_OPZIONE_CREA As String = "aspnet_Opzione_Crea"
        Private Const SP_OPZIONE_CARICA As String = "aspnet_Opzione_Carica"
        Private Const SP_OPZIONE_AGGIORNA As String = "aspnet_Opzione_Aggiorna"

        Public Overrides Function CreaNuovaOpzione(ByVal nuovaOpzione As Opzione) As Integer

            If nuovaOpzione Is Nothing Then
                Throw New ArgumentNullException("nuovaAutocertificazione")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, nuovaOpzione.Id)
            AddParamToSQLCmd(sqlCmd, "@Oggetto", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaOpzione.Oggetto)
            AddParamToSQLCmd(sqlCmd, "@TestoMail", SqlDbType.NVarChar, 4000, ParameterDirection.Input, nuovaOpzione.TestoMail)
            AddParamToSQLCmd(sqlCmd, "@smtp", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaOpzione.Smtp)
            AddParamToSQLCmd(sqlCmd, "@NomeUtente", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaOpzione.NomeUtente)
            AddParamToSQLCmd(sqlCmd, "@Password", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaOpzione.Password)
            AddParamToSQLCmd(sqlCmd, "@SSL", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaOpzione.SSL)
            AddParamToSQLCmd(sqlCmd, "@Porta", SqlDbType.Int, 0, ParameterDirection.Input, nuovaOpzione.Porta)
            AddParamToSQLCmd(sqlCmd, "@Mittente", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaOpzione.Mittente)
            AddParamToSQLCmd(sqlCmd, "@DestinatarioTest", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaOpzione.DestinatarioTest)
            AddParamToSQLCmd(sqlCmd, "@OggettoAutocertificazione", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovaOpzione.OggettoAutocertificazione)
            AddParamToSQLCmd(sqlCmd, "@TestoMailAutocertificazione", SqlDbType.NVarChar, 4000, ParameterDirection.Input, nuovaOpzione.TestoMailAutocertificazione)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_OPZIONE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaOpzione() As Opzione

            Dim sqlCmd As SqlCommand = New SqlCommand()            
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_OPZIONE_CARICA)

            Dim opzioneList As New List(Of Opzione)()
            TExecuteReaderCmd(Of Opzione)(sqlCmd, AddressOf TGenerateOpzioneListFromReader(Of Opzione), opzioneList)

            If opzioneList.Count > 0 Then
                Return opzioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaOpzione(ByVal opzioneDaAggiornare As Opzione) As Boolean

            If opzioneDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("opzioneDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Oggetto", SqlDbType.NVarChar, 256, ParameterDirection.Input, opzioneDaAggiornare.Oggetto)
            AddParamToSQLCmd(sqlCmd, "@TestoMail", SqlDbType.NVarChar, 4000, ParameterDirection.Input, opzioneDaAggiornare.TestoMail)
            AddParamToSQLCmd(sqlCmd, "@Smtp", SqlDbType.NVarChar, 256, ParameterDirection.Input, opzioneDaAggiornare.Smtp)
            AddParamToSQLCmd(sqlCmd, "@NomeUtente", SqlDbType.NVarChar, 256, ParameterDirection.Input, opzioneDaAggiornare.NomeUtente)
            AddParamToSQLCmd(sqlCmd, "@Password", SqlDbType.NVarChar, 256, ParameterDirection.Input, opzioneDaAggiornare.Password)
            AddParamToSQLCmd(sqlCmd, "@SSL", SqlDbType.Bit, 0, ParameterDirection.Input, opzioneDaAggiornare.SSL)
            AddParamToSQLCmd(sqlCmd, "@Porta", SqlDbType.Int, 0, ParameterDirection.Input, opzioneDaAggiornare.Porta)
            AddParamToSQLCmd(sqlCmd, "@Mittente", SqlDbType.NVarChar, 256, ParameterDirection.Input, opzioneDaAggiornare.Mittente)
            AddParamToSQLCmd(sqlCmd, "@DestinatarioTest", SqlDbType.NVarChar, 256, ParameterDirection.Input, opzioneDaAggiornare.DestinatarioTest)
            AddParamToSQLCmd(sqlCmd, "@OggettoAutocertificazione", SqlDbType.NVarChar, 256, ParameterDirection.Input, opzioneDaAggiornare.OggettoAutocertificazione)
            AddParamToSQLCmd(sqlCmd, "@TestoMailAutocertificazione", SqlDbType.NVarChar, 4000, ParameterDirection.Input, opzioneDaAggiornare.TestoMailAutocertificazione)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_OPZIONE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function


        ' LOG
        Private Const SP_LOG_CREA As String = "sp_Log_Crea"
        Private Const SP_LOG_CARICA As String = "sp_Log_Carica"
        Private Const SP_LOG_AGGIORNA As String = "sp_Log_Aggiorna"
        Private Const SP_LOG_CARICANONLETTI As String = "sp_Log_CaricaNonLetti"
        
        Public Overrides Function CreaNuovoLog(ByVal nuovoLog As Log) As Integer

            If nuovoLog Is Nothing Then
                Throw New ArgumentNullException("Log")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoLog.Data)
            AddParamToSQLCmd(sqlCmd, "@Ora", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovoLog.Ora)
            AddParamToSQLCmd(sqlCmd, "@Origine", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovoLog.Origine)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovoLog.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@Utente", SqlDbType.NVarChar, 256, ParameterDirection.Input, nuovoLog.Utente)
            AddParamToSQLCmd(sqlCmd, "@Letto", SqlDbType.Bit, 0, ParameterDirection.Input, nuovoLog.Letto)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOG_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaLog(Id As Integer) As Log

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOG_CARICA)

            Dim logList As New List(Of Log)()
            TExecuteReaderCmd(Of Log)(sqlCmd, AddressOf TGenerateLogListFromReader(Of Log), logList)

            If logList.Count > 0 Then
                Return logList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaLog(ByVal logDaAggiornare As Log) As Boolean

            If logDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("logDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Data", SqlDbType.NVarChar, 256, ParameterDirection.Input, logDaAggiornare.Data)
            AddParamToSQLCmd(sqlCmd, "@Ora", SqlDbType.NVarChar, 4000, ParameterDirection.Input, logDaAggiornare.Ora)
            AddParamToSQLCmd(sqlCmd, "@Origine", SqlDbType.NVarChar, 256, ParameterDirection.Input, logDaAggiornare.Origine)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 256, ParameterDirection.Input, logDaAggiornare.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@Utente", SqlDbType.NVarChar, 256, ParameterDirection.Input, logDaAggiornare.Utente)
            AddParamToSQLCmd(sqlCmd, "@Letto", SqlDbType.NVarChar, 256, ParameterDirection.Input, logDaAggiornare.Letto)
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, logDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOG_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function CaricaLogNonLetti() As Integer

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOG_CARICANONLETTI)

            Dim logList As New List(Of Log)()
            TExecuteReaderCmd(Of Log)(sqlCmd, AddressOf TGenerateLogListFromReader(Of Log), logList)

            Return logList.Count
            

        End Function

        '*************** New category function

        ' CATEGORIAPRODUTTORE
        Private Const SP_CATEGORIAPRONEW_CREA As String = "sp_CategoriaProduttoreNew_Crea"
        Private Const SP_CATEGORIAPRONEW_CARICA As String = "sp_CategoriaProduttoreNew_Carica"
        Private Const SP_CATEGORIAPRONEW_AGGIORNA As String = "sp_CategoriaProduttoreNew_Aggiorna"
        Private Const SP_CATEGORIAPRONEW_ELIMINA As String = "sp_CategoriaProduttoreNew_Elimina"
        Private Const SP_CATEGORIAPRONEW_CARICA2 As String = "sp_CategoriaProduttoreNew_Carica2"
        Private Const SP_CATEGORIAPRONEW_VERIFICA As String = "sp_CategoriaProduttoreNew_Verifica"
        Private Const SP_CATEGORIAPRONEW_LISTA As String = "sp_CategoriaProduttoreNew_Lista"
        Private Const SP_CATEGORIAPRONEW_AGGIORNADISATTIVA As String = "sp_CategoriaProduttoreNew_AggiornaDisattiva"

        Public Overrides Function CreanuovaCategoriaProduttoreNew(ByVal nuovaCategoriaPro As Categoria_ProduttoreNew) As Integer

            If nuovaCategoriaPro Is Nothing Then
                Throw New ArgumentNullException("nuovaCategoriaPro")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, nuovaCategoriaPro.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovaCategoriaPro.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Costo", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoriaPro.Costo)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoriaPro.Peso)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaCategoriaPro.Professionale)
            AddParamToSQLCmd(sqlCmd, "@ValoreDiForecast", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoriaPro.ValoreDiForecast)
            AddParamToSQLCmd(sqlCmd, "@Disattiva", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaCategoriaPro.Disattiva)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaCategoriaProduttoreNew(ByVal Id As Integer) As Categoria_ProduttoreNew

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_CARICA)

            Dim categoriaProduttoreList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categoriaProduttoreList)

            If categoriaProduttoreList.Count > 0 Then
                Return categoriaProduttoreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaCategoriaProduttoreNew(ByVal categoriaProDaAggiornare As Categoria_ProduttoreNew) As Boolean

            If categoriaProDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("categoriaProDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaAggiornare.IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Costo", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaProDaAggiornare.Costo)
            AddParamToSQLCmd(sqlCmd, "@Peso", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaProDaAggiornare.Peso)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, categoriaProDaAggiornare.Professionale)
            AddParamToSQLCmd(sqlCmd, "@ValoreDiForecast", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaProDaAggiornare.ValoreDiForecast)
            AddParamToSQLCmd(sqlCmd, "@Disattiva", SqlDbType.Bit, 0, ParameterDirection.Input, categoriaProDaAggiornare.Disattiva)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaCategoriaProduttoreNew(ByVal categoriaProDaEliminare As Categoria_ProduttoreNew) As Boolean

            If categoriaProDaEliminare Is Nothing Then
                Throw New ArgumentNullException("categoriaProDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaProDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_ELIMINA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function

        Public Overrides Function CaricaCategoriaProduttoreNEW(ByVal IdCategoria As Integer, IdProduttore As Integer, ByVal Professionale As Boolean) As Categoria_ProduttoreNew

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, Professionale)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_CARICA2)

            Dim categoriaProduttoreList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categoriaProduttoreList)

            If categoriaProduttoreList.Count > 0 Then
                Return categoriaProduttoreList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function VerificaCategoriaProduttoreNew(ByVal IdCategoria As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, IdCategoria)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_VERIFICA)

            Dim categoriaClienteList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categoriaClienteList)

            If categoriaClienteList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function ListaCategoriaProduttoreNew(ByVal IdProduttore As Integer, ByVal Professionale As Boolean) As List(Of Categoria_ProduttoreNew)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Professionale", SqlDbType.Bit, 0, ParameterDirection.Input, Professionale)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_LISTA)

            Dim categoriaProduttoreList As New List(Of Categoria_ProduttoreNew)()
            TExecuteReaderCmd(Of Categoria_ProduttoreNew)(sqlCmd, AddressOf TGenerateCategoriaProduttoreNewListFromReader(Of Categoria_ProduttoreNew), categoriaProduttoreList)

            If categoriaProduttoreList.Count > 0 Then
                Return categoriaProduttoreList
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaCategorieProduttori(ByVal IdCategoria As Integer, ByVal Disattiva As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdCategoria", SqlDbType.Int, 0, ParameterDirection.Input, IdCategoria)
            AddParamToSQLCmd(sqlCmd, "@Disattiva", SqlDbType.Bit, 0, ParameterDirection.Input, Disattiva)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIAPRONEW_AGGIORNADISATTIVA)

            ExecuteScalarCmd(sqlCmd)
            Return True


        End Function

        ' CATEGORIA
        Private Const SP_CATEGORIANEW_CREA As String = "sp_CategoriaNew_Crea"
        Private Const SP_CATEGORIANEW_CARICA As String = "sp_CategoriaNew_Carica"
        Private Const SP_CATEGORIANEW_AGGIORNA As String = "sp_CategoriaNew_Aggiorna"
        Private Const SP_CATEGORIANEW_ELIMINA As String = "sp_CategoriaNew_Elimina"
        Private Const SP_CATEGORIANEW_LISTA As String = "sp_CategoriaNew_Lista3"

        Public Overrides Function CreanuovaCategoriaNew(ByVal nuovaCategoria As CategoriaNew) As Integer

            If nuovaCategoria Is Nothing Then
                Throw New ArgumentNullException("nuovaCategoria")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Nome", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.Nome)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Valore", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoria.Valore)
            AddParamToSQLCmd(sqlCmd, "@IdMacrocategoria", SqlDbType.Int, 0, ParameterDirection.Input, nuovaCategoria.IdMacrocategoria)
            AddParamToSQLCmd(sqlCmd, "@Codifica", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.Codifica)
            AddParamToSQLCmd(sqlCmd, "@Raggruppamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaCategoria.Raggruppamento)
            AddParamToSQLCmd(sqlCmd, "@DataModifica", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovaCategoria.DataModifica)
            AddParamToSQLCmd(sqlCmd, "@PesoPerUnita", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaCategoria.PesoPerUnita)
            AddParamToSQLCmd(sqlCmd, "@Disattiva", SqlDbType.Bit, 0, ParameterDirection.Input, nuovaCategoria.Disattiva)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIANEW_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaCategoriaNew(ByVal Id As Integer) As CategoriaNew

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIANEW_CARICA)

            Dim categoriaList As New List(Of CategoriaNew)()
            TExecuteReaderCmd(Of CategoriaNew)(sqlCmd, AddressOf TGenerateCategoriaNewListFromReader(Of Categoria), categoriaList)

            If categoriaList.Count > 0 Then
                Return categoriaList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaCategoriaNew(ByVal categoriaDaAggiornare As CategoriaNew) As Boolean

            If categoriaDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("categoriaDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Nome", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.Nome)
            AddParamToSQLCmd(sqlCmd, "@TipoDiDato", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.TipoDiDato)
            AddParamToSQLCmd(sqlCmd, "@Valore", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaDaAggiornare.Valore)
            AddParamToSQLCmd(sqlCmd, "@IdMacrocategoria", SqlDbType.Int, 0, ParameterDirection.Input, categoriaDaAggiornare.IdMacrocategoria)
            AddParamToSQLCmd(sqlCmd, "@Codifica", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.Codifica)
            AddParamToSQLCmd(sqlCmd, "@Raggruppamento", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoriaDaAggiornare.Raggruppamento)
            AddParamToSQLCmd(sqlCmd, "@DataModifica", SqlDbType.DateTime, 0, ParameterDirection.Input, categoriaDaAggiornare.DataModifica)
            AddParamToSQLCmd(sqlCmd, "@PesoPerUnita", SqlDbType.Decimal, 0, ParameterDirection.Input, categoriaDaAggiornare.PesoPerUnita)
            AddParamToSQLCmd(sqlCmd, "@Disattiva", SqlDbType.Bit, 0, ParameterDirection.Input, categoriaDaAggiornare.Disattiva)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIANEW_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaCategoriaNew(ByVal categoriaDaEliminare As CategoriaNew) As Boolean

            If categoriaDaEliminare Is Nothing Then
                Throw New ArgumentNullException("categoriaDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, categoriaDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIANEW_ELIMINA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function

         Public Overrides Function ListaCategorieNew() As List(Of CategoriaNew)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CATEGORIANEW_LISTA)

            Dim categoriaList As New List(Of CategoriaNew)()
            TExecuteReaderCmd(Of CategoriaNew)(sqlCmd, AddressOf TGenerateCategoriaNewListFromReader(Of Categoria), categoriaList)

            If categoriaList.Count > 0 Then
                Return categoriaList
            Else
                Return Nothing
            End If

        End Function


        Public overrides Function IsPileEnabled(idProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@idProduttore", SqlDbType.Int, 0, ParameterDirection.Input, idProduttore)            

            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_CategoriaNew_IsPileEnable")

            Dim categoriaList As New List(Of CategoriaNew)()
            TExecuteReaderCmd(Of CategoriaNew)(sqlCmd, AddressOf TGenerateCategoriaNewListFromReader(Of Categoria), categoriaList)

            If categoriaList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public overrides Function IsAEEEnabled(idProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@idProduttore", SqlDbType.Int, 0, ParameterDirection.Input, idProduttore)            

            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_CategoriaNew_IsAEEEnable")

            Dim categoriaList As New List(Of CategoriaNew)()
            TExecuteReaderCmd(Of CategoriaNew)(sqlCmd, AddressOf TGenerateCategoriaNewListFromReader(Of Categoria), categoriaList)

            If categoriaList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public overrides Function IsVeicoliEnabled(idProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@idProduttore", SqlDbType.Int, 0, ParameterDirection.Input, idProduttore)            

            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_CategoriaNew_IsVeicoliEnable")

            Dim categoriaList As New List(Of CategoriaNew)()
            TExecuteReaderCmd(Of CategoriaNew)(sqlCmd, AddressOf TGenerateCategoriaNewListFromReader(Of Categoria), categoriaList)

            If categoriaList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Overrides Function IsIndustrialiEnabled(idProduttore As Integer) As Boolean

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@idProduttore", SqlDbType.Int, 0, ParameterDirection.Input, idProduttore)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, "sp_CategoriaNew_IsIndustrialiEnable")

            Dim categoriaList As New List(Of CategoriaNew)()
            TExecuteReaderCmd(Of CategoriaNew)(sqlCmd, AddressOf TGenerateCategoriaNewListFromReader(Of Categoria), categoriaList)

            If categoriaList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        ' FASCIA DI PESO
        Private Const SP_FASCIADIPESO_CREA As String = "sp_FasciaDiPeso_Crea"
        Private Const SP_FASCIADIPESO_CARICA As String = "sp_FasciaDiPeso_Carica"
        Private Const SP_FASCIADIPESO_AGGIORNA As String = "sp_FasciaDiPeso_Aggiorna"
        Private Const SP_FASCIADIPESO_ELIMINA As String = "sp_FasciaDiPeso_Elimina"
        Private Const SP_FASCIADIPESO_Verifica As String = "sp_Pannello_VerificafasciaDiPeso"
        Private Const SP_FASCIADIPESO_Verifica2 As String = "sp_FasciaDiPeso_Verifica"

        Public Overrides Function CreaNuovaFasciaDiPeso(ByVal nuovaFasciaDiPeso As FasciaDiPeso) As Integer

            If nuovaFasciaDiPeso Is Nothing Then
                Throw New ArgumentNullException("nuovafasciaDipeso")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaFasciaDiPeso.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@DataUltMod", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovaFasciaDiPeso.DataUltModifica)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_FASCIADIPESO_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaFasciaDiPeso(ByVal Id As Integer) As FasciaDiPeso

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_FASCIADIPESO_CARICA)

            Dim fasciaDiPesoList As New List(Of FasciaDiPeso)()
            TExecuteReaderCmd(Of FasciaDiPeso)(sqlCmd, AddressOf TGenerateFasciaDipesoListFromReader(Of FasciaDiPeso), fasciaDiPesoList)

            If fasciaDiPesoList.Count > 0 Then
                Return fasciaDiPesoList(0)
            Else
                Return Nothing
            End If


        End Function

        Public Overrides Function AggiornaFasciaDiPeso(ByVal fasciaDiPesoDaAggiornare As FasciaDiPeso) As Boolean

            If fasciaDiPesoDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("fasciaDiPesoDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 255, ParameterDirection.Input, fasciaDiPesoDaAggiornare.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@DataUltModifica", SqlDbType.DateTime, 0, ParameterDirection.Input, fasciaDiPesoDaAggiornare.DataUltModifica)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, fasciaDiPesoDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_FASCIADIPESO_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaFasciaDiPeso(ByVal fasciaDiPesoDaEliminare As FasciaDiPeso) As Boolean

            If fasciaDiPesoDaEliminare Is Nothing Then
                Throw New ArgumentNullException("fasciaDiPesoDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, fasciaDiPesoDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_FASCIADIPESO_ELIMINA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function

        Public Overrides Function VerificaFasciaDiPeso(ByVal IdFascia As Integer) As Boolean

            ' Verifica non sia presente alcun pannello con la fasdcia di peso indicata

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdFascia", SqlDbType.Int, 0, ParameterDirection.Input, IdFascia)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_FASCIADIPESO_VERIFICA)

            Dim PannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function VerificaFasciaDiPeso(ByVal fasciaDiPesoDaVerificare As FasciaDiPeso) As Boolean

            ' Verifica non sia presente alcun abbinamento con la fasdcia di peso indicata

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdFascia", SqlDbType.Int, 0, ParameterDirection.Input, fasciaDiPesoDaVerificare.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_FASCIADIPESO_Verifica2)

            Dim fasciaDiPesoList As New List(Of FasciaDiPeso)()
            TExecuteReaderCmd(Of FasciaDiPeso)(sqlCmd, AddressOf TGenerateFasciaDipesoListFromReader(Of FasciaDiPeso), fasciaDiPesoList)

            If fasciaDiPesoList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        ' TIPOLOGIA CELLA
        Private Const SP_TIPOLOGIACELLA_CREA As String = "sp_TipologiaCella_Crea"
        Private Const SP_TIPOLOGIACELLA_CARICA As String = "sp_TipologiaCella_Carica"
        Private Const SP_TIPOLOGIACELLA_AGGIORNA As String = "sp_TipologiaCella_Aggiorna"
        Private Const SP_TIPOLOGIACELLA_ELIMINA As String = "sp_TipologiaCella_Elimina"
        Private Const SP_TIPOLOGIACELLA_VERIFICA As String = "sp_Pannello_VerificaTipologiaCella"
        Private Const SP_TIPOLOGIACELLA_VERIFICA2 As String = "sp_TipologiaCella_Verifica"

        Public Overrides Function CreaNuovaTipologiaCella(ByVal nuovaTipologiaCella As TipologiaCella) As Integer

            If nuovaTipologiaCella Is Nothing Then
                Throw New ArgumentNullException("nuovaTipologiaCella")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 255, ParameterDirection.Input, nuovaTipologiaCella.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@DataUltModifica", SqlDbType.DateTime, 0, ParameterDirection.Input, nuovaTipologiaCella.DataUltModifica)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_TIPOLOGIACELLA_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaTipologiaCella(ByVal Id As Integer) As TipologiaCella

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_TIPOLOGIACELLA_CARICA)

            Dim TipologiaCellaList As New List(Of TipologiaCella)()
            TExecuteReaderCmd(Of TipologiaCella)(sqlCmd, AddressOf TGenerateTipologiaCellaListFromReader(Of TipologiaCella), TipologiaCellaList)

            If TipologiaCellaList.Count > 0 Then
                Return TipologiaCellaList(0)
            Else
                Return Nothing
            End If


        End Function

        Public Overrides Function AggiornaTipologiaCella(ByVal tipologiaCellaDaAggiornare As TipologiaCella) As Boolean

            If tipologiaCellaDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("tipologiaCellaDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@Descrizione", SqlDbType.NVarChar, 255, ParameterDirection.Input, tipologiaCellaDaAggiornare.Descrizione)
            AddParamToSQLCmd(sqlCmd, "@DataUltModifica", SqlDbType.DateTime, 0, ParameterDirection.Input, tipologiaCellaDaAggiornare.DataUltModifica)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, tipologiaCellaDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_TIPOLOGIACELLA_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaTipologiaCella(ByVal TipologiaCellaDaEliminare As TipologiaCella) As Boolean

            If TipologiaCellaDaEliminare Is Nothing Then
                Throw New ArgumentNullException("TipologiaCellaDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, TipologiaCellaDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_TIPOLOGIACELLA_ELIMINA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function

        Public Overrides Function VerificaTipologiaCella(ByVal IdTipologia As Integer) As Boolean

            ' Verifica non sia presente alcun pannello con la fascia di peso indicata

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdTipologia", SqlDbType.Int, 0, ParameterDirection.Input, IdTipologia)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_TIPOLOGIACELLA_VERIFICA)

            Dim PannelloList As New List(Of Pannello)()
            TExecuteReaderCmd(Of Pannello)(sqlCmd, AddressOf TGeneratePannelloListFromReader(Of Pannello), PannelloList)

            If PannelloList.Count > 0 Then
                Return PannelloList.Count
            Else
                Return 0
            End If

        End Function

        Public Overrides Function VerificaTipologiaCella(ByVal TipologiaCellaDaVerificare As TipologiaCella) As Boolean

            ' Verifica non sia presente alcun pannello con la fasdcia di peso indicata

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdTipologia", SqlDbType.Int, 0, ParameterDirection.Input, TipologiaCellaDaVerificare.Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_TIPOLOGIACELLA_VERIFICA2)

            Dim TipologiaCellaList As New List(Of TipologiaCella)()
            TExecuteReaderCmd(Of TipologiaCella)(sqlCmd, AddressOf TGenerateTipologiaCellaListFromReader(Of TipologiaCella), TipologiaCellaList)

            If TipologiaCellaList.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function


        ' ABBINAMENTO TIPOLOGIA CELLA / FASCIA DI PESO 
        Private Const SP_ABBINAMENTO_CREA As String = "sp_Abbinamento_Crea"
        Private Const SP_ABBINAMENTO_CARICA As String = "sp_Abbinamento_Carica"
        Private Const SP_ABBINAMENTO_CARICADAPRODUTTORE As String = "sp_Abbinamento_CaricaDaProduttore"
        Private Const SP_ABBINAMENTO_AGGIORNA As String = "sp_Abbinamento_Aggiorna"
        Private Const SP_ABBINAMENTO_ELIMINA As String = "sp_Abbinamento_Elimina"

        Public Overrides Function CreaNuovoAbbinamento(ByVal nuovaAbbinamento As AbbinamentoTipologiaFascia) As Integer

            If nuovaAbbinamento Is Nothing Then
                Throw New ArgumentNullException("nuovaAbbinamento")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 255, ParameterDirection.Input, nuovaAbbinamento.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@IdTipologia", SqlDbType.Int, 0, ParameterDirection.Input, nuovaAbbinamento.IdTipologiaCella)
            AddParamToSQLCmd(sqlCmd, "@IdFascia", SqlDbType.Int, 0, ParameterDirection.Input, nuovaAbbinamento.IdFasciaDiPeso)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Decimal, 0, ParameterDirection.Input, nuovaAbbinamento.CostoMatricola)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ABBINAMENTO_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaAbbinamento(ByVal Id As Integer) As AbbinamentoTipologiaFascia

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ABBINAMENTO_CARICA)

            Dim AbbinamentoList As New List(Of AbbinamentoTipologiaFascia)()
            TExecuteReaderCmd(Of AbbinamentoTipologiaFascia)(sqlCmd, AddressOf TGenerateAbbinamentoListFromReader(Of AbbinamentoTipologiaFascia), AbbinamentoList)

            If AbbinamentoList.Count > 0 Then
                Return AbbinamentoList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function CaricaAbbinamento(ByVal IdProduttore As Integer, ByVal IdTipologia As Integer, ByVal IdFascia As Integer) As AbbinamentoTipologiaFascia

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@IdTipologia", SqlDbType.Int, 0, ParameterDirection.Input, IdTipologia)
            AddParamToSQLCmd(sqlCmd, "@IdFascia", SqlDbType.Int, 0, ParameterDirection.Input, IdFascia)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ABBINAMENTO_CARICADAPRODUTTORE)

            Dim AbbinamentoList As New List(Of AbbinamentoTipologiaFascia)()
            TExecuteReaderCmd(Of AbbinamentoTipologiaFascia)(sqlCmd, AddressOf TGenerateAbbinamentoListFromReader(Of AbbinamentoTipologiaFascia), AbbinamentoList)

            If AbbinamentoList.Count > 0 Then
                Return AbbinamentoList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaAbbinamento(ByVal abbinamentoDaAggiornare As AbbinamentoTipologiaFascia) As Boolean

            If abbinamentoDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("abbinamentoDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 255, ParameterDirection.Input, abbinamentoDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@IdTipologia", SqlDbType.Int, 0, ParameterDirection.Input, abbinamentoDaAggiornare.IdTipologiaCella)
            AddParamToSQLCmd(sqlCmd, "@IdFascia", SqlDbType.Int, 0, ParameterDirection.Input, abbinamentoDaAggiornare.IdFasciaDiPeso)
            AddParamToSQLCmd(sqlCmd, "@CostoMatricola", SqlDbType.Decimal, 0, ParameterDirection.Input, abbinamentoDaAggiornare.CostoMatricola)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, abbinamentoDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ABBINAMENTO_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        Public Overrides Function EliminaAbbinamento(ByVal AbbinamentoDaEliminare As AbbinamentoTipologiaFascia) As Boolean

            If AbbinamentoDaEliminare Is Nothing Then
                Throw New ArgumentNullException("AbbinamentoDaEliminare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, AbbinamentoDaEliminare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ABBINAMENTO_ELIMINA)
            ExecuteScalarCmd(sqlCmd)


            Return True

        End Function


        ' CERTIFICATO DI ADESIONE
        Private Const SP_CERTIFICATO_CREA As String = "sp_Certificato_Crea"
        Private Const SP_CERTIFICATO_CARICA As String = "sp_Certificato_Carica"
        Private Const SP_CERTIFICATO_CARICA2 As String = "sp_Certificato_Carica2"
        Private Const SP_CERTIFICATO_AGGIORNA As String = "sp_Certificato_Aggiorna"

        Public Overrides Function CreaNuovoCertificato(ByVal nuovoCertificato As Certificato) As Integer

            If nuovoCertificato Is Nothing Then
                Throw New ArgumentNullException("nuovoCertificato")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, nuovoCertificato.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, nuovoCertificato.Anno)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 20, ParameterDirection.Input, nuovoCertificato.Protocollo)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CERTIFICATO_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaCertificato(ByVal IdCertificato As Integer) As Certificato

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, IdCertificato)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CERTIFICATO_CARICA)

            Dim CertificatoList As New List(Of Certificato)()
            TExecuteReaderCmd(Of Certificato)(sqlCmd, AddressOf TGenerateCertificatoListFromReader(Of Certificato), CertificatoList)

            If CertificatoList.Count > 0 Then
                Return CertificatoList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function CaricaCertificato2(ByVal IdProduttore As Integer, ByVal Anno As Integer) As Certificato

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, Anno)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CERTIFICATO_CARICA2)

            Dim CertificatoList As New List(Of Certificato)()
            TExecuteReaderCmd(Of Certificato)(sqlCmd, AddressOf TGenerateCertificatoListFromReader(Of Certificato), CertificatoList)

            If CertificatoList.Count > 0 Then
                Return CertificatoList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaCertificato(ByVal certificatoDaAggiornare As Certificato) As Boolean

            If certificatoDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("certificatoDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 255, ParameterDirection.Input, certificatoDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@Anno", SqlDbType.Int, 0, ParameterDirection.Input, certificatoDaAggiornare.Anno)
            AddParamToSQLCmd(sqlCmd, "@Protocollo", SqlDbType.NVarChar, 20, ParameterDirection.Input, certificatoDaAggiornare.Protocollo)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, certificatoDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_CERTIFICATO_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        ' Log iscrizione categoria 4.14
        Private Const SP_LOGISCRIZIONE_CREA As String = "sp_LogIscrizione_Crea"
        Private Const SP_LOGISCRIZIONE_CARICA As String = "sp_LogIscrizione_Carica"
        Private Const SP_LOGISCRIZIONE_CARICA2 As String = "sp_LogIscrizione_Carica2"
        Private Const SP_LOGISCRIZIONE_AGGIORNA As String = "sp_LogIscrizione_Aggiorna"

        Public Overrides Function CreaNuovoLogIscrizione(ByVal nuovoLogIscrizione As LogIscrizione) As Integer

            If nuovoLogIscrizione Is Nothing Then
                Throw New ArgumentNullException("nuovoLogIscrizione")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 255, ParameterDirection.Input, nuovoLogIscrizione.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@DataIscrizione", SqlDbType.Date, 0, ParameterDirection.Input, nuovoLogIscrizione.DataIscrizione)
            AddParamToSQLCmd(sqlCmd, "@DataDisiscrizione", SqlDbType.Date, 0, ParameterDirection.Input, nuovoLogIscrizione.DataDisiscrizione)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOGISCRIZIONE_CREA)
            ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)

        End Function

        Public Overrides Function CaricaLogIscrizione(ByVal IdLogIscrizione As Integer) As LogIscrizione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdLogIscrizione", SqlDbType.Int, 0, ParameterDirection.Input, IdLogIscrizione)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOGISCRIZIONE_CARICA)

            Dim LogIscrizioneList As New List(Of LogIscrizione)()
            TExecuteReaderCmd(Of LogIscrizione)(sqlCmd, AddressOf TGenerateLogIscrizioneListFromReader(Of LogIscrizione), LogIscrizioneList)

            If LogIscrizioneList.Count > 0 Then
                Return LogIscrizioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function CaricaLogIscrizione2(ByVal IdProduttore As Integer) As LogIscrizione

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 0, ParameterDirection.Input, IdProduttore)
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOGISCRIZIONE_CARICA2)

            Dim LogIscrizioneList As New List(Of LogIscrizione)()
            TExecuteReaderCmd(Of LogIscrizione)(sqlCmd, AddressOf TGenerateLogIscrizioneListFromReader(Of LogIscrizione), LogIscrizioneList)

            If LogIscrizioneList.Count > 0 Then
                Return LogIscrizioneList(0)
            Else
                Return Nothing
            End If

        End Function

        Public Overrides Function AggiornaLogIscrizione(ByVal logIscrizioneDaAggiornare As LogIscrizione) As Boolean

            If logIscrizioneDaAggiornare Is Nothing Then
                Throw New ArgumentNullException("logIscrizioneDaAggiornare")
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand()
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParamToSQLCmd(sqlCmd, "@IdProduttore", SqlDbType.Int, 255, ParameterDirection.Input, logIscrizioneDaAggiornare.IdProduttore)
            AddParamToSQLCmd(sqlCmd, "@DataIscrizione", SqlDbType.Date, 0, ParameterDirection.Input, logIscrizioneDaAggiornare.DataIscrizione)
            AddParamToSQLCmd(sqlCmd, "@DataDisiscrizione", SqlDbType.Date, 0, ParameterDirection.Input, logIscrizioneDaAggiornare.DataDisiscrizione)

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, logIscrizioneDaAggiornare.Id)

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_LOGISCRIZIONE_AGGIORNA)
            ExecuteScalarCmd(sqlCmd)

            Return True

        End Function

        ' SQL HELPER METHODS

        Private Sub AddParamToSQLCmd(ByVal sqlCmd As SqlCommand, ByVal paramId As String, ByVal sqlType As SqlDbType, _
                                        ByVal paramSize As Integer, ByVal paramDirection As ParameterDirection, ByVal paramvalue As Object)

            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("sqlCmd")
            End If

            If paramId = String.Empty Then
                Throw New ArgumentOutOfRangeException("paramId")
            End If

            Dim newSqlParam As SqlParameter = New SqlParameter()
            newSqlParam.ParameterName = paramId
            newSqlParam.SqlDbType = sqlType
            newSqlParam.Direction = paramDirection

            If paramSize > 0 Then
                newSqlParam.Size = paramSize
            End If

            If Not paramvalue Is Nothing Then
                newSqlParam.Value = paramvalue
            End If

            sqlCmd.Parameters.Add(newSqlParam)

        End Sub

        Private Sub ExecuteScalarCmd(ByVal sqlCmd As SqlCommand)

            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("sqlCmd")
            End If

            Using cn As SqlConnection = New SqlConnection(Me.ConnectionString)

                sqlCmd.Connection = cn
                cn.Open()
                sqlCmd.ExecuteScalar()

            End Using

        End Sub

        Private Function ExecuteScalarCmd2(ByVal sqlCmd As SqlCommand) As Object
            ' Validate Command Properties
            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If
            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("sqlCmd")
            End If
            Dim result As Object = Nothing

            Dim cn As New SqlConnection(Me.ConnectionString)
            Try
                sqlCmd.Connection = cn
                cn.Open()
                result = sqlCmd.ExecuteScalar()
            Finally
                cn.Dispose()
            End Try

            Return result
        End Function 'ExecuteScalarCmd

        Private Sub SetCommandType(ByVal sqlCmd As SqlCommand, ByVal cmdType As CommandType, ByVal cmdText As String)

            sqlCmd.CommandType = cmdType
            sqlCmd.CommandText = cmdText

        End Sub

        Private Sub TExecuteReaderCmd(Of T)(ByVal sqlCmd As SqlCommand, ByVal gcfr As TGenerateListFromReader(Of T), ByRef List As List(Of T))

            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("sqlCmd")
            End If

            Using cn As SqlConnection = New SqlConnection(Me.ConnectionString)

                sqlCmd.Connection = cn 'aspnet_Impianto_CaricaDaId'.
                cn.Open()
                gcfr(sqlCmd.ExecuteReader(), List)

            End Using

        End Sub


        ' LIST HELPER METHODS

        Private Sub TGenerateProduttoreListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef produttoreList As List(Of Produttore))

            Dim Periodicita As Integer
            Dim Attivo As Boolean
            Dim CostoMatricola As Decimal
            Dim ServizioRappresentanza As Decimal
            Dim Sconto As Decimal

            Do While returnData.Read()

                If returnData("Periodicita") Is DBNull.Value Then
                    Periodicita = 0
                Else
                    Periodicita = returnData("Periodicita")
                End If

                If returnData("Attivo") Is DBNull.Value Then
                    Attivo = False
                Else
                    Attivo = returnData("Attivo")
                End If

                If returnData("CostoMatricola") Is DBNull.Value Then
                    CostoMatricola = 0
                Else
                    CostoMatricola = CDec(returnData("CostoMatricola"))
                End If

                If returnData("ServizioDiRappresentanza") Is DBNull.Value Then
                    ServizioRappresentanza = 0
                Else
                    ServizioRappresentanza = CDec(returnData("ServizioDiRappresentanza"))
                End If

                If returnData("Sconto") Is DBNull.Value Then
                    Sconto = 0 
                Else
                    Sconto = CDec(returnData("Sconto"))
                End If

                Dim Produttore As Produttore = New Produttore(CInt(returnData("Id")), CStr(returnData("Codice")).ToString, "" + CStr("" + returnData("RagioneSociale")).ToString, CStr("" + returnData("Email")).ToString, Periodicita, Attivo, CStr("" + returnData("Indirizzo")), _
                                                              CStr("" + returnData("Cap")), CStr("" + returnData("Citta")), CStr("" + returnData("Rappresentante")), CStr("" + returnData("Note")), CStr("" + returnData("EmailNotifiche")), _
                                                              CStr("" + returnData("CodiceFiscale")), CBool(returnData("Domestico")), CBool(returnData("Professionale")), CBool(returnData("EscludiMassivo")), _
                                                              CInt(returnData("MeseAdesione")), CDec(returnData("QuotaConsortile")), CStr("" + returnData("CodiceSDI")), CStr("" + returnData("PartitaIVA")), _
                                                                CStr("" + returnData("PEC")), CStr("" + returnData("IBAN")), CStr("" + returnData("Telefono")), CStr("" + returnData("RegistroAEE")), _
                                                                CStr("" + returnData("RegistroPile")), CostoMatricola, ServizioRappresentanza, Sconto)

                produttoreList.Add(Produttore)

            Loop

        End Sub

        Private Sub TGeneratePannelloListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef pannelloList As List(Of Pannello))

            Do While returnData.Read()

                Dim DataConformita As Object
                Dim Dismesso As Boolean
                Dim IdfasciaDiPeso As Integer
                Dim IdTipologiaCella As Integer
                Dim CostoMatricola As Decimal
                Dim DataRitiro As Object

                DataConformita = returnData("DataConformita")
                DataRitiro = returnData("DataRitiro")

                If returnData("DataConformita") Is DBNull.Value Then
                    DataConformita = DefaultValues.GetDateTimeMinValue
                Else
                    DataConformita = CDate(returnData("DataConformita"))
                End If

                If returnData("Dismesso") Is DBNull.Value Then
                    Dismesso = 0
                Else
                    Dismesso = CBool(returnData("Dismesso"))
                End If

                If returnData("IdFasciaDiPeso") Is DBNull.Value Then
                    IdFasciaDiPeso = 0
                Else
                    IdFasciaDiPeso = CInt(returnData("IdFasciaDiPeso"))
                End If

                If returnData("IdTipologiaCella") Is DBNull.Value Then
                    IdTipologiaCella = 0
                Else
                    IdTipologiaCella = CInt(returnData("IdTipologiaCella"))
                End If

                If returnData("CostoMatricola") Is DBNull.Value Then
                    CostoMatricola = 0
                Else
                    CostoMatricola = CDec(returnData("CostoMatricola"))
                End If

                If returnData("DataRitiro") Is DBNull.Value Then
                    DataRitiro = DefaultValues.GetDateTimeMinValue
                Else
                    DataRitiro = CDate(returnData("DataRitiro"))
                End If

                Dim Pannello As Pannello = New Pannello(CInt(returnData("Id")), CStr(returnData("Matricola")).ToString, "" + CStr("" + returnData("Modello")).ToString, CDec(returnData("Peso")), _
                                                        CDate(returnData("DataInizioGaranzia")), CInt(returnData("IdMarca")), CStr("" + returnData("Produttore")).ToString, CInt(returnData("IdImpianto")), _
                                                        CDate(returnData("DataCaricamento")), CBool(returnData("Conforme")), CStr("" + returnData("Protocollo")).ToString, CInt(returnData("NrComunicazione")), _
                                                        CInt(returnData("Anno")), DataConformita, Dismesso, IdfasciaDiPeso, IdTipologiaCella, CostoMatricola, DataRitiro, CStr("" + returnData("NumeroFIR")).ToString)
                pannelloList.Add(Pannello)

            Loop

        End Sub

        Private Sub TGenerateClienteListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef clienteList As List(Of Cliente))

            Do While returnData.Read()

                Dim Periodicita As Integer
                Dim Attivo As Boolean

                If returnData("Periodicita") Is DBNull.Value Then
                    Periodicita = 0
                Else
                    Periodicita = returnData("Periodicita")
                End If

                If returnData("Attivo") Is DBNull.Value Then
                    Attivo = 0
                Else
                    Attivo = returnData("Attivo")
                End If

                Dim Cliente As Cliente = New Cliente(CInt(returnData("IdCliente")), CStr("" + returnData("RagioneSociale")), CStr("" + returnData("Indirizzo")), CStr("" + returnData("Cap")), CStr("" + returnData("Citta")), CStr("" + returnData("Provincia")), CStr("" + returnData("Email")), CStr("" + returnData("Telefono")), CStr("" + returnData("Fax")), CStr("" + returnData("PartitaIva")), CStr("" + returnData("Contatto")), _
                                                     CStr("" + returnData("Cognome")), CStr("" + returnData("Nome")), CStr("" + returnData("CodiceFiscale")), Periodicita, Attivo, CStr("" + returnData("Note")))
                clienteList.Add(Cliente)

            Loop

        End Sub

        Private Sub TGenerateImpiantoListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef impiantoList As List(Of Impianto))

            Do While returnData.Read()


                Dim Impianto As Impianto = New Impianto(CInt(returnData("Id")), CStr("" + returnData("Codice")), CStr("" + returnData("Descrizione")), CStr("" + returnData("Indirizzo")), CStr("" + returnData("Cap")), CStr("" + returnData("Citta")), _
                                                CStr("" + returnData("Provincia")), CStr("" + returnData("Latitudine")), CStr("" + returnData("Longitudine")), CInt(returnData("IdCliente")), CDate(returnData("DataCreazione")), _
                                                CStr("" + returnData("Responsabile").ToString), CStr("" + returnData("NRPraticaGSE").ToString), CStr("" + returnData("Regione").ToString), CStr("" + returnData("ContoEnergia").ToString), CDate(returnData("DataEntrataInEsercizio")), _
                                                CStr("" + returnData("Attestato").ToString), CDate(returnData("DataAttestato")), CInt(returnData("NRAttestato")), _
                                                CStr("" + returnData("NomeProduttore").ToString))
                impiantoList.Add(Impianto)

            Loop

        End Sub

        Private Sub TGenerateUtenteClienteListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef UtenteClienteList As List(Of UtenteCliente))

            Do While returnData.Read()


                Dim UtenteCliente As UtenteCliente = New UtenteCliente(CInt(returnData("Id")), returnData("UserId"), CInt(returnData("IdCliente")))
                UtenteClienteList.Add(UtenteCliente)

            Loop

        End Sub

        Private Sub TGenerateUtenteProduttoreListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef UtenteProduttoreList As List(Of UtenteProduttore))

            Do While returnData.Read()


                Dim UtenteProduttore As UtenteProduttore = New UtenteProduttore(CInt(returnData("Id")), returnData("UserId"), CInt(returnData("IdProduttore")))
                UtenteProduttoreList.Add(UtenteProduttore)

            Loop

        End Sub

        Private Sub TGenerateErroreImportazioneListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef erroreList As List(Of ErroreImportazione))

            Do While returnData.Read()

                Dim DataRitiro As Date

                If returnData("DataRitiro") Is DBNull.Value Then
                    DataRitiro = DefaultValues.GetDateTimeMinValue
                Else
                    DataRitiro = CDate(returnData("DataRitiro"))
                End If

                Dim ErroreImportazione As ErroreImportazione = New ErroreImportazione(CInt(returnData("Id")), CStr("" + returnData("Matricola")).ToString, CStr("" + returnData("Errore")).ToString, CBool(returnData("Importabile")), CStr("" + returnData("Modello")).ToString, CDec(returnData("Peso")), _
                                                                CDate(returnData("DataInizioGaranzia")), CInt(returnData("IdMarca")), CStr("" + returnData("Produttore")).ToString, CInt(returnData("IdImpianto")), CDate(returnData("DataCaricamento")), _
                                                                                      CStr(returnData("UserName")), CBool(returnData("InErrore")), CInt(returnData("AnnoCaricamento")), CInt(returnData("IdProduttore")), CInt(returnData("TipoImportazione")), _
                                                                                        CStr("" + returnData("Protocollo")), CStr("" + returnData("Impianto")), CStr("" + returnData("Stato")), CInt(returnData("IdTipologia")), CInt(returnData("Idfascia")), _
                                                                                        CDec(returnData("CostoMatricola")), DataRitiro, CStr("" + returnData("NumeroFIR")))

                erroreList.Add(ErroreImportazione)

            Loop

        End Sub

        Private Sub TGenerateProtocolloListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef ProtocolloList As List(Of Protocollo))

            Do While returnData.Read()

                Dim DataAttestato As Date
                Dim IdProduttore As Integer
                Dim DataFattura As Date
                Dim DataProforma As Date

                If returnData("DataAttestato") Is DBNull.Value Then
                    DataAttestato = DefaultValues.GetDateTimeMinValue
                Else
                    DataAttestato = CDate(returnData("DataAttestato"))
                End If

                If returnData("IdProduttore") Is DBNull.Value Then
                    IdProduttore = DefaultValues.GetCategoryIdMinValue
                Else
                    IdProduttore = CInt(returnData("IdProduttore"))
                End If

                If returnData("DataFattura") Is DBNull.Value Then
                    DataFattura = DefaultValues.GetDateTimeMinValue
                Else
                    DataFattura = CDate(returnData("DataFattura"))
                End If

                If returnData("DataProforma") Is DBNull.Value Then
                    DataProforma = DefaultValues.GetDateTimeMinValue
                Else
                    DataProforma = CDate(returnData("DataProforma"))
                End If

                Dim Protocollo As Protocollo = New Protocollo(CInt(returnData("Id")), CStr("" + returnData("Protocollo")), CDate(returnData("Data")), CStr("" + returnData("NrFattura")).ToString, _
                                                              CStr("" + returnData("CCT")).ToString, CStr("" + returnData("UserName")), DataAttestato, CStr("" + returnData("NrAttestato")), _
                                                              IdProduttore, CDec(returnData("CostoServizio")), DataFattura, CStr("" + returnData("NrProforma").ToString), DataProforma)
                ProtocolloList.Add(Protocollo)

            Loop

        End Sub

        Private Sub TGenerateCategoriaListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef categoriaList As List(Of Categoria))

            Do While returnData.Read()

                Dim Valore As Decimal

                If returnData("Valore") Is DBNull.Value Then
                    Valore = 0
                Else
                    Valore = returnData("Valore")
                End If


                Dim Categoria As Categoria = New Categoria(CInt(returnData("Id")), CStr("" + returnData("Nome")).ToString, CStr("" + returnData("TipoDiDato")).ToString, Valore, CInt(returnData("IdMacroCategoria")), CStr("" + returnData("Codifica")), CStr("" + returnData("Raggruppamento")), CDate(returnData("Data Modifica")))

                categoriaList.Add(Categoria)

            Loop

        End Sub

        Private Sub TGenerateCategoriaProduttoreListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef categoriaClienteList As List(Of Categoria_Produttore))

            Do While returnData.Read()
                Dim Costo As Decimal
                If returnData("Costo") Is DBNull.Value Then
                    Costo = 0
                Else
                    Costo = returnData("Costo")
                End If

                Dim Categoria_Produttore As Categoria_Produttore = New Categoria_Produttore(CInt(returnData("Id")), CInt(returnData("IdCategoria")), CInt(returnData("IdProduttore")), Costo, CDec(returnData("Peso")), _
                                                    CBool(returnData("Professionale")))

                categoriaClienteList.Add(Categoria_Produttore)

            Loop

        End Sub

        Private Sub TGenerateCategoriaNewListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef categoriaList As List(Of CategoriaNew))

            Do While returnData.Read()

                Dim Valore As Decimal
                Dim DataModifica As Date
                Dim PesoPerUnita As Decimal

                If returnData("Valore") Is DBNull.Value Then
                    Valore = 0
                Else
                    Valore = returnData("Valore")
                End If


                If returnData("Data Modifica") Is DBNull.Value Then
                    DataModifica = DefaultValues.GetDateTimeMinValue
                Else
                    DataModifica = CDate(returnData("Data Modifica"))
                End If

                If returnData("PesoPerUnita") Is DBNull.Value Then
                    PesoPerUnita = 0
                Else
                    PesoPerUnita = returnData("PesoPerUnita")
                End If

                Dim Categoria As CategoriaNew = New CategoriaNew(CInt(returnData("Id")), CStr("" + returnData("Nome")).ToString, CStr("" + returnData("TipoDiDato")).ToString, Valore, CInt(returnData("IdMacroCategoria")), CStr("" + returnData("Codifica")), CStr("" + returnData("Raggruppamento")), DataModifica, PesoPerUnita, CBool(returnData("Disattiva")))

                categoriaList.Add(Categoria)

            Loop

        End Sub

        Private Sub TGenerateCategoriaProduttoreNewListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef categoriaClienteList As List(Of Categoria_ProduttoreNew))

            Do While returnData.Read()
                Dim Costo As Decimal
                Dim ValoreForecast As Decimal

                If returnData("Costo") Is DBNull.Value Then
                    Costo = 0
                Else
                    Costo = returnData("Costo")
                End If

                If returnData("ValoreDiForecast") Is DBNull.Value Then
                    ValoreForecast = 0
                Else
                    ValoreForecast = returnData("ValoreDiForecast")
                End If

                Dim Categoria_Produttore As Categoria_ProduttoreNew = New Categoria_ProduttoreNew(CInt(returnData("Id")), CInt(returnData("IdCategoria")), CInt(returnData("IdProduttore")), _
					Costo, CDec(returnData("Peso")), CBool(returnData("Professionale")), ValoreForecast, Cbool(returnData("Disattiva")))

                categoriaClienteList.Add(Categoria_Produttore)

            Loop

        End Sub

        Private Sub TGenerateDichiarazioneListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef dichiarazioneList As List(Of Dichiarazione))

            Do While returnData.Read()

                Dim Importo As Decimal
                Dim DataFattura As Date

                If returnData("Importo") Is DBNull.Value Then
                    Importo = 0
                Else
                    Importo = returnData("Importo")
                End If

                If returnData("DataFattura") Is DBNull.Value Then
                    DataFattura = DefaultValues.GetDateTimeMinValue
                Else
                    DataFattura = returnData("DataFattura")
                End If

                Dim Dichiarazione As Dichiarazione = New Dichiarazione(CInt(returnData("Id")), CDate(returnData("Data")), CInt(returnData("IdProduttore")), CDate(returnData("DataRegistrazione")), CStr("" + returnData("Utente")), _
                                                                       CBool(returnData("Confermata")), CDate(returnData("DataConferma")), Importo, CBool(returnData("AutocertificazioneProdotta")), CBool(returnData("Fatturata")), CBool(returnData("Professionale")), _
                                                                           CBool(returnData("OldVersion")), CStr("" + returnData("NrFattura").ToString), DataFattura, Cbool(returnData("Autostimata")))

                dichiarazioneList.Add(Dichiarazione)

            Loop

        End Sub

        Private Sub TGenerateRigaDichiarazioneListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef rigadichiarazioneList As List(Of RigaDichiarazione))

            Dim DataAggiornamento As Date

            Do While returnData.Read()

                If returnData("DataAggiornamento") Is DBNull.Value Then
                    DataAggiornamento = DefaultValues.GetDateTimeMinValue
                Else
                    DataAggiornamento = returnData("DataAggiornamento")
                End If

                Dim RigaDichiarazione As RigaDichiarazione = New RigaDichiarazione(CInt(returnData("Id")), CInt(returnData("IdDichiarazione")), CInt(returnData("IdCategoria")), _
                    CStr("" + returnData("TipoDiDato")), CInt(returnData("Pezzi")), CDec(returnData("Kg")), CDec(returnData("KgDichiarazione")), CDec(returnData("CostoUnitario")), _
                    CDec(returnData("Importo")), CStr("" + returnData("UtenteAggiornamento")), DataAggiornamento)

                rigadichiarazioneList.Add(RigaDichiarazione)

            Loop

        End Sub

        Private Sub TGenerateRigaDichiarazioneRaggListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef rigadichiarazioneList As List(Of RigaDichiarazioneRaggruppate))

            Do While returnData.Read()

                Dim RigaDichiarazioneRagg As RigaDichiarazioneRaggruppate = New RigaDichiarazioneRaggruppate(CStr(returnData("Raggruppamento")), CDec(returnData("Totale")))

                rigadichiarazioneList.Add(RigaDichiarazioneRagg)

            Loop

        End Sub


        Private Sub TGenerateAutocertificazioneListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef autocertificazioneList As List(Of Autocertificazione))

            Do While returnData.Read()

                Dim DataConferma As Date
                Dim Rettificata As Boolean
				Dim DataFattura as Date

                If returnData("DataConferma") Is DBNull.Value Then
                    DataConferma = DefaultValues.GetDateTimeMinValue
                Else
                    DataConferma = returnData("DataConferma")
                End If

                If returnData("Rettificata") Is DBNull.Value Then
                    Rettificata = False
                Else
                    Rettificata = returnData("Rettificata")
                End If
				
				If returnData("DataFattura") Is DBNull.Value Then
                    DataFattura = DefaultValues.GetDateTimeMinValue
                Else
                    DataFattura = returnData("DataFattura")
                End If

                Dim Autocertificazione As Autocertificazione = New Autocertificazione(CInt(returnData("Id")), CInt(returnData("IdProduttore")), CInt(returnData("Anno")), _
                                                                                      CDate("" + returnData("Data")), CStr("" + returnData("PathFile").ToString), _
                                                                                      CStr("" + returnData("NomeFile").ToString), CStr("" + returnData("PathFilePile").ToString), _
                                                                                      CStr("" + returnData("NomeFilePile").ToString), CStr("" + returnData("PathFileIndustriali").ToString), _
                                                                                      CStr("" + returnData("NomeFileIndustriali").ToString), CStr("" + returnData("PathFileVeicoli").ToString), _
                                                                                      CStr("" + returnData("NomeFileVeicoli").ToString), CBool(returnData("UploadEseguito")), _
                                                                                      CBool(returnData("Confermata")), DataConferma, Rettificata, CStr("" + returnData("NrFattura").ToString), DataFattura)

                autocertificazioneList.Add(Autocertificazione)

            Loop

        End Sub

        Private Sub TGenerateRigaCertificazioneRaggListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef rigaCertificazioneList As List(Of RigaAutocertificazioneRaggruppate))

            Do While returnData.Read()

                Dim RigaAutocertRaggruppate As RigaAutocertificazioneRaggruppate = New RigaAutocertificazioneRaggruppate(CStr(returnData("Raggruppamento")), CDec(returnData("Totale")))

                rigaCertificazioneList.Add(RigaAutocertRaggruppate)

            Loop

        End Sub

        Private Sub TGenerateOpzioneListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef opzioneList As List(Of Opzione))

            Do While returnData.Read()

                Dim Opzione As Opzione = New Opzione(CInt(returnData("Id")), CStr("" + returnData("Oggetto")), CStr("" + returnData("TestoMail")), _
                                                     CStr("" + returnData("ServerSMTP")), CStr("" + returnData("UserID")), CStr(returnData("Password")), _
                                                     CBool(returnData("SSL")), CInt(returnData("Porta")), CStr("" + returnData("Mittente")), _
                                                     CStr("" + returnData("DestinatarioTest")), CStr("" + returnData("OggettoAutocertificazione")), _
                                                     CStr("" + returnData("TestoMailAutocertificazione")))

                opzioneList.Add(Opzione)

            Loop

        End Sub

        Private Sub TGenerateLogListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef logList As List(Of Log))

            Do While returnData.Read()

                Dim Log As Log = New Log(CInt(returnData("Id")), CDate(returnData("Data")), CDate(returnData("Ora")), CStr("" + returnData("Origine")), CStr("" + returnData("Descrizione")), CStr("" + returnData("Utente")), CBool(returnData("Letto")))

                logList.Add(Log)

            Loop

        End Sub

        Private Sub TGenerateListOfModelliFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef Lista As List(Of String))

            Do While returnData.Read()
                Dim myString As String = New String(CStr(returnData("Modello")))

                Lista.Add(myString)
            Loop
        End Sub

        Private Sub TGenerateListOfProtocolliFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef Lista As List(Of String))

            Do While returnData.Read()
                Dim myString As String = New String(CStr(returnData("Protocollo")))

                Lista.Add(myString)
            Loop
        End Sub
        Private Sub TGenerateListOfProduttoriFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef Lista As List(Of String))

            Do While returnData.Read()
                Dim myString As String = New String(CStr(returnData("Produttore")))

                Lista.Add(myString)
            Loop
        End Sub
        Private Sub TGenerateListOfMarcheFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef Lista As List(Of String))

            Do While returnData.Read()
                Dim myString As String = New String(CStr(returnData("RagioneSociale")))

                Lista.Add(myString)
            Loop
        End Sub

        Private Sub TGenerateListOfPannelliFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef Lista As List(Of String))

            Do While returnData.Read()
                Dim myString As String = New String(CStr(returnData("Matricola")))

                Lista.Add(myString)
            Loop
        End Sub

         Private Sub TGenerateRigaCertificazioneListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef rigaCertificazioneList As List(Of RigaAutocertificazione))

            Do While returnData.Read()

                Dim DataAggiornamento As Date

                If returnData("DataAggiornamento") Is DBNull.Value Then
                    DataAggiornamento = DefaultValues.GetDateTimeMinValue
                Else
                    DataAggiornamento = returnData("DataAggiornamento")
                End If

                Dim RigaAutocertificazione As RigaAutocertificazione = New RigaAutocertificazione(CInt(returnData("Id")), CInt(returnData("IdCertificazione")), CInt(returnData("IdCategoria")), CStr("" + returnData("TipoDiDato")), _
                                                                                                  CInt(returnData("Pezzi")), CDec(returnData("Kg")), CInt(returnData("PezziRettifica")), CDec(returnData("KgRettifica")), _
                                                                                                  CInt(returnData("DifferenzaPezzi")), CDec(returnData("DifferenzaKg")), CDec(returnData("KgCertificazione")), CDec(returnData("KgDiffCertificazione")), _
                                                                                                  CDec(returnData("CostoUnitario")), CDec(returnData("KgCertSoci")), CDec(returnData("Importo")), CStr("" + returnData("UtenteAggiornamento")), _
                                                                                                  DataAggiornamento)

                rigaCertificazioneList.Add(RigaAutocertificazione)

            Loop

        End Sub

        Private Sub TGenerateFasciaDipesoListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef fasciaDipesoList As List(Of FasciaDiPeso))

            Do While returnData.Read()

                Dim FasciaDiPeso As FasciaDiPeso = New FasciaDiPeso(CInt(returnData("Id")), CStr("" + returnData("Descrizione")), CDate(returnData("DataUltModifica")))

                fasciaDipesoList.Add(FasciaDiPeso)

            Loop

        End Sub

        Private Sub TGenerateTipologiaCellaListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef tipologiaCellaList As List(Of TipologiaCella))

            Do While returnData.Read()

                Dim TipologiaCella As TipologiaCella = New TipologiaCella(CInt(returnData("Id")), CStr("" + returnData("Descrizione")), CDate(returnData("DataUltModifica")))

                tipologiaCellaList.Add(TipologiaCella)

            Loop

        End Sub

        Private Sub TGenerateAbbinamentoListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef abbinamentoList As List(Of AbbinamentoTipologiaFascia))

            Do While returnData.Read()

                Dim AbbinamentoTipologiaFascia As AbbinamentoTipologiaFascia = New AbbinamentoTipologiaFascia(CInt(returnData("Id")), CInt(returnData("IdProduttore")), CInt(returnData("IdTipologia")), CInt(returnData("IdFascia")), CDec(returnData("CostoMatricola")))

                abbinamentoList.Add(AbbinamentoTipologiaFascia)

            Loop

        End Sub

        Private Sub TGenerateCertificatoListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef CertificatoList As List(Of Certificato))

            Do While returnData.Read()

                Dim Certificato As Certificato = New Certificato(CInt(returnData("Id")), CInt(returnData("IdProduttore")), CInt(returnData("Anno")), CStr("" + returnData("Protocollo")))

                CertificatoList.Add(Certificato)

            Loop

        End Sub

        Private Sub TGenerateLogIscrizioneListFromReader(Of T)(ByVal returnData As SqlDataReader, ByRef LogIscrizioneList As List(Of LogIscrizione))

            Dim DataIscrizione As Date
            Dim DataDisiscrizione As Date

            Do While returnData.Read()

                If returnData("DataIscrizione") Is DBNull.Value Then
                    DataIscrizione = DefaultValues.GetDateTimeMinValue
                Else
                    DataIscrizione = returnData("DataIscrizione")
                End If

                If returnData("DataDisiscrizione") Is DBNull.Value Then
                    DataDisiscrizione = DefaultValues.GetDateTimeMinValue
                Else
                    DataDisiscrizione = returnData("DataDisiscrizione")
                End If

                Dim LogIscrizione As LogIscrizione = New LogIscrizione(CInt(returnData("Id")), CInt(returnData("IdProduttore")), DataIscrizione, DataDisiscrizione)

                LogIscrizioneList.Add(LogIscrizione)

            Loop

        End Sub

    End Class

End Namespace
