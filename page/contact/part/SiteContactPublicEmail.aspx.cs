﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elanat
{
    public partial class SiteContactPublicEmail : System.Web.UI.Page
    {
        public SiteContactPublicEmailModel model = new SiteContactPublicEmailModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            model.PublicEmailValue = Request.QueryString["public_email_value"];
            model.PublicEmailCssClass = Request.QueryString["public_email_css_class"];
            model.PublicEmailAttribute = Request.QueryString["public_email_attribute"];


            model.SetValue();


            model.SetImportantField();
        }
    }
}