<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Autocertificazione.aspx.vb" Inherits="WebApp_Dichiarazioni_Autocertificazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
      <h1>Informazioni Autocertificazione Eco-contributi</h1>

     <div class="toolbar">
        <ul>           
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Torna</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdConferma"  CausesValidation="false" CssClass="confirm_button">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Conferma</span>
                </asp:LinkButton>                 
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdRiapri"  CausesValidation="false" CssClass="apri_button2">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Riapri</span>
                </asp:LinkButton>                 
            </li>
        </ul>
        <div class="clear"></div>
    </div>

    <div class="form form-horizontal">

         <div class="control-group">            
            <label class="control-label">Anno:</label>
            <div class="controls">
               <asp:TextBox CssClass="medium v-name" ID="txtAnno" runat="server" Enabled="false" />
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Produttore:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProduttore" CssClass="chzn-select medium-select select" DataSourceID="sqlClienti" AppendDataBoundItems="true"  DataTextField="RagioneSociale" DataValueField="Id" Enabled="false">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sqlClienti" SelectCommand="SELECT * FROM tbl_Produttori ORDER BY RagioneSociale" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" ></asp:SqlDataSource>
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Data:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtData" runat="server" Enabled="false" />
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Data conferma:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDataConferma" runat="server" Enabled="false" />
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Documenti caricati:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkCaricato"  CssClass="styled" Enabled="false" />
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Confermata:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkConfermata"  CssClass="styled" Enabled="false" />
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Rettificata:</label>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chkRettificata"  CssClass="styled" Enabled="false" />
            </div>
        </div>

        <div class="control-group" runat="server" id="divFileUpload">
            <label class="control-label">File AEE da caricare:</label>
            <div class="controls">
                <div class="uploader black">
                    <div id="divUploadAee" runat="server">
                        <asp:TextBox runat="server" ReadOnly="True" ID="fileNameTextBox" CssClass="filename"></asp:TextBox>
                        <asp:Button runat="server" readonly="True" ID="fileNameButton" CssClass="button_files" Text="Sfoglia..." ></asp:Button>                    
                        <asp:FileUpload runat="server" ID="fileUpload"  />                    
                    </div>
                    <ul>                               
                        <li class="button-profile-2" style="margin-left:0px;margin-top:3px" runat="server" id="liCaricaAee">
                            <asp:LinkButton runat="server" ID="cmdCarica" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Carica</span>
                            </asp:LinkButton>                        
                        </li>
                        <li class="button-profile-2" style="margin-left:3px;margin-top:3px" runat="server" id="liScaricaAEE">
                            <asp:LinkButton runat="server" ID="cmdScaricaAEE" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Scarica</span>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="control-group" runat="server" id="divPile">
            <label class="control-label">File PILE da caricare:</label>
            <div class="controls">
                <div class="uploader black">
                    <div runat="server" id="uploadPile">
                        <asp:TextBox runat="server" ReadOnly="True" ID="txtFilePile" CssClass="filename"></asp:TextBox>
                        <asp:Button runat="server" readonly="True" ID="Button1" CssClass="button_files" Text="Sfoglia..." ></asp:Button>                    
                        <asp:FileUpload runat="server" ID="fileUploadPile"  />                    
                    </div>
                    <ul>           
                        <li class="button-profile-2" style="margin-left:0px;margin-top:3px" runat="server" id="licaricaPile">
                            <asp:LinkButton runat="server" ID="cmdCaricaPile" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Carica</span>
                            </asp:LinkButton>
                        </li>
                        <li class="button-profile-2" style="margin-left:3px;margin-top:3px" runat="server" id="liScaricaPile">
                            <asp:LinkButton runat="server" ID="cmdScaricaPIle" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Scarica</span>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="control-group" runat="server" id="divVeicoli">
            <label class="control-label">File Veicoli da caricare:</label>
            <div class="controls">
                <div class="uploader black">
                    <div runat="server" id="divVeicoliUp">
                        <asp:TextBox runat="server" ReadOnly="True" ID="txtFileVeicoli" CssClass="filename"></asp:TextBox>
                        <asp:Button runat="server" readonly="True" ID="Button2" CssClass="button_files" Text="Sfoglia..." ></asp:Button>                    
                        <asp:FileUpload runat="server" ID="fileUploadVeicoli"  />                    
                    </div>
                    <ul>           
                        <li class="button-profile-2" style="margin-left:0px;margin-top:3px" runat="server" id="liCaricaVeicoli">
                            <asp:LinkButton runat="server" ID="cmdCaricaVeicoli" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Carica</span>
                            </asp:LinkButton>
                        </li>
                         <li class="button-profile-2" style="margin-left:3px;margin-top:3px" runat="server" id="liScaricaVeicoli">
                            <asp:LinkButton runat="server" ID="cmdScaricaVeicoli" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Scarica</span>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

         <div class="control-group" runat="server" id="divIndustriali">
            <label class="control-label">File Acc. industriali da caricare:</label>
            <div class="controls">
                <div class="uploader black">
                    <div runat="server" id="upIndustria">
                        <asp:TextBox runat="server" ReadOnly="True" ID="txtFileIndustriali" CssClass="filename"></asp:TextBox>
                        <asp:Button runat="server" readonly="True" ID="Button3" CssClass="button_files" Text="Sfoglia..." ></asp:Button>                    
                        <asp:FileUpload runat="server" ID="fileUploadIndustriali"  />                    
                    </div>
                    <ul>           
                        <li class="button-profile-2" style="margin-left:0px;margin-top:3px" runat="server" id="liCaricaIndustriali">
                            <asp:LinkButton runat="server" ID="cmdcaricaIndustriali" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Carica</span>
                            </asp:LinkButton>
                        </li>
                        <li class="button-profile-2" style="margin-left:3px;margin-top:3px" runat="server" id="liScaricaIndustriali">
                            <asp:LinkButton runat="server" ID="cmdScaricaIndustriali" CausesValidation="false">
                                <span class="ui-icon toolbar-icon"></span>
                                <span class="desc">Scarica</span>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="control-group" runat="server" id="ctrlAEE">            
            <label class="control-label" style="margin-top:19px">Autocertificazione AEE:</label>
            <div class="controls">
                <asp:ImageButton runat="server" ID="cmdAutocertificazione" ImageUrl="~/images/docpdf.png" AlternateText="Autocertificazione AEE" />
            </div>
        </div>

         <div class="control-group" runat="server" id="ctrlPILE">            
            <label class="control-label" style="margin-top:19px">Autocertificazione Pile:</label>
            <div class="controls">
                <asp:ImageButton runat="server" ID="cmdAutoPile" ImageUrl="~/images/docpdf.png" AlternateText="Autocertificazione Pile" />
            </div>
        </div>

         <div class="control-group" runat="server" id="ctrlIndustrial">                        
            <label class="control-label" style="margin-top:19px">Autocertificazione Batterie industriali:</label>
            <div class="controls">
                <asp:ImageButton runat="server" ID="cmdIndustriali" ImageUrl="~/images/docpdf.png" AlternateText="Autocertificazione Batterie industriali" />
            </div>
        </div>
         <div class="control-group" runat="server" id="ctrlVeicoli">                        
            <label class="control-label" style="margin-top:19px">Autocertificazione batterie veicoli:</label>
            <div class="controls">
                <asp:ImageButton runat="server" ID="cmdVeicoli" ImageUrl="~/images/docpdf.png" AlternateText="Autocertificazione batterie veicoli" />
            </div>
        </div>

    </div>

         <script>
         function pageLoad() {
             $(function () {
                 
                 $(".confirm_button").click(function () {
                     var callFrom = $(this);

                     jConfirm("Procedere con la conferma della certificazione?",
                             'Conferma utente',
                             function (r) {
                                 if (r) {
                                     __doPostBack(callFrom.attr('id').replace(/_/g, '$'), '');
                                 }
                             });
                     return false;
                 });

                 $(".apri_button2").click(function () {
                     var callFrom = $(this);

                     jConfirm("Procedere con la riapertura della certificazione?",
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

