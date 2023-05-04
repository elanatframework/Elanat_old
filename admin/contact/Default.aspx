﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="elanat.AdminContact" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="<%=elanat.AspxHtmlValue.CurrentAdminLanguageDirection()%>">
<head>
    <title><%=model.ContactLanguage%></title>
    <script src="<%=elanat.AspxHtmlValue.AdminPath()%>/contact/script/contact.js"></script>
    <!-- Start Client Variant -->
    <script type="text/javascript" src="<%=elanat.AspxHtmlValue.SitePath()%>action/text_creator/admin_client_variant"></script>
    <script type="text/javascript" src="<%=elanat.AspxHtmlValue.SitePath()%>action/text_creator/admin_client_language_variant"></script>
    <!-- End Client Variant -->	
    <script src="<%=elanat.AspxHtmlValue.SitePath()%>client/script/global.js"></script>
    <script type="text/javascript" src="<%=elanat.AspxHtmlValue.SitePath()%>client/script/admin/admin.js" ></script>
    <script type="text/javascript" src="<%=elanat.AspxHtmlValue.SitePath()%>client/script/page_load/admin/page_load.js" ></script>
    <%=elanat.AspxHtmlValue.CurrentAdminStyleTag()%>
    <link rel="stylesheet" type="text/css" href="<%=elanat.AspxHtmlValue.SitePath()%>client/style/global.css" />
    <link rel="stylesheet" type="text/css" href="<%=elanat.AspxHtmlValue.SitePath()%>client/style/admin_global.css" />
    <%=elanat.AspxHtmlValue.CurrentBoxTag()%>
</head>
<body onload="el_RunAction(); el_PartPageLoad();">

    <div class="el_head">
        <%=model.ContactLanguage%>
    </div>
    <div class="el_part_row">
        <div id="div_ContactTitle" class="el_title" onclick="el_HidePart(this); el_SetIframeAutoHeight()">
            <%=model.ContactLanguage%>
            <div class="el_dash"></div>
        </div>
        <div class="el_item">
            <div id="div_TableBox" class="el_item">
                <%=model.ContentValue%>
            </div>
        </div>
    </div>

</body>
</html>
