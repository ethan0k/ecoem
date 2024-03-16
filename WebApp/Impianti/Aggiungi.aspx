<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Aggiungi.aspx.vb" Inherits="WebApp_Impianti_Aggiungi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Caricamento manuale pannelli</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAggiungi">
                    <span class="ui-icon ui-icon-document toolbar-icon"></span><span class="desc">Aggiungi</span>
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

       <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Matricola:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtMatricola" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ID="validator" ControlToValidate="txtMatricola" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                </div>
        </div>
        <div class="control-group">            
            <label class="control-label" runat="server" id="Label1">Codice produttore:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCodiceProduttore" runat="server" />
            </div>
        </div>
        <div class="control-group" runat="server" id="divClienti">            
            <label class="control-label" runat="server" id="lblCliente">Cliente:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="chzn-select medium-select select" DataSourceID="sqlClienti" DataTextField="RagioneSociale" DataValueField="IdCliente" AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlClienti" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [IdCliente], [RagioneSociale] FROM [tbl_Clienti] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label" runat="server" id="Label2">Impianto:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlImpianti" CssClass="chzn-select medium-select select" AppendDataBoundItems="true" DataSourceID="sqlImpianti" DataTextField="Descrizione" DataValueField="Id">
                <asp:ListItem Text="Selezionare" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="sqlImpianti" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT Id, Descrizione, IdCliente FROM tbl_Impianti WHERE (IdCliente = @IdCliente) ORDER BY Descrizione">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCliente" Name="IdCliente" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
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

