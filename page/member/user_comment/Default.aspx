﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="elanat.MemberUserComment" %>
    <div class="el_head">
         <%=model.UserCommentLanguage%>
    </div>
    <div class="el_part_row">
        <div id="div_TableBox" class="el_item">
            <%=model.ContentValue%>
        </div>
    </div>