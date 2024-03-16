<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ListaDichiarazioniProf.aspx.vb" Inherits="WebApp_DichiarazioniProf_ListaDichiarazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Lista dichiarazioni professionali</h1>

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
                <asp:LinkButton runat="server" ID="cmdEsporta"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span> </asp:LinkButton>
            </li>
            <%--<li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsportaMacro"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta macro</span> </asp:LinkButton>
            </li>--%>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsportaDett"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta dett.</span> </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="clear"></div>
    <div class="form form-horizontal">
        <asp:UpdatePanel runat="server" ID="upd1">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlProduttore" />
            <asp:AsyncPostBackTrigger ControlID="ddlStato" />
            <asp:AsyncPostBackTrigger ControlID="cmdAnnulla" />
        </Triggers>
        <ContentTemplate>

        <div class="row-fluid">            
            <div class="span3" id="DivFiltroProduttore" runat="server">
                <div class="control-group">
                    <label>Produttore:</label>
                        <asp:DropDownList runat="server" ID="ddlProduttore" DataSourceID="sqlClienti" DataValueField="Id" AutoPostBack="true" DataTextField="RagioneSociale" AppendDataBoundItems="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>                        
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="sqlClienti" SelectCommand="SELECT * FROM tbl_Produttori Where Professionale = 1 ORDER BY RagioneSociale" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" ></asp:SqlDataSource>
                </div>
            </div>             
            <div class="span3">
                <div class="control-group">
                    <label>Stato:</label>
                    <asp:DropDownList runat="server" ID="ddlStato"  DataValueField="Id" DataTextField="Stato" AutoPostBack="true" AppendDataBoundItems="true" CssClass="chzn-select medium-select select" >
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>                        
                        <asp:ListItem Text="Non confermata" Value="0"></asp:ListItem>                        
                        <asp:ListItem Text="Confermata" Value="1"></asp:ListItem>                        
                    </asp:DropDownList>
                </div>
            </div>    
             <div class="span3">
                <div class="control-group">
                    <label>Data:</label>
                    <asp:TextBox runat="server" ID="txtData"   CssClass="medium rangedate" AutoComplete="off"></asp:TextBox>
                </div>
            </div>                     
        </div>
        <%--<div class="row-fluid">            
            <div class="span3" id="Div1" runat="server">
                <div class="control-group">
                    <label>Professionale:</label>
                        <asp:DropDownList runat="server" ID="ddlProfessionale"  AutoPostBack="true" CssClass="chzn-select medium-select select">
                        <asp:ListItem Text="Selezionare.." Value="-1"></asp:ListItem>                        
                        <asp:ListItem Text="Sì" Value="1"></asp:ListItem>   
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>   
                    </asp:DropDownList>
                    
                </div>
            </div>             
        </div>--%>

    <div id="example_filter2" class="dataTables_filter form-inline">
        <div class="clear"></div>
    </div>

    
            <asp:ListView ID="Listview1" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id">
                <LayoutTemplate>
                    <div id="itemPlaceholderContainer" runat="server">
                        <table class="display">
                            <thead>
                                <tr>
                                    <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Data" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Data" ></asp:Label></asp:LinkButton></th>
                                    <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Produttore" runat="server" ><asp:Label runat="server" ID="Label2" Text="Produttore" ></asp:Label></asp:LinkButton></th>
                                    <th class="title-th"><asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="DataRegistrazione" runat="server" ><asp:Label runat="server" ID="Label3" Text="Data Registrazione" ></asp:Label></asp:LinkButton></th>
                                    <th class="title-th"><asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="Confermata" runat="server" ><asp:Label runat="server" ID="Label7" Text="Confermata" ></asp:Label></asp:LinkButton></th>                                    
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
                            <asp:DataPager ID="testDataPager" runat="server" PageSize="15" PagedControlID="Listview1">
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
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id")%>' Visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                        </td>
                       <td>
                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("DataRegistrazione", "{0:dd/MM/yyyy}")%>' />
                        </td>                
                        <td>
                            <%#IIf(Eval("Confermata"), "Sì", "No")%>
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
                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("Data", "{0:dd/MM/yyyy}")%>' />
                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("Id")%>' Visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("RagioneSociale")%>' />
                        </td>                 
                        <td>
                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("DataRegistrazione", "{0:dd/MM/yyyy}")%>' />
                        </td>
                        <td>
                            <%#IIf(Eval("Confermata"), "Sì", "No")%>
                        </td>                       
                        <td>
                            <ul class="button-table-head">
                                <li>
                                    <div class="edit-icon">
                                        <asp:LinkButton ID="LinkButton8" runat="server" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("ID")%>'>
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
        </ContentTemplate>
    </asp:UpdatePanel>
        <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>"  SelectCommandType="Text">        
        </asp:SqlDataSource>

    </div>

<script>
    function pageLoad() {
        $(function () {
            $(".rangedate").daterangepicker(
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

