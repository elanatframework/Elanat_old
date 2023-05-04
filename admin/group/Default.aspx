﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="elanat.AdminGroup" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="<%=elanat.AspxHtmlValue.CurrentAdminLanguageDirection()%>">
<head>
    <title><%=model.GroupLanguage%></title>
    <script src="<%=elanat.AspxHtmlValue.AdminPath()%>/group/script/group.js"></script>
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
        <%=model.GroupLanguage%>
    </div>

    <div class="el_button_box">
        <input id="btn_AddItem" type="button" value="<%=model.AddLanguage%>" class="el_add" el_action="add_group" onclick="el_ShowHideSection(this, 'div_AddGroup'); el_SetIframeAutoHeight(); el_SaveAction(this, event)" />
        <input id="btn_Refresh" type="button" value="<%=model.RefreshLanguage%>" onclick="el_Refresh()" />
    </div>

    <div id="div_AddGroup" class="el_hidden">

        <form id="frm_AdminGroup" method="post" action="<%=elanat.AspxHtmlValue.AdminPath()%>/group/Default.aspx">

            <div class="el_part_row">
                <div id="div_AddGroupTitle" class="el_title" onclick="el_HidePart(this); el_SetIframeAutoHeight()">
                    <%=model.AddGroupLanguage%>
                    <div class="el_dash"></div>
                </div>
                <div class="el_item">
                    <%=model.GroupNameLanguage%>
                </div>
                <div class="el_item">
                    <input id="txt_GroupName" name="txt_GroupName" type="text" value="<%=model.GroupNameValue%>" class="el_text_input<%=model.GroupNameCssClass%>" <%=model.GroupNameAttribute%> />
                </div>
                <div class="el_item">
                    <span class="el_checkbox_input">
                        <input id="cbx_GroupActive" name="cbx_GroupActive" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.GroupActiveValue)%> /><label for="cbx_GroupActive"><%=model.GroupActiveLanguage%></label></span>
                </div>
                <div class="el_item">
                    <%=model.GroupRoleLanguage%>
                </div>
                <div class="el_item">
                    <%=model.GroupRoleTemplateValue%>
                </div>
                <div class="el_item">
                    <input id="btn_AddGroup" name="btn_AddGroup" type="submit" class="el_button_input" value="<%=model.AddGroupLanguage%>" onclick="el_AjaxPostBack(this, true, 'frm_AdminGroup')" />
                </div>
            </div>

        </form>

    </div>

    <div class="el_part_row">
        <div id="div_TableBox" class="el_item">
            <%=model.ContentValue%>
        </div>
    </div>

</body>
</html>
