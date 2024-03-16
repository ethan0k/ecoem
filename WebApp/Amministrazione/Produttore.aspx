<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Produttore.aspx.vb" Inherits="WebApp_Amministrazione_Produttore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni produttore</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdSalva">
                    <span class="ui-icon ui-icon-document toolbar-icon"></span><span class="desc">Salva</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla" CausesValidation="False" >
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Annulla</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCategorie" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Categorie</span>
                </asp:LinkButton>
            </li>
             <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCategorieAbbinate" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Categorie abbinate</span>
                </asp:LinkButton>
            </li>
             <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCatProf" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Categorie professionali</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCatProfAbbinate" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Categorie professionali abbinate</span>
                </asp:LinkButton>
            </li>
<%--            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCostoMatricola" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Costo matricola</span>
                </asp:LinkButton>
            </li>--%>
             <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAbbinamenti" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Tariffe PV</span>
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
            <label class="control-label">Codice:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtCodice" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ID="testValidation1" ControlToValidate="txtCodice" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Ragione sociale:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtRagioneSociale" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtRagioneSociale" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Indirizzo:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtIndirizzo" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtIndirizzo" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Cap:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCap" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtCap" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Citta:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCitta" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtCitta" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Codice fiscale:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCodiceFiscale" runat="server" />
                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Partita IVA:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtPartitaIVA" runat="server" />
                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Codice SDI:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCodiceSDI" runat="server" />
                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Rappresentante legale:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtRappresentante" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtRappresentante" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">IBAN:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtIBAN" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtRappresentante" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Email:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtEmail" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEmail" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Email notifiche:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtEmailNotifiche" runat="server" />                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Telefono:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtTelefono" runat="server" />                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">PEC:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtPEC" runat="server" />                                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Mese adesione:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlMeseAdesione" CssClass="chzn-select medium-select select"  AppendDataBoundItems="true"  DataTextField="Periodo" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                    <asp:ListItem Text="Gennaio" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Febbraio" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Aprile" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Maggio" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Giugno" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Luglio" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                    <asp:ListItem Text="Settembre" Value="9"></asp:ListItem>
                    <asp:ListItem Text="Ottobre" Value="10"></asp:ListItem>
                    <asp:ListItem Text="Novembre" Value="11"></asp:ListItem>
                    <asp:ListItem Text="Dicembre" Value="12"></asp:ListItem>
                </asp:DropDownList>                  
            </div>
        </div>
        <div class="control-group">            
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
        <div class="control-group">            
            <label class="control-label">Quota consortile:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name numeric" ID="txtQuota" runat="server" />                                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Registro AEE:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtRegistroAEE" runat="server" />                                
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Registro Pile:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtRegistroPile" runat="server" />                                
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Attivo:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkAttivo"  CssClass="styled"  />
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Domestico:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkDomestico"  CssClass="styled"  />
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Professionale:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkProfessionale"  CssClass="styled"  />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Escludi da elaborazione massiva:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="ChkEscludi"  CssClass="styled"  />
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Servizio di rappresentanza:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtServizioRappresentanza" runat="server" />                                                
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Sconto:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtSconto" runat="server" />                                                
            </div>
        </div>
        <div class="control-group">            
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

         function pageLoad() {
             $(function () {
                 $(".numeric").numeric({ decimal: "," });
                                 
                 });
                 
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

