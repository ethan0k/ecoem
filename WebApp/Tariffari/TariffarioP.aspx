<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TariffarioP.aspx.vb" Inherits="WebApp_Tariffari_TariffarioP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Tariffario categorie professionali</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta">
                    <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span>
                </asp:LinkButton>
            </li>
        </ul>
    </div>

    <div class="clear"></div>

    <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id" >
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>                            
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Nome" runat="server" ><asp:Label runat="server" ID="Label2" Text="Nome" ></asp:Label></asp:LinkButton></th>                                
                            <th class="title-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Costo" runat="server" ><asp:Label runat="server" ID="Label5" Text="Costo" ></asp:Label></asp:LinkButton></th>                                
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </tbody>
                </table>
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
                    <asp:label runat="server" ID="txtTipoDiDato" CssClass="numeric"  Text='<%# Eval("TipoDiDato")%>' Visible="false"></asp:label> 
                    <asp:label runat="server" ID="txtCosto" CssClass="numeric" Text='<%# Eval("Costo", "{0:n3}")%>' style="text-align:right"></asp:label> 
                </td>               
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>                                
                    <td>
                        <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Nome")%>' />
                    </td>    
                    <td>
                        <asp:label runat="server" ID="txtTipoDiDato" CssClass="numeric"  Text='<%# Eval("TipoDiDato")%>' Visible="false"></asp:label> 
                        <asp:label runat="server" ID="txtCosto" CssClass="numeric" Text='<%# Eval("Costo", "{0:n3}")%>' style="text-align:right"></asp:label> 
                    </td>                              
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>

        <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" 
            SelectCommand="sp_TariffarioProduttore" SelectCommandType="StoredProcedure">            
            <SelectParameters>
                <asp:Parameter Name="IdProduttore" DbType="Int32" />
                <asp:Parameter Name="Professionale" DefaultValue="1" />                
            </SelectParameters>
        </asp:SqlDataSource> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

