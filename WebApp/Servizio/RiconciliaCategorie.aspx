<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RiconciliaCategorie.aspx.vb" Inherits="WebApp_Servizio_RiconciliaCategorie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="cmdRiconcilia" runat="server" Text="Riconcilia categorie" />
        <br />
        <br />
        <asp:Label ID="lblEsito" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
