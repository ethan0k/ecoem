Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Data
Imports System.Drawing

Partial Class WebApp_Impianti_ListaImpianti
    Inherits System.Web.UI.Page

    Protected Sub cmdNuovo_Click(sender As Object, e As EventArgs) Handles cmdNuovo.Click
        Response.Redirect("Impianto.aspx")
    End Sub

    Protected Sub Listview1_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles Listview1.ItemCommand

        divError.Visible = False

        If String.Equals(e.CommandName, "Edit") Then
            Response.Redirect("Impianto.aspx?id=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Elimina") Then
            Dim MyImpianto As Impianto = Impianto.Carica(e.CommandArgument.ToString())
            If MyImpianto.Controlla Then
                MyImpianto.Elimina()
                Listview1.DataBind()

            Else
                divError.Visible = True
                lblError.Text = "Impossibile procedere. Esistono pannelli abbinati."
                Exit Sub
            End If

        ElseIf String.Equals(e.CommandName, "Pannelli") Then
            Response.Redirect("DettaglioPannelli.aspx?IdImpianto=" & e.CommandArgument.ToString())

        ElseIf String.Equals(e.CommandName, "Certificato") Then

            Dim Impianto As Impianto = Impianto.Carica(e.CommandArgument.ToString())
            Dim ListaPannelli As List(Of Pannello) = Pannello.Lista(Impianto.Id)
            Dim PesoModulo As Decimal
            Dim ModelloModulo As String
            Dim MarcaModulo As String
            Dim i As Integer

            If ListaPannelli Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Nessun pannello abbinato all'impianto!', 'Errore');", True)
                Exit Sub
            End If

            If Impianto.Verifica Then ' Sono tutti conformi i pannelli?

                If Impianto.Attestato = String.Empty Then ' E' stato generato l'attestato?
                    Impianto.GeneraAttestato()
                End If

                Dim pdfTools = New PdfTools()
                Dim serialNumbers = New List(Of String)()
                Dim Modelli = New List(Of String)()
                Dim Marche As New List(Of String)()
                Dim currentProduttore As Produttore
                Dim firstLoop As Boolean = True
                Dim Peso As New List(Of String)()
                For Each Pannello In ListaPannelli
                    serialNumbers.Add(Pannello.Matricola)
                    Modelli.Add(Pannello.Modello)
                    Marche.Add(Pannello.Produttore)
                    Peso.Add(Pannello.Peso.ToString)
                    If firstLoop Then
                        currentProduttore = Produttore.Carica(Pannello.IdMarca)
                        firstLoop = False
                        'PesoModulo = Pannello.Peso                   
                        'ModelloModulo = Pannello.Modello
                        'MarcaModulo = Pannello.Produttore
                    End If

                    If currentProduttore.Id <> Pannello.IdMarca Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Produttori diversi. Contattare il responsabile del consorzio!', 'Errore');", True)
                        Exit Sub
                    End If
                    If Impianto.NomeProduttore <> "" Then
                        currentProduttore.RagioneSociale = Impianto.NomeProduttore
                    End If
                Next

                pdfTools.CreateDocument(Server.MapPath("~/Template/Certificato_new.pdf"), Response.OutputStream, New DocInfo() With { _
                    .No = Impianto.Attestato, _
                    .Producer = currentProduttore.RagioneSociale, _
                    .FirstName = Impianto.Responsabile, _
                    .LastName = "", _
                    .Address = Impianto.Indirizzo, _
                    .GseNumber = Impianto.NrPraticaGSE, _
                    .ServiceStartDate = Impianto.DataEntrataInEsercizio, _
                    .City = Impianto.Città, _
                    .Region = Impianto.Regione, _
                    .Cap = Impianto.Cap, _
                    .Longitude = Impianto.Longitudine, _
                    .Latitude = Impianto.Latitudine, _
                    .ContoEnergia = Trim(Impianto.ContoEnergia), _
                    .Quantity = ListaPannelli.Count, _
                    .ModuleSerialNumbers = serialNumbers _
                    }, 28, 50, Marche, Peso, Modelli, serialNumbers)

                Response.AddHeader("Content-Disposition", "attachment; filename=Certificato.pdf")
                Response.ContentType = "application/pdf"

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Impossibile stampare. Contattare il responsabile del consorzio!', 'Errore');", True)
            End If


        End If

    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        If Not Page.IsPostBack Then

            If Page.User.IsInRole("Utente") Then
                divCliente.Attributes.CssStyle.Add("Display", "None")
                divCodice.Attributes.CssStyle.Add("Margin-left", "1")
                Dim userGuid As Guid = DirectCast(Membership.GetUser().ProviderUserKey, Guid)
                Dim UtenteCliente As UtenteCliente = UtenteCliente.Carica(userGuid)

                ' Assegna il valore predefinito
                ddlCliente.SelectedValue = UtenteCliente.IdCliente
                SqlDatasource1.DataBind()

                Listview1.DataBind()
            ElseIf Page.User.IsInRole("Amministratore") Then
                SqlDatasource1.DataBind()
            Else
                Response.Redirect("..\Login.aspx")
            End If
        End If

    End Sub

    Protected Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        If Not Page.User.IsInRole("Utente") Then
            ddlCliente.SelectedIndex = 0
        End If

        txtDescrizione.Text = String.Empty
        txtCodice.Text = String.Empty
    End Sub

    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        dt.Columns.Remove("id")

        Using pck As New ExcelPackage()
            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaImpianti")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:L1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("J1:K1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using


            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaImpianti.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using

        'Response.ContentType = "application/vnd.ms-excel"
        'Response.Charset = " "
        'Response.AddHeader("Content-Disposition", "attachment; filename=ListaImpianti.xls;")

        'Response.Write("<table border=0><tr><td style='font-family:Arial; font-size:10pt;'>Codice" & _
        '       "</td><td style='font-family:Arial; font-size:10pt;'>Descrizione</td>" & _
        '       "<td style='font-family:Arial; font-size:10pt;'>Indirizzo</td>" & _
        '       "<td style='font-family:Arial; font-size:10pt;'>Cap</td>" & _
        '       "<td style='font-family:Arial; font-size:10pt;'>Città</td>" & _
        '        "<td style='font-family:Arial; font-size:10pt;'>Provincia</td>" & _
        '        "<td style='font-family:Arial; font-size:10pt;'>Latitudine</td>" & _
        '        "<td style='font-family:Arial; font-size:10pt;'>Longitudine</td>" & _
        '        "<td style='font-family:Arial; font-size:10pt;'>Data entrata in esercizio</td>" & _
        '        "<td style='font-family:Arial; font-size:10pt;'>Data Creazione</td>" & _
        '       "</tr>")

        'For Each row As DataRow In dt.Rows

        '    Response.Write("<table border=0><tr><td style='font-family:Arial; font-size:10pt;'>" & row.Item(1) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(2) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(3) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(4) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(5) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(6) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(7) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(8) & _
        '                "</td><td style='font-family:Arial; font-size:10pt;'>" & row.Item(9) & _
        '                "</td></tr>")

        'Next

        'Response.End()


    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Page.User.IsInRole("Monitor") Then
            cmdNuovo.Visible = False
        End If
    End Sub

    Protected Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        Dim targetButton As LinkButton = e.Item.FindControl("cmdDelete")

        targetButton.Visible = Not User.IsInRole("Monitor")

    End Sub
End Class
