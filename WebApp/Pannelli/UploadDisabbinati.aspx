<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UploadDisabbinati.aspx.vb" Inherits="WebApp_Pannelli_UploadDisabbinati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
<style type="text/css">
.modal {

  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  left: 0;
  top: 0;
  left:0;
  right:0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

.modal-content {
  background-color: #fefefe;
  margin: 15% auto; /* 15% from the top and centered */
  border: 1px solid #888;
  width: 100%; /* Could be more or less, depending on screen size */
}
.cssload-thecube {
	width: 73px;
	height: 73px;
	margin: 0 auto;
	margin-top: 49px;
	position: relative;
	transform: rotateZ(45deg);
		-o-transform: rotateZ(45deg);
		-ms-transform: rotateZ(45deg);
		-webkit-transform: rotateZ(45deg);
		-moz-transform: rotateZ(45deg);
}
.cssload-thecube .cssload-cube {
	position: relative;
	transform: rotateZ(45deg);
		-o-transform: rotateZ(45deg);
		-ms-transform: rotateZ(45deg);
		-webkit-transform: rotateZ(45deg);
		-moz-transform: rotateZ(45deg);
}
.cssload-thecube .cssload-cube {
	float: left;
	width: 50%;
	height: 50%;
	position: relative;
	transform: scale(1.1);
		-o-transform: scale(1.1);
		-ms-transform: scale(1.1);
		-webkit-transform: scale(1.1);
		-moz-transform: scale(1.1);
}
.cssload-thecube .cssload-cube:before {
	content: "";
	position: absolute;
	top: 0;
	left: 0;
	width: 100%;
	height: 100%;
	background-color: rgb(43,160,199);
	animation: cssload-fold-thecube 2.76s infinite linear both;
		-o-animation: cssload-fold-thecube 2.76s infinite linear both;
		-ms-animation: cssload-fold-thecube 2.76s infinite linear both;
		-webkit-animation: cssload-fold-thecube 2.76s infinite linear both;
		-moz-animation: cssload-fold-thecube 2.76s infinite linear both;
	transform-origin: 100% 100%;
		-o-transform-origin: 100% 100%;
		-ms-transform-origin: 100% 100%;
		-webkit-transform-origin: 100% 100%;
		-moz-transform-origin: 100% 100%;
}
.cssload-thecube .cssload-c2 {
	transform: scale(1.1) rotateZ(90deg);
		-o-transform: scale(1.1) rotateZ(90deg);
		-ms-transform: scale(1.1) rotateZ(90deg);
		-webkit-transform: scale(1.1) rotateZ(90deg);
		-moz-transform: scale(1.1) rotateZ(90deg);
}
.cssload-thecube .cssload-c3 {
	transform: scale(1.1) rotateZ(180deg);
		-o-transform: scale(1.1) rotateZ(180deg);
		-ms-transform: scale(1.1) rotateZ(180deg);
		-webkit-transform: scale(1.1) rotateZ(180deg);
		-moz-transform: scale(1.1) rotateZ(180deg);
}
.cssload-thecube .cssload-c4 {
	transform: scale(1.1) rotateZ(270deg);
		-o-transform: scale(1.1) rotateZ(270deg);
		-ms-transform: scale(1.1) rotateZ(270deg);
		-webkit-transform: scale(1.1) rotateZ(270deg);
		-moz-transform: scale(1.1) rotateZ(270deg);
}
.cssload-thecube .cssload-c2:before {
	animation-delay: 0.35s;
		-o-animation-delay: 0.35s;
		-ms-animation-delay: 0.35s;
		-webkit-animation-delay: 0.35s;
		-moz-animation-delay: 0.35s;
}
.cssload-thecube .cssload-c3:before {
	animation-delay: 0.69s;
		-o-animation-delay: 0.69s;
		-ms-animation-delay: 0.69s;
		-webkit-animation-delay: 0.69s;
		-moz-animation-delay: 0.69s;
}
.cssload-thecube .cssload-c4:before {
	animation-delay: 1.04s;
		-o-animation-delay: 1.04s;
		-ms-animation-delay: 1.04s;
		-webkit-animation-delay: 1.04s;
		-moz-animation-delay: 1.04s;
}



@keyframes cssload-fold-thecube {
	0%, 10% {
		transform: perspective(136px) rotateX(-180deg);
		opacity: 0;
	}
	25%,
				75% {
		transform: perspective(136px) rotateX(0deg);
		opacity: 1;
	}
	90%,
				100% {
		transform: perspective(136px) rotateY(180deg);
		opacity: 0;
	}
}

@-o-keyframes cssload-fold-thecube {
	0%, 10% {
		-o-transform: perspective(136px) rotateX(-180deg);
		opacity: 0;
	}
	25%,
				75% {
		-o-transform: perspective(136px) rotateX(0deg);
		opacity: 1;
	}
	90%,
				100% {
		-o-transform: perspective(136px) rotateY(180deg);
		opacity: 0;
	}
}

@-ms-keyframes cssload-fold-thecube {
	0%, 10% {
		-ms-transform: perspective(136px) rotateX(-180deg);
		opacity: 0;
	}
	25%,
				75% {
		-ms-transform: perspective(136px) rotateX(0deg);
		opacity: 1;
	}
	90%,
				100% {
		-ms-transform: perspective(136px) rotateY(180deg);
		opacity: 0;
	}
}

@-webkit-keyframes cssload-fold-thecube {
	0%, 10% {
		-webkit-transform: perspective(136px) rotateX(-180deg);
		opacity: 0;
	}
	25%,
				75% {
		-webkit-transform: perspective(136px) rotateX(0deg);
		opacity: 1;
	}
	90%,
				100% {
		-webkit-transform: perspective(136px) rotateY(180deg);
		opacity: 0;
	}
}

@-moz-keyframes cssload-fold-thecube {
	0%, 10% {
		-moz-transform: perspective(136px) rotateX(-180deg);
		opacity: 0;
	}
	25%,
				75% {
		-moz-transform: perspective(136px) rotateX(0deg);
		opacity: 1;
	}
	90%,
				100% {
		-moz-transform: perspective(136px) rotateY(180deg);
		opacity: 0;
	}
}
</style>
    <h1>Upload pannelli dismessi</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdSalva"> <span class="ui-icon ui-icon-document toolbar-icon"></span><span class="desc">Carica</span> </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla"> <span class="ui-icon toolbar-icon"></span><span class="desc">Annulla</span> </asp:LinkButton>
            </li>
        </ul>       
        <div class="clear"></div>
        
    </div>

    <div class="form form-horizontal">

        <div class="control-group">            
            <label class="control-label">Download Template:</label>
                <div class="controls" style="margin-top:7px">
                    <asp:HyperLink runat="server" ID="hlkTemplate" Text="Template_Excel" NavigateUrl="~/Template/TemplateDisabbinati.xlsx"></asp:HyperLink>
                </div>
        </div>
   
        <div class="control-group" runat="server" id="divProduttore">            
            <label class="control-label">Produttore:</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProduttore" CssClass="chzn-select medium-select select" AppendDataBoundItems="true" DataSourceID="SqlDataSource2" DataTextField="RagioneSociale" DataValueField="Id">
                    <asp:ListItem Text="Selezionare .." Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="SELECT [Id], [RagioneSociale] FROM [tbl_Produttori] ORDER BY [RagioneSociale]"></asp:SqlDataSource>
            </div>
        </div>


        <div class="control-group">
            <label class="control-label">File da caricare:</label>
            <div class="controls">
                <div class="uploader black">
                    <asp:TextBox runat="server" ReadOnly="True" ID="fileNameTextBox" CssClass="filename"></asp:TextBox>
                    <asp:Button runat="server" readonly="True" ID="fileNameButton" CssClass="button_files" Text="Sfoglia..."></asp:Button>
                    <asp:FileUpload runat="server" ID="fileUpload" />
                </div>
            </div>
        </div>

        <div class="control-group" id="divAnno" runat="server" visible="false">
            <label class="control-label">Anno protocollo:</label>
            <div class="controls">
                <div class="uploader black">
                    <asp:TextBox runat="server" ID="txtAnno" CssClass="filename"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <h2>Record importati</h2>
     <div class="toolbar">
        <ul>
            
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdImporta" OnClientClick="showDiv();"> <span class="ui-icon ui-icon-check toolbar-icon"></span><span class="desc">Importa</span> </asp:LinkButton>
            </li>     
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEsporta"> <span class="ui-icon ui-ui-icon-arrowrefresh-1-n  toolbar-icon"></span><span class="desc">Esporta</span> </asp:LinkButton>
            </li>
             <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdEliminaTutti"> <span class="ui-icon ui-icon-trash toolbar-icon"></span><span class="desc">Elimina tutti</span> </asp:LinkButton>
            </li>
            
        </ul>
    </div>
    <div class="clear"></div>
    <asp:ListView ID="ListaErrori" runat="server" DataSourceID="SqlDatasource1" DataKeyNames="Id">
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server">
                <table class="display">
                    <thead>
                        <tr>
                            <th class="info-th"><asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Matricola" runat="server" ><asp:Label runat="server" ID="lbl01" Text="Matricola" ></asp:Label></asp:LinkButton></th>
                            <th class="title-th"><asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="Errore" runat="server" ><asp:Label runat="server" ID="Label2" Text="Errore" ></asp:Label></asp:LinkButton></th>
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
                    <asp:DataPager ID="testInfoDataPager" runat="server" PageSize="2" PagedControlID="ListaErrori">
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
                    <asp:DataPager ID="testDataPager" runat="server" PageSize="10" PagedControlID="ListaErrori">
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
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Errore")%>' />
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
                    <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("Errore")%>' />
                </td>               
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <asp:SqlDataSource runat="server" ID="SqlDatasource1" ConnectionString="<%$ ConnectionStrings:aspnet_staterKits_TimeTracker %>" SelectCommand="sp_Errore_Lista" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="IdCaricamento" SessionField="IdCaricamento" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <div id='myHiddenDiv' style="display: none" class="modal">
        <div class="modal-content">
            <div style="text-align:center">
            <asp:Label runat="server" ID="lbl" Text="Attendere il completamento dell'operazione" 
                Font-Size="Medium" ForeColor="GrayText"></asp:Label>   
            </div>
            <div class="cssload-thecube">
	            <div class="cssload-cube cssload-c1"></div>
	            <div class="cssload-cube cssload-c2"></div>
	            <div class="cssload-cube cssload-c4"></div>
	            <div class="cssload-cube cssload-c3"></div>
            </div>
        </div>
    </div>

 <script type="text/javascript">
     function showDiv()
     {
        document.getElementById('myHiddenDiv').style.display =""; 
        setTimeout('document.images["myAnimatedImage"].src="work.gif"', 200); 
    } 

</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

