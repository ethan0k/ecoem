Imports ASPNET.StarterKit.BusinessLogicLayer

Imports System.Data.SqlClient
Partial Class WebApp_Servizio_RiconciliaCategorie
    Inherits System.Web.UI.Page

    Protected Sub cmdRiconcilia_Click(sender As Object, e As EventArgs) Handles cmdRiconcilia.Click

        Dim ListaProduttori As List(Of Produttore)
        Dim ListaCategorieProduttore As List(Of Categoria_Produttore)
        Dim SqlDr As SqlDataReader
        Dim SqlCmd As SqlCommand
        Dim connStr As String
        Dim i As Integer

        connStr = ConfigurationManager.ConnectionStrings("aspnet_staterKits_TimeTracker").ConnectionString

        Dim SqlConn As New SqlConnection(connStr)
        SqlConn.Open()

        ListaProduttori = Produttore.Lista
        For Each Produttore In ListaProduttori
            ListaCategorieProduttore = Categoria_Produttore.Lista(Produttore.Id, False)
            If Not ListaCategorieProduttore Is Nothing Then
                For Each CatProduttore In ListaCategorieProduttore
                    SqlCmd = New SqlCommand("Select * from tbl_Riconciliazione_Categorie WHERE OLD_ID = " & CatProduttore.IdCategoria.ToString, SqlConn)
                    SqlDr = SqlCmd.ExecuteReader

                    Do While SqlDr.Read
                        Dim categProduttore As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(SqlDr.GetInt32(2), Produttore.Id, CatProduttore.Professionale)
                        If categProduttore Is Nothing Then
                            Dim NuovaCategoriaProduttore As New Categoria_ProduttoreNew
                            NuovaCategoriaProduttore.IdCategoria = SqlDr.GetInt32(2)
                            NuovaCategoriaProduttore.IdProduttore = CatProduttore.IdProduttore
                            NuovaCategoriaProduttore.Peso = CatProduttore.Peso
                            NuovaCategoriaProduttore.Professionale = CatProduttore.Professionale
                            NuovaCategoriaProduttore.Costo = CatProduttore.Costo

                            NuovaCategoriaProduttore.Save()
                            i += 1
                        End If
                    Loop

                    SqlDr.Close()
                    SqlCmd.Dispose()
                Next
            End If

            ListaCategorieProduttore = Categoria_Produttore.Lista(Produttore.Id, True)
            If Not ListaCategorieProduttore Is Nothing Then
                For Each CatProduttore In ListaCategorieProduttore
                    SqlCmd = New SqlCommand("Select * from tbl_Riconciliazione_Categorie WHERE OLD_ID = " & CatProduttore.IdCategoria.ToString, SqlConn)
                    SqlDr = SqlCmd.ExecuteReader

                    Do While SqlDr.Read
                        Dim categProduttore As Categoria_ProduttoreNew = Categoria_ProduttoreNew.Carica(SqlDr.GetInt32(2), Produttore.Id, CatProduttore.Professionale)
                        If categProduttore Is Nothing Then
                            Dim NuovaCategoriaProduttore As New Categoria_ProduttoreNew
                            NuovaCategoriaProduttore.IdCategoria = SqlDr.GetInt32(2)
                            NuovaCategoriaProduttore.IdProduttore = CatProduttore.IdProduttore
                            NuovaCategoriaProduttore.Peso = CatProduttore.Peso
                            NuovaCategoriaProduttore.Professionale = CatProduttore.Professionale
                            NuovaCategoriaProduttore.Costo = CatProduttore.Costo

                            NuovaCategoriaProduttore.Save()
                            i += 1
                        End If
                    Loop

                    SqlDr.Close()
                    SqlCmd.Dispose()
                Next
            End If
        Next

        lblEsito.Text = "Elaborazione completata. " & i.ToString & " inserimenti eseguiti"
    End Sub
End Class
