﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elanat
{
    public partial class ActionGetViewViewMore : System.Web.UI.Page
    {
        public ActionGetViewViewMoreModel model = new ActionGetViewViewMoreModel();

        protected void Page_Load(object sender, EventArgs e)
        {		
            if (string.IsNullOrEmpty(Request.QueryString["view_id"]))
            {
                Response.Write("false");
                return;
            }

            if (Request.QueryString["view_id"].ToString().IsNumber())
                Response.Write(model.GetViewMore(Request.QueryString["view_id"]));
            else
                Response.Write("false");
        }
    }
}