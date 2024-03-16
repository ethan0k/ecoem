Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Data
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class WebApp_DichiarazioniProf_ListaDichiarazioni
    Inherits System.Web.UI.Page

    Dim CurrentUser As MembershipUser

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.IsInRole("Produttore") Then
            Dim Produttore As Produttore
            CurrentUser = Membership.GetUser()
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)
            Produttore = Produttore.Carica(UtenteProduttore.IdProduttore)
            cmdEsporta.Visible = False
            cmdEsportaDett.Visible = False
            If Not Produttore.Professionale Then
                Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
            Else
                ddlProduttore.SelectedValue = Produttore.Id
                ddlProduttore.Enabled = False
                UpdateData(Produttore.Id)
            End If

        ElseIf Not Page.User.IsInRole("Amministratore") And Not Page.User.IsInRole("Operatore") Then
            Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")

        Else
            UpdateData(0)
        End If

    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand


        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Dichiarazione.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then

            Dim Dichiarazione As Dichiarazione = Dichiarazione.Carica(e.CommandArgument.ToString())
            If Dichiarazione.Confermata Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile procedere. Dichiarazione confermata. Contattare l'amministratore', 'Errore');", True)
                Exit Sub
            Else
                Dichiarazione.Elimina()
                Listview1.DataBind()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dichiarazione eliminata', 'Conferma');", True)
                Exit Sub
            End If

        End If

    End Sub

    Private Function CalcolaDataDichiarazione() As Date

        Dim DataDichiarazione As Date

        DataDichiarazione = DateSerial(Year(Today) - 1, 12, 31)

        Return DataDichiarazione

    End Function

    Private Sub UpdateData(IdProduttore As Integer)

        Dim strQueryString As String

        strQueryString = "SELECT tbl_Dichiarazioni.id, tbl_Dichiarazioni.Data, tbl_Dichiarazioni.IdProduttore, tbl_Produttori.RagioneSociale, tbl_Dichiarazioni.DataRegistrazione, tbl_Dichiarazioni.Utente, tbl_Dichiarazioni.Confermata, tbl_Dichiarazioni.fatturata, " & _
                        "tbl_Dichiarazioni.Professionale, tbl_Dichiarazioni.DataConferma  , (SELECT SUM(Importo) FROM tbl_RigheDichiarazioni Where tbl_RigheDichiarazioni.IdDichiarazione = tbl_Dichiarazioni.Id) as Importo " & _
                        "FROM tbl_Dichiarazioni INNER JOIN tbl_Produttori ON tbl_Produttori.Id = tbl_Dichiarazioni.IdProduttore WHERE tbl_Dichiarazioni.Professionale = 1 "

        If IdProduttore = 0 Then
            If ddlProduttore.SelectedIndex <> 0 Then
                strQueryString = strQueryString + "AND tbl_Dichiarazioni.IdProduttore = " & ddlProduttore.SelectedValue
            End If
        Else
            strQueryString = strQueryString + "AND tbl_Dichiarazioni.IdProduttore = " & IdProduttore
        End If

        If ddlStato.SelectedIndex <> 0 Then
            strQueryString = strQueryString + "AND tbl_Dichiarazioni.Confermata =  " & ddlStato.SelectedValue
        End If

        If txtData.Text <> "" Then
            If GetFirstDate(txtData.Text) <> Nothing And GetSecondDate(txtData.Text) <> Nothing Then
                strQueryString = strQueryString + " AND tbl_Dichiarazioni.Data Between '" & GetFirstDate(txtData.Text).ToString("yyyy-MM-dd") & "' And '" & GetSecondDate(txtData.Text).ToString("yyyy-MM-dd") & "'"
            Else
                strQueryString = strQueryString + " AND tbl_Dichiarazioni.Data = '" & GetFirstDate(txtData.Text).ToString("yyyy-MM-dd") & "'"
            End If
        End If

        strQueryString = strQueryString + " ORDER BY tbl_Dichiarazioni.Data DESC;"

        SqlDatasource1.SelectCommand = strQueryString
        Listview1.DataBind()
    End Sub

    Protected Function GetFirstDate(strDate As String) As Date

        Dim Pos As Integer = InStr(strDate, "-")

        If Pos > 0 Then
            Return String.Format("{0:dd\/MM\/yyyy}", Mid(strDate, 1, Pos - 1))
        Else
            Return String.Format("{0:dd\/MM\/yyyy}", strDate)
        End If

    End Function

    Protected Function GetSecondDate(strDate As String) As Date

        Dim Pos As Integer = InStr(strDate, "-")

        If Pos > 0 Then
            Return String.Format("{0:dd\/MM\/yyyy}", Mid(strDate, Pos + 1, Len(strDate) - Pos))
        Else
            Return Nothing
        End If

    End Function

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click

        ddlProduttore.SelectedIndex = 0
        ddlStato.SelectedIndex = 0
        txtData.Text = String.Empty

        If Page.User.IsInRole("Produttore") Then
            Dim Produttore As Produttore
            CurrentUser = Membership.GetUser()
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)
            Produttore = Produttore.Carica(UtenteProduttore.IdProduttore)
            ddlProduttore.SelectedValue = Produttore.Id
            ddlProduttore.Enabled = False
            UpdateData(Produttore.Id)
        Else
            UpdateData(0)

        End If
    End Sub

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click

        Dim Produttore As Produttore

        If Page.User.IsInRole("Produttore") Then

            Dim ListaCategorie As List(Of Categoria_ProduttoreNew)
            ListaCategorie = Categoria_ProduttoreNew.Lista(ddlProduttore.SelectedValue, True)
            If ListaCategorie Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Anagrafica Produttore incompleta.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

            CurrentUser = Membership.GetUser()
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)
            Produttore = Produttore.Carica(UtenteProduttore.IdProduttore)

            If Not Produttore.Attivo Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Anagrafica Produttore inattiva.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If

            If Produttore.Attivo Then

                Dim DataDichiarazione As Date
                DataDichiarazione = CalcolaDataDichiarazione()
                Dim CurrentDichiarazione As Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione, True)
                If CurrentDichiarazione Is Nothing Then
                    CurrentDichiarazione = New Dichiarazione
                    CurrentDichiarazione.IdProduttore = Produttore.Id
                    CurrentDichiarazione.Data = DataDichiarazione
                    CurrentDichiarazione.Utente = Page.User.Identity.Name.ToString
                    CurrentDichiarazione.DataRegistrazione = Today.ToShortDateString
                    CurrentDichiarazione.Professionale = True
                    CurrentDichiarazione.Save()

                    ' Crea le righe della dichiarazione
                    'Dim ListaCategorie As List(Of Categoria_Produttore)
                    'ListaCategorie = Categoria_Produttore.Lista(Produttore.Id)
                    For Each CategoriaInLista In ListaCategorie
                        Dim NuovaRiga As New RigaDichiarazione                        
                        Dim Categoria As CategoriaNew = CategoriaNew.Carica(CategoriaInLista.IdCategoria)
                        NuovaRiga.IdCategoria = CategoriaInLista.IdCategoria
                        NuovaRiga.IdDichiarazione = CurrentDichiarazione.Id
                        NuovaRiga.TipoDiDato = Categoria.TipoDiDato
                        NuovaRiga.Pezzi = 0
                        NuovaRiga.Kg = 0
                        NuovaRiga.CostoUnitario = CategoriaInLista.Costo
                        NuovaRiga.Importo = 0
                        NuovaRiga.DataAggiornamento = Today
                        NuovaRiga.UtenteAggiornamento = Page.User.Identity.Name.ToString
                        NuovaRiga.Save()
                    Next

                    Response.Redirect("Dichiarazione.aspx?Id=" & CurrentDichiarazione.Id)
                Else
                    If CurrentDichiarazione.Professionale Then
                        Response.Redirect("Dichiarazione.aspx?Id=" & CurrentDichiarazione.Id)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dichiarazione professionale non trovata.'" & ", 'Messaggio errore');", True)
                        Exit Sub
                    End If
                End If


            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Produttore non attivo.'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If
        ElseIf Page.User.IsInRole("Amministratore") Then
            Response.Redirect("Dichiarazione.aspx")
        End If

    End Sub

    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()
        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaDichiarazioniProfessionali")

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

            Using rng As ExcelRange = ws.Cells("b1:b1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using
            Using rng As ExcelRange = ws.Cells("e1:e1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using
            Using rng As ExcelRange = ws.Cells("h1:h1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using
            Using rng As ExcelRange = ws.Cells("i1:i1048576")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:A1")
                rng.Value = "Id"
            End Using

            Using rng As ExcelRange = ws.Cells("C1:C1")
                rng.Value = "Cod. Produttore"
            End Using

            Using rng As ExcelRange = ws.Cells("D1:D1")
                rng.Value = "Ragione sociale"
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1")
                rng.Value = "Data registrazione"
            End Using

            Using rng As ExcelRange = ws.Cells("h1:h1")
                rng.Value = "Data conferma"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:i1048576")
                rng.AutoFitColumns()
            End Using
            Using rng As ExcelRange = ws.Cells("h2:h10000")
                'For Each cell In rng
                '    Dim DataValore As Date

                '    DataValore = CDate(cell.Value)
                '    If DataValore < #1/1/2001# Then
                '        cell.Value = ""

                '    End If
                'Next
            End Using


            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaDichiarazioni.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

      Protected Sub cmdEsportaDett_Click(sender As Object, e As EventArgs) Handles cmdEsportaDett.Click

         Dim dt As New DataTable()
        Dim connString As String = System.Configuration.ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString.ToString

        Using yourConnection As New SqlConnection(connString)
            Dim strSql As String
            strSql = "Select tbl_Produttori.RagioneSociale, CADENZA = CASE WHEN tbl_Produttori.Periodicita = 1 THEN 'Mensile' WHEN tbl_Produttori.Periodicita = 2 THEN 'Bimestrale' WHEN tbl_Produttori.Periodicita = 3 THEN 'Trimestrale' END " & _
                    ", tbl_Macrocategorie.Voce, tbl_Macrocategorie.Nome as Macro, tbl_Categorie.Nome, tbl_Categorie.Codifica, SUM(tbl_RigheDichiarazioni.Kg) AS TotaleKg , SUM(tbl_RigheDichiarazioni.Pezzi) AS TotalePezzi, SUM(tbl_RigheDichiarazioni.Importo) AS Importo, " & _
                    "(SELECT Confermata FROM tbl_Dichiarazioni WHERE Id = tbl_RigheDichiarazioni.IdDichiarazione) AS Confermata,   " & _
                    "(SELECT Fatturata FROM tbl_Dichiarazioni WHERE Id = tbl_RigheDichiarazioni.IdDichiarazione) AS Fatturata, tbl_Dichiarazioni.Data   " & _
                        "FROM tbl_Produttori INNER JOIN tbl_Dichiarazioni ON tbl_Produttori.Id = tbl_Dichiarazioni.IdProduttore  AND tbl_Dichiarazioni.Professionale = 1 AND tbl_Dichiarazioni.OldVersion = 1"


            If txtData.Text <> "" Then
                If GetFirstDate(txtData.Text) <> Nothing And GetSecondDate(txtData.Text) <> Nothing Then
                    strSql = strSql + " And tbl_Dichiarazioni.Data Between '" & GetFirstDate(txtData.Text).ToString("yyyy-MM-dd") & "' And '" & GetSecondDate(txtData.Text).ToString("yyyy-MM-dd") & "' "
                Else
                    strSql = strSql + " AND tbl_Dichiarazioni.Data = '" & GetFirstDate(txtData.Text).ToString("yyyy-MM-dd") & "' "
                End If
            End If

            If ddlProduttore.SelectedIndex <> 0 Then
                strSql = strSql + " And tbl_Produttori.id= " & ddlProduttore.SelectedValue
            End If

            strSql = strSql + " INNER JOIN tbl_RigheDichiarazioni On tbl_Dichiarazioni.id = tbl_RigheDichiarazioni.IdDichiarazione  INNER Join tbl_Categorie ON tbl_RigheDichiarazioni.IdCategoria = tbl_Categorie.Id " & _
                                "INNER Join tbl_Macrocategorie ON tbl_Categorie.IdMacroCategoria = tbl_Macrocategorie.Id " & _
                                "Group By tbl_Produttori.RagioneSociale, tbl_RigheDichiarazioni.IdDichiarazione, tbl_Produttori.Periodicita, " & _
                                "tbl_Macrocategorie.Nome, tbl_Macrocategorie.Voce, tbl_Categorie.Nome, tbl_Categorie.Codifica, tbl_Dichiarazioni.Data UNION " & _
                                "Select tbl_Produttori.RagioneSociale, CADENZA = CASE WHEN tbl_Produttori.Periodicita = 1 THEN 'Mensile' WHEN tbl_Produttori.Periodicita = 2 THEN 'Bimestrale' WHEN tbl_Produttori.Periodicita = 3 THEN 'Trimestrale' END " & _
                                ", tbl_MacrocategorieNew.Voce, tbl_MacrocategorieNew.Nome as Macro, tbl_CategorieNew.Nome, tbl_CategorieNew.Codifica, SUM(tbl_RigheDichiarazioni.Kg) AS TotaleKg , SUM(tbl_RigheDichiarazioni.Pezzi) AS TotalePezzi, SUM(tbl_RigheDichiarazioni.Importo) AS Importo, " & _
                                "(SELECT Confermata FROM tbl_Dichiarazioni WHERE Id = tbl_RigheDichiarazioni.IdDichiarazione) AS Confermata,   " & _
                                "(SELECT Fatturata FROM tbl_Dichiarazioni WHERE Id = tbl_RigheDichiarazioni.IdDichiarazione) AS Fatturata, tbl_Dichiarazioni.Data   " & _
                                "FROM tbl_Produttori INNER JOIN tbl_Dichiarazioni ON tbl_Produttori.Id = tbl_Dichiarazioni.IdProduttore  AND tbl_Dichiarazioni.Professionale = 1  And tbl_Dichiarazioni.OldVersion = 0 "

            If txtData.Text <> "" Then
                If GetFirstDate(txtData.Text) <> Nothing And GetSecondDate(txtData.Text) <> Nothing Then
                    strSql = strSql + " And tbl_Dichiarazioni.Data Between '" & GetFirstDate(txtData.Text).ToString("yyyy-MM-dd") & "' And '" & GetSecondDate(txtData.Text).ToString("yyyy-MM-dd") & "' "
                Else
                    strSql = strSql + " AND tbl_Dichiarazioni.Data = '" & GetFirstDate(txtData.Text).ToString("yyyy-MM-dd") & "' "
                End If
            End If

            If ddlProduttore.SelectedIndex <> 0 Then
                strSql = strSql + " And tbl_Produttori.id= " & ddlProduttore.SelectedValue
            End If

            strSql = strSql + " INNER JOIN tbl_RigheDichiarazioni On tbl_Dichiarazioni.id = tbl_RigheDichiarazioni.IdDichiarazione  INNER Join tbl_CategorieNew ON tbl_RigheDichiarazioni.IdCategoria = tbl_CategorieNew.Id " & _
                                "INNER Join tbl_MacrocategorieNew ON tbl_CategorieNew.IdMacroCategoria = tbl_MacrocategorieNew.Id " & _
                                "Group By tbl_Produttori.RagioneSociale, tbl_RigheDichiarazioni.IdDichiarazione, tbl_Produttori.Periodicita, " & _
                                "tbl_MacrocategorieNew.Nome, tbl_MacrocategorieNew.Voce, tbl_CategorieNew.Nome, tbl_CategorieNew.Codifica, tbl_Dichiarazioni.Data "

            strSql = strSql + " Order by RagioneSociale"

            Dim cmd As New SqlCommand(strSql, yourConnection)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            If Not Page.User.IsInRole("Amministratore") Then
                dt.Columns.Remove("Fatturata")
            End If


        End Using

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaDichiarazioni")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:l1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("d1:d1048576")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("f1:f1048576")
                rng.Style.Numberformat.Format = "#,##0.00"
            End Using

            Using rng As ExcelRange = ws.Cells("l1:l1048576")
                rng.Style.Numberformat.Format = "dd-mm-yy"
            End Using

            Using rng As ExcelRange = ws.Cells("B1:B1")
                rng.Value = "Cadenza"
            End Using

            Using rng As ExcelRange = ws.Cells("E1:E1")
                rng.Value = "Categoria"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:l1048576")
                rng.AutoFitColumns()
            End Using

            ws.Column(5).Width = 30

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaDichiarazioni.xlsx")
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(pck.GetAsByteArray())
                Response.End()

            End Using


    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        If Page.User.IsInRole("Operatore") Then
            If (e.Item.ItemType = ListViewItemType.DataItem) Then

                Dim myLinkDeleteButton As LinkButton = CType(e.Item.FindControl("DeleteButton"), LinkButton)
                myLinkDeleteButton.Visible = False
            End If
        End If

    End Sub
End Class
