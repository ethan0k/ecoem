<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Manualistica.aspx.vb" Inherits="WebApp_Dashboard_Manualistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

        <h1>Manuali</h1>

        <div class="clear"></div>

        <asp:ListView runat="server" ID="ListView1" >
            <LayoutTemplate>
                <div>
                    <div runat="server" id="itemPlaceHolder">
                    </div>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank"  ImageUrl="~/images/docpdf.png" NavigateUrl='<%# Eval("FilePath")%>'><asp:Label runat="server" ID="lbl1" Text='<%# Eval("FileName")%>'></asp:Label></asp:HyperLink>                
                <asp:Label runat="server" ID="Label1" Text='<%# Eval("FileName")%>'></asp:Label>
            </ItemTemplate>
        </asp:ListView>
    

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

