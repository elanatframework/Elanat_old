﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elanat
{
    public partial class SiteCommentPostalCode : System.Web.UI.Page
    {
        public SiteCommentPostalCodeModel model = new SiteCommentPostalCodeModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            model.PostalCodeValue = Request.QueryString["postal_code_value"];
            model.PostalCodeCssClass = Request.QueryString["postal_code_css_class"];
            model.PostalCodeAttribute = Request.QueryString["postal_code_attribute"];


            model.SetValue();


            model.SetImportantField();
        }
    }
}