<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Certificati.aspx.vb" Inherits="WebApp_Tariffari_Certificati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
        <h1>Certificati di adesione</h1>


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
        </div>

        <div id="example_filter2" class="dataTables_filter form-inline">
            <div class="clear"></div>
        </div>

        <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id">
            <LayoutTemplate>
                <div id="itemPlaceholderContainer" runat="server">
                    <table class="display">
                        <thead>
                            <tr>
                                <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Anno" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Anno" ></asp:Label></asp:LinkButton></th>
                                <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Protocollo" runat="server" ><asp:Label runat="server" ID="Label3" Text="Protocollo" ></asp:Label></asp:LinkButton></th>                                
                                <th class="action-th" style="padding-right:30px"">Download</th>
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
                        <asp:DataPager ID="testDataPager" runat="server" PageSize="15" PagedControlID="Listview1">
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
                        <asp:Label ID="lblData" runat="server" Text='<%# Eval("Anno")%>' />
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>' Visible="false" />
                    </td>                   
                    <td>
                        <asp:Label runat="server" ID="lblPrtocollo" Text='<%# Eval("Protocollo")%>' />
                    </td>                                    
                    <td style="float:left">
                        <ul class="button-table-head">
                            <li>
                                <div class="doc-icon">
                                    <asp:LinkButton ID="cmdCertificato" runat="server" CommandName="Certificato" ToolTip="Scarica certificato" CommandArgument='<%#Eval("Id")%>'>
                                        <span>Attestato</span>
                                    </asp:LinkButton>
                                </div>
                            </li>
                        </ul>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="gray-tr">
                    <td>
                        <asp:Label ID="lblData" runat="server" Text='<%# Eval("Anno")%>' />
                        <asp:Label ID="Label4" runat="server"  Text='<%# Eval("Id")%>' Visible="false" />
                    </td>                   
                    <td>
                        <asp:Label ID="lblProtocollo" runat="server" Text='<%# Eval("Protocollo")%>' />
                    </td>                                    
                    <td class="th_status">
                        <ul class="button-table-head">
                            <li>
                                <div class="doc-icon">
                                    <asp:LinkButton ID="cmdCertificato" runat="server" CommandName="Download" ToolTip="Scarica certificato" CommandArgument='<%#Eval("Id")%>'>
                                        <span>Attestato</span>
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

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

