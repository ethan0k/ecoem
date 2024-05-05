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

                pdfTools.CreateDocument(Server.MapPath("~/Template/Certificato_new.pdf"), Response.OutputStream, New DocInfo() With {
                    .No = Impianto.Attestato,
                    .Producer = currentProduttore.RagioneSociale,
                    .FirstName = Impianto.Responsabile,
                    .LastName = "",
                    .Address = Impianto.Indirizzo,
                    .GseNumber = Impianto.NrPraticaGSE,
                    .ServiceStartDate = Impianto.DataEntrataInEsercizio,
                    .City = Impianto.Citta,
                    .Region = Impianto.Regione,
                    .Cap = Impianto.Cap,
                    .Longitude = Impianto.Longitudine,
                    .Latitude = Impianto.Latitudine,
                    .ContoEnergia = Trim(Impianto.ContoEnergia),
                    .Quantity = ListaPannelli.Count,
                    .ModuleSerialNumbers = serialNumbers
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

        Dim dt As New DataTable
        Dim ListaImpianti As List(Of Impianto)

        If ddlCliente.SelectedIndex <> 0 Then
            ListaImpianti = Impianto.Lista(ddlCliente.SelectedValue)
        Else
            ListaImpianti = Impianto.Lista(0)
        End If

        dt.Columns.Add("Codice")
        dt.Columns.Add("Descrizione")
        dt.Columns.Add("Indirizzo")
        dt.Columns.Add("Cap")
        dt.Columns.Add("Città")
        dt.Columns.Add("Provincia")
        dt.Columns.Add("Regione")
        dt.Columns.Add("Latitudine")
        dt.Columns.Add("Longitudine")
        dt.Columns.Add("Responsabile")
        dt.Columns.Add("NrPraticaGSE")
        dt.Columns.Add("ContoEnergia")
        dt.Columns.Add("DataEntrataInEsercizio")
        dt.Columns("DataEntrataInEsercizio").DataType = System.Type.GetType("System.DateTime")
        dt.Columns.Add("NrMatricole")
        dt.Columns("NrMatricole").DataType = System.Type.GetType("System.Int32")
        'dt.Columns.Add("NrAttestato")
        'dt.Columns.Add("DataAttestato")
        dt.Columns.Add("Produttori")
        dt.Columns.Add("Protocolli")
        If Page.User.IsInRole("Amministratore") Then
            dt.Columns.Add("EuroVersati")
            dt.Columns("EuroVersati").DataType = System.Type.GetType("System.Decimal")
        End If
        dt.Columns.Add("PdfScaricabile")
        For Each Impianto In ListaImpianti
            Dim newRow As DataRow = dt.NewRow
            newRow("Codice") = Impianto.Codice
            newRow("Descrizione") = Impianto.Descrizione
            newRow("Indirizzo") = Impianto.Indirizzo
            newRow("CAP") = Impianto.Cap
            newRow("Città") = Impianto.Citta
            newRow("Provincia") = Impianto.Provincia
            newRow("Regione") = Impianto.Regione
            newRow("Latitudine") = Impianto.Latitudine
            newRow("Longitudine") = Impianto.Longitudine
            newRow("Responsabile") = Impianto.Responsabile
            newRow("NrPraticaGSE") = Impianto.NrPraticaGSE
            newRow("ContoEnergia") = Impianto.ContoEnergia
            newRow("DataEntrataInEsercizio") = Impianto.DataEntrataInEsercizio
            newRow("NrMatricole") = Impianto.TotaleMatricole
            'newRow("NrAttestato") = Impianto.NrAttestato
            'newRow("DataAttestato") = Impianto.DataAttestato
            newRow("Produttori") = Impianto.ListaProduttori
            newRow("Protocolli") = Impianto.ListaProtocolli
            If Page.User.IsInRole("Amministratore") Then
                newRow("EuroVersati") = Impianto.Valore.ToString("#,0.00")
            End If
            newRow("PdfScaricabile") = Impianto.Verifica
            dt.Rows.Add(newRow)
        Next

        Using pck As New ExcelPackage()
            'Create the worksheet
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("ListaImpianti")

            'Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            ws.Cells("A1").LoadFromDataTable(dt, True)

            'Format the header for column 1-3
            Using rng As ExcelRange = ws.Cells("A1:r1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
            End Using

            Using rng As ExcelRange = ws.Cells("m1:m1048576")
                rng.Style.Numberformat.Format = "dd/mm/yy"
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=ListaImpianti.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()
        End Using



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
