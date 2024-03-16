<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaAutocertificazioni.aspx.vb" Inherits="WebApp_Dichiarazioni_ListaAutocertificazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

     <h1>Lista autocertificazioni ECO-Contributi</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla"> <span class="ui-icon ui-icon-pencil toolbar-icon"></span><span class="desc">Annulla</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCerca"> <span class="ui-icon ui-icon-search toolbar-icon"></span><span class="desc">Cerca</span> </asp:LinkButton>
            </li>            
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdNuova"> <span class="ui-icon ui-icon-pencil toolbar-icon">></span><span class="desc">Nuova</span> </asp:LinkButton>
            </li> 
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdExportAEE"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">AEE</span> </asp:LinkButton>
            </li> 
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdExportPile"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc"> Pile</span> </asp:LinkButton>
            </li> 
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdExportVeicoli"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Acc. veicoli</span> </asp:LinkButton>
            </li> 
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdIndustrial"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Acc. industriali</span> </asp:LinkButton>
            </li> 
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAeeCategorie"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">AEE Categorie</span> </asp:LinkButton>
            </li> 
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdReportRettifiche"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Rettifiche</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdRettificheEuro"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Rett. EURO</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">                
                <asp:LinkButton runat="server" ID="cmdChiudi" tooltip="Chiude tutte le certificazioni aperte"
                    OnClientClick="return confirm('Confermare tutte le certificazioni aperte?');" > <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Chiudi</span> </asp:LinkButton>
            </li>
        </ul>
    </div>

    <div class="clear"></div>
    <div class="form form-horizontal">
         <div class="row-fluid">            
            <div class="span3" id="DivFiltroProduttore" runat="server">
                    <div class="control-group">
                        <label>Produttore:</label>
                            <asp:DropDownList runat="server" ID="ddlProduttore" DataSourceID="sqlClienti" DataValueField="Id" AutoPostBack="true" DataTextField="RagioneSociale" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                            <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>                        
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="sqlClienti" SelectCommand="SELECT * FROM tbl_Produttori ORDER BY RagioneSociale" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" ></asp:SqlDataSource>
                    </div>
                </div>     
            
            <div class="span3">
                <div class="control-group">
                    <label>Anno:</label>
                    <asp:TextBox runat="server" ID="txtAnno"   CssClass="medium rangedate numeric" AutoComplete="off"></asp:TextBox>
                </div>
        </div>   
          
            <div class="span3" id="Div1" runat="server">
            <div class="control-group">
                <label>Documento caricato:</label>
                    <asp:DropDownList runat="server" ID="ddlCaricato"  AutoPostBack="true" CssClass="chzn-select medium-select select">
                    <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>                        
                    <asp:ListItem Text="Sì" Value="1"></asp:ListItem>   
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>   
                </asp:DropDownList>                    
            </div>
        </div>             
        
            <div class="span3" id="Div2" runat="server">
                <div class="control-group">
                    <label>Confermate:</label>
                        <asp:DropDownList runat="server" ID="ddlConfermate"  AutoPostBack="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>                        
                        <asp:ListItem Text="Sì" Value="1"></asp:ListItem>   
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>   
                    </asp:DropDownList>                    
                </div>
            </div>             
        </div>
        <div class="row-fluid">            
            <div class="span3" id="Div3" runat="server">
                <div class="control-group">
                    <label>Rettificata:</label>
                        <asp:DropDownList runat="server" ID="ddlRettificata"  AutoPostBack="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>                        
                        <asp:ListItem Text="Sì" Value="1"></asp:ListItem>   
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>   
                    </asp:DropDownList>                    
                </div>
            </div>             
        </div>
</div>

    <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id">
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>
                            <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Anno" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Anno" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th" style="width: 20%"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Produttore" runat="server" ><asp:Label runat="server" ID="Label2" Text="Produttore" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th" style="width: 17%"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Data" runat="server" ><asp:Label runat="server" ID="Label3" Text="Data Registrazione" ></asp:Label></asp:LinkButton></th>        
                            <th class="title-th" style="width: 14%"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="UploadEseguito" runat="server" ><asp:Label runat="server" ID="Label5" Text="Doc. caricato" ></asp:Label></asp:LinkButton></th>        
                            <th class="title-th" style="width: 14%"><asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="Confermata" runat="server" ><asp:Label runat="server" ID="Label6" Text="Confermata" ></asp:Label></asp:LinkButton></th>        
                            <th class="title-th" style="width: 14%"><asp:LinkButton ID="LinkButton7" CommandName="Sort" CommandArgument="DataConferma" runat="server" ><asp:Label runat="server" ID="Label11" Text="Data conferma" ></asp:Label></asp:LinkButton></th>        
                            <th class="title-th" style="width: 14%"><asp:LinkButton ID="LinkButton6" CommandName="Sort" CommandArgument="Rettificata" runat="server" ><asp:Label runat="server" ID="Label7" Text="Rettificata" ></asp:Label></asp:LinkButton></th>        
                            <th class="action-th">Azioni</th>
                        </tr>
                    </thead>
                        <tbody>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </tbody>
                </table>
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
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Anno")%>'  />
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("Data", "{0:dd/MM/yyyy}")%>' />
                        </td>                                                               
                        <td>
                            <%#IIf(Eval("UploadEseguito"), "Sì", "No")%>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("Confermata")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server"  Text='<%# Eval("DataConferma", "{0:dd/MM/yyyy}")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("Rettificata")%>' />
                        </td>                        
                        <td class="action-th">
                            <ul class="button-table-head" style="float:left">                                
                                <li>
                                    <div class="adjust-icon">
                                        <asp:LinkButton ID="cmdRettifica" runat="server" CommandName="Rettifica" Text="Rettifica" ToolTip="Rettifica" CommandArgument='<%#Eval("ID")%>'>
                                            <span>Rettifica</span>
                                        </asp:LinkButton>
                                    </div>
                                </li>
                                <li>
                                    <div class="edit-icon">
                                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Edit</span>
                                        </asp:LinkButton>
                                    </div>
                                </li>
                                <li runat="server" id="DeleteLi">
                                    <div class="delete-icon">
                                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Delete</span>
                                        </asp:LinkButton>
                                    </div>
                                </li>
                            </ul>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td>                            
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Anno")%>'  />
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("Data", "{0:dd/MM/yyyy}")%>' />
                        </td>                                                               
                        <td>
                            <%#IIf(Eval("UploadEseguito"), "Sì", "No")%>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("Confermata")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server"  Text='<%# Eval("DataConferma", "{0:dd/MM/yyyy}")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("Rettificata")%>' />
                        </td>                        
                        <td class="action-th">
                            <ul class="button-table-head" style="float:left">     
                                <li>
                                    <div class="adjust-icon">
                                        <asp:LinkButton ID="cmdRettifica" runat="server" CommandName="Rettifica" Text="Rettifica" ToolTip="Rettifica" CommandArgument='<%#Eval("ID")%>'>
                                            <span>Rettifica</span>
                                        </asp:LinkButton>
                                    </div>
                                </li>                                
                                <li>
                                    <div class="edit-icon">
                                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Edit</span>
                                        </asp:LinkButton>
                                    </div>
                                </li>
                                <li runat="server" id="DeleteLi">
                                    <div class="delete-icon">
                                        <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Delete</span>
                                        </asp:LinkButton>
                                    </div>
                                </li>
                            </ul>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>

     
    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"  SelectCommandType="Text">        
    </asp:SqlDataSource>

     <script>
         function pageLoad() {
             $(function () {
                 $(".numeric").numeric({ decimal: "," });
                                 
             });
         }
    </script>         
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

