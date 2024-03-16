<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UploadManuale.aspx.vb" Inherits="WebApp_Amministrazione_UploadManuale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Upload manuali</h1>

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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

