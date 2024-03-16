<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Registrazione.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <title>Ecoem | Login</title>
    <meta name="description" content=""/>
    <link rel="shortcut icon" href="images/favicon.ico" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1" />
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Cuprum" />
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Bree+Serif" />
    <link rel="stylesheet" href="css/jquery-ui-1.10.3.custom.css" media="screen" />
    <link rel="stylesheet" href="css/validationEngine.jquery.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/bootstrap-responsive.css" />
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/tables.css" />
    <link rel="stylesheet" href="css/chosen.css" />
    <link rel="stylesheet" href="css/tipTip.css" />
    <link rel="stylesheet" href="css/jquery.jscrollpane.css" />
</head>
<body>
    <form id="aspnetForm" runat="server" class="customForm">

        <asp:ScriptManager ID="mainScriptManager" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/lib/jquery-1.9.1.min.js" />
                <asp:ScriptReference Path="~/lib/jquery-ui-1.10.3.custom.min.js" />
                <asp:ScriptReference Path="~/lib/bootstrap.js" />
                <asp:ScriptReference Path="~/lib/jquery.tipTip.minified.js" />
                <asp:ScriptReference Path="~/lib/forms.js" />
                <asp:ScriptReference Path="~/lib/chosen.jquery.js" />
                <asp:ScriptReference Path="~/lib/jquery.jscrollpane.min.js" />
                <asp:ScriptReference Path="~/lib/jquery.tzCheckbox.js" />
                <asp:ScriptReference Path="~/lib/jquery.alerts.js" />
                <asp:ScriptReference Path="~/lib/functions.js" />
                
            </Scripts>
        </asp:ScriptManager>

        <!--[if lt IE 9]>
	    <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	    <![endif]-->
        <!--[if lte IE 8]><script type="text/javascript" src="lib/excanvas.min.js"></script><![endif]-->
        <script>
            jQuery(document).ready(function () {

            });
        </script>

        <div id="container-main">
		    <div id="container">

                <!--Header-->
		        <div class="header"> 
                    <!--User Profile-->
                    <div class="header">
                        <!--User Profile-->
                            <div class="profile">
                                
                                    <h1 style="color:white">Consorzio Ecoem</h1> 
                                
                            </div>
                        <!--User Profile END-->
                    <div class="search">                     
                        
                    </div>
                    <div class="buttons-head">
                        <div class="button-profile-2">
                                
                        </div>
                    </div>

                </div>
                </div>
		        <!--Header END-->
        
                 <!--SpeedBar-->
                <div class="speedbar">
                    <div class="bar-online"><strong>Server date:</strong> <%= Now.ToString("dd/mm/yyyy")%> </div>
                </div>
                <div class="speedbar-shadow"></div>
                <!--SpeedBar END-->

                <div id="content">
                    <div class="content">
                        <h1 style="color:black">Form di registrazione</h1>

                            <div class="toolbar">
                                <ul>
                                    <li class="button-profile-2">
                                        <asp:LinkButton runat="server" ID="cmdInvia" CausesValidation="true"> <span class="ui-icon ui-icon-document toolbar-icon"></span><span class="desc">Invia</span> </asp:LinkButton>
                                    </li>
                                    <li class="button-profile-2">
                                        <asp:LinkButton runat="server" ID="cmdAnnulla" CausesValidation="false"> <span class="ui-icon ui-icon-cancel toolbar-icon"></span><span class="desc">Annulla</span> </asp:LinkButton>
                                    </li>
                                </ul>
                                <div class="clear"></div>
                            </div>

                            <div class="form form-horizontal">
                                <div class="control-group">            
                                    <label class="control-label">Cognome referente:</label>
                                        <div class="controls">
                                            <asp:TextBox CssClass="medium v-name" ID="txtCognome" runat="server" />
                                            <asp:RequiredFieldValidator runat="server" ID="validator" ControlToValidate="txtCognome" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                        </div>
                                </div>
                                <div class="control-group">            
                                    <label class="control-label">Nome referente:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtNome" runat="server" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtNome" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="control-group">            
                                    <label class="control-label">Azienda:</label>
                                    <div class="controls">
                                        <asp:CheckBox runat="server" ID="chkAzienda"  CssClass="styled" Enabled="false" />
                                    </div>
                                </div>
                                 <div class="control-group">            
                                    <label class="control-label">Indirizzo:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtIndirizzo" runat="server" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtIndirizzo" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="control-group">            
                                    <label class="control-label">Cap:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtCap" runat="server" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtCap" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="control-group">            
                                    <label class="control-label">Comune:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtComune" runat="server" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtComune" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="control-group">            
                                    <label class="control-label">Indirizzo email:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtEmail" runat="server" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEMail" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="control-group">            
                                    <label class="control-label">Telefono:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtTelefono" runat="server" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtTelefono" CssClass="error" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="control-group">            
                                    <label class="control-label">Partita IVA:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtPartitaIva" runat="server" />
                                    </div>
                                </div>
                                <div class="control-group">            
                                    <label class="control-label">Codice fiscale:</label>
                                    <div class="controls">
                                        <asp:TextBox CssClass="medium v-name" ID="txtCodiceFiscale" runat="server" />
                                    </div>
                                </div>
                            </div>
                        <div class="clear"></div>
                    </div>
                    <!--class .content end-->

                </div>
            </div>
              <!--sideLeft-->
            <aside id="sideLeft">
            </aside>
        </div>

            <script>
                ValidatorUpdateIsValid = function () {
                    Page_IsValid = AllValidatorsValid(Page_Validators);
                    SetValidatorStyles();
                }

                SetValidatorStyles = function () {
                    var i;
                    // clear all
                    for (i = 0; i < Page_Validators.length; i++) {
                        var inputControl = document.getElementById(Page_Validators[i].controltovalidate);
                        if (null != inputControl) {
                            WebForm_RemoveClassName(inputControl, 'error');
                        }
                    }
                    // set invalid
                    for (i = 0; i < Page_Validators.length; i++) {
                        inputControl = document.getElementById(Page_Validators[i].controltovalidate);
                        if (null != inputControl && !Page_Validators[i].isvalid) {
                            WebForm_AppendToClassName(inputControl, 'error');
                        }
                    }
                }
    </script>
    </form>
</body>
</html>
