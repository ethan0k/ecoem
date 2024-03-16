<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Nuova.aspx.vb" Inherits="WebApp_Dichiarazioni_Nuova" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Generazione autocertificazione</h1>

            
    <div class="toolbar">
        <ul>
           
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Annulla</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsegui" CssClass="confirm_button" CausesValidation="true" >
                    <span class="ui-icon ui-icon-document toolbar-icon confirm_button"></span><span class="desc">Genera</span>
                </asp:LinkButton>
            </li>  
             <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta" CausesValidation="true" >
                    <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span>
                </asp:LinkButton>
            </li>  
        </ul>

    </div>

    <div class="clear"></div>

    <div class="alert red hideit" id="divError" runat="server" visible="false">
        <div class="left">
            <span class="red-icon"></span>
            <span class="alert-text"><asp:Literal runat="server" ID="lblError" Text="Errore: Oops mi spiace." ></asp:Literal></span>
        </div>
    </div>  

    <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Anno:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name numeric" ID="txtAnno" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtAnno" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">            
            <label class="control-label">Produttore:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProduttore" DataSourceID="sqlClienti" DataValueField="Id" AutoPostBack="true" DataTextField="RagioneSociale" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="- Tutti -" Value="-1"></asp:ListItem>                        
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="sqlClienti" SelectCommand="SELECT * FROM tbl_Produttori ORDER BY RagioneSociale" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" ></asp:SqlDataSource>
            </div>
        </div>

        <asp:ListView ID="ListaErrori" runat="server" >
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>
                            <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Ragione sociale" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Ragione sociale" ></asp:Label></asp:LinkButton></th>
                            <th class="info-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Autocertificazione prodotta" runat="server" ><asp:Label runat="server" ID="Label2" Text="Autocertificazione prodotta" ></asp:Label></asp:LinkButton></th>
                            <th class="info-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Dichiarazioni nel periodo" runat="server" ><asp:Label runat="server" ID="Label8" Text="Dichiarazioni nel periodo" ></asp:Label></asp:LinkButton></th>
                            <th class="info-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="Dichiarazioni certificate" runat="server" ><asp:Label runat="server" ID="Label9" Text="Dichiarazioni certificate" ></asp:Label></asp:LinkButton></th>
                            <th class="info-th"><asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="Dichiarazioni non confermate" runat="server" ><asp:Label runat="server" ID="Label10" Text="Dichiarazioni non confermate" ></asp:Label></asp:LinkButton></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </tbody>
                </table>
            </div>
           
            <div class="clear"></div>
            </div>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td><asp:Label runat="server" ID="label1" Text="Nessun dato presente!" Font-Bold="true"></asp:Label> </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr class="gray-tr">
                <td>
                    <asp:Label ID="lblRagione" runat="server" Text='<%# Eval("Ragione Sociale")%>'  />
                </td>  
                 <td>
                    <asp:Label ID="lblAutProd" runat="server" Text='<%# Eval("Autocertificazione prodotta")%>'  />
                </td>                
                <td>
                    <asp:Label ID="lblDichInPeriodo" runat="server" Text='<%# Eval("Dichiarazioni nel periodo")%>'  />
                </td>  
                <td>
                    <asp:Label ID="lblDichCertificate" runat="server" Text='<%# Eval("Dichiarazioni certificate")%>'  />
                </td>                
                <td>
                    <asp:Label ID="lblDichNonConfermate" runat="server" Text='<%# Eval("Dichiarazioni non confermate")%>'  />
                </td>  
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblRagione" runat="server" Text='<%# Eval("Ragione Sociale")%>'  />
                </td>  
                 <td>
                    <asp:Label ID="lblAutProd" runat="server" Text='<%# Eval("Autocertificazione prodotta")%>'  />
                </td>                
                <td>
                    <asp:Label ID="lblDichInPeriodo" runat="server" Text='<%# Eval("Dichiarazioni nel periodo")%>'  />
                </td>  
                <td>
                    <asp:Label ID="lblDichCertificate" runat="server" Text='<%# Eval("Dichiarazioni certificate")%>'  />
                </td>                
                <td>
                    <asp:Label ID="lblDichNonConfermate" runat="server" Text='<%# Eval("Dichiarazioni non confermate")%>'  />
                </td>             
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>            
</div>

    
            

<script>
    function pageLoad() {
        $(function () {
            $(".numeric").numeric({ decimal: ",", negative: false });  

                 
            $(".confirm_button").click(function () {
                var callFrom = $(this);

                jConfirm("Procedere con la creazione delle certificazioni?",
                        'Conferma utente',
                        function (r) {
                            if (r) {
                                __doPostBack(callFrom.attr('id').replace(/_/g, '$'), '');
                            }
                        });
                return false;
            });
            
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });


    }
                   
</script>    

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

