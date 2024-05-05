<%@ Page Title="Informazioni categoria" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Categoria.aspx.vb" Inherits="WebApp_Amministrazione_Categoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni categoria</h1>

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
            <label class="control-label">Macrocategoria:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlMacroCategoria" CssClass="chzn-select medium-select select" DataSourceID="sqlMacro" AppendDataBoundItems="true"  DataTextField="Nome" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlMacro" SelectCommandType="Text" SelectCommand="SELECT * FROM tbl_MacroCategorieNew Order By Nome" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"></asp:SqlDataSource>
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Nome:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNome" runat="server" />
            </div>
        </div>         

        <div class="control-group">            
            <label class="control-label">Tipo di dato:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlTipoDiDato" CssClass="chzn-select medium-select select" AppendDataBoundItems="true"  DataTextField="TipoDiDato" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                    <asp:ListItem Text="Quantità" Value="Quantità"></asp:ListItem>
                    <asp:ListItem Text="Valore" Value="Valore"></asp:ListItem>
                </asp:DropDownList>
                
            </div>
        </div>  
         <div class="control-group">            
            <label class="control-label">Codifica:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCodifica" runat="server"/>
            </div>
        </div>       
        <div class="control-group">            
            <label class="control-label">Raggruppamento:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtRaggruppamento" runat="server" />
            </div>
        </div>  
        <div class="control-group">            
            <label class="control-label">Costo:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCosto" runat="server" />
            </div>
        </div>  
        <div class="control-group">            
            <label class="control-label">Peso per unità:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtPesoPerUnita" runat="server" />
            </div>
        </div> 
        <div class="control-group">            
            <label class="control-label">Disattiva:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkDisattiva"  CssClass="styled"  />
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


