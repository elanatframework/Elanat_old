﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteSignUpAbout.aspx.cs" Inherits="elanat.SiteSignUpAbout" %>
            <div id="pnl_About">
                <div class="el_item">
                    <%=model.AboutLanguage%>
                </div>
                <div class="el_item">
                    <textarea id="txt_About" name="txt_About" class="el_textarea_input<%=model.AboutCssClass%>" <%=model.AboutAttribute%>><%=model.AboutValue%></textarea>
                </div>
            </div>