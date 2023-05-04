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
    public class ActionViewNewRowModel
    {
        public string ViewIdValue { get; set; }

        public void SetValue()
        {
            // Set View Template
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "template/admin/" + StaticObject.GetCurrentAdminTemplatePhysicalPath()));
            string RowBoxTemplate = doc.SelectSingleNode("template_root/table/view/new_row/box").InnerTextAfterSetNodeVariant(StaticObject.GetCurrentAdminGlobalLanguage());
            string RowListItemTemplate = doc.SelectSingleNode("template_root/table/view/new_row/list_item").InnerTextAfterSetNodeVariant(StaticObject.GetCurrentAdminGlobalLanguage());
            string SumRowBoxTemplate = "";
            string SumRowListItemTemplate = "";
            string TmpRowBoxTemplate = RowBoxTemplate;

            List<string> ItemList = GlobalClass.GetItemViewList(HttpContext.Current.Server.MapPath(StaticObject.AdminPath + "/view/" + "App_Data/item_view/item_view.xml"));

            XmlNode ItemNode = doc.SelectSingleNode("template_root/table/view/item");


            DataUse.View duv = new DataUse.View();
            duv.FillCurrentViewWithReturnDr(ViewIdValue);


            foreach (string Text in ItemList)
            {
                string ItemBoxTemplate = ItemNode[Text].InnerTextAfterSetNodeVariant(StaticObject.GetCurrentAdminGlobalLanguage());
                string TmpItemBoxTemplate = ItemBoxTemplate.Replace("$_db " + Text + ";", duv.GetReturnDrColumnValue(Text));

                // If Exist More Column For Replace
                if (ItemNode[Text].Attributes["more_column"] != null)
                {
                    char[] DelimiterChars = { ',' };
                    string[] MoreColumnList = ItemNode[Text].Attributes["more_column"].InnerText.Split(DelimiterChars);

                    foreach (string Column in MoreColumnList)
                        TmpItemBoxTemplate = TmpItemBoxTemplate.Replace("$_db " + Column + ";", duv.GetReturnDrColumnValue(Column));
                }

                SumRowListItemTemplate += RowListItemTemplate.Replace("$_asp item;", TmpItemBoxTemplate);
            }

            foreach (XmlNode node in ItemNode.ChildNodes)
                if (node.Attributes["active"] != null)
                    if (node.Attributes["active"].Value.TrueFalseToBoolean())
                        TmpRowBoxTemplate = TmpRowBoxTemplate.Replace("$_db " + node.Name + ";", duv.GetReturnDrColumnValue(node.Name));

            TmpRowBoxTemplate = TmpRowBoxTemplate.Replace("$_asp view_id;", duv.ReturnDr["view_id"].ToString());
            TmpRowBoxTemplate = (duv.ReturnDr["view_active"].ToString().TrueFalseToBoolean()) ? TmpRowBoxTemplate.Replace("$_asp check_active_inactive;", "active").Replace("$_asp check_Active_Inactive;", "Inactive") : TmpRowBoxTemplate.Replace("$_asp check_active_inactive;", "inactive").Replace("$_asp check_Active_Inactive;", "Active");

            SumRowBoxTemplate += TmpRowBoxTemplate.Replace("$_asp item;", SumRowListItemTemplate);

            HttpContext.Current.Response.Write(SumRowBoxTemplate);
        }
    }
}