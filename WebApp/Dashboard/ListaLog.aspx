<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaLog.aspx.vb" Inherits="WebApp_Dashboard_ListaLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
      <h1>Lista LOG</h1>

      <div class="toolbar">
        <ul>            
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCerca"> <span class="ui-icon ui-icon-search toolbar-icon"></span><span class="desc">Cerca</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla"> <span class="ui-icon ui-icon-refresh toolbar-icon"></span><span class="desc">Annulla</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span> </asp:LinkButton>
            </li>            
        </ul>
    </div>
    <div class="clear"></div>

    <div class="form form-horizontal">

        <asp:ListView ID="ListaLog" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id">
            <LayoutTemplate>
                <div id="itemPlaceholderContainer" runat="server">
                    <table class="display">
                        <thead>
                            <tr>
                                <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="ID" runat="server" ><asp:Label runat="server" ID="lbl01" Text="ID" ></asp:Label></asp:LinkButton></th>
                                <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Data" runat="server" ><asp:Label runat="server" ID="Label2" Text="Data" ></asp:Label></asp:LinkButton></th>
                                <th class="title-th"><asp:LinkButton ID="LinkButton9" CommandName="Sort" CommandArgument="Origine" runat="server" ><asp:Label runat="server" ID="Label7" Text="Origine" ></asp:Label></asp:LinkButton></th>
                                <th class="title-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="Descrizione" runat="server" ><asp:Label runat="server" ID="Label5" Text="Descrizione" ></asp:Label></asp:LinkButton></th>                                                                
                                <th class="title-th"><asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="Utente" runat="server" ><asp:Label runat="server" ID="Label10" Text="Utente" ></asp:Label></asp:LinkButton></th>                                                                
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
                        <asp:DataPager ID="testInfoDataPager" runat="server" PageSize="2" PagedControlID="ListaLog">
                            <Fields>
                                <asp:TemplatePagerField>
                                    <PagerTemplate>
                                        Pagina
                                        <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# IIf(Container.TotalRowCount > 0, (Container.StartRowIndex / Container.PageSize) + 1, 0) %>" />
                                        di
                                        <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Math.Ceiling(Convert.ToDouble(Container.TotalRowCount) / Container.PageSize)%>" />
                                        (<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />&nbsp;records)
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>
                    </div>
                    <div class="dataTables_paginate paging_full_numbers" id="example_paginate">
                        <asp:DataPager ID="testDataPager" runat="server" PageSize="10" PagedControlID="ListaLog">
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
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>'/>
                    </td>
                    <td>
                        <asp:Label ID="lblData" runat="server" Text='<%# Eval("Ora")%>' />
                    </td>
                    <td>
                        <asp:Label ID="lblImporto" runat="server" Text='<%# Eval("Origine")%>'/>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#Left(Eval("Descrizione"), 15) & ".."%>' ToolTip='<%#Eval("Descrizione") %>'/>                           
                    </td>                         
                    <td>
                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("Utente")%>'/>                        
                    </td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                 <tr class="gray-tr">
                    <td>                        
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>'/>
                    </td>
                    <td>
                        <asp:Label ID="lblData" runat="server" Text='<%# Eval("Data", "{0:dd/MM/yyyy}") + " " + Eval("Ora", "{0:hh:mm:ss}")%>' />
                    </td>           
                    <td>
                        <asp:Label ID="lblImporto" runat="server" Text='<%# Eval("Origine")%>'/>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#Left(Eval("Descrizione"), 15) & ".."%>' ToolTip='<%#Eval("Descrizione") %>'/>                           
                    </td>                         
                    <td>
                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("Utente")%>'/>                        
                    </td>                    
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>

         <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"  
             SelectCommandType="StoredProcedure" SelectCommand="sp_Log_Lista">    
             <SelectParameters>
                <asp:Parameter Name="UserName" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

