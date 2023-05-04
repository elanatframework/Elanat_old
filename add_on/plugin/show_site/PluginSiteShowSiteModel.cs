﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace elanat
{
    public class PluginSiteShowSiteModel
    {
        public void SetValue()
        {
            string PathBoxTemplate = Template.GetSiteTemplate("view/site/box");
            string PathListItemTemplate = Template.GetSiteTemplate("view/site/list_item");

            DataBaseSocket db = new DataBaseSocket();
            DataBaseDataReader dbdr = new DataBaseDataReader();
			dbdr.dr = db.GetProcedure("get_site_list");

            string TmpPathListItemTemplate = "";
            string SumPathListItemTemplate = "";

            if (dbdr.dr != null && dbdr.dr.HasRows)
                while (dbdr.dr.Read())
                {
                    TmpPathListItemTemplate = PathListItemTemplate;
                    TmpPathListItemTemplate = TmpPathListItemTemplate.Replace("$_asp site_global_name;", dbdr.dr["site_global_name"].ToString());
                    TmpPathListItemTemplate = TmpPathListItemTemplate.Replace("$_asp site_name;", dbdr.dr["site_name"].ToString() + "(" + dbdr.dr["site_global_name"].ToString() + ")");

                    SumPathListItemTemplate += TmpPathListItemTemplate;
                }

            db.Close();

            HttpContext.Current.Response.Write(PathBoxTemplate.Replace("$_asp item;", SumPathListItemTemplate).Replace("$_asp site_path;", StaticObject.SitePath));
        }
    }
}