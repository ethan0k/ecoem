
Imports ASPNET.StarterKit.BusinessLogicLayer

Partial Class WebApp_Amministrazione_Default
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim ListaProduttori As List(Of Produttore)
        Dim i As Integer

        ListaProduttori = Produttore.Lista
        For Each Produttore In ListaProduttori
            Dim ListaDichiarazioni As List(Of Dichiarazione)
            ListaDichiarazioni = Dichiarazione.Lista(Produttore.Id, 0)
            If Not ListaDichiarazioni Is Nothing Then
                For Each Dichiarazione In ListaDichiarazioni
                    If Dichiarazione.OldVersion And Dichiarazione.Data >= #01/01/2018# Then
                        Dim ListaRighe As List(Of RigaDichiarazione)

                        ListaRighe = RigaDichiarazione.Lista(Dichiarazione.Id)
                        For Each riga In ListaRighe
                            If riga.IdCategoria = 68 Then
                                If riga.Pezzi <> 0 Then
                                    i += 1
                                    'Dim catProduttore As Categoria_ProduttoreNe
                                    'catProduttore = Categoria_ProduttoreNew.Carica(riga.IdCategoria, Produttore.Id, 0)
                                    riga.KgDichiarazione = 0.1 * riga.Pezzi
                                    riga.Save()
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        Next

        Label1.Text = i.ToString + " righe modificate"
    End Sub
End Class
