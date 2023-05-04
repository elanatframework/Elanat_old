﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace elanat
{
    public class ActionEditTextFileSetCodeHighlighterModel
    {
        public string TextFilePathValue { get; set; }

        public void SetValue()
        {
            //  Data Stream Path Access
            Security sc = new Security();
            sc.DataStreamPathAccess(TextFilePathValue);

            SetConfigurationText();
        }

        public void SetConfigurationText()
        {
            string[] Lines = File.ReadAllLines(HttpContext.Current.Server.MapPath(TextFilePathValue));
            string TextFileValue = "";

            int LinesLength = Lines.Length;
            foreach (string line in Lines)
            {
                TextFileValue += line;
                if (LinesLength != 1)
                    TextFileValue += Environment.NewLine;

                LinesLength--;
            }

            HttpContext.Current.Response.Write(TextFileValue);
        }
    }
}