<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Pannello.aspx.vb" Inherits="WebApp_Pannelli_Pannello" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni pannello</h1>

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
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdDisabbina" CausesValidation="false" Visible="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Disabbina</span>
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
            <label class="control-label">Matricola:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtMatricola" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ID="testValidation1" ControlToValidate="txtMatricola" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Modello:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtModello" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtModello" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Peso:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtPeso" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPeso" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data inizio garanzia:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataInizioGaranzia" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDataInizioGaranzia" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Marca:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtMarca" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtMarca" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group" runat="server" id="divProduttore">            
            <label class="control-label">Produttore:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtProduttore" runat="server"  />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtProduttore" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group" runat="server" id="divImpianto">            
            <label class="control-label">Impianto:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtImpianto" runat="server" readonly="true"/>
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Conforme:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkConforme"  CssClass="styled" Enabled="false" />
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Data conformità:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataConformità"  runat="server" readonly="true"/>
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Protocollo:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtProtocollo" runat="server" ReadOnly="true" />
            </div>
        </div>
        <div class="control-group" runat="server" id="DivConsorziato">            
            <label class="control-label">Consorziato:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProduttore" CssClass="chzn-select medium-select select" AppendDataBoundItems="true" DataSourceID="SqlDataSource2" DataTextField="RagioneSociale" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [Id], [RagioneSociale] FROM [tbl_Produttori] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data caricamento:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataCaricamento" runat="server" readonly="true"/>
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Dismesso:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkDismesso"  CssClass="styled" Enabled="false" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data ritiro:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataritiro" runat="server" readonly="true"/>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Numero FIR:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNumeroFIR" runat="server" readonly="true"/>
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

