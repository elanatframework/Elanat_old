﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elanat
{
    public partial class SiteCommentTitle : System.Web.UI.Page
    {
        public SiteCommentTitleModel model = new SiteCommentTitleModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            model.TitleValue = Request.QueryString["title_value"];
            model.TitleCssClass = Request.QueryString["title_css_class"];


            model.SetValue();


            model.SetImportantField();
        }
    }
}