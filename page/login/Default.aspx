﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="elanat.SiteLogin" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="<%=elanat.AspxHtmlValue.CurrentSiteLanguageDirection()%>">
<head>
    <title><%=model.LoginLanguage%></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="<%=elanat.AspxHtmlValue.SitePath()%>page/login/favicon.ico" />
    <!-- Start Client Variant -->
    <script type="text/javascript" src="<%=elanat.AspxHtmlValue.SitePath()%>action/text_creator/site_client_variant/"></script>
    <script type="text/javascript" src="<%=elanat.AspxHtmlValue.SitePath()%>action/text_creator/site_client_language_variant"></script>
    <!-- End Client Variant -->	
    <script src="<%=elanat.AspxHtmlValue.SitePath()%>client/script/global.js"></script>
	<script type="text/javascript" src="<%=elanat.AspxHtmlValue.SitePath()%>client/script/site/site.js"></script>
    <%=elanat.AspxHtmlValue.CurrentSiteStyleTag()%>
    <link rel="stylesheet" type="text/css" href="<%=elanat.AspxHtmlValue.SitePath()%>client/style/global.css" />
    <link rel="stylesheet" type="text/css" href="<%=elanat.AspxHtmlValue.SitePath()%>client/style/site_global.css" />
</head>
<body onload="el_PartPageLoad(); el_LoadCaptcha();">

    <div class="el_page_center">
        <div class="el_head">
            <%=model.LoginLanguage%>
        </div>

        <form id="frm_SiteLogin" method="post" action="<%=elanat.AspxHtmlValue.SitePath()%>page/login/Default.aspx" defaultbutton="btn_Login">

            <div class="el_part_row">
                <div id="div_LoginTitle" class="el_title" onclick="el_HidePart(this); el_SetIframeAutoHeight()">
                    <%=model.LoginToSiteLanguage%>
                    <div class="el_dash"></div>
                </div>
                <div class="el_item">
                    <%=model.UserNameOrUserEmailLanguage%>
                </div>
                <div class="el_item">
                    <input id="txt_UserNameOrUserEmail" name="txt_UserNameOrUserEmail" type="text" value="<%=model.UserNameOrUserEmailValue%>" class="el_text_input el_important_field" />
                </div>

                <div class="el_item">
                    <%=model.PasswordLanguage%>
                </div>
                <div class="el_item">
                    <input id="txt_Password" name="txt_Password" type="password" class="el_text_input el_important_field" />
                </div>

                <div id="pnl_SecretKey">
                    <div class="el_item">
                        <%=model.SecretKeyLanguage%>
                    </div>
                    <div class="el_item">
                        <input id="txt_SecretKey" name="txt_SecretKey" type="password" autocomplete="off" readonly="true" onfocus="this.removeAttribute('readonly');" class="el_text_input<%=model.SecretKeyCssClass%>" />
                    </div>
                </div>

                <div class="el_item">
                    <div class="el_captcha_value"></div>
                </div>

                <div class="el_item">
                    <%=model.LanguageLanguage%>
                </div>
                <div class="el_item">
                    <select id="ddlst_Language" name="ddlst_Language" class="el_alone_select_input">
                        <%=model.LanguageOptionListValue %>
                    </select>
                    <input id="btn_Login" name="btn_Login" type="submit" class="el_button_input" value="<%=model.LoginLanguage%>" onclick="el_AjaxPostBack(this, true, 'frm_SiteLogin')" />
                </div>
            </div>
                
            <div class="el_part_row">
                <div class="el_item">
                    <a href="<%=elanat.AspxHtmlValue.SitePath()%>page_content/forget_password/">
                        <%=model.ForgetPasswordLanguage%>
                    </a>
                </div>
                <div class="el_item">
                    <a href="<%=elanat.AspxHtmlValue.SitePath()%>page_content/confirm_email/">
                        <%=model.ConfirmEmailLanguage%>
                    </a>
                </div>
                <div class="el_item">
                    <a href="<%=elanat.AspxHtmlValue.SitePath()%>page_content/sign_up/">
                        <%=model.SignUpLanguage%>
                    </a>
                </div>
            </div>

            <input id="hdn_ReturnUrl" name="hdn_ReturnUrl" type="hidden" value="<%=model.ReturnUrlValue%>" />

        </form>

    </div>

</body>
</html>