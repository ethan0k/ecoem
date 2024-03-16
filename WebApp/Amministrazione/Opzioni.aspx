<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Opzioni.aspx.vb" Inherits="WebApp_Amministrazione_Opzioni" ValidateRequest="false" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="speedBarPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">

    <h1>Opzioni</h1>

    <div class="toolbar">
        <ul>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdSalva">
                    <span class="ui-icon ui-icon-document toolbar-icon"></span><span class="desc">Salva</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdAnnulla" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Annulla</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdTest" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Test invio mail dich.</span>
                </asp:LinkButton>
            </li>
            <li class="button-profile-2">
                <asp:LinkButton runat="server" ID="cmdTestAutocertificazione" CausesValidation="false">
                    <span class="ui-icon 
                         toolbar-icon"></span><span class="desc">Test invio mail Autocert.</span>
                </asp:LinkButton>
            </li>
        </ul>
        <div class="clear"></div>
    </div>

    <div class="alert red hideit" id="divError" runat="server" visible="false">
        <div class="left">
            <span class="red-icon"></span>
            <span class="alert-text"><asp:Literal runat="server" ID="lblError" Text="Errore: Oops mi spiace." ></asp:Literal></span>
        </div>
    </div>  

    <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Server SMTP:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtSmtp" runat="server" />                    
                </div>
        </div>
       <div class="control-group">            
         <label class="control-label">Porta:</label>
             <div class="controls">
                 <asp:TextBox CssClass="medium v-name" ID="txtPorta" runat="server" />                    
             </div>
      </div>
      <div class="control-group">            
        <label class="control-label">SSL:</label>
           <div class="controls">
           <asp:CheckBox runat="server" ID="chkUsaSSL" />    
        </div>    
</div>
    </div>


     <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Nome utente:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtNomeUtente" runat="server" />                    
                </div>
        </div>
    </div>

    <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Password:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtPassword" runat="server" />                    
                </div>
        </div>
        <div class="control-group">            
        <label class="control-label">Destinatario test:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtDestinatarioTest" runat="server" />                    
        </div>
