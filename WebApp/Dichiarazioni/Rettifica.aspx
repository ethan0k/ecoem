<%@ Page Title="Dettaglio dichiarazione" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Rettifica.aspx.vb" Inherits="WebApp_Amministrazione_Rettifica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Rettifica Autocertificazione ECO contributi <div style="color:red;display:inline"><asp:Literal runat="server" ID="titleConfermata" Text="Confermata" Visible="false"></asp:Literal> </div> </h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Annulla</span>
                </asp:LinkButton>
            </li>            
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdVaiACertificazione"  CausesValidation="false" CssClass="fattura_button">
                    <span class="ui-icon 
                            toolbar-icon"></span><span class="desc">Scarica Autocertificazione</span>
                </asp:LinkButton>                 
            </li>
             <%--<li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta"  CausesValidation="false" CssClass="esporta_button2">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Esporta</span>
                </asp:LinkButton>                 
            </li>--%>
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

        <div class="row-fluid">
            <div class="span3">
                <div class="control-group">
                    <label>
                        Produttore:
                    </label>
                    <asp:DropDownList runat="server" ID="ddlProduttori" DataSourceID="sqlDsProduttori" DataTextField="RagioneSociale" DataValueField="Id" AppendDataBoundItems="true" CssClass="chzn-select medium-select select confirm_ddl" AutoPostBack="true">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="sqlDsProduttori" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [Id], [RagioneSociale] FROM [tbl_Produttori] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>Data:</label>
                    <asp:TextBox runat="server" ID="txtData"  CssClass="medium" Enabled="false"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="txtOldDIchiarazione" />
                </div>
            </div>            
            <div class="span3">
                <div class="control-group">
                    <label>Data conferma:</label>
                    <asp:Label runat="server" ID="txtDataConferma" Font-Bold="true"  CssClass="medium"></asp:Label>                                        
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>Confermata:</label>
                    <asp:Label runat="server" ID="lblConfermata" Font-Bold="true"  CssClass="medium" text="No" ></asp:Label>                    
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="upd2">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ListView1" />                
            </Triggers>
            <ContentTemplate>
                <div class="row-fluid">
                    <div class="span3">
                        <div class="control-group">
                            <label>Importo:</label>
                            <asp:TextBox runat="server" ID="txtImporto" Font-Bold="true" style="text-align:right"   CssClass="medium" Enabled="false"></asp:TextBox>                    
                        </div>
                    </div>                                     
                    <div class="span3">
                        <div class="control-group">
                            <label>Anno:</label>
                            <asp:Label runat="server" ID="lblAnno" Font-Bold="true"  CssClass="medium" text="No" ></asp:Label>                    
                        </div>
                    </div>   
                    <div class="span3">
                        <div class="control-group">
                            <label>Rettificata:</label>
                            <asp:Label runat="server" ID="lblRettificata" Font-Bold="true"  CssClass="medium" text="No" ></asp:Label>                    
                        </div>
                    </div>  
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel runat="server" ID="upd1">        
        <ContentTemplate>
    <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id">
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <div class="table">
                    <table class="display">
                        <thead>
                            <tr>
                                <th class="title-th" colspan="1" style="float:none"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Categoria" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Categoria" ></asp:Label></asp:LinkButton></th>                            
                                <th class="info-th" style="width:100px" colspan="1" style="float:none"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Pezzi" runat="server" ><asp:Label runat="server" ID="Label3" Text="Pezzi" ></asp:Label></asp:LinkButton></th>
                                <th class="info-th" style="width:100px" colspan="1" style="float:none"><asp:LinkButton ID="LinkButton6" CommandName="Sort" CommandArgument="Kg" runat="server" ><asp:Label runat="server" ID="Label6" Text="Kg" ></asp:Label></asp:LinkButton></th>
                                <th class="info-th" style="width:100px" colspan="1" style="float:none"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="PezziRettifica" runat="server" ><asp:Label runat="server" ID="Label2" Text="Nuovo Valore Tot. Pezzi" ></asp:Label></asp:LinkButton></th>
                                <th class="info-th" style="width:100px" colspan="1" style="float:none"><asp:LinkButton ID="LinkButton7" CommandName="Sort" CommandArgument="KgRettifica" runat="server" ><asp:Label runat="server" ID="Label8" Text="Nuovo Valore Tot. Kg" ></asp:Label></asp:LinkButton></th>
                                <th class="info-th" style="width:150px" colspan="1" style="float:none"><asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="CostoUnitario" runat="server" ><asp:Label runat="server" ID="Label7" Text="Costo unitario" ></asp:Label></asp:LinkButton></th>
                                <th class="info-th" style="width:150px"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="Importo" runat="server" ><asp:Label runat="server" ID="Label5" Text="ECO Contributi" ></asp:Label></asp:LinkButton></th>                            
                                <th class="info-th" style="width:150px" runat="server" id="colToHide" scope="col" ><asp:LinkButton ID="linkbutton" CommandName="Sort" CommandArgument="UtenteAggiornamento" runat="server" ><asp:Label runat="server" ID="Label11" Text="Utente agg." ></asp:Label></asp:LinkButton></th>                            
                                <th class="info-th" style="width:150px" runat="server" id="colToHide2" scope="col" ><asp:LinkButton ID="linkbutton8" CommandName="Sort" CommandArgument="DataAggiornamento" runat="server" ><asp:Label runat="server" ID="Label12" Text="Data agg." ></asp:Label></asp:LinkButton></th>                            
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div>
                <div class="dataTables_info" id="example_info">
                    <asp:DataPager ID="testInfoDataPager" runat="server" PageSize="2" PagedControlID="Listview1">
                        <Fields>
                            <asp:TemplatePagerField>
                                <PagerTemplate>
                                    Pagina
                                    <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" />
                                    di
                                    <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Math.Ceiling(Convert.ToDouble(Container.TotalRowCount) / Container.PageSize)%>" />
                                    (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />&nbsp;records)
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                </div>
                <div class="dataTables_paginate paging_full_numbers" id="example_paginate">
                    <asp:DataPager ID="testDataPager" runat="server" PageSize="10" PagedControlID="Listview1">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowLastPageButton="False" ShowNextPageButton="False" ShowPreviousPageButton="True"
                                ButtonCssClass="paginate_button" PreviousPageText="Precedente"
                                FirstPageText="Primo" />
                            <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="paginate_button" CurrentPageLabelCssClass="paginate_active" />
                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="False" ShowLastPageButton="True" ShowNextPageButton="True" ShowPreviousPageButton="False"
                                ButtonCssClass="paginate_button" NextPageText="Successivo"
                                LastPageText="Ultimo" />
                        </Fields>
                    </asp:DataPager>
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
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Categoria")%>' />
                </td>
               <td>
                   <asp:Label ID="txtPezzi" runat="server" CssClass="numeric" AutoComplete="off" Text='<%# Eval("Pezzi")%>' Style="text-align:right;width:100px" Enabled="false"/>                    
                </td>                
                <td>
                   <asp:Label ID="txtKg" runat="server" CssClass="numeric" AutoComplete="off" Text='<%# Eval("Kg", "{0:n2}")%>' Style="text-align:right;width:100px" Enabled="false"/>                    
                </td>                
                <td>
                   <asp:Textbox ID="txtPezziRettifica" runat="server" CssClass="numeric" AutoComplete="off" Text='<%# Eval("PezziRettifica", "{0:n2}")%>' Style="text-align:right;width:100px" OnTextChanged="txtPezziRettifica_TextChanged" AutoPostBack="true"/>                    
                </td>                
                <td>
                   <asp:Textbox ID="txtKgRettifica" runat="server" CssClass="numeric" AutoComplete="off" Text='<%# Eval("KgRettifica", "{0:n2}")%>' Style="text-align:right;width:100px"  OnTextChanged="txtKgRettifica_TextChanged" AutoPostBack="true"/>                    
                </td>                
                <td>
                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("CostoUnitario", "{0:C3}")%>' />
                </td>
                 <td>
                    <asp:Label ID="lblImporto" runat="server" Text='<%# Eval("Importo", "{0:C}")%>' />
                </td>
                <td runat="server" id="ColToHide">
                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("UtenteAggiornamento")%>' />
                </td>
                <td runat="server" id="ColToHide2">
                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("DataAggiornamento", "{0:d}")%>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Categoria")%>' />
                </td>
               <td>
                   <asp:Label ID="txtPezzi" runat="server" AutoComplete="off" CssClass="numeric" Text='<%# Eval("Pezzi")%>'  Style="text-align:right;width:100px" Enabled="false"/>                    
                </td>                
                <td>
                   <asp:Label ID="txtKg" runat="server" CssClass="numeric" AutoComplete="off" Text='<%# Eval("Kg", "{0:n2}")%>' Style="text-align:right;width:100px" Enabled="false"/>                    
                </td>           
                <td>
                   <asp:Textbox ID="txtPezziRettifica" runat="server" CssClass="numeric" AutoComplete="off" Text='<%# Eval("PezziRettifica", "{0:n2}")%>' Style="text-align:right;width:100px" OnTextChanged="txtPezziRettifica_TextChanged" AutoPostBack="true"/>                    
                </td>
                <td>
                   <asp:Textbox ID="txtKgRettifica" runat="server" CssClass="numeric" AutoComplete="off" Text='<%# Eval("KgRettifica", "{0:n2}")%>' Style="text-align:right;width:100px"  OnTextChanged="txtKgRettifica_TextChanged" AutoPostBack="true"/>                    
                </td> 
                <td>
                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("CostoUnitario", "{0:C3}")%>' />
                </td>
                 <td>
                    <asp:Label ID="lblImporto" runat="server" Text='<%# Eval("Importo", "{0:C}")%>' />
                </td>
                <td runat="server" id="ColToHide">
                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("UtenteAggiornamento")%>' />
                </td>
                <td runat="server" id="ColToHide2">
                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("DataAggiornamento", "{0:d}")%>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
            </ContentTemplate>
    </asp:UpdatePanel>

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="sp_RigheCertificazioni_Lista" SelectCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:querystringparameter name="IdCertificazione" querystringfield="Id" />
        </SelectParameters> 
    </asp:SqlDataSource>

     <script>
         function pageLoad() {
             $(function () {
                 $(".numeric").numeric({ decimal: ",", negative: false });

                 
                 $(".confirm_button").click(function () {
                     var callFrom = $(this);

                     jConfirm("Procedere con la conferma dell'autocertificazione?",
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

                     jConfirm("Procedere con la riapertura dell'autocertificazione?",
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

