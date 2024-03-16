<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaImpianti.aspx.vb" Inherits="WebApp_Impianti_ListaImpianti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Lista impianti</h1>

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
                <asp:LinkButton runat="server" ID="cmdEsporta">
                    <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span>
                </asp:LinkButton>
            </li>
        </ul>
    </div>

    <div class="clear"></div>

     <div class="alert red hideit" id="divError" runat="server" visible="false">
        <div class="left">
            <span class="red-icon"></span>
            <span class="alert-text"><asp:Literal runat="server" ID="lblError" Text="Errore: Oops mi spiace." ></asp:Literal></span>
        </div>
    </div>  

    <div class="clear"></div>
    <div class="form form-horizontal">
        <div class="row-fluid">
            <div class="span3"  runat="server" id="divCliente">
                <div class="control-group">
                    <label>
                        Cliente:
                    </label>
                    <asp:DropDownList runat="server" ID="ddlCliente" DataSourceID="sqlClienti" DataTextField="RagioneSociale" DataValueField="IdCliente" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="sqlClienti" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [IdCliente], [RagioneSociale] FROM [tbl_Clienti] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
                </div>
            </div>
            <div class="span3" runat="server" id="divCodice">
                <div class="control-group">
                    <label>Codice:</label>
                    <asp:TextBox runat="server" ID="txtCodice" CssClass="medium"></asp:TextBox>
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>Descrizione:</label>
                    <asp:TextBox runat="server" ID="txtDescrizione" CssClass="medium"></asp:TextBox>
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
                            <th class="title-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Descrizione" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Descrizione" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Provincia" runat="server" ><asp:Label runat="server" ID="Label2" Text="Provincia" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Latitudine" runat="server" ><asp:Label runat="server" ID="Label3" Text="Latitudine" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="Longitudine" runat="server" ><asp:Label runat="server" ID="Label5" Text="Longitudine" ></asp:Label></asp:LinkButton></th>
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Descrizione")%>' />
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Provincia")%>'/>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Latitudine")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("Longitudine")%>' />
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
                            <div class="sun-icon">
                                <asp:LinkButton ID="cmdPannelli" runat="server" CommandName="Pannelli" ToolTip="Lista pannelli abbinati" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Pannelli</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="doc-icon">
                                <asp:LinkButton ID="cmdCertificato" runat="server" CommandName="Certificato" ToolTip="Scarica certificato" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Certificato</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="cmdDelete" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');"  CommandArgument='<%#Eval("ID")%>'>
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Descrizione")%>' />
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Provincia")%>'/>
                </td>
                <td>
                    <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Latitudine")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("Longitudine")%>' />
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
                            <div class="sun-icon">                                
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Pannelli" ToolTip="Lista pannelli abbinati" CommandArgument='<%#Eval("ID")%>'>
                                    <span>Pannelli</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="doc-icon">
                                <asp:LinkButton ID="cmdCertificato" runat="server" CommandName="Certificato" ToolTip="Scarica certificato" CommandArgument='<%#Eval("ID")%>' >
                                    <span>Certificato</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="cmdDelete" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');" CommandArgument='<%#Eval("ID")%>'>
                            <span>Delete</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                    </ul>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="sp_Impianti_Lista" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCliente" Name="IdCliente" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="txtDescrizione" Name="Descrizione" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter ControlID="txtCodice" Name="Codice" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

