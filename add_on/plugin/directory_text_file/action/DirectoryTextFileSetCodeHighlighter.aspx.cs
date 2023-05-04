﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elanat
{
    public partial class ActionDirectoryTextFileSetCodeHighlighter : System.Web.UI.Page
    {
        public ActionDirectoryTextFileSetCodeHighlighterModel model = new ActionDirectoryTextFileSetCodeHighlighterModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["text_file_path"]))
                return;

            model.TextFilePathValue = Request.QueryString["text_file_path"].ToString();

            model.SetValue();
        }
    }
}