﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace elanat
{
    public class SiteContactTitleModel
    {
        public string TitleLanguage { get; set; }

        public string TitleValue { get; set; }

        public string TitleCssClass { get; set; }

        public string TitleAttribute { get; set; }

        public void SetValue()
        {
            // Set Language
            TitleLanguage = Language.GetAddOnsLanguage("title", StaticObject.GetCurrentSiteGlobalLanguage(), StaticObject.SitePath + "page/contact/");
        }

        public void SetImportantField()
        {
            NameValueCollection InputRequest = new NameValueCollection();

            InputRequest.Add("txt_ContactTitle", "");

            ValidationClass vc = new ValidationClass();
            vc.SetImportantField(InputRequest, true, StaticObject.SitePath + "page/contact/");


            TitleAttribute += vc.ImportantInputAttribute["txt_ContactTitle"];

            TitleCssClass = TitleCssClass.AddHtmlClass(vc.ImportantInputClass["txt_ContactTitle"]);
        }
    }
}