</div>
    </div>

    <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Indirizzo email mittente:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtMittente" type="Email" runat="server" />                    
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Oggetto mail verifica dichiarazione:</label>
                <div class="controls">
                    <asp:TextBox CssClass="medium v-name" ID="txtOggetto" runat="server" />                    
                </div>
        </div>
    </div>

    <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Testo mail verifica dichiarazione:</label>
                <div class="controls">
                    <FTB:FreeTextBox ID="freeTestoMail" Width="100%" TabMode="NextControl" AutoGenerateToolbarsFromString="True" BreakMode="LineBreak"  RemoveServerNameFromUrls = "False"  
                   ToolbarLayout="bold,italic,underline, bulletedlist,numberedlist, FontFacesMenu, FontSizesMenu, FontForeColorsMenu,InsertImageFromGallery| 
                    CreateLink, RemoveFormat, JustifyLeft, JustifyRight, JustifyCenter, JustifyFull, Undo, Redo| InsertTextBox, InsertTextArea, InsertDiv, InsertImageFromGallery, Preview" 
                   imageGalleryUrl="ftb_imagegallery.aspx?rif=/imagespath/&cif=~/images/" ImageGalleryPath="~/images/" 
                   BackColor="White" Focus="True" EnableHtmlMode="False" HtmlModeCss="input" AllowHtmlMode="False" runat="server" AssemblyResourceHandlerPath="" AutoConfigure="" AutoHideToolbar="True" AutoParseStyles="True" BaseUrl="" ButtonDownImage="False" ButtonFileExtention="gif" ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource" ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21" ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False" DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False" DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" DownLevelRows="10" EditorBorderColorDark="128, 128, 128" EditorBorderColorLight="128, 128, 128" EnableSsl="False" EnableToolbars="True" FormatHtmlTagsToXhtml="True" GutterBackColor="129, 169, 226" GutterBorderColorDark="128, 128, 128" GutterBorderColorLight="255, 255, 255" Height="350px" HelperFilesParameters="" HelperFilesPath="" HtmlModeDefaultsToMonoSpaceFont="True" InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource" Language="en-US" PasteMode="Default" ReadOnly="False" RemoveScriptNameFromBookmarks="True" RenderMode="NotSet" ScriptMode="External" ShowTagPath="False" SslUrl="/." StartMode="DesignMode" StripAllScripting="False" SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" Text="" TextDirection="LeftToRight" ToolbarBackColor="Transparent" ToolbarBackgroundImage="True" ToolbarImagesLocation="InternalResource" ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" UseToolbarBackGroundImage="True"></FTB:FreeTextBox>
                </div>
        </div>

    </div>
    <div class="form form-horizontal">
        <div class="control-group">            
            <label class="control-label">Oggetto mail verifica autocertificazione:</label>
            <div class="controls">
                <asp:TextBox CssClass="medium v-name" ID="txtOggettoAutocertificazione" runat="server" />                    
            </div>
        </div>

        <div class="control-group">            
            <label class="control-label">Testo mail verifica autocertificazione:</label>
            <div class="controls">
                <FTB:FreeTextBox ID="freeTestoMailAutocertificazione" Width="100%" TabMode="NextControl" AutoGenerateToolbarsFromString="True" BreakMode="LineBreak"  RemoveServerNameFromUrls = "False"  
               ToolbarLayout="bold,italic,underline, bulletedlist,numberedlist, FontFacesMenu, FontSizesMenu, FontForeColorsMenu,InsertImageFromGallery| 
                CreateLink, RemoveFormat, JustifyLeft, JustifyRight, JustifyCenter, JustifyFull, Undo, Redo| InsertTextBox, InsertTextArea, InsertDiv, InsertImageFromGallery, Preview" 
               imageGalleryUrl="ftb_imagegallery.aspx?rif=/imagespath/&cif=~/images/" ImageGalleryPath="~/images/" 
               BackColor="White" Focus="True" EnableHtmlMode="False" HtmlModeCss="input" AllowHtmlMode="False" runat="server" AssemblyResourceHandlerPath="" AutoConfigure="" AutoHideToolbar="True" AutoParseStyles="True" BaseUrl="" ButtonDownImage="False" ButtonFileExtention="gif" ButtonFolder="Images" ButtonHeight="20" ButtonImagesLocation="InternalResource" ButtonOverImage="False" ButtonPath="" ButtonSet="Office2003" ButtonWidth="21" ClientSideTextChanged="" ConvertHtmlSymbolsToHtmlCodes="False" DesignModeBodyTagCssClass="" DesignModeCss="" DisableIEBackButton="False" DownLevelCols="50" DownLevelMessage="" DownLevelMode="TextArea" DownLevelRows="10" EditorBorderColorDark="128, 128, 128" EditorBorderColorLight="128, 128, 128" EnableSsl="False" EnableToolbars="True" FormatHtmlTagsToXhtml="True" GutterBackColor="129, 169, 226" GutterBorderColorDark="128, 128, 128" GutterBorderColorLight="255, 255, 255" Height="350px" HelperFilesParameters="" HelperFilesPath="" HtmlModeDefaultsToMonoSpaceFont="True" InstallationErrorMessage="InlineMessage" JavaScriptLocation="InternalResource" Language="en-US" PasteMode="Default" ReadOnly="False" RemoveScriptNameFromBookmarks="True" RenderMode="NotSet" ScriptMode="External" ShowTagPath="False" SslUrl="/." StartMode="DesignMode" StripAllScripting="False" SupportFolder="/aspnet_client/FreeTextBox/" TabIndex="-1" Text="" TextDirection="LeftToRight" ToolbarBackColor="Transparent" ToolbarBackgroundImage="True" ToolbarImagesLocation="InternalResource" ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" UseToolbarBackGroundImage="True"></FTB:FreeTextBox>
            </div>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="submenuLevel3PlaceHolder" Runat="Server">
</asp:Content>

