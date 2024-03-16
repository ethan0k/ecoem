<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaUtenti.aspx.vb" Inherits="WebApp_Amministrazione_ListaUtenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <h1>Lista utenti</h1>

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
                        Username:
                    </label>
                    <asp:TextBox runat="server" ID="txtUserName" CssClass="medium"></asp:TextBox>
                </div>
            </div>
            <div class="span3">
                <div class="control-group">
                    <label>Nominativo:</label>
                    <asp:TextBox runat="server" ID="txtNominativo" CssClass="medium"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <div id="example_filter2" class="dataTables_filter form-inline">
        <div class="clear"></div>
    </div>

    <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="UserId">
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>
                            <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="UserName" runat="server" ><asp:Label runat="server" ID="lbl01" Text="UserName" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Comment" runat="server" ><asp:Label runat="server" ID="Label4" Text="Nominativo" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="Email" runat="server" ><asp:Label runat="server" ID="Label5" Text="Email" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="LastLoginDate" runat="server" ><asp:Label runat="server" ID="Label6" Text="Ultimo accesso" ></asp:Label></asp:LinkButton></th>
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Username")%>' />
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("UserId")%>' Visible="false" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Comment")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("Email")%>' />
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("LastLoginDate")%>' />
                </td>
                <td>
                    <ul class="button-table-head">
                        <li>
                            <div class="edit-icon">
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("UserID")%>' ToolTip="Modifica">
                            <span>Edit</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="changePwd-icon">
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="ChangePwd" Text="ChangePwd" CommandArgument='<%#Eval("UserID")%>' ToolTip="Cambia password">
                                    <span>Change Password</span>
                                </asp:LinkButton>
                            </div>
                        </li>


                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" ToolTip="Elimina" CommandArgument='<%#Eval("UserName")%>' >
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
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Username")%>' />
                </td>
                <td>
                    <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Comment")%>' />
                </td>
                <td>
                    <asp:Label ID="descriptionLabel" runat="server" Text='<%# Eval("Email") %>' />
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("LastLogindate") %>' />
                </td>
                <td>
                    <ul class="button-table-head">
                        <li>
                            <div class="edit-icon">
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("UserID")%>'>
                                    <span>Edit</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="changePwd-icon">
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="ChangePwd" Text="ChangePwd" CommandArgument='<%#Eval("UserID")%>' ToolTip="Modifica password">
                                    <span>Change Password</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                        <li>
                            <div class="delete-icon">
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Elimina" CommandArgument='<%#Eval("UserName")%>' >
                                    <span>Delete</span>
                                </asp:LinkButton>
                            </div>
                        </li>
                    </ul>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="aspnet_ListaUtenti" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtUserName" ConvertEmptyStringToNull="false"  
                Name="UserName" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtNominativo" ConvertEmptyStringToNull="false"  
                Name="Nominativo" PropertyName="Text" Type="String" /> 
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

