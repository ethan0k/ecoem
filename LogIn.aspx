<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LogIn.aspx.vb" Inherits="LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Ecoem | Login</title>
    <meta name="description" content="" />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />

    <link rel="stylesheet" href="css/validationEngine.jquery.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/login.css" type="text/css" media="screen" />
</head>
<body>
    <form id="aspnetForm" runat="server" autocomplete="off" aria-autocomplete="none">
        <div id="container">
            <div class="main">
                <div class="main_in_main">
                    <div class="content">
                        <h1>
                            <asp:HyperLink ID="logoHyperLink" runat="server" CssClass="logo" NavigateUrl="~/" Text="White dream"></asp:HyperLink>
                        </h1>

                        <div id="form-login">
                            <asp:Login ID="adminLogin" runat="server">
                                <LayoutTemplate>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="validate[required] text-input" placeholder="username" ValidationGroup="adminLogin"></asp:TextBox>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="validate[required] text-input pass" placeholder="password" ValidationGroup="adminLogin"></asp:TextBox>
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Login" CssClass="button-login" ValidationGroup="adminLogin" />
                                    <asp:Label ID="FailureText" runat="server" EnableViewState="False" CssClass="error"></asp:Label>
                                </LayoutTemplate>
                            </asp:Login>
                        </div>
                    </div>
                </div>
                <div style="text-align: center">
                    <asp:LinkButton runat="server" ID="NewUserLink" Text="Registrati" ForeColor="white" PostBackUrl="Registrazione.aspx" Visible="False"></asp:LinkButton>
                </div>
            </div>

        </div>

        <asp:ScriptManager ID="mainScriptManager" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="https://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js" />
                <asp:ScriptReference Path="~/lib/jquery.validationEngine.js" />
                <asp:ScriptReference Path="~/lib/jquery.validationEngine-en.js" />
            </Scripts>
        </asp:ScriptManager>

        <!--[if lt IE 9]>
	    <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	    <![endif]-->

        <script>
            jQuery(document).ready(function () {
                // binds form submission and fields to the validation engine
                jQuery("#aspnetForm").validationEngine();
            });
        </script>
    </form>
</body>
</html>
