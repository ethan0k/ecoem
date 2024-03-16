<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Protocollo.aspx.vb" Inherits="WebApp_Pannelli_Protocollo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni protocollo</h1>

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
            <label class="control-label">Protocollo:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtProtocollo" runat="server" ReadOnly="true" />
                </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtData" runat="server" ReadOnly="true" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Nr. fattura:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNrFattura" runat="server" Enabled="false" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data fattura:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataFattura" runat="server" Enabled="false"/>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Nr. proforma:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNrProforma" runat="server" Enabled="false"/>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data proforma:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataProforma" runat="server" Enabled="false"/>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data Attestato:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataAttestato" runat="server" ReadOnly="true"/>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Nr Attestato:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNrAttestato" runat="server" ReadOnly="true" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">CCT:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCCT" runat="server" />
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Costo del servizio:</label>
            <div class="controls">
                <asp:Label CssClass="medium v-name" ID="lblCostoServizio" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Utente:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtUsername" runat="server" readonly="true"/>
            </div>
        </div>
    </div>

    <script>
        ValidatorUpdateIsValid = function () {
            Page_IsValid = AllValidatorsValid(Page_Validators);
            SetValidatorStyles();
        }

        SetValidatorStyles = function () {
            var i;
            // clear all
            for (i = 0; i < Page_Validators.length; i++) {
                var inputControl = document.getElementById(Page_Validators[i].controltovalidate);
                if (null != inputControl) {
                    WebForm_RemoveClassName(inputControl, 'error');
                }
            }
            // set invalid
            for (i = 0; i < Page_Validators.length; i++) {
                inputControl = document.getElementById(Page_Validators[i].controltovalidate);
                if (null != inputControl && !Page_Validators[i].isvalid) {
                    WebForm_AppendToClassName(inputControl, 'error');
                }
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

