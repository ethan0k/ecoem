Imports ASPNET.StarterKit.BusinessLogicLayer
Imports System.IO
Imports System.Globalization


Partial Class WebApp_Dashboard_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim cultureToUse As New CultureInfo("it-IT")
        Dim Numero As Decimal

        If Page.User.IsInRole("Amministratore") Then

            totPannelli.Text = Pannello.TotalePannelli.ToString("#,##0", cultureToUse)
            totProduttori.Text = Produttore.TotaleProduttori.ToString("#,##0", cultureToUse)
            totImpianti.Text = Impianto.TotaleImpianti.ToString("#,##0", cultureToUse)
            totPannelliConformi.Text = Pannello.TotaleConformi.ToString("#,##0", cultureToUse)
            totPannelliDismessi.Text = Pannello.TotaleDismessi.ToString("#,##0", cultureToUse)

            Try
                'totR1.Text = Dichiarazione.TotaleKgCategoria("R1", Year(Today)).ToString("#,##0.00")
                'Numero = Dichiarazione.TotaleKgCategoria("R1", Year(Today)) + Dichiarazione.TotaleKgCategoriaNew("R1", Year(Today))
                Numero = Dichiarazione.TotaleKgCategoriaNew("R1", Year(Today))
                totR1.Text = Numero.ToString("#,##0.00")
            Catch ex As Exception
                totR1.Text = 0
            End Try

            Try
                'totR2.Text = Dichiarazione.TotaleKgCategoria("R2", Year(Today)).ToString("#,##0.00")
                'Numero = Dichiarazione.TotaleKgCategoria("R2", Year(Today)) + Dichiarazione.TotaleKgCategoriaNew("R2", Year(Today))
                Numero = Dichiarazione.TotaleKgCategoriaNew("R2", Year(Today))
                totR2.Text = Numero.ToString("#,##0.00")
            Catch ex As Exception
                totR2.Text = 0
            End Try

            Try
                'totR3.Text = Dichiarazione.TotaleKgCategoria("R3", Year(Today)).ToString("#,##0.00")
                'Numero = Dichiarazione.TotaleKgCategoria("R3", Year(Today)) + Dichiarazione.TotaleKgCategoriaNew("R3", Year(Today))
                Numero = Dichiarazione.TotaleKgCategoriaNew("R3", Year(Today))
                totR3.Text = Numero.ToString("#,##0.00")
            Catch ex As Exception
                totR3.Text = 0
            End Try

            Try
                'totR4.Text = Dichiarazione.TotaleKgCategoria("R4", Year(Today)).ToString("#,##0.00")
                'Numero = Dichiarazione.TotaleKgCategoria("R4", Year(Today)) + Dichiarazione.TotaleKgCategoriaNew("R4", Year(Today))
                Numero = Dichiarazione.TotaleKgCategoriaNew("R4", Year(Today))
                totR4.Text = Numero.ToString("#,##0.00")
            Catch ex As Exception
                totR4.Text = 0
            End Try

            Try
                'totR5.Text = Dichiarazione.TotaleKgCategoria("R5", Year(Today)).ToString("#,##0.00")
                'Numero = Dichiarazione.TotaleKgCategoria("R5", Year(Today)) + Dichiarazione.TotaleKgCategoriaNew("R5", Year(Today))
                Numero = Dichiarazione.TotaleKgCategoriaNew("R5", Year(Today))
                totR5.Text = Numero.ToString("#,##0.00")
            Catch ex As Exception
                totR5.Text = 0
            End Try

            Try
                'totBatterie.Text = Dichiarazione.TotaleKgCategoria("", Year(Today)).ToString("#,##0.00")
                'Numero = Dichiarazione.TotaleKgCategoria("", Year(Today)) + Dichiarazione.TotaleKgCategoriaNew("", Year(Today))
                Numero = Dichiarazione.TotaleKgCategoriaNew("", Year(Today))
                totBatterie.Text = Numero.ToString("#,##0.00")
            Catch ex As Exception
                totBatterie.Text = 0
            End Try

            If Page.User.IsInRole("Monitor") Then
                lblTotaleAbbinati.Visible = False
                lblTotaleConformi.Visible = False
            Else
                totPannelliAbbinati.Text = Pannello.TotaleAbbinati.ToString("#,##0", cultureToUse)
            End If

        ElseIf Page.User.IsInRole("Monitor") Then
            totProduttori.Text = Produttore.TotaleProduttori.ToString("#,##0", cultureToUse)
            totImpianti.Text = Impianto.TotaleImpianti.ToString("#,##0", cultureToUse)
            totPannelliConformi.Text = Pannello.TotaleConformi.ToString("#,##0", cultureToUse)
            lblTotaleAbbinati.Visible = False
            lblTotalePannelli.Visible = False
            lblTotR1.Visible = False
            lblTotR2.Visible = False
            lblTotR3.Visible = False
            lblTotR4.Visible = False
            lblTotR5.Visible = False
            lblTotBatterie.Visible = False
            string_last.Visible = False

        ElseIf Page.User.IsInRole("Produttore") Then
            lblTotalePannelli.Visible = False
            lblTotaleAbbinati.Visible = False
            lblTotaleImpianti.Visible = False
            lblTotaleConformi.Visible = False
            lblTotaleDismessi.Visible = False
            lblTotR1.Visible = False
            lblTotR2.Visible = False
            lblTotR3.Visible = False
            lblTotR4.Visible = False
            lblTotR5.Visible = False
            lblTotBatterie.Visible = False
            string_last.Visible = False
            Dim UtenteProduttore As UtenteProduttore = UtenteProduttore.Carica(Membership.GetUser().ProviderUserKey)
            totPannelli.Text = Pannello.TotalePannelliProduttore(UtenteProduttore.IdProduttore).ToString("#,##0", cultureToUse)
        ElseIf Page.User.IsInRole("Utente") Then
            lblTotaleAbbinati.Visible = False
            lblTotaleConformi.Visible = False
            string_last.Visible = False
            lblTotalePannelli.Visible = False
            lblTotaleDismessi.Visible = False
            lblTotR1.Visible = False
            lblTotR2.Visible = False
            lblTotR3.Visible = False
            lblTotR4.Visible = False
            lblTotR5.Visible = False
            lblTotBatterie.Visible = False
            Dim UtenteCliente As UtenteCliente = UtenteCliente.Carica(Membership.GetUser().ProviderUserKey)
            totImpianti.Text = Impianto.TotaleImpianti(UtenteCliente.IdCliente).ToString("#,##0", cultureToUse)
        Else

        End If
    End Sub


End Class
