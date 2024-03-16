
Imports System.Data
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml

Partial Class WebApp_Tariffari_TariffarioP
    Inherits System.Web.UI.Page

    Private Sub WebApp_Tariffari_TariffarioP_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
        Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(userGuid)

        SqlDatasource1.SelectParameters("IdProduttore").DefaultValue = UtenteProduttore.IdProduttore
        SqlDatasource1.SelectParameters("Professionale").DefaultValue = 1

    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()
        dt.Columns.Remove("Id")

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Tariffario professionale")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            Using rng As ExcelRange = ws.Cells("A1:d10")
                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=TariffarioProfessionale.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound


        If (e.Item.ItemType = ListViewItemType.DataItem) Then
            Dim LabelCosto As Label = CType(e.Item.FindControl("txtCosto"), Label)
            Dim LabelTipoDiDato As Label = CType(e.Item.FindControl("txtTipoDiDato"), Label)

            If LabelTipoDiDato.Text = "Valore" Then
                LabelCosto.Text = "Eur/Kg " + LabelCosto.Text
            Else
                LabelCosto.Text = "Eur/pezzo " + LabelCosto.Text
            End If
        End If

    End Sub
End Class
