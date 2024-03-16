<%@ Page Title="Lista pannelli" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaPannelli.aspx.vb" Inherits="WebApp_Pannelli_ListaPannelli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
   <script type="text/javascript">
       function pageLoad() {
           $(function () {

               if ($('#<%=txtDataCaricamento.ClientID%>').is(':visible')) {
                    $('#<%=txtDataCaricamento.ClientID%>').daterangepicker();
                }                
            })

       }

   </script>

    <h1>Lista pannelli</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdNuovo"> <span class="ui-icon ui-icon-pencil toolbar-icon"></span><span class="desc">Nuovo</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCerca"> <span class="ui-icon ui-icon-search toolbar-icon"></span><span class="desc">Cerca</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla"> <span class="ui-icon ui-icon-refresh toolbar-icon"></span><span class="desc">Annulla</span> </asp:LinkButton>
            </li>
             <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdCertifica" OnClientClick="return confirm('Attenzione verrà applicata la conformità ai pannelli selezionati! Procedere?');"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Conforme</span> </asp:LinkButton>
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
                    <label>Matricola:</label>
                    <asp:TextBox runat="server" ID="txtMatricola"  CssClass="medium"></asp:TextBox>
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>Modello:</label>
                    <asp:TextBox runat="server" ID="txtModello"  CssClass="medium"></asp:TextBox>
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>Protocollo:</label>
                    <asp:TextBox runat="server" ID="txtProtocollo"  CssClass="medium"></asp:TextBox>
                </div>
            </div>
        </div>
         <div class="row-fluid">
            <div class="span3">
                <div class="control-group">
                    <label>
                        Stato:
                    </label>
                    <asp:DropDownList runat="server" ID="ddlStato" DataValueField="Id" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Attivo" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Dismesso" Value="1"></asp:ListItem>
                    </asp:DropDownList>                    
                </div>
            </div>
             <div class="span3">
                <div class="control-group">
                    <label>
                        Conformità:
                    </label>
                    <asp:DropDownList runat="server" ID="ddlConformita" DataValueField="Id" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Conforme" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Non conforme" Value="0"></asp:ListItem>
                    </asp:DropDownList>                    
                </div>                
            </div>
            <div class="span3">
                <div class="control-group">
                        <label>
                            Data caricamento:
                        </label>
                        <asp:TextBox runat="server" ID="txtDataCaricamento"  CssClass="medium" autocomplete="off"></asp:TextBox>
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
                            <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Matricola" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Matricola" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Modello" runat="server" ><asp:Label runat="server" ID="Label2" Text="Modello" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Produttore" runat="server" ><asp:Label runat="server" ID="Label3" Text="Produttore" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="Protocollo" runat="server" ><asp:Label runat="server" ID="Label7" Text="Protocollo" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="Conforme" runat="server" ><asp:Label runat="server" ID="Label5" Text="Conforme" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton6" CommandName="Sort" CommandArgument="DataConformita" runat="server" ><asp:Label runat="server" ID="label6" Text="Data conformità" ></asp:Label></asp:LinkButton></th>
                            <th class="action-th" style="padding-right:30px"">Azioni</th>
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Matricola")%>' />
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>' Visible="false" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Modello")%>' />
                </td>
               <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Protocollo")%>' />
                </td>
                <td>
                    <%#IIf(Eval("Conforme"), "Sì", "No")%>
                </td>
                 <td>
                    <asp:Label ID="lblDataConformita" runat="server" Text='<%# Eval("DataConformita","{0:dd/MM/yyyy}")%>' />
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
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" OnClientClick="return confirm('Attenzione il record verrà eliminato e non sarà più recuperabile! Sei sicuro?');" CommandArgument='<%#Eval("Id")%>'>
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Matricola")%>' />
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>' Visible="false" />
                </td>
                <td>
                    <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Modello")%>' />
                </td>
                 <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Protocollo")%>' />
                </td>
                <td>
                    <%#IIf(Eval("Conforme"), "Sì", "No")%>
                </td>
                <td>
                    <asp:Label ID="lblDataConformita" runat="server" Text='<%# Eval("DataConformita", "{0:dd/MM/yyyy}")%>' />
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

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommandType="Text">
    </asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="SqlDataExport" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="sp_Pannelli_Lista_export" SelectCommandType="StoredProcedure"> <%--CancelSelectOnNullParameter="false"--%>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlProduttori" Name="IdProduttore" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
            <asp:ControlParameter ControlID="txtMatricola" Name="Matricola" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" DefaultValue="NULL" />
            <asp:ControlParameter ControlID="txtModello" Name="Modello" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" DefaultValue="NULL"/>
            <asp:ControlParameter ControlID="txtProtocollo" Name="Protocollo" PropertyName="Text" Type="String" ConvertEmptyStringToNull="false" DefaultValue="NULL"/>
            <asp:ControlParameter ControlID="ddlStato" Name="Dismesso" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
            <asp:ControlParameter ControlID="ddlConformita" Name="Conformita" PropertyName="SelectedValue" Type="Int32" DefaultValue="-1" />
            <asp:Parameter Name="Ruolo" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

