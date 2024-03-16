<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaManuali.aspx.vb" Inherits="WebApp_Amministrazione_ListaManuali" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

     <h1>Lista manuali</h1>

      <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdNuovo">
                    <span class="ui-icon ui-icon-pencil toolbar-icon"></span><span class="desc">Aggiungi</span>
                </asp:LinkButton>
            </li>            
        </ul>
    </div>
    
    <div class="clear"></div>
    
    <div id="example_filter2" class="dataTables_filter form-inline">
        <div class="clear"></div>
    </div>

    <asp:ListView ID="Listview1" runat="server"  DataKeyNames="FileName">
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>
                            <th class="info-th"><asp:LinkButton ID="LinkButton1"   runat="server" ><asp:Label runat="server" ID="lbl01" Text="Nome file" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton2"  runat="server" ><asp:Label runat="server" ID="Label2" Text="Data" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton3"  runat="server" ><asp:Label runat="server" ID="Label3" Text="Dimensione" ></asp:Label></asp:LinkButton></th>
                            <th class="action-th" style="text-align:right;padding-right:10px">Azioni</th>
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
                    <asp:HyperLink ID="HyperFileName" runat="server"><asp:Label ID="idLabel" runat="server" Text='<%# Eval("FileName")%>' /></asp:HyperLink>                        
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Data")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# String.Format("{0:N0}", Eval("Dimension"))%>'/>
                </td>
                <td>
                    <ul class="button-table-head">                        
                        <li>
                            <div class="doc-icon">
                                <asp:LinkButton ID="DownloadButton" runat="server" CommandName="Download" CommandArgument='<%#Eval("FileName")%>'>
                                    <span>Download</span>
                                </asp:LinkButton>
                            </div>
                            <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server"  OnClientClick="return confirm('Cancellare il file?');" CommandName="Elimina" CommandArgument='<%#Eval("FilePath")%>'>
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
                    <asp:HyperLink ID="HyperFileName" runat="server" ><asp:Label ID="idLabel" runat="server" Text='<%# Eval("FileName")%>' /></asp:HyperLink>                    
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Data")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# String.Format("{0:N0}", Eval("Dimension"))%>'/>
                </td>
                <td>
                    <ul class="button-table-head">                       
                        <li>
                            <div class="doc-icon">
                                <asp:LinkButton ID="DownloadButton" runat="server"  CommandName="Download" CommandArgument='<%#Eval("FileName")%>'>
                                    <span>Download</span>
                                </asp:LinkButton>
                            </div>
                             <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server" OnClientClick="return confirm('Cancellare il file?');" CommandName="Elimina" CommandArgument='<%#Eval("FilePath")%>' >
                                    <span>Delete</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                    </ul>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

