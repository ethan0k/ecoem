Imports ASPNET.StarterKit.BusinessLogicLayer
Imports ASPNET.StarterKit.DataAccessLayer
Imports System.Data
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class WebApp_Dichiarazioni_ListaAutocertificazioni
    Inherits System.Web.UI.Page


    Dim CurrentUser As MembershipUser

    Private Sub WebApp_Dichiarazioni_ListaAutocertificazioni_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.IsInRole("Produttore") Then
            Dim Produttore As Produttore
            CurrentUser = Membership.GetUser()
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)
            Produttore = Produttore.Carica(UtenteProduttore.IdProduttore)
            ddlProduttore.SelectedValue = Produttore.Id
            ddlProduttore.Enabled = False
            UpdateData(Produttore.Id)
            cmdNuova.Visible = False
            cmdExportAEE.Visible = False
            cmdAeeCategorie.Visible = False
            cmdExportPile.Visible = False
            cmdExportVeicoli.Visible = False
            cmdIndustrial.Visible = False
            cmdReportRettifiche.Visible = False
            cmdRettificheEuro.Visible = False
        ElseIf Not Page.User.IsInRole("Amministratore") And Not Page.User.IsInRole("Operatore") Then
            Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
        Else
            UpdateData(0)
            cmdNuova.visible = True
        End If



    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Autocertificazione.aspx?id=" & e.CommandArgument.ToString())
        ElseIf String.Equals(e.CommandName, "Elimina") Then
            Dim AutocertToDelete As Autocertificazione = Autocertificazione.Carica(e.CommandArgument.ToString())
            AutocertToDelete.Elimina(Page.User.Identity.Name.ToString)
            Listview1.DataBind()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Certificazione eliminata!', 'Conferma');", True)
            Exit Sub
        ElseIf String.Equals(e.CommandName, "Certificato") Then
            Dim AutoCert As Autocertificazione = Autocertificazione.Carica(e.CommandArgument)
            If AutoCert.UploadEseguito Then
                Response.ContentType = "application/octet-stream"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + AutoCert.NomeFile)
                Response.TransmitFile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("AutocertificazioniPath").ToString & "\" & AutoCert.NomeFile))
                Response.End()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun documento caricato!', 'Errore');", True)
                Exit Sub
            End If
        ElseIf String.Equals(e.CommandName, "Conferma") Then
            
            Dim AutoCert As Autocertificazione = Autocertificazione.Carica(e.CommandArgument)
            If AutoCert.UploadEseguito Then
                AutoCert.Confermata = True
                AutoCert.DataConferma = Today
                AutoCert.Save
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Autocertificazione confermata!', 'Messaggio');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile confermare. Tutti i documenti devono essere caricati!', 'Errore');", True)
                Exit Sub
            End If

        ElseIf String.Equals(e.CommandName, "Rettifica") Then
             Response.Redirect("Rettifica.aspx?id=" & e.CommandArgument.ToString())


        End If

    End Sub

    Private Sub UpdateData(IdProduttore As Integer)

        Dim strQueryString As String

        strQueryString = "SELECT tbl_Autocertificazioni.id, tbl_Autocertificazioni.Anno, tbl_Autocertificazioni.IdProduttore, tbl_Produttori.RagioneSociale, tbl_Autocertificazioni.Data, tbl_Autocertificazioni.Uploadeseguito,  " & _
                        "tbl_Autocertificazioni.PathFile , " & _
                        "CAST(CASE tbl_Autocertificazioni.Confermata WHEN 1 THEN 'Sì' WHEN 0 THEN 'No' END AS NVARCHAR) as Confermata, " & _
                        "CAST(CASE tbl_Autocertificazioni.Rettificata WHEN 1 THEN 'Sì' WHEN 0 THEN 'No' END AS NVARCHAR) as Rettificata, " & _
						"tbl_Autocertificazioni.DataConferma " & _ 
                        "FROM tbl_Autocertificazioni INNER JOIN tbl_Produttori ON tbl_Produttori.Id = tbl_Autocertificazioni.IdProduttore WHERE 1= 1 "

        If IdProduttore = 0 Then
            If ddlProduttore.SelectedIndex <> 0 Then
                strQueryString = strQueryString + "AND tbl_Autocertificazioni.IdProduttore = " & ddlProduttore.SelectedValue
            End If
        Else
            strQueryString = strQueryString + "AND tbl_Autocertificazioni.IdProduttore = " & IdProduttore
        End If

        If txtAnno.Text <> "" And IsNumeric(txtAnno.Text) Then
            strQueryString = strQueryString + "AND tbl_Autocertificazioni.Anno = " & CInt(txtAnno.Text)
        End If

        If ddlCaricato.SelectedIndex <> 0 Then
            If ddlCaricato.SelectedValue = 1 Then
                strQueryString += "AND tbl_Autocertificazioni.UploadEseguito = 1"
            Else
                strQueryString += "AND tbl_Autocertificazioni.UploadEseguito = 0"
            End If
        End If

        If ddlConfermate.SelectedIndex <> 0 Then
            If ddlConfermate.SelectedValue = 1 Then
                strQueryString += "AND tbl_Autocertificazioni.Confermata = 1"
            Else
                strQueryString += "AND tbl_Autocertificazioni.Confermata= 0"
            End If
        End If

        If ddlRettificata.SelectedIndex <> 0 Then
            If ddlRettificata.SelectedValue = 1 Then
                strQueryString +=  "AND tbl_Autocertificazioni.Rettificata = 1"
            Else
                strQueryString += "AND tbl_Autocertificazioni.Rettificata = 0"
            End If
        End If

        strQueryString +=  " ORDER BY tbl_Autocertificazioni.Anno DESC;"

        SqlDatasource1.SelectCommand = strQueryString
        Listview1.DataBind()

    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click

        ddlProduttore.SelectedIndex = 0
        ddlRettificata.SelectedIndex = 0
        ddlConfermate.selectedindex=0
        ddlCaricato.SelectedIndex=0
        txtAnno.Text = String.Empty

    End Sub

    Private Sub cmdNuova_Click(sender As Object, e As EventArgs) Handles cmdNuova.Click

        response.redirect("Nuova.aspx")
    End Sub

    Private Sub cmdExportAEE_Click(sender As Object, e As EventArgs) Handles cmdExportAEE.Click

        Dim dt, dt2 As New DataTable
        Dim SqlConnection As new SqlConnection
        Dim cmd As New SqlCommand
        Dim Adapter As New SqlDataAdapter(cmd)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim CheckCat As New CategoriaNew

        If txtAnno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Compilare il parametro anno'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        SqlConnection.ConnectionString = connString
        cmd.Connection = SqlConnection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_ReportAEE"

        If ddlProduttore.SelectedValue <> "-1" Then
            cmd.Parameters.AddWithValue("IdProduttore",ddlProduttore.SelectedValue)            
        Else
            cmd.Parameters.AddWithValue("IdProduttore","")            
        End If
        
        cmd.Parameters.AddWithValue("Anno",txtAnno.Text)            

        Adapter.Fill(dt)

        For Each riga as DataRow In dt.Rows            
            If NOT CheckCat.IsAeeEnable(riga.Item(2)) Then
                riga.Delete
            End If
        Next

        dt.AcceptChanges

        cmd.CommandText = "sp_ReportAEE2"
        Adapter.Fill(dt2)

        ' Definisce la chiave primaria
        Dim primaryKey(1) As DataColumn
        primaryKey(1) = dt2.Columns("Id")
        dt2.PrimaryKey = primaryKey

        For Each riga as DataRow In dt.Rows
            Dim result() As DataRow = dt2.Select("Id = " & riga.Item(0))
            If result.Length > 0 Then
                riga.Delete
                'dt.LoadDataRow(result(0).ItemArray,False)                
            End If
        Next

        dt.AcceptChanges
        
        For Each riga as DataRow In dt2.Rows
            dt.LoadDataRow(riga.ItemArray,False)                
        Next

        dt.AcceptChanges

        dt.DefaultView.Sort() = "RagioneSociale"
 
        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ReportAEE")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt.DefaultView.ToTable, True)

            'Format the header for column 
            Using rng As ExcelRange = ws.Cells("A1:P1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using
            Using rng As ExcelRange = ws.Cells("G1:G1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("J2:K1000")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:N1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ReportAEE.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

    Private Sub cmdExportPile_Click(sender As Object, e As EventArgs) Handles cmdExportPile.Click

        Dim dt,dt2 As New DataTable
        Dim SqlConnection As new SqlConnection
        Dim cmd As New SqlCommand
        Dim Adapter As New SqlDataAdapter(cmd)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString

        If txtAnno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Compilare il parametro anno'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        SqlConnection.ConnectionString = connString
        
        cmd.Connection = SqlConnection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_Reportpile"

        If ddlProduttore.SelectedValue <> "-1" Then
            cmd.Parameters.AddWithValue("IdProduttore",ddlProduttore.SelectedValue)            
        Else
            cmd.Parameters.AddWithValue("IdProduttore","")            
        End If
        
        cmd.Parameters.AddWithValue("Anno",txtAnno.Text)            
        
        Adapter.Fill(dt)
        
        Dim CheckCat As new CategoriaNew
        For Each riga as DataRow In dt.Rows            
            If NOT CheckCat.IsPileEnable(riga.Item(2)) Then
                riga.Delete
            End If
        Next

        dt.AcceptChanges

        cmd.CommandText = "sp_ReportPile2"
        Adapter.Fill(dt2)

        ' Definisce la chiave primaria
        Dim primaryKey(1) As DataColumn
        primaryKey(1) = dt2.Columns("Id")
        dt2.PrimaryKey = primaryKey

        For Each riga as DataRow In dt.Rows
            Dim result() As DataRow = dt2.Select("Id = " & riga.Item(0))
            If result.Length > 0 Then
                riga.Delete
                'dt.LoadDataRow(result(0).ItemArray,False)                
            End If
        Next

        dt.AcceptChanges
        
        For Each riga as DataRow In dt2.Rows
            dt.LoadDataRow(riga.ItemArray,False)                
        Next

        dt.AcceptChanges

        dt.DefaultView.Sort() = "RagioneSociale"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ReportPILE")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt.DefaultView.ToTable, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:S1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("F2:P1000")
                 rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:N1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ReportPILE.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using
    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        dt.Columns.Remove("Pathfile")

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaPannelli")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:I1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("e1:e1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using            

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaAutocertificazioni.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

    End Sub

    Private Sub cmdExportVeicoli_Click(sender As Object, e As EventArgs) Handles cmdExportVeicoli.Click

        Dim dt,dt2 As New DataTable
        Dim SqlConnection As new SqlConnection
        Dim cmd As New SqlCommand
        Dim Adapter As New SqlDataAdapter(cmd)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim CheckCat As new CategoriaNew

        If txtAnno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Compilare il parametro anno'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        SqlConnection.ConnectionString = connString

        cmd.Connection = SqlConnection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_ReportVeicoli"

        If ddlProduttore.SelectedValue <> "-1" Then
            cmd.Parameters.AddWithValue("IdProduttore",ddlProduttore.SelectedValue)            
        Else
            cmd.Parameters.AddWithValue("IdProduttore","")            
        End If
                
        cmd.Parameters.AddWithValue("Anno",txtAnno.Text)            
        
        Adapter.Fill(dt)       
        
        For Each riga as DataRow In dt.Rows            
            If NOT CheckCat.IsVeicoliEnable(riga.Item(2)) Then
                riga.Delete
            End If
        Next

        dt.AcceptChanges

        cmd.CommandText = "sp_ReportVeicoli2"
        Adapter.Fill(dt2)

        ' Definisce la chiave primaria
        Dim primaryKey(1) As DataColumn
        primaryKey(1) = dt2.Columns("Id")
        dt2.PrimaryKey = primaryKey

        For Each riga as DataRow In dt.Rows
            Dim result() As DataRow = dt2.Select("Id = " & riga.Item(0))
            If result.Length > 0 Then
                riga.Delete
                'dt.LoadDataRow(result(0).ItemArray,False)                
            End If
        Next

        dt.AcceptChanges
        
        For Each riga as DataRow In dt2.Rows
            dt.LoadDataRow(riga.ItemArray,False)                
        Next

        dt.AcceptChanges

        dt.DefaultView.Sort() = "RagioneSociale"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ReportVeicoli")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt.DefaultView.ToTable, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:T1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("G1:G1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("J2:P1000")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:N1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ReportVeicoli.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

    Private Sub cmdIndustrial_Click(sender As Object, e As EventArgs) Handles cmdIndustrial.Click

        Dim dt,dt2 As New DataTable        
        Dim SqlConnection As new SqlConnection
        Dim cmd As New SqlCommand
        Dim Adapter As New SqlDataAdapter(cmd)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ToString

        SqlConnection.ConnectionString = connString

        ' Verifica compilazione parametro anno
        If txtAnno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Compilare il parametro anno'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        cmd.Connection = SqlConnection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_ReportIndustriali"
        If ddlProduttore.SelectedValue <> "-1" Then
            cmd.Parameters.AddWithValue("IdProduttore",ddlProduttore.SelectedValue)            
        Else
            cmd.Parameters.AddWithValue("IdProduttore","")            
        End If
        
        cmd.Parameters.AddWithValue("Anno",txtAnno.Text)            

        Adapter.Fill(dt)
        
        Dim CheckCat As new CategoriaNew
        For Each riga as DataRow In dt.Rows            
            If NOT CheckCat.IsIndustrialiEnable(riga.Item(2)) Then
                riga.Delete
            End If
        Next

        dt.AcceptChanges

        cmd.CommandText = "sp_ReportIndustriali2"
        Adapter.Fill(dt2)

        ' Definisce la chiave primaria
        Dim primaryKey(1) As DataColumn
        primaryKey(1) = dt2.Columns("Id")
        dt2.PrimaryKey = primaryKey

       For Each riga as DataRow In dt.Rows
            Dim result() As DataRow = dt2.Select("Id = " & riga.Item(0))
            If result.Length > 0 Then
                riga.Delete
                'dt.LoadDataRow(result(0).ItemArray,False)                
            End If
        Next

        dt.AcceptChanges
        
        For Each riga as DataRow In dt2.Rows
            dt.LoadDataRow(riga.ItemArray,False)                
        Next

        dt.AcceptChanges

        dt.DefaultView.Sort() = "RagioneSociale"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ReportIndustriali")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt.DefaultView.ToTable, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:W1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using
            Using rng As ExcelRange = ws.Cells("G1:G1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("J2:P1000")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:N1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ReportIndustriali.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using


    End Sub

    Private Sub cmdAeeCategorie_Click(sender As Object, e As EventArgs) Handles cmdAeeCategorie.Click

        Dim SqlConnection As New SqlConnection
        Dim cmd As New SqlCommand
        Dim Adapter As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        Dim NewDt As New DataTable
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString

        If txtAnno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Compilare il parametro anno'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        SqlConnection.ConnectionString = connString
        cmd.Connection = SqlConnection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_ReportAEECategorie" ' Genera le colonne della tabella

        cmd.Parameters.AddWithValue("Anno", txtAnno.Text)

        Adapter.Fill(dt)

        NewDt = New DataTable("Pivot")

     
        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ReportAEECategorie")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt.DefaultView.ToTable, True)

            'Format the header for column 
            Using rng As ExcelRange = ws.Cells("A1:K1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("D1:D1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("f2:f1000")
                rng.Style.Numberformat.Format = "#,##0,00"
            End Using

            Using rng As ExcelRange = ws.Cells("G2:G1000")
                rng.Style.Numberformat.Format = "#,##0"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:N1000")
                rng.AutoFitColumns()
            End Using

            ws.Column(3).Width = 20


            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ReportAEECategorie.xlsx")
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(pck.GetAsByteArray())
                Response.End()

            End Using

    End Sub

    Private Sub cmdReportRettifiche_Click(sender As Object, e As EventArgs) Handles cmdReportRettifiche.Click

        Dim dt As New DataTable
        Dim SqlConnection As New SqlConnection
        Dim cmd As New SqlCommand
        Dim Adapter As New SqlDataAdapter(cmd)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim CheckCat As New CategoriaNew

        If txtAnno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Compilare il parametro anno'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        SqlConnection.ConnectionString = connString
        cmd.Connection = SqlConnection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_ReportRettifiche"

        If ddlProduttore.SelectedValue <> "-1" Then
            cmd.Parameters.AddWithValue("IdProduttore", ddlProduttore.SelectedValue)
        Else
            cmd.Parameters.AddWithValue("IdProduttore", "")
        End If

        cmd.Parameters.AddWithValue("Anno", txtAnno.Text)

        Adapter.Fill(dt)

        dt.DefaultView.Sort() = "RagioneSociale"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ReportRettifiche")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt.DefaultView.ToTable, True)

            'Format the header for column 
            Using rng As ExcelRange = ws.Cells("A1:S1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("H1:H1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("I2:R1000")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:N1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ReportRettifiche.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

    Private Sub cmdRettificheEuro_Click(sender As Object, e As EventArgs) Handles cmdRettificheEuro.Click

        Dim dt As New DataTable
        Dim SqlConnection As New SqlConnection
        Dim cmd As New SqlCommand
        Dim Adapter As New SqlDataAdapter(cmd)
        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim CheckCat As New CategoriaNew

        If txtAnno.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Compilare il parametro anno'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        SqlConnection.ConnectionString = connString
        cmd.Connection = SqlConnection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_ReportRettificheEuro"

        If ddlProduttore.SelectedValue <> "-1" Then
            cmd.Parameters.AddWithValue("IdProduttore", ddlProduttore.SelectedValue)
        Else
            cmd.Parameters.AddWithValue("IdProduttore", "")
        End If

        cmd.Parameters.AddWithValue("Anno", txtAnno.Text)

        Adapter.Fill(dt)

        dt.DefaultView.Sort() = "RagioneSociale"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ReportRettificheEuro")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt.DefaultView.ToTable, True)

            'Format the header for column 
            Using rng As ExcelRange = ws.Cells("A1:S1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("G1:G1000")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("I2:R1000")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:N1000")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ReportRettificheEuro.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using


    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        Dim delSection As HtmlControl = e.Item.FindControl("DeleteLi")

        delSection.Visible = Page.User.IsInRole("Amministratore")

    End Sub

    Private Sub cmdChiudi_Click(sender As Object, e As EventArgs) Handles cmdChiudi.Click


        Dim ListaCertificazioni As List(Of Autocertificazione)
        Dim Conteggio As Integer

        ListaCertificazioni = Autocertificazione.ListaAutocertificazioniAperte

        If Not ListaCertificazioni Is Nothing Then
            For Each certificazione In ListaCertificazioni
                certificazione.Confermata = True
                certificazione.DataConferma = Today.ToShortDateString
                certificazione.Save()
                Conteggio += 1
            Next

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Sono state confermate " & Conteggio.ToString & " autocertificazioni.', 'Conferma');", True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessuna dichiarazione aperte trovata.', 'Conferma');", True)
        End If

    End Sub

End Class
