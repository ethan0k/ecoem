Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Partial Class WebApp_Pannelli_VerificaPannelli
    Inherits System.Web.UI.Page

    Private Sub WebApp_Pannelli_UploadDismessi_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            SqlDatasource1.SelectParameters("UserName").DefaultValue = Page.User.Identity.Name.ToString
            SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 0

            If IsStagingEmpty() Then
                lblEsito.Visible = False
                cmdEsporta.Visible = False
                cmdEliminaTutti.Visible = False
                dvGroup.Visible = False
            Else
                dvGroup.Visible = True
            End If
        End If

    End Sub

    Private Sub cmdSalva_Click(sender As Object, e As EventArgs) Handles cmdSalva.Click

        Dim table As New DataTable

        If Not fileUpload.HasFile Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun file selezionato!', 'Errore');", True)
            Exit Sub
        End If

        Dim fileName As String = fileUpload.FileName

        If Not Right(fileName, 4) = "xlsx" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Estensione file errata!', 'Errore');", True)
            Exit Sub
        Else
            fileName = Left(fileName, Len(fileName) - 5) + Today.ToBinary.ToString + TimeOfDay.ToBinary.ToString + ".xlsx"
        End If

        Dim pck = New OfficeOpenXml.ExcelPackage()

        Dim folder As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("UploadResourcePath"))

        Dim PathFile As String = folder & "\" & fileName

        Try
            fileUpload.SaveAs(PathFile)
        Catch ex As Exception

            Throw New System.Exception(ex.Message)
            Exit Sub

        End Try

        pck.Load(New IO.FileInfo(PathFile).OpenRead)
        If pck.Workbook.Worksheets.Count <> 0 Then
            Dim ws = pck.Workbook.Worksheets.First

            If ws.Name <> "Verifica" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve avere nome = Verifica', 'Errore');", True)
                Exit Sub
            End If

            If ws.Dimension.End.Column <> 1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Il foglio di lavoro deve contenere 1 colonna', 'Errore');", True)
                Exit Sub
            End If

            Dim hasHeader = True ' adjust accordingly '
            For Each firstRowCell In ws.Cells(1, 1, 1, ws.Dimension.End.Column)
                table.Columns.Add(If(hasHeader, firstRowCell.Text, String.Format("Column {0}", firstRowCell.Start.Column)))
            Next

            Dim startRow = If(hasHeader, 2, 1)
            For rowNum = startRow To ws.Dimension.End.Row
                Dim wsRow = ws.Cells(rowNum, 1, rowNum, ws.Dimension.End.Column)
                table.Rows.Add(wsRow.Select(Function(cell) cell.Text).ToArray)
            Next

            If table.Columns(0).ColumnName.ToString <> "Matricola" Then
                pck.Dispose()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('La prima colonna del foglio di lavoro deve avere nome = Matricola', 'Errore');", True)
                Exit Sub
            End If
        End If

        Dim row As DataRow
        Dim Count As Integer
        Dim Matricola As String
        Dim Errore As String
        Dim Importabile As Boolean
        Dim NrImportabili As Integer
        Dim i As Integer

        For i = 0 To table.Rows.Count - 1

            row = table.Rows(i)
            Count += 1

            Try
                If Not row.IsNull("Matricola") Then
                    Matricola = Convert.ToString(row("Matricola"))
                Else
                    Matricola = ""
                End If

                Importabile = True
                Errore = String.Empty
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('" & ex.Message.Replace("'", "") & " alla riga " & Count.ToString & "'" & ", 'Messaggio errore');", True)
                ' Aggiungere testo da visualizzare
                cmdEsporta.Visible = True
                cmdEliminaTutti.Visible = True
                lblEsito.Visible = True
                lblEsito.Text = "Sono presenti errori nell'importazione"
                lblEsito.ForeColor = System.Drawing.Color.Red
                dvGroup.Visible = True
                SqlDatasource1.DataBind()
                Exit Sub
            End Try

            If Matricola <> "" Then

                Dim PannelliTrovati As List(Of Pannello) = Pannello.Lista(Matricola)

                If PannelliTrovati Is Nothing Then
                    Errore = "Matricola non presente in archivio."
                    Importabile = True
                Else
                    Errore = "Matricola in archivio."
                    Importabile = False
                End If

                Dim newErrore As New ErroreImportazione

                If Importabile Then

                    newErrore.InErrore = False
                    newErrore.Matricola = Matricola
                    newErrore.Errore = Errore
                    newErrore.IdProduttore = 0
                    newErrore.DataCaricamento = Today
                    newErrore.UserName = Page.User.Identity.Name.ToString
                    newErrore.DataInizioGaranzia = #01/01/1900#
                    newErrore.DataCaricamento = Today.ToShortDateString()
                    newErrore.IdImpianto = 0
                    newErrore.TipoImportazione = 4 ' Verifica
                    newErrore.Protocollo = String.Empty
                    newErrore.Impianto = "Non abbinato"
                    newErrore.Stato = ""
                    newErrore.IdTipologia = 0
                    newErrore.IdFascia = 0
                    newErrore.CostoMatricola = 0
                    newErrore.NumeroFIR = String.Empty
                    newErrore.DataRitiro = DefaultValues.GetDateTimeMinValue
                    newErrore.Save()

                Else
                    For Each CurrPannello In PannelliTrovati

                        NrImportabili += 1
                        newErrore = New ErroreImportazione
                        newErrore.InErrore = True
                        If CurrPannello.Conforme Then
                            newErrore.Modello = "Conforme"
                        Else
                            newErrore.Modello = ""
                        End If
                        newErrore.Matricola = CurrPannello.Matricola
                        newErrore.Errore = Errore
                        newErrore.IdProduttore = 0
                        newErrore.DataCaricamento = Today
                        newErrore.UserName = Page.User.Identity.Name.ToString
                        newErrore.DataInizioGaranzia = #01/01/1900#
                        newErrore.DataCaricamento = Today.ToShortDateString()
                        newErrore.IdImpianto = CurrPannello.IdImpianto
                        If CurrPannello.IdImpianto <> 0 Then
                            Dim Impianto As Impianto = Impianto.Carica(CurrPannello.IdImpianto)
                            If not Impianto is Nothing Then
                                newErrore.Impianto =Impianto.Descrizione
                            Else
                                newErrore.Impianto = "Impianto " & CurrPannello.IdImpianto & " ELIMINATO"
                            End If
                        Else
                            newErrore.Impianto ="Non abbinato"
                        End If
                        If CurrPannello.Dismesso Then
                            newErrore.Stato = "Dismessa"
                        Else
                            newErrore.Stato = "Attiva"
                        End If
                        newErrore.TipoImportazione = 4 ' Verifica
                        newErrore.Protocollo = CurrPannello.Protocollo
                        newErrore.IdTipologia = 0
                        newErrore.IdFascia = 0
                        newErrore.CostoMatricola = 0
                        newErrore.DataRitiro = CurrPannello.DataRitiro
                        newErrore.NumeroFIR = CurrPannello.NumeroFIR
                        newErrore.Save()
                    Next
                End If
            End If
        Next

        SqlDatasource1.DataBind()
        ListaErrori.DataBind()

        If Not IsStagingEmpty() Then

            cmdEsporta.Visible = True
            cmdEliminaTutti.Visible = True
            dvGroup.Visible = True
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Caricamento completato! " & Count.ToString & " pannelli verificati.');", True)

        End If

    End Sub


    Private Sub cmdEliminaTutti_Click(sender As Object, e As EventArgs) Handles cmdEliminaTutti.Click

        ErroreImportazione.EliminaTuttiVerifica(Page.User.Identity.Name.ToString)
        ListaErrori.DataBind()

        cmdEsporta.Visible = False
        cmdEliminaTutti.Visible = False
        lblEsito.Visible = False
        dvGroup.Visible = False

    End Sub

    Private Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        dt.Columns.Remove("Id")
        dt.Columns.Remove("Peso")
        dt.Columns.Remove("IdMarca")
        dt.Columns.Remove("UserName")
        dt.Columns.Remove("IdProduttore")
        dt.Columns.Remove("Produttore")
        dt.Columns.Remove("TipoImportazione")
        dt.Columns.Remove("DataCaricamento")
        dt.Columns.Remove("DataInizioGaranzia")
        dt.Columns.Remove("AnnoCaricamento")
        dt.Columns.Remove("Importabile")
        dt.Columns("InErrore").ColumnName = "In archivio"
        dt.Columns("Errore").ColumnName = "Note"
        dt.Columns("Modello").ColumnName = "Conforme"

        Using pck As New ExcelPackage()

            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaRecordVerificati")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:G1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaRecordImportati.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

    End Sub

    Private Sub BTeRRORI_Click(sender As Object, e As EventArgs) Handles btErrori.Click

        SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 1
        SqlDatasource1.DataBind()
        btErrori.CssClass = "btn btn-danger"
        btTutti.CssClass = "btn btn-default"

    End Sub

    Private Sub btTutti_Click(sender As Object, e As EventArgs) Handles btTutti.Click

        SqlDatasource1.SelectParameters("SoloErrori").DefaultValue = 0
        SqlDatasource1.DataBind()
        btErrori.CssClass = "btn btn-default"
        btTutti.CssClass = "btn btn-danger"

    End Sub

    Private Function IsStagingEmpty() As Boolean

        Dim dv As System.Data.DataView = CType(SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView)

        Return dv.Count = 0

    End Function
End Class
