<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Tariffario.aspx.vb" Inherits="WebApp_Tariffari_Tariffario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Tariffario registrazione pannelli</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta">
                    <span class="ui-icon ui-ui-icon-arrowrefresh-1-n   toolbar-icon"></span><span class="desc">Esporta</span>
                </asp:LinkButton>
            </li>
        </ul>
    </div>

    <div class="clear"></div>

    <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id">
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>                            
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Tipologiacella" runat="server" ><asp:Label runat="server" ID="Label2" Text="Tipologia cella" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="FasciaDiPeso" runat="server" ><asp:Label runat="server" ID="Label3" Text="Fascia di peso" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="CostoMatricola" runat="server" ><asp:Label runat="server" ID="Label5" Text="Costo matricola" ></asp:Label></asp:LinkButton></th>
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
                    <asp:DataPager ID="testInfoDataPager" runat="server" PageSize="10" PagedControlID="Listview1">
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
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("TipologiaCella") %>' />
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("FasciaDiPeso") %>' />
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("CostoMatricola", "{0:n3}") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>               
                <td>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("TipologiaCella") %>' />
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("FasciaDiPeso") %>' />
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("CostoMatricola", "{0:n3}") %>' />
                </td>                   
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="sp_Abbinamenti_Lista" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="IdProduttore" QueryStringField="Id" />
        </SelectParameters>
    </asp:SqlDataSource>
 

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

