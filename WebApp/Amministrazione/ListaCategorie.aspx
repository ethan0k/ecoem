<%@ Page Title="Lista categorie" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaCategorie.aspx.vb" Inherits="WebApp_Amministrazione_ListaCategorie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
<script>

$(document).ready(function () {
    $("#<%= cmdAggiorna.ClientID %>").bind('click', function () {
        jConfirm("Procedere con l'aggiornamento dei costi per tutti i produttori?", 'Confirmation Dialog', function (r) {
            if (r == true) {

                //Unbind client side click event and submit btn
                $("#<%= cmdAggiorna.ClientID %>").unbind('click');
                $("#<%= cmdAggiorna.ClientID %>")[0].click();
            }
        });

        //Always prevent a postback by returning false
        return false;
    });
});
</script>

        <h1>Lista categorie</h1>
        <div class="toolbar">
            <ul>
                <li class="button-profile-2">
                    <asp:LinkButton runat="server" ID="cmdNuovo">
                        <span class="ui-icon ui-icon-pencil toolbar-icon"></span><span class="desc">Nuovo</span>
                    </asp:LinkButton>
                </li>
                <li class="button-profile-2">
                    <asp:LinkButton runat="server" ID="cmdCerca">
                        <span class="ui-icon ui-icon-search toolbar-icon"></span><span class="desc">Cerca</span>
                    </asp:LinkButton>
                </li>
                <li class="button-profile-2">
                    <asp:LinkButton runat="server" ID="cmdAnnulla">
                        <span class="ui-icon ui-icon-refresh toolbar-icon"></span><span class="desc">Annulla</span>
                    </asp:LinkButton>
                </li>
                <li class="button-profile-2">
                    <asp:LinkButton runat="server" ID="cmdAggiorna">
                        <span class="ui-icon ui-icon-grip-diagonal-se toolbar-icon"></span><span class="desc">Aggiorna</span>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div class="clear"></div>
        <div class="form form-horizontal">
            <div class="row-fluid">
                <div class="span3">
                    <div class="control-group">
                        <label>
                            Categoria:
                        </label>
                        <asp:TextBox runat="server" ID="txtFiltroCategoria" placeholder="Search" CssClass="medium"></asp:TextBox>
                    </div>
                </div>    
                 <div class="span3">
                    <div class="control-group">
                        <label>Macrocategoria:</label>
                        <asp:DropDownList runat="server" ID="ddlMacroCategoria" CssClass="chzn-select medium-select select" DataSourceID="sqlMacro" AppendDataBoundItems="true"  DataTextField="Nome" DataValueField="Id">
                            <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="sqlMacro" SelectCommandType="Text" SelectCommand="SELECT * FROM tbl_MacroCategorieNew Order By Nome" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"></asp:SqlDataSource>
                    </div>
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
                                <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Nome" runat="server" ><asp:Label runat="server" ID="Label2" Text="Nome" ></asp:Label></asp:LinkButton></th>
                                <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="TipoDiDato" runat="server" ><asp:Label runat="server" ID="Label3" Text="Tipo di dato" ></asp:Label></asp:LinkButton></th>
                                <th class="title-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Valore" runat="server" ><asp:Label runat="server" ID="Label5" Text="Valore" ></asp:Label></asp:LinkButton></th>
                                <th class="action-th"><div style="text-align:right;padding-right:20px">Azioni</div></th>
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
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Nome") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblTipoDato" runat="server" Text='<%# Eval("TipoDiDato")%>' />
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("Valore")%>' />
                    </td>
                    <td>
                        <ul class="button-table-head">
                            <li>
                                <div class="edit-icon">
                                    <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("ID")%>'>
                                <span>Edit</span>
                                    </asp:LinkButton>
                                </div>
                            </li>
                            <li>
                                <div class="delete-icon">
                                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" CommandArgument='<%#Eval("ID")%>'>
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
                        <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Nome")%>' />
                    </td>
                    <td>
                        <asp:Label ID="lblTipoDato" runat="server" Text='<%# Eval("TipoDiDato") %>' />
                    </td>
                     <td>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Valore")%>' />
                    </td>
                    <td>
                        <ul class="button-table-head">
                            
                            <li>
                                <div class="edit-icon">
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("ID")%>'>
                                <span>Edit</span>
                                    </asp:LinkButton>
                                </div>
                            </li>
                            <li>
                                <div class="delete-icon">
                                    <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" CommandArgument='<%#Eval("ID")%>'>
                                <span>Delete</span>
                                    </asp:LinkButton>
                                </div>
                            </li>
                        </ul>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>

        <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="sp_CategoriaNew_Lista" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter Name="Categoria" DbType="String" ControlID="txtFiltroCategoria" PropertyName="Text" DefaultValue="" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="IdMacroCategoria" DbType="Int32" ControlID="ddlMacrocategoria" PropertyName="SelectedValue" DefaultValue="0" />
            </SelectParameters>
        </asp:SqlDataSource>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

