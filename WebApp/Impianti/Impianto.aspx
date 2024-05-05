<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Impianto.aspx.vb" Inherits="WebApp_Impianti_Impianto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Informazioni impianto</h1>

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
                <asp:LinkButton runat="server" ID="cmdDisabbina" Visible="false" CausesValidation="false" CssClass="confirm_button">
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
        <div class="control-group" runat="server" id="divCliente">            
            <label class="control-label" runat="server" id="lblCliente">Cliente:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="chzn-select medium-select select" DataSourceID="sqlClienti" DataTextField="RagioneSociale" DataValueField="IdCliente" AppendDataBoundItems="true">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlClienti" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [IdCliente], [RagioneSociale] FROM [tbl_Clienti] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Descrizione:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDescrizione" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtDescrizione" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Indirizzo:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtIndirizzo" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtIndirizzo" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Cap:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCap" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtCap" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Città:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtCittà" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtCittà" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Provincia:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProvincia" CssClass="chzn-select medium-select select" >
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                    <asp:ListItem Text="Agrigento" Value="AG"></asp:ListItem>
                    <asp:ListItem Text="Alessandria" Value="AL"></asp:ListItem>
                    <asp:ListItem Text="Ancona" Value="AN"></asp:ListItem>
                    <asp:ListItem Text="Aosta" Value="AO"></asp:ListItem>
                    <asp:ListItem Text="Arezzo" Value="AR"></asp:ListItem>
                    <asp:ListItem Text="Ascoli Piceno" Value="AP"></asp:ListItem>
                    <asp:ListItem Text="Asti" Value="AT"></asp:ListItem>                   
                    <asp:ListItem Text="Avellino" Value="AV"></asp:ListItem>
                    <asp:ListItem Text="Bari" Value="BA"></asp:ListItem>
                    <asp:ListItem Text="Barletta-Andria-Trani" Value="BT"></asp:ListItem>
                    <asp:ListItem Text="Belluno" Value="BL"></asp:ListItem>
                    <asp:ListItem Text="Benevento" Value="BN"></asp:ListItem>
                    <asp:ListItem Text="Bergamo" Value="BG"></asp:ListItem>
                    <asp:ListItem Text="Biella" Value="BI"></asp:ListItem>
                    <asp:ListItem Text="Bologna" Value="BO"></asp:ListItem>
                    <asp:ListItem Text="Bolzano" Value="BZ"></asp:ListItem>
                    <asp:ListItem Text="Brescia" Value="BS"></asp:ListItem>
                    <asp:ListItem Text="Brindisi" Value="BR"></asp:ListItem>
                    <asp:ListItem Text="Cagliari" Value="CA"></asp:ListItem>
                    <asp:ListItem Text="Caltanissetta" Value="CL"></asp:ListItem>
                    <asp:ListItem Text="Campobasso" Value="CB"></asp:ListItem>
                    <asp:ListItem Text="Carbonia-Iglesias" Value="CI"></asp:ListItem>
                    <asp:ListItem Text="Caserta" Value="CE"></asp:ListItem>
                    <asp:ListItem Text="Catania" Value="CT"></asp:ListItem>
                    <asp:ListItem Text="Catanzaro" Value="CZ"></asp:ListItem>
                    <asp:ListItem Text="Chieti" Value="CH"></asp:ListItem>
                    <asp:ListItem Text="Como" Value="CO"></asp:ListItem>
                    <asp:ListItem Text="Cosenza" Value="CS"></asp:ListItem>                   
                    <asp:ListItem Text="Cremona" Value="CR"></asp:ListItem>
                    <asp:ListItem Text="Crotone" Value="KR"></asp:ListItem>
                    <asp:ListItem Text="Cuneo" Value="CN"></asp:ListItem>
                    <asp:ListItem Text="Enna" Value="EN"></asp:ListItem>
                    <asp:ListItem Text="Fermo" Value="FM"></asp:ListItem>
                    <asp:ListItem Text="Ferrara" Value="FE"></asp:ListItem>
                    <asp:ListItem Text="Firenze" Value="FI"></asp:ListItem>
                    <asp:ListItem Text="Bologna" Value="BO"></asp:ListItem>
                    <asp:ListItem Text="Foggia" Value="FG"></asp:ListItem>
                    <asp:ListItem Text="Forlì-Cesena" Value="FC"></asp:ListItem>
                    <asp:ListItem Text="Frosinone" Value="FR"></asp:ListItem>
                    <asp:ListItem Text="Genova" Value="GE"></asp:ListItem>
                    <asp:ListItem Text="Gorizia" Value="GO"></asp:ListItem>
                    <asp:ListItem Text="Grosseto" Value="GR"></asp:ListItem>
                    <asp:ListItem Text="Imperia" Value="IM"></asp:ListItem>
                    <asp:ListItem Text="Isernia" Value="IS"></asp:ListItem>
                    <asp:ListItem Text="La Spezia" Value="SP"></asp:ListItem>
                    <asp:ListItem Text="L'Aquila" Value="AQ"></asp:ListItem>
                    <asp:ListItem Text="Latina" Value="LT"></asp:ListItem>
                    <asp:ListItem Text="Lecce" Value="LE"></asp:ListItem>
                    <asp:ListItem Text="Lecco" Value="LC"></asp:ListItem>                   
                    <asp:ListItem Text="Livorno" Value="LI"></asp:ListItem>
                    <asp:ListItem Text="Lodi" Value="LO"></asp:ListItem>
                    <asp:ListItem Text="Lucca" Value="LU"></asp:ListItem>
                    <asp:ListItem Text="Macerata" Value="MC"></asp:ListItem>
                    <asp:ListItem Text="Mantova" Value="MN"></asp:ListItem>
                    <asp:ListItem Text="Massa-Carrara" Value="MS"></asp:ListItem>
                    <asp:ListItem Text="Matera" Value="MT"></asp:ListItem>
                    <asp:ListItem Text="Messina" Value="ME"></asp:ListItem>
                    <asp:ListItem Text="Milano" Value="MI"></asp:ListItem>
                    <asp:ListItem Text="Modena" Value="MO"></asp:ListItem>
                    <asp:ListItem Text="Monza e della Brianza" Value="MB"></asp:ListItem>
                    <asp:ListItem Text="Napoli" Value="NA"></asp:ListItem>
                    <asp:ListItem Text="Novara" Value="NO"></asp:ListItem>
                    <asp:ListItem Text="Nuoro" Value="NU"></asp:ListItem>
                    <asp:ListItem Text="Olbia-Tempio" Value="OT"></asp:ListItem>
                    <asp:ListItem Text="Oristano" Value="OR"></asp:ListItem>
                    <asp:ListItem Text="Padova" Value="PD"></asp:ListItem>
                    <asp:ListItem Text="Palermo" Value="PA"></asp:ListItem>
                    <asp:ListItem Text="Parma" Value="PR"></asp:ListItem>
                    <asp:ListItem Text="Pavia" Value="PV"></asp:ListItem>
                    <asp:ListItem Text="Perugia" Value="PG"></asp:ListItem>                   
                    <asp:ListItem Text="Pesaro e Urbino" Value="PU"></asp:ListItem>
                    <asp:ListItem Text="Pescara" Value="PE"></asp:ListItem>
                    <asp:ListItem Text="Piacenza" Value="PC"></asp:ListItem>
                    <asp:ListItem Text="Pisa" Value="PI"></asp:ListItem>
                    <asp:ListItem Text="Pistoia" Value="PT"></asp:ListItem>
                    <asp:ListItem Text="Pordenone" Value="PN"></asp:ListItem>
                    <asp:ListItem Text="Potenza" Value="PZ"></asp:ListItem>
                    <asp:ListItem Text="Prato" Value="PO"></asp:ListItem>
                    <asp:ListItem Text="Ragusa" Value="RG"></asp:ListItem>
                    <asp:ListItem Text="Ravenna" Value="RA"></asp:ListItem>
                    <asp:ListItem Text="Reggio Calabria" Value="RC"></asp:ListItem>
                    <asp:ListItem Text="Reggio Emilia" Value="RE"></asp:ListItem>
                    <asp:ListItem Text="Rieti" Value="RI"></asp:ListItem>
                    <asp:ListItem Text="Rimini" Value="RN"></asp:ListItem>
                    <asp:ListItem Text="Roma" Value="RM"></asp:ListItem>
                    <asp:ListItem Text="Rovigo" Value="RO"></asp:ListItem>
                    <asp:ListItem Text="Salerno" Value="SA"></asp:ListItem>
                    <asp:ListItem Text="Medio Campidano" Value="VS"></asp:ListItem>
                    <asp:ListItem Text="Sassari" Value="SS"></asp:ListItem>
                    <asp:ListItem Text="Savona" Value="SV"></asp:ListItem>
                    <asp:ListItem Text="Siena" Value="SI"></asp:ListItem>
                    <asp:ListItem Text="Siracusa" Value="SC"></asp:ListItem>
                    <asp:ListItem Text="Sondrio" Value="SO"></asp:ListItem>
                    <asp:ListItem Text="Taranto" Value="TA"></asp:ListItem>
                    <asp:ListItem Text="Teramo" Value="TE"></asp:ListItem>
                    <asp:ListItem Text="Terni" Value="TR"></asp:ListItem>
                    <asp:ListItem Text="Torino" Value="TO"></asp:ListItem>
                    <asp:ListItem Text="Ogliastra" Value="OG"></asp:ListItem>
                    <asp:ListItem Text="Trapani" Value="TP"></asp:ListItem>
                    <asp:ListItem Text="Trento" Value="TN"></asp:ListItem>
                    <asp:ListItem Text="Treviso" Value="TV"></asp:ListItem>
                    <asp:ListItem Text="Trieste" Value="TS"></asp:ListItem>
                    <asp:ListItem Text="Udine" Value="UD"></asp:ListItem>
                    <asp:ListItem Text="Varese" Value="VA"></asp:ListItem>
                    <asp:ListItem Text="Venezia" Value="VE"></asp:ListItem>
                    <asp:ListItem Text="Verbano-Cusio-Ossola" Value="VB"></asp:ListItem>
                    <asp:ListItem Text="Vercelli" Value="VC"></asp:ListItem>
                    <asp:ListItem Text="Verona" Value="VR"></asp:ListItem>
                    <asp:ListItem Text="Vibo Valentia" Value="VV"></asp:ListItem>
                    <asp:ListItem Text="Vicenza" Value="VI"></asp:ListItem>
                    <asp:ListItem Text="Viterbo" Value="VT"></asp:ListItem>

                </asp:DropDownList>
                
            </div>
        </div>
         <div class="control-group">            
            <label class="control-label">Regione:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlRegione" CssClass="chzn-select medium-select select" >
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                    <asp:ListItem Text="Abruzzo" Value="Abruzzo"></asp:ListItem>
                    <asp:ListItem Text="Basilicata" Value="Basilicata"></asp:ListItem>
                    <asp:ListItem Text="Calabria" Value="Calabria"></asp:ListItem>
                    <asp:ListItem Text="Campania" Value="Campania"></asp:ListItem>
                    <asp:ListItem Text="Emilia Romagna" Value="Emilia Romagna"></asp:ListItem>
                    <asp:ListItem Text="Friuli Venezia Giulia" Value="Friuli Venezia Giulia"></asp:ListItem>
                    <asp:ListItem Text="Lazio" Value="Lazio"></asp:ListItem>                   
                    <asp:ListItem Text="Liguria" Value="Liguria"></asp:ListItem>
                    <asp:ListItem Text="Lombardia" Value="Lombardia"></asp:ListItem>
                    <asp:ListItem Text="Marche" Value="Marche"></asp:ListItem>
                    <asp:ListItem Text="Molise" Value="Molise"></asp:ListItem>
                    <asp:ListItem Text="Piemonte" Value="Piemonte"></asp:ListItem>
                    <asp:ListItem Text="Puglia" Value="Puglia"></asp:ListItem>
                    <asp:ListItem Text="Sardegna" Value="Sardegna"></asp:ListItem>
                    <asp:ListItem Text="Sicilia" Value="Sicilia"></asp:ListItem>
                    <asp:ListItem Text="Toscana" Value="Toscana"></asp:ListItem>
                    <asp:ListItem Text="Trentino Alto Adige" Value="Trentino Alto Adige"></asp:ListItem>
                    <asp:ListItem Text="Umbria" Value="Umbria"></asp:ListItem>
                    <asp:ListItem Text="Valle d'Aosta" Value="Valle d'Aosta"></asp:ListItem>
                    <asp:ListItem Text="Veneto" Value="Veneto"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Data entrata in esercizio:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtDataEntrataInEsercizio" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ID="validator" ControlToValidate="txtDataEntrataInEsercizio" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Latitudine:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtLatitudine" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtLatitudine" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Longitudine:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtLongitudine" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtLongitudine" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Conto energia:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlContoEnergia" CssClass="chzn-select medium-select select" >
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                    <asp:ListItem Text="I CONTO ENERGIA" Value="I"></asp:ListItem>
                    <asp:ListItem Text="II CONTO ENERGIA" Value="II"></asp:ListItem>
                    <asp:ListItem Text="III CONTO ENERGIA" Value="III"></asp:ListItem>
                    <asp:ListItem Text="IV CONTO ENERGIA" Value="IV"></asp:ListItem>
                    <asp:ListItem Text="V CONTO ENERGIA" Value="V"></asp:ListItem>
                    <asp:ListItem Text="NO CONTO ENERGIA" Value="N"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group" runat="server" id="groupCosto" visible="false">            
             <label class="control-label">Costo:</label>
             <div class="controls">
                 <asp:TextBox CssClass="medium v-name" ID="txtCosto" runat="server" Enabled="false" />
             </div>
         </div>
        <div class="control-group">            
            <label class="control-label">Responsabile:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtResponsabile" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtResponsabile" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Nr. pratica GSE:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNrPraticaGSE" runat="server"/>
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtNrPraticaGSE" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
         <div class="control-group" runat="server" id="divNomeProd" visible="false">            
            <label class="control-label">Nome produttore in stampa:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtNomeProduttore" runat="server"/>                
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

     <script>
         function pageLoad() {
             $(function () {

                 $(".confirm_button").click(function () {
                     var callFrom = $(this);

                     jConfirm("Tutti i pannelli di questo impianto saranno disabbinati. Procedere?",
                         'Conferma utente',
                         function (r) {
                             if (r) {
                                 __doPostBack(callFrom.attr('id').replace(/_/g, '$'), '');
                             }
                         });
                     return false;
                 });

             });

         }
     </script>     
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

