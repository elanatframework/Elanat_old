﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteContactWebsite.aspx.cs" Inherits="elanat.SiteContactWebsite" %>
            <div id="pnl_Website">
                <div class="el_item">
                    <%=model.WebsiteLanguage%>
                </div>
                <div class="el_item">
                    <input id="txt_Website" name="txt_Website" type="text" value="<%=model.WebsiteValue%>" class="el_text_input el_left_to_right<%=model.WebsiteCssClass%>" <%=model.WebsiteAttribute%> />
                </div>
            </div>