<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="VerificaDichiarazioni.aspx.vb" Inherits="WebApp_Dichiarazioni_VerificaDichiarazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
      <h1>Verifica dichiarazioni Professionali</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdVerifica"> <span class="ui-icon ui-icon-pencil toolbar-icon"></span><span class="desc">Verifica</span> </asp:LinkButton>
            </li>                       
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span> </asp:LinkButton>
            </li>   
             <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdInviaMail"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Invia Mail</span> </asp:LinkButton>
            </li>  
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAutodichiarazione"> <span class="ui-icon ui-icon-arrowthick-1-nw  toolbar-icon"></span><span class="desc">Autodichiarazione</span> </asp:LinkButton>
            </li> 
        </ul>
    </div>

    <div class="clear"></div>
    
    <div class="form form-horizontal">
        <div class="row-fluid">            
            <div class="span3" id="DivFiltroProduttore" runat="server">
                <div class="control-group">
                    <label>Data di riferimento:</label>
                    <asp:TextBox runat="server" ID="txtData" CssClass="medium rangedate" AutoComplete="off"></asp:TextBox>
                </div>
            </div>  
        </div>
    </div>
    
    <asp:ListView ID="Listview1" runat="server" >
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>
                            <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Data" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Data" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Produttore" runat="server" ><asp:Label runat="server" ID="Label2" Text="Produttore" ></asp:Label></asp:LinkButton></th>                            
                            <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Produttore" runat="server" ><asp:Label runat="server" ID="Label5" Text="Periodicità" ></asp:Label></asp:LinkButton></th>                            
                            <th class="title-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="Mail" runat="server" ><asp:Label runat="server" ID="Label7" Text="Mail inviata" ></asp:Label></asp:LinkButton></th>                            
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
                    <asp:Label ID="lblData" runat="server" Text='<%# Eval("Data", "{0:dd/MM/yyyy}")%>' />
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Produttore")%>' />
                </td>  
                <td>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Periodicità")%>' />
                </td>   
                <td>
                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("Mail")%>' />
                </td>            
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Data", "{0:dd/MM/yyyy}")%>' />
                </td>
                <td>
                    <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Produttore")%>' />
                </td>   
                 <td>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Periodicità")%>' />
                </td>     
                  <td>
                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("Mail")%>' />
                </td>                            
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>


<script>
    function pageLoad() {
        $(function () {
            $(".rangedate").datepicker(
                {
                    locale: {
                        format: 'YYYY-MM-DD'
                    }

                })
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    }

</script>     
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

