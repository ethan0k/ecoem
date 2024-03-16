Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data

Partial Class WebApp_Impianti_DettaglioPannelli
    Inherits System.Web.UI.Page


    Private CurrentImpianto As Impianto

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Impianti/ListaImpianti.aspx")
    End Sub

    Private Sub GetCurrentImpianto()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("IdImpianto") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("IdImpianto")), IdFromQueryString) Then
            CurrentImpianto = Impianto.Carica(IdFromQueryString)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            GetCurrentImpianto()

            If Not CurrentImpianto Is Nothing Then
                titolo.Text = "Pannelli abbinati all'impianto " & CurrentImpianto.Descrizione
            End If
        End If
    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaImpianti.aspx")
    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        If String.Equals(e.CommandName, "Elimina") Then
            If Page.User.IsInRole("Utente") Then
                Exit Sub
            End If

            Dim PannelloDaEliminare As Pannello = Pannello.Carica(e.CommandArgument.ToString())
            PannelloDaEliminare.IdImpianto = 0
            PannelloDaEliminare.Save()

            Listview1.DataBind()
        End If

    End Sub


    Protected Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        If e.Item.ItemType = ListViewItemType.DataItem AndAlso Page.User.IsInRole("Monitor") Then
            Dim Delete As LinkButton = DirectCast(e.Item.FindControl("cmdDelete"), LinkButton)

            Delete.Style.Add("Display", "None")
        End If


    End Sub

    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        dt.Columns.Remove("IdMarca")

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaPannelli")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            Using rng As ExcelRange = ws.Cells("D1:D1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("F1:F1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            Using rng As ExcelRange = ws.Cells("A1:i1048576")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaPannelli.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

    End Sub
End Class
