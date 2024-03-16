
Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_CatProfAbbinate
    Inherits System.Web.UI.Page

    Private CurrentProduttore As Produttore

    Private Sub WebApp_Amministrazione_CategorieAbbinate_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.Master, MasterPage).OverrideMenuUrl("/WebApp/Amministrazione/ListaProduttori.aspx")
    End Sub

    Private Sub WebApp_Amministrazione_CategorieAbbinate_Load(sender As Object, e As EventArgs) Handles Me.Load

        GetCurrentProduttore()

        If Not Page.IsPostBack Then


            If CurrentProduttore IsNot Nothing Then
                Titolo.Text = "Categorie professionali abbinate al produttore " & CurrentProduttore.RagioneSociale
            End If

        End If

        If Request.QueryString("Readonly") = "1" Then
            SqlDatasource1.SelectCommand = "SELECT * FROM tbl_CategorieNew INNER JOIN tbl_Categorie_ProduttoreNew ON tbl_Categorie_ProduttoreNew.IdCategoria = tbl_CategorieNew.Id WHERE tbl_Categorie_ProduttoreNew.Professionale = 1 And tbl_Categorie_ProduttoreNew.IdProduttore = " & CurrentProduttore.Id & " Order By IdMacroCategoria"
        End If


    End Sub

    Private Sub GetCurrentProduttore()

        Dim IdFromQueryString As Integer
        If Not Request.QueryString("Id") Is Nothing AndAlso Int32.TryParse(CStr(Request.QueryString("Id")), IdFromQueryString) Then
            CurrentProduttore = Produttore.Carica(IdFromQueryString)
        End If

    End Sub

    Private Sub Listview1_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles Listview1.ItemDataBound

        If (e.Item.ItemType = ListViewItemType.DataItem) Then

            Dim myCheckBox As CheckBox = CType(e.Item.FindControl("Abbinato"), CheckBox)
            Dim myTextBox As TextBox = CType(e.Item.FindControl("txtCosto"), TextBox)
            Dim myTextBoxPeso As TextBox = CType(e.Item.FindControl("txtPeso"), TextBox)
            Dim dataItem As ListViewDataItem = DirectCast(e.Item, ListViewDataItem)

            Dim CatPRO As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(CInt(Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString), CInt(Request.QueryString("Id")), True)
            If CatPRO IsNot Nothing Then
                myCheckBox.Checked = True
                'myTextBox.Enabled = True
                myTextBoxPeso.Enabled = True
                'myTextBox.Text = String.Format("{0:#,##0.000}", CatPRO.Costo)
                myTextBoxPeso.Text = String.Format("{0:#,##0.000}", CatPRO.Peso)
            Else

                myCheckBox.Checked = False
                'myTextBox.Text = String.Empty
                'myTextBox.Enabled = False
                myTextBoxPeso.Enabled = False
                myTextBoxPeso.Text = String.Empty
            End If

        End If

    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim cb As CheckBox = DirectCast(sender, CheckBox)
        Dim item As ListViewItem = DirectCast(cb.NamingContainer, ListViewItem)
        Dim dataItem As ListViewDataItem = DirectCast(item, ListViewDataItem)
        Dim txtCosto As TextBox = CType(Listview1.Items(dataItem.DisplayIndex).FindControl("txtCosto"), TextBox)
        Dim txtPeso As TextBox = CType(Listview1.Items(dataItem.DisplayIndex).FindControl("txtPeso"), TextBox)
        Dim code As String = Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString()

        If cb.Checked Then
            Dim CatPRO As New Categoria_ProduttoreNew
            CatPRO.IdCategoria = CInt(code)
            CatPRO.IdProduttore = CInt(Request.QueryString("Id"))
            CatPRO.Professionale = True
            CatPRO.Save()
            txtPeso.Enabled = True
            'txtCosto.Enabled = True
            If CatPRO.IdCategoria = 30 Then ' 4.14 Pannelli fotovoltaici
                Dim NewLogIscrizione As LogIscrizione = LogIscrizione.CaricaDaProduttore(CatPRO.IdProduttore)
                If NewLogIscrizione Is Nothing Then
                    NewLogIscrizione = New LogIscrizione
                    NewLogIscrizione.IdProduttore = CatPRO.IdProduttore
                    NewLogIscrizione.DataIscrizione = Today
                    NewLogIscrizione.DataDisiscrizione = DefaultValues.GetDateTimeMinValue
                    NewLogIscrizione.Save()
                End If
            End If
        Else
            Dim CatPRO As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(CInt(code), CInt(Request.QueryString("Id")), True)
            CatPRO.Elimina()
            txtPeso.Enabled = False

            If CatPRO.IdCategoria = 30 Then ' 4.14 Pannelli fotovoltaici
                Dim CatDomestica As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(CInt(code), CInt(Request.QueryString("Id")), False)
                If CatDomestica Is Nothing Then
                    Dim NewLogIscrizione As LogIscrizione = LogIscrizione.CaricaDaProduttore(CatPRO.IdProduttore)
                    If Not NewLogIscrizione Is Nothing Then
                        NewLogIscrizione.DataDisiscrizione = Today
                        NewLogIscrizione.Save()
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub txtCosto_Changed(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim txtCosto As TextBox = DirectCast(sender, TextBox)
        Dim item As ListViewItem = DirectCast(txtCosto.NamingContainer, ListViewItem)
        Dim dataItem As ListViewDataItem = DirectCast(item, ListViewDataItem)
        Dim code As String = Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString()

        Dim CatPRO As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(CInt(code), CInt(Request.QueryString("Id")), True)

        CatPRO.Costo = txtCosto.Text
        CatPRO.Save()

        txtCosto.Text = String.Format("{0:#,##0.000}", CatPRO.Costo)
    End Sub

    Protected Sub txtPeso_Changed(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim txtPeso As TextBox = DirectCast(sender, TextBox)
        Dim item As ListViewItem = DirectCast(txtPeso.NamingContainer, ListViewItem)
        Dim dataItem As ListViewDataItem = DirectCast(item, ListViewDataItem)
        Dim code As String = Listview1.DataKeys(dataItem.DisplayIndex).Value.ToString()

        Dim CatPRO As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(CInt(code), CInt(Request.QueryString("Id")), True)

        CatPRO.Peso = txtPeso.Text
        CatPRO.Save()

        txtPeso.Text = String.Format("{0:#,##0.000}", CatPRO.Peso)
    End Sub
End Class
