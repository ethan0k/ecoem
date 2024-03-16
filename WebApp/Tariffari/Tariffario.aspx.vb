Imports System.Data
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml

Partial Class WebApp_Tariffari_Tariffario
    Inherits System.Web.UI.Page

    Private Sub WebApp_Tariffari_Tariffario_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
        Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(userGuid)

        SqlDatasource1.SelectParameters("IdProduttore").DefaultValue = UtenteProduttore.IdProduttore

    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()
        dt.Columns.Remove("Id")

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Tariffario pannelli")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            Using rng As ExcelRange = ws.Cells("A1:d10")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=TariffarioPannelli.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using
    End Sub
End Class
