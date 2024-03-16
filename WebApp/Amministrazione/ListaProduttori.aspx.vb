Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Data
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.SqlTypes

Partial Class WebApp_Amministrazione_ListaProduttori
    Inherits System.Web.UI.Page

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("Produttore.aspx")
    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Produttore.aspx?id=" & e.CommandArgument.ToString())
        ElseIf String.Equals(e.CommandName, "Elimina") Then

            Dim Produttore As Produttore = Produttore.Carica(CInt(e.CommandArgument.ToString()))
            If Not Produttore.Verifica Then
                Produttore.Elimina()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Produttore eliminato!', 'Informazione');", True)
                Listview1.DataBind()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile eliminare. Esistono pannelli abbinati!', 'Errore');", True)
            End If
        End If

    End Sub

    Private Sub SqlDatasource1_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDatasource1.Selecting
        If e.Command.Parameters("@RagSoc").Value Is Nothing Or e.Command.Parameters("@RagSoc").Value = "Search" Then
            e.Command.Parameters("@RagSoc").Value = ""
        End If
    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        dt.Columns.Add("Periodicità")
        dt.Columns.Add("Atti")
        dt.Columns.Add("Profe")
        dt.Columns.Add("Dome")
        dt.Columns.Add("MA")
        dt.Columns("RagioneSociale").ColumnName = "Ragione sociale"
        dt.Columns("EmailNotifiche").ColumnName = "Email notifiche"
        dt.Columns("Codicefiscale").ColumnName = "Codice fiscale"

        For Each row In dt.Rows
            Select Case row("Periodicita")
                Case 1
                    row("Periodicità") = "Mensile"

                Case 2
                    row("Periodicità") = "Bimestrale"

                Case 3
                    row("Periodicità") = "Trimestrale"
                Case 6
                    row("Periodicità") = "Semestrale"

                Case Else
                    row("Periodicità") = "Non dichiarata"
            End Select

            Select Case row("MeseAdesione")
                Case 0
                    row("MA") = ""
                Case 1
                    row("MA") = "Gennaio"
                Case 2
                    row("MA") = "Febbraio"
                Case 3
                    row("MA") = "Marzo"
                Case 4
                    row("MA") = "Aprile"
                Case 5
                    row("MA") = "Maggio"
                Case 6
                    row("MA") = "Giugno"
                Case 7
                    row("MA") = "Luglio"
                Case 8
                    row("MA") = "Agosto"
                Case 9
                    row("MA") = "Settembre"
                Case 10
                    row("MA") = "Ottobre"
                Case 11
                    row("MA") = "Novembre"
                Case 12
                    row("MA") = "Dicembre"
            End Select
        Next

        For Each row In dt.Rows
            Select Case row("Attivo")
                Case False
                    row("Atti") = "No"

                Case True
                    row("Atti") = "Sì"
            End Select
        Next

        For each row In dt.Rows
            Select Case row("Professionale")
                Case False
                    row("Profe") = "No"

                Case True
                    row("Profe") = "Sì"
            End Select            
        Next

        For each row In dt.Rows
            Select Case row("Domestico")
                Case False
                    row("Dome") = "No"

                Case True
                    row("Dome") = "Sì"
            End Select            
        Next

        dt.Columns.Remove("Periodicita")
        dt.Columns.Remove("Attivo")
        dt.Columns.Remove("Professionale")
        dt.Columns.Remove("Domestico")
        dt.Columns.Remove("MeseAdesione")
        dt.Columns("Atti").ColumnName = "Attivo"
        dt.Columns("Profe").ColumnName = "Professionale"
        dt.Columns("Dome").ColumnName = "Domestico"
        dt.Columns("MA").ColumnName = "Mese adesione"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaProduttori")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:T1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("A1:T1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaProduttori.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using


    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click

        txtRagioneSociale.Text = String.Empty
        SqlDatasource1.DataBind

    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        If (e.Item.ItemType = ListViewItemType.DataItem) Then

            Dim myLinkDeleteButton As LinkButton = CType(e.Item.FindControl("DeleteButton"), LinkButton)
            myLinkDeleteButton.Visible = False
        End If

    End Sub

    Private Sub cmdTariffario_Click(sender As Object, e As EventArgs) Handles cmdTariffario.Click

        Dim conn As New SqlConnection
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim dt As DataTable = New DataTable()
        Dim cmd As New SqlCommand
        Dim NrRiga As Integer

        conn.ConnectionString = connString
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_Tariffario"

        Dim Adapter As New SqlDataAdapter(cmd)

        Adapter.Fill(dt)

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Tariffario")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)
            NrRiga = 1 ' Per le intestazioni aggiunge 1
            Dim MyCol As ExcelRange = ws.Cells("A2:A")
            For Each cell In MyCol
                NrRiga += 1
                If cell.Value <> 0 Then
                    If VerificaCosti(cell.Value, "R1F") Then
                        ws.Cells(NrRiga, 7).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "R1C") Then
                        ws.Cells(NrRiga, 8).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "R2") Then
                        ws.Cells(NrRiga, 9).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "R3") Then
                        ws.Cells(NrRiga, 10).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "R4") Then
                        ws.Cells(NrRiga, 11).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "R4PV") Then
                        ws.Cells(NrRiga, 12).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "R5PZ") Then
                        ws.Cells(NrRiga, 13).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "R5") Then
                        ws.Cells(NrRiga, 14).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "IND2.1") Then
                        ws.Cells(NrRiga, 26).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "IND2.2") Then
                        ws.Cells(NrRiga, 27).Style.Font.Color.SetColor(Color.Red)
                    End If
                    If VerificaCosti(cell.Value, "IND2.3") Then
                        ws.Cells(NrRiga, 28).Style.Font.Color.SetColor(Color.Red)
                    End If
                Else
                    Exit For
                End If
            Next

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:AI1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("G2:AG100")
                rng.Style.Numberformat.Format = "#,##0.000"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:AI1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=Tariffario.xlsx")
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(pck.GetAsByteArray())
                Response.End()

            End Using
    End Sub

    Function VerificaCosti(ByVal IdProduttore As Integer, ByVal Categoria As String) As Boolean

        ' Verifica se nella stessa categoria
        ' a parità di produttore sono presenti costi differenti

        Dim conn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim ConteggioCosti As Integer
        Dim StrSql As String = ""
        Dim returnData As SqlDataReader

        Select Case Categoria
            Case "R1F"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo >0 and c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (18,5,6,91,92,93,94,95,96,97,98,1,2,3)"
            Case "R1C"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (4,88,99,100,101)"
            Case "R2"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (19,20,17)"
            Case "R3"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo >0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (116,7,8,9,10,11)"
            Case "R4PV"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (30)"
            Case "R4"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (21,22,23,24,25,26,27,28,29,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,89,90)"
            Case "R5"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (86,87)"
            Case "R5PZ"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (12,13,14,15,16)"
            Case "IND2.1"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (76,102)"
            Case "IND2.2"
                StrSql = "select Distinct Costo FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (103,77)"
            Case "IND2.3"
                StrSql = "select Distinct COUNT(Costo) FROM tbl_Categorie_ProduttoreNew c where c.Costo > 0 And c.idproduttore = " & IdProduttore & " and c.IdCategoria IN (78,104,105,79,80,106,107,108,109,110,111,112,113,114,115)"
            Case Else

        End Select

        conn.ConnectionString = connString
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = StrSql

        Try
            conn.Open()
            returnData = cmd.ExecuteReader
            Dim dt As New DataTable
            dt.Load(returnData)
            ConteggioCosti = dt.Rows.Count
        Catch
            ConteggioCosti = 0
        Finally
            conn.Dispose()
        End Try

        If ConteggioCosti > 1 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
