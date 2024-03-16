<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaProtocolli.aspx.vb" Inherits="WebApp_Pannelli_ListaProtocolli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Lista protocolli</h1>

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

        <div class="row-fluid">
            <div class="span3">
                <div class="control-group">
                    <label>
                        Protocollo:
                    </label>
                    <asp:TextBox runat="server" ID="txtProtocollo"  CssClass="medium"></asp:TextBox>
                </div>               
            </div>
            <div class="span3" runat="server" id="divProduttore" visible="false">
                <div class="control-group">
                    <label>
                        Produttore:
                    </label>
                    <asp:DropDownList runat="server" ID="ddlProduttori" DataSourceID="sqlDsProduttori" DataTextField="RagioneSociale" DataValueField="Id" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="sqlDsProduttori" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [Id], [RagioneSociale] FROM [tbl_Produttori] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>
                        Conformità:
                    </label>
                    <asp:DropDownList runat="server" ID="ddlConformita" DataValueField="Id" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Conforme" Value="Sì"></asp:ListItem>
                        <asp:ListItem Text="Non conforme" Value="No"></asp:ListItem>
                    </asp:DropDownList>
                    
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
                            <th class="info-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="RagioneSociale" runat="server" ><asp:Label runat="server" ID="Label7" Text="Produttore" ></asp:Label></asp:LinkButton></th>
                            <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Protocollo" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Protocollo" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="Data" runat="server" ><asp:Label runat="server" ID="Label11" Text="Data" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="NrPannelli" runat="server" ><asp:Label runat="server" ID="Label5" Text="Pannelli associati" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton6" CommandName="Sort" CommandArgument="CostoServizio" runat="server" ><asp:Label runat="server" ID="Label9" Text="Costo del servizio" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Conforme" runat="server" ><asp:Label runat="server" ID="Label2" Text="Conforme" ></asp:Label></asp:LinkButton></th>
                            <th class="action-th" style="padding-right:55px">Azioni</th>
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
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                </td>
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Protocollo")%>' />
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>' Visible="false" />
                </td>                
                <td>                     
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Data","{0:dd/MM/yyyy}")%>' />
                </td>
                 <td>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("NrPannelli", "{0:N0}")%>' />
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("CostoServizio", "{0:c}")%>' />
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Conforme")%>' />
                </td>
                <td>
                    <ul class="button-table-head">
                        <li>
                            <div class="doc-icon">
                                <asp:LinkButton ID="cmdCertificato" runat="server" CommandName="Certificato" ToolTip="Scarica attestato" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Attestato</span>
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
                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato insieme a tutti i pannelli abbinati. Procedere?');" CommandArgument='<%#Eval("Id")%>'>
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
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                </td>
               <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Protocollo")%>' />
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>' Visible="false" />
                </td>                
                 <td>                     
                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("Data","{0:dd/MM/yyyy}")%>' />
                </td>
                 <td>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("NrPannelli", "{0:N0}")%>' />
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("CostoServizio", "{0:c}")%>' />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Conforme")%>' />
                </td>
                <td>
                    <ul class="button-table-head">
                        <li>
                            <div class="doc-icon">
                                <asp:LinkButton ID="cmdCertificato" runat="server" CommandName="Certificato" ToolTip="Scarica attestato" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Attestato</span>
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
                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');" CommandArgument='<%#Eval("Id")%>'>
                            <span>Delete</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                    </ul>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="sp_Protocolli_Lista" SelectCommandType="StoredProcedure"> <%--CancelSelectOnNullParameter="false"--%>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtProtocollo" Name="Protocollo" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" DefaultValue="NULL"/>
            <asp:ControlParameter ControlID="ddlProduttori" Name="IdProduttore" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
            <asp:ControlParameter ControlID="ddlConformita" Name="Conformita" PropertyName="SelectedValue" Type="String" DefaultValue="NULL" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

