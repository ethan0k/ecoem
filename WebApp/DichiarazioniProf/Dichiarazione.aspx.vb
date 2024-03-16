
Imports ASPNET.StarterKit.BusinessLogicLayer
Imports OfficeOpenXml.Style
Imports System.Drawing
Imports System.Data

Partial Class WebApp_Amministrazione_Dichiarazione
    Inherits System.Web.UI.Page

    Dim CurrentDichiarazione As Dichiarazione
    Dim CurrentUser As MembershipUser

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/DichiarazioniProf/ListaDichiarazioniProf.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_Dichiarazione_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.User.IsInRole("Amministratore") And Not Page.User.IsInRole("Produttore") Then
            Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
        End If

        GetCurrentDichiarazione()

        If Not CurrentDichiarazione Is Nothing Then

            If Not CurrentDichiarazione.Professionale Then
                Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
            End If

            ddlProduttori.Enabled = False
            CurrentUser = Membership.GetUser()
            ' Verifica identità
            If Page.User.IsInRole("Produttore") Then
                Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(CurrentUser.ProviderUserKey)

                If (UtenteProduttore.IdProduttore <> CurrentDichiarazione.IdProduttore) Then
                    Response.Redirect("~/WebApp/Dashboard/Dashboard.aspx")
                End If

                cmdRiapri.Visible = False
                cmdFatturata.Visible = False
            End If

            If Page.User.IsInRole("Amministratore") Then
                If CurrentDichiarazione.Confermata Then
                    cmdRiapri.Visible = True
                Else
                    cmdRiapri.Visible = False
                End If
            End If

            txtData.Text = CurrentDichiarazione.Data
            ddlProduttori.SelectedValue = CurrentDichiarazione.IdProduttore
            'txtImporto.Text = String.Format("{0:#,##0.00}", CurrentDichiarazione.Importo)
            lblPeriodo.Text = GetPeriodo(CurrentDichiarazione.IdProduttore, CurrentDichiarazione.Data)


            If CurrentDichiarazione.Confermata Then
                txtDataConferma.Text = CurrentDichiarazione.DataConferma
                titleConfermata.Visible = True
                lblConfermata.Text = "Si"
                cmdConferma.Visible = False
            Else
                titleConfermata.Visible = False
                lblConfermata.Text = "No"
            End If

            If CurrentDichiarazione.OldVersion Then
                OldVersion.Value = 1
            Else
                OldVersion.Value = 0
            End If

        Else
            ' Amministratore crea dichiarazione nuova
            ddlProduttori.Enabled = True
            cmdRiapri.Visible = False

        End If

    End Sub

    Private Sub cmdAnnulla_Click(sender As Object, e As EventArgs) Handles cmdAnnulla.Click
        Response.Redirect("ListaDichiarazioniProf.aspx")
    End Sub

    Private Sub GetCurrentDichiarazione()
        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentDichiarazione = Dichiarazione.Carica(IdFromQueryString)
        End If
    End Sub

    Private Sub GetCurrentUser()

        If Not Request.QueryString("Id") Is Nothing Then
            Dim userkey As Guid = New Guid(Page.User.Identity.Name)
            CurrentUser = Membership.GetUser()
        End If

    End Sub
    Protected Sub txtPezzi_TextChanged(sender As Object, e As EventArgs)

        Dim CatProduttore As Categoria_ProduttoreNew
        Dim RigaDichiarazione As RigaDichiarazione
        Dim txtPezzi As TextBox = DirectCast(sender, TextBox)
        Dim item As ListViewItem = DirectCast(txtPezzi.NamingContainer, ListViewItem)
        Dim dataItem As ListViewDataItem = DirectCast(item, ListViewDataItem)
        Dim IdKey As String = Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString()
        Dim Dichiarazione As Dichiarazione

        RigaDichiarazione = RigaDichiarazione.Carica(CInt(IdKey))
        CatProduttore = Categoria_ProduttoreNew.Carica(RigaDichiarazione.IdCategoria, ddlProduttori.SelectedValue, True)
        RigaDichiarazione.Pezzi = txtPezzi.Text
        RigaDichiarazione.KgDichiarazione = CatProduttore.Peso * CInt(txtPezzi.Text)
        'RigaDichiarazione.Importo = RigaDichiarazione.Pezzi * RigaDichiarazione.CostoUnitario
        RigaDichiarazione.Save()

        'lblImporto.Text = String.Format("{0:#,##0.00}", RigaDichiarazione.Importo)

        Dichiarazione = Dichiarazione.Carica(CInt(Request.QueryString("Id")))

        'txtImporto.Text = String.Format("{0:#,##0.00}", Dichiarazione.Importo)

    End Sub

    Protected Sub txtKg_TextChanged(sender As Object, e As EventArgs)

        Dim RigaDichiarazione As RigaDichiarazione
        Dim txtKg As TextBox = DirectCast(sender, TextBox)
        Dim item As ListViewItem = DirectCast(txtKg.NamingContainer, ListViewItem)
        Dim dataItem As ListViewDataItem = DirectCast(item, ListViewDataItem)
        Dim IdKey As String = Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString()
        Dim Dichiarazione As Dichiarazione
        'Dim lblImporto As Label = CType(Listview1.Items(dataItem.DisplayIndex).FindControl("lblImporto"), Label)

        RigaDichiarazione = RigaDichiarazione.Carica(CInt(IdKey))
        RigaDichiarazione.Kg = txtKg.Text
        RigaDichiarazione.KgDichiarazione = txtKg.Text
        'RigaDichiarazione.Importo = RigaDichiarazione.Kg * RigaDichiarazione.CostoUnitario
        RigaDichiarazione.Save()

        'lblImporto.Text = String.Format("{0:#,##0.00}", RigaDichiarazione.Importo)

        Dichiarazione = Dichiarazione.Carica(CInt(Request.QueryString("Id")))

        'txtImporto.Text = String.Format("{0:#,##0.00}", Dichiarazione.Importo)

    End Sub

    Private Sub cmdConferma_Click(sender As Object, e As EventArgs) Handles cmdConferma.Click

        GetCurrentDichiarazione()

        If Not CurrentDichiarazione Is Nothing Then
            CurrentDichiarazione.Confermata = True
            CurrentDichiarazione.DataConferma = Today.ToShortDateString
            'CurrentDichiarazione.Data = Today.ToShortDateString
            CurrentDichiarazione.Save()

            Listview1.Enabled = False
            cmdConferma.Visible = False
            lblConfermata.Text = "Si"
            txtDataConferma.Text = CurrentDichiarazione.DataConferma
            txtData.Text = CurrentDichiarazione.Data
            cmdRiapri.Visible = True
        End If

    End Sub

    Private Sub ddlProduttori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduttori.SelectedIndexChanged

        Dim Dichiarazione As New Dichiarazione
        Dim ListaCategorie As List(Of Categoria_ProduttoreNew)
        Dim Produttore As Produttore = Produttore.Carica(ddlProduttori.SelectedValue)

        ListaCategorie = Categoria_ProduttoreNew.Lista(ddlProduttori.SelectedValue, True)
        If ListaCategorie Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Anagrafica Produttore senza categorie professionali.'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        Dim DataDichiarazione As Date
        DataDichiarazione = CalcolaDataDichiarazione(Produttore.Periodicita)
        Dichiarazione = Dichiarazione.Carica(Produttore.Id, DataDichiarazione,1)
        If Dichiarazione Is Nothing Then
            Dichiarazione = New Dichiarazione
            Dichiarazione.IdProduttore = ddlProduttori.SelectedValue
            Dichiarazione.Data = DataDichiarazione
            Dichiarazione.DataRegistrazione = Today.ToShortDateString
            Dichiarazione.Utente = Page.User.Identity.Name
            Dichiarazione.Professionale = True
            Dichiarazione.Save()

            For Each CategoriaInLista In ListaCategorie
                Dim NuovaRiga As New RigaDichiarazione
                Dim Categoria As CategoriaNew = CategoriaNew.Carica(CategoriaInLista.IdCategoria)
                NuovaRiga.IdCategoria = CategoriaInLista.IdCategoria
                NuovaRiga.IdDichiarazione = Dichiarazione.Id
                NuovaRiga.TipoDiDato = Categoria.TipoDiDato
                NuovaRiga.Pezzi = 0
                NuovaRiga.CostoUnitario = CategoriaInLista.Costo
                NuovaRiga.Importo = 0
                NuovaRiga.DataAggiornamento = Today
                NuovaRiga.UtenteAggiornamento = Page.User.Identity.Name
                NuovaRiga.Save()
            Next

            ddlProduttori.Enabled = False
            txtData.Text = Dichiarazione.Data
            'txtImporto.Text = 0
            cmdRiapri.Visible = False
        End If

        Response.Redirect("Dichiarazione.aspx?id=" & Dichiarazione.Id)


    End Sub
    Private Function CalcolaDataDichiarazione(Periodicita As Integer) As Date

        Dim DataDichiarazione As Date

        DataDichiarazione = DateSerial(Year(Today) - 1, 12, 31)

        Return DataDichiarazione

    End Function

    Private Sub cmdRiapri_Click(sender As Object, e As EventArgs) Handles cmdRiapri.Click

        GetCurrentDichiarazione()

        If CurrentDichiarazione.AutocertificazioneProdotta Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dichiarazione certificata. Impossibile riaprire'" & ", 'Messaggio errore');", True)
            Exit Sub
        End If

        CurrentDichiarazione.Confermata = False
        CurrentDichiarazione.DataConferma = DefaultValues.GetDateTimeMinValue.ToShortDateString
        CurrentDichiarazione.Save()

        cmdConferma.Visible = True
        cmdRiapri.Visible = False
        txtDataConferma.Text = ""
        lblConfermata.Text = "No"
        titleConfermata.Visible = False

    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        If (e.Item.ItemType = ListViewItemType.DataItem) Then

            Dim item As ListViewDataItem = DirectCast(e.Item, ListViewDataItem)
            Dim KeyId As String = Listview1.DataKeys(item.DisplayIndex)("Id").ToString()
            Dim RigaDichiarazione As RigaDichiarazione

            RigaDichiarazione = RigaDichiarazione.Carica(CInt(KeyId.ToString))

            If CurrentDichiarazione.Confermata Then
                Dim myTextkg As TextBox = CType(e.Item.FindControl("txtKg"), TextBox)
                Dim myTextbox As TextBox = CType(e.Item.FindControl("txtPezzi"), TextBox)
                myTextbox.Enabled = False
                myTextkg.Enabled = False

                myTextkg.ToolTip = "Campo disabilitato"
                myTextkg.ToolTip = "Campo disabilitato"
            End If

        End If

    End Sub

    Protected Sub cmdFatturata_Click(sender As Object, e As EventArgs) Handles cmdFatturata.Click

        GetCurrentDichiarazione()

        If Not CurrentDichiarazione.Fatturata Then
            If Not CurrentDichiarazione.Confermata Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "jAlert('Dichiarazione non confermata. Impossibile procedere'" & ", 'Messaggio errore');", True)
                Exit Sub
            End If
        End If


        CurrentDichiarazione.Fatturata = Not CurrentDichiarazione.Fatturata
        CurrentDichiarazione.Save()

    End Sub

    Protected Sub cmdEsporta_Click(sender As Object, e As EventArgs) Handles cmdEsporta.Click

        GetCurrentDichiarazione()

        Dim dt As DataTable = DirectCast(Me.SqlDatasource1.[Select](DataSourceSelectArguments.Empty), DataView).ToTable()

        dt.Columns.Remove("ID")
        dt.Columns.Remove("IdCategoria")
        dt.Columns.Remove("TipoDiDato")
        dt.Columns("CostoUnitario").ColumnName = "Costo unitario"

        Using pck As New OfficeOpenXml.ExcelPackage()

            'Create the worksheet
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets.Add("Dichiarazione")

            Using rng As OfficeOpenXml.ExcelRange = ws.Cells("A1:A1")
                rng.Style.Font.Bold = True
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid
                rng.Style.Font.Size = 16
                'Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189))
                'Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White)
                rng.Value = "Dichiarazione ECO contributi"
                ws.Cells(1, 1, 1, 4).Merge = True
            End Using

            ws.Cells(3, 1).Value = "Produttore"
            ws.Cells(4, 1).Value = "Data"
            ws.Cells(5, 1).Value = "Data conferma"
            ws.Cells(6, 1).Value = "Importo"

            Using rng As OfficeOpenXml.ExcelRange = ws.Cells("A3:A6")
                rng.Style.Font.UnderLine = True
                rng.AutoFitColumns()
            End Using

            ws.Cells(3, 2).Value = ddlProduttori.SelectedItem
            ws.Cells(4, 2).Value = CDate(txtData.Text)
            ws.Cells(4, 2).Style.Numberformat.Format = "dd/mm/yy"
            ws.Cells(5, 2).Value = CDate(txtDataConferma.Text)
            ws.Cells(5, 2).Style.Numberformat.Format = "dd/mm/yy"
            ws.Cells(6, 2).Value = 0
            ws.Cells(6, 2).Style.Numberformat.Format = "#,##0.00"

            ws.Cells("A9").LoadFromDataTable(dt, True)

            Using rng As OfficeOpenXml.ExcelRange = ws.Cells("A1:g100")

                rng.AutoFitColumns()
            End Using

            'Write it back to the client
            Response.AddHeader("content-disposition", "attachment;  filename=Dichiarazione.xlsx")
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(pck.GetAsByteArray())
            Response.End()

        End Using

    End Sub

    Private Function GetPeriodo(IdProduttore As Integer, DataDichiarazione As Date) As String

        If DataDichiarazione < #1/1/2001# Then
            Return ""
        End If

        Return ("Annuale")

    End Function


End Class
