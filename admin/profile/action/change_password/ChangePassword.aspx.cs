﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

namespace elanat
{
    public partial class ActionChangePassword : System.Web.UI.Page
    {
        public ActionChangePasswordModel model = new ActionChangePasswordModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["btn_ChangePassword"]))
                btn_ChangePassword_Click(sender, e);


            model.SetValue();


            // Set Required Field Validation
            model.SetImportantField();
        }

        protected void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            // Evaluate Field Check
            model.EvaluateField(Request.Form);
            if (model.FindEvaluateError)
            {
                ResponseForm rf = new ResponseForm(StaticObject.GetCurrentAdminGlobalLanguage());

                foreach (string EvaluateError in model.EvaluateErrorList)
                    rf.AddLocalMessage(EvaluateError, "problem");

                rf.RedirectToResponseFormPage();
            }


            if (Request.Form["txt_UserPassword"] != Request.Form["txt_RepeatPassword"])
            {
                model.PasswordEqualityErrorView();

                return;
            }

            model.UserPasswordValue = Request.Form["txt_UserPassword"];


            model.ChangePassword();

            model.SuccessView();
        }
    }
}