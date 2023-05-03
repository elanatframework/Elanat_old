﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace elanat
{
    public partial class SiteContact : System.Web.UI.Page
    {
        public SiteContactModel model = new SiteContactModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["btn_SendContact"]))
                btn_SendContact_Click(sender, e);


            if (string.IsNullOrEmpty(Request.Form["ddlst_Type"]))
                if (!string.IsNullOrEmpty(Request.QueryString["contact_type"]))
                    model.TypeOptionListSelectedValue = Request.QueryString["contact_type"].ToString();


            model.SetValue();
        }

        protected void btn_SendContact_Click(object sender, EventArgs e)
        {
            model.UseUploadPathValue = Request.Form["cbx_UseUploadPath"] == "on";
            model.UploadPathTextValue = Request.Form["txt_UploadPath"];
            model.UploadPathUploadValue = Request.Files["upd_UploadPath"];

            if (!string.IsNullOrEmpty(Request.Form["txt_Text"]))
                model.TextValue = Request.Form["txt_Text"];

            if (!string.IsNullOrEmpty(Request.Form["txt_Title"]))
                model.TitleValue = Request.Form["txt_Title"];

            if (!string.IsNullOrEmpty(Request.Form["ddlst_BirthdayYear"]))
            {
                model.BirthdayYearOptionListSelectedValue = Request.Form["ddlst_BirthdayYear"];
                model.BirthdayMountOptionListSelectedValue = Request.Form["ddlst_BirthdayMount"];
                model.BirthdayDayOptionListSelectedValue = Request.Form["ddlst_BirthdayDay"];
            }
            else
            {
                model.BirthdayYearOptionListSelectedValue = "0000";
                model.BirthdayMountOptionListSelectedValue = "00";
                model.BirthdayDayOptionListSelectedValue = "00";
            }

            if (!string.IsNullOrEmpty(Request.Form["txt_Email"]))
                model.EmailValue = Request.Form["txt_Email"];

            if (!string.IsNullOrEmpty(Request.Form["ddlst_Type"]))
                model.TypeOptionListSelectedValue = Request.Form["ddlst_Type"];

            if (!string.IsNullOrEmpty(Request.Form["txt_About"]))
                model.AboutValue = Request.Form["txt_About"];

            if (!string.IsNullOrEmpty(Request.Form["txt_Address"]))
                model.AddressValue = Request.Form["txt_Address"];

            if (!string.IsNullOrEmpty(Request.Form["txt_City"]))
                model.CityValue = Request.Form["txt_City"];

            if (!string.IsNullOrEmpty(Request.Form["txt_Country"]))
                model.CountryValue = Request.Form["txt_Country"];

            if (!string.IsNullOrEmpty(Request.Form["txt_MobileNumber"]))
                model.MobileNumberValue = Request.Form["txt_MobileNumber"];

            if (!string.IsNullOrEmpty(Request.Form["txt_PhoneNumber"]))
                model.PhoneNumberValue = Request.Form["txt_PhoneNumber"];

            if (!string.IsNullOrEmpty(Request.Form["txt_PostalCode"]))
                model.PostalCodeValue = Request.Form["txt_PostalCode"];

            if (!string.IsNullOrEmpty(Request.Form["txt_PublicEmail"]))
                model.PublicEmailValue = Request.Form["txt_PublicEmail"];

            if (!string.IsNullOrEmpty(Request.Form["txt_RealName"]))
                model.RealNameValue = Request.Form["txt_RealName"];

            if (!string.IsNullOrEmpty(Request.Form["txt_RealLastName"]))
                model.RealLastNameValue = Request.Form["txt_RealLastName"];

            if (!string.IsNullOrEmpty(Request.Form["txt_StateOrProvince"]))
                model.StateOrProvinceValue = Request.Form["txt_StateOrProvince"];

            if (!string.IsNullOrEmpty(Request.Form["txt_Website"]))
                model.WebsiteValue = Request.Form["txt_Website"];

            if (!string.IsNullOrEmpty(Request.Form["txt_ZipCode"]))
                model.ZipCodeValue = Request.Form["txt_ZipCode"];

            if (!string.IsNullOrEmpty(Request.Form["rdbtn_Gender"]))
            {
                model.GenderMaleValue = Request.Form["rdbtn_Gender"] == "rdbtn_GenderMale";
                model.GenderFemaleValue = Request.Form["rdbtn_Gender"] == "rdbtn_GenderFemale";
                model.GenderUnknownValue = Request.Form["rdbtn_Gender"] == "rdbtn_GenderUnknown";
            }
            else
                model.GenderUnknownValue = true;

            model.CaptchaTextValue = Request.Form["txt_Captcha"];


            // Evaluate Field Check
            model.EvaluateField(Request.Form);
            if (model.FindEvaluateError)
            {
                ResponseForm rf = new ResponseForm(StaticObject.GetCurrentSiteGlobalLanguage());

                foreach (string EvaluateError in model.EvaluateErrorList)
                    rf.AddLocalMessage(EvaluateError, "problem");

                rf.RedirectToResponseFormPage();
            }


            if (!model.CaptchaTextValue.MatchByCaptcha())
            {
                model.CaptchaIncorrectErrorView();
                return;
            }


            model.SendContact();


            model.SuccessView();
        }
    }
}