<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FasciaDiPeso.aspx.vb" Inherits="WebApp_Amministrazione_FasciaDiPeso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni fascia di peso</h1>

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
            <label class="control-label">Descrizione:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDescrizione" runat="server" />
            </div>
        </div>         
        
        <div class="control-group">            
            <label class="control-label">Data ultima modifica:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataModifica" runat="server" Enabled="false" />
            </div>
        </div>
    </div>
</asp:Content>


