<%@ Page Title="Categorie abbinate al cliente" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CategorieAbbinate.aspx.vb" Inherits="WebApp_Amministrazione_CategorieAbbinate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1><asp:Literal runat="server" ID="Titolo" Text="Categorie abbinate al cliente"></asp:Literal></h1>  
    
        

        <div id="example_filter2" class="dataTables_filter form-inline">
            <div class="clear"></div>
        </div>
    <asp:UpdatePanel runat="server" ID="upd1" >
        <ContentTemplate>
        <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id" >
            <LayoutTemplate>
                <div id="itemPlaceholderContainer" runat="server">
                    <table class="display">
                        <thead>
                            <tr>
                                <th style="width:1%;text-align:center"><asp:Label runat="server" ID="Label3" Text="Abbinato" ></asp:Label></th>                            
                                <th style="width:1%;text-align:center"><asp:Label runat="server" ID="Label8" Text="Disattivo" ></asp:Label></th>                            
                                <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Nome" runat="server" ><asp:Label runat="server" ID="Label2" Text="Nome" ></asp:Label></asp:LinkButton></th>                                
                                <th class="title-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Costo" runat="server" ><asp:Label runat="server" ID="Label5" Text="Costo" ></asp:Label></asp:LinkButton></th>                                
                                <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Peso" runat="server" ><asp:Label runat="server" ID="Label6" Text="Peso" ></asp:Label></asp:LinkButton></th>                                
                                <th class="title-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="ValoreDiForecast" runat="server" ><asp:Label runat="server" ID="Label7" Text="Valore forecast" ></asp:Label></asp:LinkButton></th>                                
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
                    <td style="text-align:center">
                        <asp:CheckBox runat="server" ID="Abbinato" oncheckedchanged="CheckBox1_CheckedChanged" AutoPostBack="true"/>
                    </td>   
                    <td style="text-align:center">
                        <asp:CheckBox runat="server" ID="chkDisattiva" Enabled="false" /> 
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Nome") %>' />
                    </td>      
                    <td>
                        <asp:textbox runat="server" ID="txtCosto" CssClass="numeric"  style="text-align:right" OnTextChanged="txtCosto_Changed" AutoPostBack="true"></asp:textbox> 
                    </td>
                    <td>
                        <asp:textbox runat="server" ID="txtPeso" CssClass="numeric"  style="text-align:right" OnTextChanged="txtPeso_Changed" AutoPostBack="true"></asp:textbox> 
                    </td>
                    <td>
                        <asp:textbox runat="server" ID="txtValoreForecast" CssClass="numeric"  style="text-align:right" OnTextChanged="txtValoreForecast_Changed" AutoPostBack="true"></asp:textbox> 
                    </td> 
                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                     <td style="text-align:center">
                        <asp:CheckBox runat="server" ID="Abbinato" oncheckedchanged="CheckBox1_CheckedChanged" AutoPostBack="true"/>
                    </td>  
                    <td style="text-align:center">
                        <asp:CheckBox runat="server" ID="chkDisattiva" Enabled="false"/> 
                    </td>
                    <td>
                        <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Nome")%>' />
                    </td>    
                     <td>
                        <asp:textbox runat="server" ID="txtCosto" CssClass="numeric"  style="text-align:right" OnTextChanged="txtCosto_Changed" AutoPostBack="true"></asp:textbox> 
                    </td>          
                    <td>
                        <asp:textbox runat="server" ID="txtPeso" CssClass="numeric"  style="text-align:right" OnTextChanged="txtPeso_Changed" AutoPostBack="true"></asp:textbox> 
                    </td>   
                     <td>
                        <asp:textbox runat="server" ID="txtValoreForecast" CssClass="numeric"  style="text-align:right" OnTextChanged="txtValoreForecast_Changed" AutoPostBack="true"></asp:textbox> 
                    </td>                    
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>
            </ContentTemplate>

</asp:UpdatePanel>    
        <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT * FROM tbl_CategorieNew Order By IdMacroCategoria">            
        </asp:SqlDataSource>          

      <script>
         function pageLoad() {
             $(function () {
                 $(".numeric").numeric({ decimal: "," });
                                 
                 });
                 
         }
    </script> 
</asp:Content>


