<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NuovoUtente.aspx.vb" Inherits="WebApp_Amministrazione_NuovoUtente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    
    <h1>Nuovo utente</h1>

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
            <label class="control-label">Nominativo:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtNominativo" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ID="testValidation1" ControlToValidate="txtNominativo" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Username:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtUserName" runat="server" AutoCompleteType="None" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtUserName" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Email:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtEmail" runat="server" AutoCompleteType="None"/>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEmail" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Ruolo:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlRuoli" CssClass="chzn-select medium-select select" AppendDataBoundItems="true" DataSourceID="sqlDsRuoli" DataTextField="Rolename" DataValueField="Rolename" AutoPostBack="true" >
                    <asp:ListItem Text="Selezionare" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlDsRuoli" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT RoleName FROM aspnet_Roles ORDER BY RoleName"></asp:SqlDataSource>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Password:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNuovaPassword" runat="server" TextMode="Password" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Conferma password:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtConfermaPassword" runat="server" TextMode="Password" />
            </div>
        </div>
        <div class="control-group" runat="server" id="divCliente" visible="false">            
            <label class="control-label">Cliente:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlClienti" CssClass="chzn-select medium-select select" AppendDataBoundItems="true" DataTextField="RagioneSociale" DataValueField="IdCliente" DataSourceID="sqlClienti">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlClienti" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [IdCliente], [RagioneSociale] FROM [tbl_Clienti] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
            </div>
        </div>
        <div class="control-group" runat="server" id="divProduttore" visible="false">            
            <label class="control-label">Produttore:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProduttori" CssClass="chzn-select medium-select select" AppendDataBoundItems="true" DataTextField="RagioneSociale" DataValueField="ID" DataSourceID="sqlProduttori">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlProduttori" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [Id], [RagioneSociale] FROM [tbl_Produttori] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
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

