<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ftb_imagegallery.aspx.vb" Inherits="WebApp_Amministrazione_ftb_imagegallery" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <FTB:ImageGallery runat="server" ID="ImageGallery1" AllowImageDelete="false" AllowImageUpload="true" AllowDirectoryCreate="false" AllowDirectoryDelete="false"></FTB:ImageGallery>

    </div>
    </form>
</body>
</html>
