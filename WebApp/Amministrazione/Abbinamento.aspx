<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Abbinamento.aspx.vb" Inherits="WebApp_Amministrazione_Abbinamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni abbinamento Tipologia cella/Fascia di peso</h1>

     <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdSalva">
                    <span class="ui-icon ui-icon-document toolbar-icon"></span><span class="desc">Salva</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Annulla</span>
                </asp:LinkButton>
            </li>
        </ul>
        <div class="clear"></div>
    </div>

    <div class="alert red hideit" id="divError" runat="server" visible="false">
        <div class="left">
            <span class="red-icon"></span>
            <span class="alert-text"><asp:Literal runat="server" ID="lblError" Text="Errore: Oops mi spiace." ></asp:Literal></span>
        </div>
    </div> 

    <div class="form form-horizontal">

         <div class="control-group">            
            <label class="control-label">Produttore:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProduttore" CssClass="chzn-select medium-select select" DataSourceID="sqlMacro" AppendDataBoundItems="true"  DataTextField="RagioneSociale" DataValueField="Id" Enabled="false">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlMacro" SelectCommand="SELECT Id,RagioneSociale FROM tbl_Produttori order By RagioneSociale" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"></asp:SqlDataSource>
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Tipologia cella:</label>
            <div class="controls">
                 <asp:DropDownList runat="server" ID="ddlTipologia" CssClass="chzn-select medium-select select" DataSourceID="sqlTipologie" AppendDataBoundItems="true"  DataTextField="Descrizione" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlTipologie" SelectCommand="SELECT Id, Descrizione FROM tbl_TipologieCelle Order By Descrizione" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"></asp:SqlDataSource>
            </div>
        </div>         

        <div class="control-group">            
            <label class="control-label">Fascia di peso:</label>
            <div class="controls">
                 <asp:DropDownList runat="server" ID="ddlfascia" CssClass="chzn-select medium-select select" DataSourceID="SqlFascie" AppendDataBoundItems="true"  DataTextField="Descrizione" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="SqlFascie" SelectCommand="SELECT Id, Descrizione from tbl_fasceDiPeso order by descrizione" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"></asp:SqlDataSource>               
            </div>
        </div>  
         <div class="control-group">            
            <label class="control-label">Costo matricola:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCostoMatricola" runat="server"/>
            </div>
        </div>        
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

