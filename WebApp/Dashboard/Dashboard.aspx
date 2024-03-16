<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="WebApp_Dashboard_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <!--Statistics in figures - 2 -->
        <ul class="stats_figures_2">
            <li runat="server" id="lblTotalePannelli"><span class="string1"><asp:Literal runat="server" ID="totPannelli" Text="12,5"></asp:Literal></span><span class="string2">Pannelli totali</span><span class="string3">caricati in archivio</span></li>
            <li runat="server" id="lblTotaleAbbinati"><span class="string1"><asp:Literal runat="server" ID="totPannelliAbbinati" Text="12,5"></asp:Literal></span><span class="string2">Pannelli abbinati</span><span class="string3">ad impianti</span></li>
            <li runat="server" id="lblTotaleConformi"><span class="string1"><asp:Literal runat="server" ID="totPannelliConformi" Text="12,5"></asp:Literal></span><span class="string2">Pannelli </span><span class="string3">conformi</span></li>
            <li runat="server" id="lblTotaleImpianti"><span id="pink" class="string1 pink"><asp:Literal runat="server" ID="totImpianti" Text="512"></asp:Literal></span><span class="string2">impianti</span><span class="string3">registrati</span></li>
            <li runat="server" id="string_last"><span id="golden" class="string1"><asp:Literal runat="server" ID="totProduttori" Text="0"></asp:Literal></span><span class="string2">Produttori</span><span class="string3">registrati</span></li>
            <li runat="server" id="lblTotaleDismessi"><span class="string1"><asp:Literal runat="server" ID="totPannelliDismessi" Text="0"></asp:Literal></span><span class="string2">Pannelli </span><span class="string3">dismessi</span></li>
            <li runat="server" id="lblTotR1"><span style="color:blueviolet" class="string1"><asp:Literal runat="server" ID="totR1" Text="0"></asp:Literal></span><span class="string2">Totale Kg R1 </span><span class="string3"></span></li>
            <li runat="server" id="lblTotR2"><span style="color:blueviolet" class="string1"><asp:Literal runat="server" ID="totR2" Text="0"></asp:Literal></span><span class="string2">Totale Kg R2 </span><span class="string3"></span></li>
            <li runat="server" id="lblTotR3"><span style="color:blueviolet" class="string1"><asp:Literal runat="server" ID="totR3" Text="0"></asp:Literal></span><span class="string2">Totale Kg R3 </span><span class="string3"></span></li>
            <li runat="server" id="lblTotR4"><span style="color:blueviolet" class="string1"><asp:Literal runat="server" ID="totR4" Text="0"></asp:Literal></span><span class="string2">Totale Kg R4 </span><span class="string3"></span></li>
            <li runat="server" id="lblTotR5"><span style="color:blueviolet" class="string1"><asp:Literal runat="server" ID="totR5" Text="0"></asp:Literal></span><span class="string2">Totale Kg R5</span><span class="string3"></span></li>
            <li runat="server" id="lblTotBatterie"><span style="color:lightcoral" class="string1"><asp:Literal runat="server" ID="totBatterie" Text="0"></asp:Literal></span><span class="string2">Totale Kg batterie </span><span class="string3"></span></li>
        </ul>
     <!--Statistics in figures - 2 END-->
    <div>
  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

