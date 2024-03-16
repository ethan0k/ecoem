﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaClienti.aspx.vb" Inherits="WebApp_Amministrazione_ListaClienti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Lista clienti/installatori</h1>

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
        </ul>
    </div>
    <div class="clear"></div>
    <div class="form form-horizontal">
        <div class="row-fluid">
            <div class="span3">
                <div class="control-group">
                    <label>
                        Ragione sociale:
                    </label>
                    <asp:TextBox runat="server" ID="txtRagioneSociale" placeholder="Search" CssClass="medium"></asp:TextBox>
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>Partita IVA:</label>
                    <asp:TextBox runat="server" ID="txtPartitaIVA" placeholder="Search" CssClass="medium"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <div id="example_filter2" class="dataTables_filter form-inline">
        <div class="clear"></div>
    </div>

    <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="IdCliente">
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>                            
                            <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Nominativo" runat="server" ><asp:Label runat="server" ID="Label3" Text="Ragione sociale" ></asp:Label></asp:LinkButton></th>
                                                        <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Contatto" runat="server" ><asp:Label runat="server" ID="Label2" Text="Contatto" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Telefono" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Telefono" ></asp:Label></asp:LinkButton></th>                            
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Nominativo")%>' />
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("IdCliente")%>' visible="false"/>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Contatto")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("Telefono")%>' />
                </td>
                <td>
                    <ul class="button-table-head">
                        <li>
                            <div class="edit-icon">
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("IDCliente")%>'>
                            <span>Edit</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');" CommandArgument='<%#Eval("IDcliente")%>'>
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("Nominativo")%>' />
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("IdCliente")%>' visible="false"/>
                </td>
                <td>
                    <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Contatto")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("Telefono") %>' />
                </td>
                <td>
                    <ul class="button-table-head">                       
                        <li>
                            <div class="edit-icon">
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("IDcliente")%>'>
                            <span>Edit</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');" CommandArgument='<%#Eval("IDcliente")%>'>
                            <span>Delete</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                    </ul>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT IdCliente, RagioneSociale, Cognome + ' ' + Nome AS Nominativo, Contatto, Telefono FROM tbl_Clienti Order By RagioneSociale" SelectCommandType="Text">        
    </asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

