﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeDirectoryPermissions.aspx.cs" Inherits="elanat.ActionChangeDirectoryPermissions" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" dir="<%=elanat.AspxHtmlValue.CurrentAdminLanguageDirection()%>">
<head>
    <title><%=model.ChangeDirectoryPermissionsLanguage%></title>
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
<body onload="el_PartPageLoad();">
    <form id="frm_ActionChangeDirectoryPermissions" method="post" action="<%=elanat.AspxHtmlValue.AdminPath()%>/file_manager/action/change_directory_permissions/ChangeDirectoryPermissions.aspx">

        <div class="el_part_row">
            <div id="div_EditDirectoryPermissionsTitle" class="el_title" onclick="el_HidePart(this); el_SetIframeAutoHeight()">
                <%=model.EditDirectoryPermissionsLanguage%>
                <div class="el_dash"></div>
            </div>
            <div class="el_item">
                <%=model.DirectoryNameLanguage%>
            </div>
            <div class="el_item">
                <%=model.DirectoryNameValue%>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_FullControl" name="cbx_FullControl" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.FullControlValue)%> /><label for="cbx_FullControl"><%=model.FullControlLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_Read" name="cbx_Read" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.ReadValue)%> /><label for="cbx_Read"><%=model.ReadLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_Write" name="cbx_Write" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.WriteValue)%> /><label for="cbx_Write"><%=model.WriteLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_CreateDirectories" name="cbx_CreateDirectories" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.CreateDirectoriesValue)%> /><label for="cbx_CreateDirectories"><%=model.CreateDirectoriesLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_CreateFiles" name="cbx_CreateFiles" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.CreateFilesValue)%> /><label for="cbx_CreateFiles"><%=model.CreateFilesLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_ChangePermissions" name="cbx_ChangePermissions" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.ChangePermissionsValue)%> /><label for="cbx_ChangePermissions"><%=model.ChangePermissionsLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_Delete" name="cbx_Delete" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.DeleteValue)%> /><label for="cbx_Delete"><%=model.DeleteLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_DeleteSubdirectoriesAndFiles" name="cbx_DeleteSubdirectoriesAndFiles" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.DeleteSubdirectoriesAndFilesValue)%> /><label for="cbx_DeleteSubdirectoriesAndFiles"><%=model.DeleteSubdirectoriesAndFilesLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_ListDirectory" name="cbx_ListDirectory" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.ListDirectoryValue)%> /><label for="cbx_ListDirectory"><%=model.ListDirectoryLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_Modify" name="cbx_Modify" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.ModifyValue)%> /><label for="cbx_Modify"><%=model.ModifyLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_ReadAttributes" name="cbx_ReadAttributes" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.ReadAttributesValue)%> /><label for="cbx_ReadAttributes"><%=model.ReadAttributesLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_ReadExtendedAttributes" name="cbx_ReadExtendedAttributes" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.ReadExtendedAttributesValue)%> /><label for="cbx_ReadExtendedAttributes"><%=model.ReadExtendedAttributesLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_ReadPermissions" name="cbx_ReadPermissions" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.ReadPermissionsValue)%> /><label for="cbx_ReadPermissions"><%=model.ReadPermissionsLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_TakeOwnership" name="cbx_TakeOwnership" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.TakeOwnershipValue)%> /><label for="cbx_TakeOwnership"><%=model.TakeOwnershipLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_Traverse" name="cbx_Traverse" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.TraverseValue)%> /><label for="cbx_Traverse"><%=model.TraverseLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_WriteAttributes" name="cbx_WriteAttributes" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.WriteAttributesValue)%> /><label for="cbx_WriteAttributes"><%=model.WriteAttributesLanguage%></label></span>
            </div>
            <div class="el_item">
                <span class="el_checkbox_input"><input id="cbx_WriteExtendedAttributes" name="cbx_WriteExtendedAttributes" type="checkbox" <%=elanat.AspxHtmlValue.BooleanToCheckedHtmlAttribute(model.WriteExtendedAttributesValue)%> /><label for="cbx_WriteExtendedAttributes"><%=model.WriteExtendedAttributesLanguage%></label></span>
            </div>
            <div class="el_item">
                <input id="btn_SaveDirectoryPermissions" name="btn_SaveDirectoryPermissions" type="submit" class="el_button_input" value="<%=model.SaveDirectoryPermissionsLanguage%>" onclick="el_AjaxPostBack(this, false, 'frm_ActionChangeDirectoryPermissions')" />
            </div>
        </div>

        <input id="hdn_DirectoryPath" name="hdn_DirectoryPath" type="hidden" value="<%=model.DirectoryPathValue%>" />
        <input id="hdn_DirectoryName" name="hdn_DirectoryName" type="hidden" value="<%=model.DirectoryNameValue%>" />

    </form>
</body>
</html>