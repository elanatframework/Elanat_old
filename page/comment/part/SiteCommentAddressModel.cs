﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace elanat
{
    public class SiteCommentAddressModel
    {
        public string AddressLanguage { get; set; }

        public string AddressValue { get; set; }

        public string AddressCssClass { get; set; }

        public string AddressAttribute { get; set; }

        public void SetValue()
        {
            // Set Language
            AddressLanguage = Language.GetAddOnsLanguage("address", StaticObject.GetCurrentSiteGlobalLanguage(), StaticObject.SitePath + "page/comment/");
        }

        public void SetImportantField()
        {
            NameValueCollection InputRequest = new NameValueCollection();

            InputRequest.Add("txt_Address", "");

            ValidationClass vc = new ValidationClass();
            vc.SetImportantField(InputRequest, true, StaticObject.SitePath + "page/comment/");


            AddressAttribute += vc.ImportantInputAttribute["txt_Address"];

            AddressCssClass = AddressCssClass.AddHtmlClass(vc.ImportantInputClass["txt_Address"]);
        }
    }
}