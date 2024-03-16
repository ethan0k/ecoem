Imports System.Data
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.ServiceModel.Dispatcher

Partial Class WebApp_Pannelli_ListaPannelli
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.IsInRole("Monitor") Then
            cmdNuovo.Visible = False
            cmdCertifica.Visible = False
        ElseIf Page.User.IsInRole("Produttore") Or Page.User.IsInRole("Operatore") Then
            cmdCertifica.Visible = False
        End If

        If (Not IsPostBack) Then
            ddlProduttori.SelectedValue = -1
        Else
            If CheckFilter() Then
                SqlDatasource1.SelectCommand = BuildSqlString()
            Else
                SqlDatasource1.SelectCommand = ""
            End If

            SqlDatasource1.DataBind()
            Listview1.DataBind()

        End If

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

        If Page.User.IsInRole("Produttore") Then
            cmdCertifica.Visible = False
            Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(userGuid)
            ddlProduttori.SelectedValue = UtenteProduttore.IdProduttore
            ddlProduttori.Enabled = False
        End If

    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If Not Page.User.IsInRole("Produttore") Then

            If String.Equals(e.CommandName, "Edit") Then
                Response.Redirect("Pannello.aspx?id=" & e.CommandArgument.ToString())

            ElseIf String.Equals(e.CommandName, "Elimina") Then
                Dim PannelloDaEliminare As Pannello = Pannello.Carica(e.CommandArgument.ToString())
                PannelloDaEliminare.Elimina()
                Listview1.DataBind()
            End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Comando non consentito', 'Errore');", True)
        End If
    End Sub

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("Pannello.aspx")
    End Sub

    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim connString As String = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString
        Dim conn As New SqlConnection
        Dim strsql As String
        Dim dt As DataTable = New DataTable()
        Dim cmd As New SqlCommand

        conn.ConnectionString = connString
        strsql = BuildSqlString()

        strsql = "SELECT pn.id, pn.Matricola, pn.Modello, pn.DataCaricamento, pn.Peso, " & _
                    "pn.DataInizioGaranzia, p.RagioneSociale, pn.Conforme, pn.Protocollo, pn.Produttore As Marca, " & _
                    "pn.DataConformita, i.NrPraticaGSE, i.Responsabile, t.Descrizione as Tipologia, " & _
                    "f.Descrizione As Fascia, pn.CostoMatricola FROM tbl_Produttori p RIGHT JOIN " & _
                    "(tbl_Impianti i RIGHT JOIN tbl_Pannelli pn ON i.Id = pn.IdImpianto) ON p.Id = pn.IdMarca " & _
                    "LEFT JOIN tbl_TipologieCelle t On t.Id = pn.IdTipologiaCella " & _
                    "LEFT JOIN tbl_FasceDipeso f On f.id = pn.IdFasciaDiPeso " & _
                    "WHERE 1= 1 "

        If (ddlProduttori.SelectedIndex <> 0) Then
            strsql = strsql & " And pn.IdMarca = " & ddlProduttori.SelectedValue
        End If

        If (txtMatricola.Text <> "") Then
            strsql = strsql & " And pn.Matricola Like '%" & txtMatricola.Text & "%'"
        End If

        If (txtModello.Text <> "") Then
            strsql = strsql & " And pn.Modello Like '%" & txtModello.Text & "%'"
        End If

        If (txtProtocollo.Text <> "") Then
            strsql = strsql + " AND pn.Protocollo Like '%" & txtProtocollo.Text + "%'"
        End If

        If (ddlStato.SelectedIndex <> 0) Then
            strsql = strsql + " AND pn.Dismesso =" & ddlStato.SelectedValue 
        End If

        If (ddlConformita.SelectedIndex <> 0) And (Not Page.User.IsInRole("Monitor")) Then
            strsql = strsql + " AND pn.Conforme =" & ddlConformita.SelectedValue 
        End If

        If Page.User.IsInRole("Monitor") Then
            strsql = strsql + " AND pn.Conforme = 1"
        End If

        If txtDataCaricamento.Text <> String.Empty Or txtDataCaricamento.Text <> Nothing Then
            If IsDate(GetFirstDate(txtDataCaricamento.Text)) And (IsDate(GetSecondDate(txtDataCaricamento.Text)) And Not GetSecondDate(txtDataCaricamento.Text) = Nothing) Then
                strsql = strsql + " AND pn.DataCaricamento Between '" & GetFirstDate(txtDataCaricamento.Text).ToString("yyyyMMdd") & "' And '" & GetSecondDate(txtDataCaricamento.Text).ToString("yyyyMMdd") & "'"
            Else
                strsql = strsql + " AND pn.DataCaricamento = '" & GetFirstDate(txtDataCaricamento.Text).ToString("yyyyMMdd") & "'"
            End If
        End If

        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = strsql

        Dim Adapter As New SqlDataAdapter(cmd)

        Adapter.Fill(dt)

        'Dim dt As DataTable = DirectCast(Me.SqlDataExport.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        If Not Page.User.IsInRole("Amministratore") Then

            dt.Columns.Remove("NrPraticaGSE")
            dt.Columns.Remove("Responsabile")

        End If

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaPannelli")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:p1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("D1:D1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("f1:f1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("k1:k1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaPannelli.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

    End Sub
    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click

        ddlProduttori.SelectedIndex = 0
        txtMatricola.Text = String.Empty
        txtModello.Text = String.Empty
        txtProtocollo.Text = String.Empty
        txtDataCaricamento.Text = String.Empty

        SqlDatasource1.SelectCommand = ""
        SqlDatasource1.DataBind()
        Listview1.DataBind()

    End Sub

    Protected Sub cmdCertifica_Click(sender As Object, e As EventArgs) Handles cmdCertifica.Click

        If txtProtocollo.Text = String.Empty Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Specificare un protocollo!', 'Errore');", True)
            Exit Sub
        End If

        If Listview1.Items.Count = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun pannello trovato!', 'Errore');", True)
            Exit Sub
        End If

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        For Each row As DataRow In dt.Rows

            Dim Pannello As Pannello = Pannello.Carica(row.Item(0))
            Pannello.Conforme = True
            Pannello.DataConformità = Today
            Pannello.Save()

        Next

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Operazione completata!', 'Informazioni');", True)

    End Sub

    Protected Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound


        Dim targetButton As LinkButton = e.Item.FindControl("DeleteButton")
        Dim DataConf As Label = e.Item.FindControl("lblDataConformita")

        targetButton.Visible = Not Page.User.IsInRole("Monitor")
        If DataConf.Text <> "" Then
            If CDate(DataConf.Text) <= #1/1/2001# Then
                DataConf.Text = ""
            End If
        End If

    End Sub

    Private Sub SqlDataExport_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDataExport.Selecting

        If Page.User.IsInRole("Produttore") Then
            cmdCertifica.Visible = False
            Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(userGuid)
            ddlProduttori.SelectedValue = UtenteProduttore.IdProduttore
            ddlProduttori.Enabled = False
        End If

    End Sub

    Private Function BuildSqlString() As String

        Dim strSql As String

        strSql = "SELECT pn.id, pn.Matricola, pn.Modello,  p.RagioneSociale, " & _
                "pn.Conforme, pn.Protocollo, pn.DataConformita " & _
                "FROM tbl_Produttori p RIGHT JOIN tbl_Pannelli pn on p.Id = pn.IdMarca " & _
                "WHERE 1= 1 "

        If (ddlProduttori.SelectedIndex <> 0) Then
            strsql = strsql & " And pn.IdMarca = " & ddlProduttori.SelectedValue
        End If

        If (txtMatricola.Text <> "") Then
            'strSql = strSql & " And pn.Matricola Like '%" & txtMatricola.Text & "%'"
            strSql = strSql & " And pn.Matricola Like '" & txtMatricola.Text & "'"
        End If

        If (txtModello.Text <> "") Then
            strsql = strsql & " And pn.Modello Like '%" & txtModello.Text & "%'"
        End If

        If (txtProtocollo.Text <> "") Then
            strsql = strsql + " AND pn.Protocollo Like '%" & txtProtocollo.Text + "%'"
        End If

        If (ddlStato.SelectedIndex <> 0) Then
            strsql = strsql + " AND pn.Dismesso =" & ddlStato.SelectedValue
        End If

        If (ddlConformita.SelectedIndex <> 0) And (Not Page.User.IsInRole("Monitor")) Then
            strsql = strsql + " AND pn.Conforme =" & ddlConformita.SelectedValue
        End If

        If Page.User.IsInRole("Monitor") Then
            strsql = strsql + " AND pn.Conforme = 1"
        End If

        If txtDataCaricamento.Text <> String.Empty Or txtDataCaricamento.Text <> Nothing Then
            If IsDate(GetFirstDate(txtDataCaricamento.Text)) And (IsDate(GetSecondDate(txtDataCaricamento.Text)) And Not GetSecondDate(txtDataCaricamento.Text) = Nothing) Then
                strSql = strSql + " AND pn.DataCaricamento Between '" & GetFirstDate(txtDataCaricamento.Text).ToString("yyyyMMdd") & "' And '" & GetSecondDate(txtDataCaricamento.Text).ToString("yyyyMMdd") & "'"
            Else
                strSql = strSql + " AND pn.DataCaricamento = '" & GetFirstDate(txtDataCaricamento.Text).ToString("yyyyMMdd") & "'"
            End If
        End If

        Return strSql

    End Function

    Private Function CheckFilter() As Boolean

        If ddlConformita.SelectedIndex <> 0 Then
            Return True
        End If

        If ddlProduttori.SelectedIndex <> 0 Then
            Return True
        End If

        If ddlStato.SelectedIndex <> 0 Then
            Return True
        End If

        If txtMatricola.Text <> String.Empty Then
            Return True
        End If

        If txtModello.Text <> String.Empty Then
            Return True
        End If

        If txtProtocollo.Text <> String.Empty Then
            Return True
        End If

        If txtDataCaricamento.Text <> String.Empty Then
            Return True
        End If

    End Function

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

    Private Sub cmdCerca_Click(sender As Object, e As EventArgs) Handles cmdCerca.Click

    End Sub
End Class
