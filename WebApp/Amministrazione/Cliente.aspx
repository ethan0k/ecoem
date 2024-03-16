<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cliente.aspx.vb" Inherits="WebApp_Amministrazione_Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni cliente</h1>

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
            <label class="control-label">Ragione sociale:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtRagioneSociale" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Partita IVA:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtPartitaIva" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Cognome:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCognome" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Nome:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNome" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Codice fiscale:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCodiceFiscale" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Indirizzo:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtIndirizzo" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Cap:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCap" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Città:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCittà" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Provincia:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtProvincia" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Email:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtEmail" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Telefono:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtTelefono" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Fax:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtFax" runat="server" />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Contatto:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtContatto" runat="server" />
            </div>
        </div>
        <div class="control-group" style="display:none">            
            <label class="control-label">Periodicità dichiarazione:</label>
            <div class="controls">
                 <asp:DropDownList runat="server" ID="ddlPeriodicita" CssClass="chzn-select medium-select select"  AppendDataBoundItems="true"  DataTextField="Periodo" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                    <asp:ListItem Text="Mensile" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Bimestrale" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Trimestrale" Value="3"></asp:ListItem>
                </asp:DropDownList>                
            </div>
        </div>
         <div class="control-group" style="display:none">            
            <label class="control-label">Attivo:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkAttivo"  CssClass="styled"  />
            </div>
        </div>
         <div class="control-group" >            
            <label class="control-label">Note:</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" Style="width:99%"></asp:TextBox>
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

