using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace elanat
{
	public class Security
	{
        public string GetHash(string Text)
        {
            // Get Md5 Hash From Input
            SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] TextBytes = System.Text.Encoding.ASCII.GetBytes(Text);
            byte[] hash = sha1.ComputeHash(TextBytes);

            // Convert Byte Array To Hex String
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }

        public void DataStreamPathAccess(string FilePathValue)
        {
            // Check Site Data Accsess
            bool IsSiteDataAccsess = false;
            if (FilePathValue.TextStartMathByValueCheck(StaticObject.SitePath + "App_Data/elanat_system_data/site_data/"))
            {
                string SiteGlobalName = FilePathValue.GetTextAfterValue(StaticObject.SitePath + "App_Data/elanat_system_data/site_data/").GetTextBeforeValue("/");

                DataUse.Site dus = new DataUse.Site();
                string SiteId = dus.GetSiteIdBySiteGlobalName(SiteGlobalName);

                DataUse.Component duc = new DataUse.Component();
                string ComponentId = duc.GetComponentIdByComponentName("site");
                duc.FillComponentRoleAccess(ComponentId);


                if (dus.GetSiteAccessShowCheck(SiteId) && duc.ComponentAccessEditAll)
                    IsSiteDataAccsess = true;
            }


            // Check Role Path Access
            Access acs = new Access();
            if (!IsSiteDataAccsess)
                if (!acs.RolePathAccessCheck(FilePathValue, HttpContext.Current.Request.Form.ToString()))
                {
                    HttpContext.Current.Response.Redirect(StaticObject.SitePath + "page/error/401");
                    return;
                }
        }

        public static string ConnectionString()
        {
            return StaticObject.ConnectionString;
        }

        public static void UseSystemAccess()
        {
            if (HttpContext.Current.Session["el_system_access_code"] == null)
                HttpContext.Current.Session.Add("el_system_access_code", StaticObject.SystemAccessCode);
        }

        public static bool IsSystemAccess(string Path)
        {
            if (HttpContext.Current.Session["el_system_access_code"] == null)
                return false;

            string SystemAccessCode = StaticObject.SystemAccessCode;

            if (!Path.Contains("el_system_access_code=" + SystemAccessCode))
                return false;

            if (HttpContext.Current.Session["el_system_access_code"].ToString() == SystemAccessCode)
            {
                HttpContext.Current.Session.Remove("el_system_access_code");
                return true;
            }

            return false;
        }

        public static string GetUserIp()
        {
            HttpRequest request = HttpContext.Current.Request;
            return request.UserHostAddress;
        }

        public static string GetServerIp()
        {
            string ServerIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ServerIp))
                ServerIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return ServerIp;
        }

        public static string GetCaptchaImage()
        {
            CurrentClientObjectClass ccoc = new CurrentClientObjectClass();

            if (ccoc.CaptchaReleaseCount > 0)
                return "";

            string CaptchaImage = Template.GetSiteGlobalTemplate("captcha/load");

            string CaptchaPath = ElanatConfig.GetElanatConfig("default_include_path/captcha_path");

            CaptchaImage = CaptchaImage.Replace("$_asp captcha_path;", CaptchaPath);

            bool CaptchaSensitivityCase = (ElanatConfig.GetNode("security/use_sensitivity_case_captcha").Attributes["active"].Value == "true");


            CaptchaImage = CaptchaImage.Replace("$_asp is_case_sensitive;", (CaptchaSensitivityCase) ? Language.GetLanguage("case_sensitive", StaticObject.GetCurrentSiteGlobalLanguage()) : "");

            return CaptchaImage;
        }

        public static bool LoginActiveCheck()
        {
            if (ElanatConfig.GetNode("active/login_active").Attributes["active"].Value == "true")
                return true;
            else
                return false;
        }

        public static void SetCodeIni(string Variable, string Value)
        {
            Variable += "=";
            var Lines = System.IO.File.ReadAllLines(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/elanat_system_data/code/code.ini"));
            CodeSocket code = new CodeSocket();
            Lines[CodeIniRowName(Variable)] = Variable + code.Coder(Value);
            System.IO.File.WriteAllLines(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/elanat_system_data/code/code.ini"), Lines);
        }

        /// <param name="Variable">admin_directory_path, connection_string, lock_login_directory_path, lock_login_password_value, mail_password, secret_key, system_access_code</param>
        public static string GetCodeIni(string Variable)
        {
            Variable += "=";
            var Lines = System.IO.File.ReadAllLines(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/elanat_system_data/code/code.ini"));
            CodeSocket code = new CodeSocket();

            string  LineValue = Lines[CodeIniRowName(Variable)].ToString().Remove(0, Variable.Length);
            if (LineValue[0] == '"')
            {
                LineValue = LineValue.Remove(0, 1);
                LineValue = LineValue.Remove(LineValue.Length - 1, 1);
            }

            return code.DeCoder(LineValue);
        }

        private static int CodeIniRowName(string Variable)
        {
            int LineNumber = 1;
            switch (Variable)
            {
                case "admin_directory_path=": LineNumber = 1; break;
                case "connection_string=": LineNumber = 2; break;
                case "lock_login_directory_path=": LineNumber = 3; break;
                case "lock_login_password_value=": LineNumber = 4; break;
                case "mail_password=": LineNumber = 5; break;
                case "secret_key=": LineNumber = 6; break;
                case "system_access_code=": LineNumber = 7; break;
            }
            return LineNumber;
        }

        public string ProtectLikeSearchSqlInjection(string SqlParameters)
        {
            string TmpSqlParameters = SqlParameters;

            TmpSqlParameters = TmpSqlParameters.Replace("[", "[[]");
            TmpSqlParameters = TmpSqlParameters.Replace("%", "[%]");
            TmpSqlParameters = TmpSqlParameters.Replace("_", "[_]");

            return TmpSqlParameters;
        }

        public string ProtectSqlInjection(string SqlParameters, bool UseLikeSearchProtections = false)
        {
            string TmpSqlParameters = SqlParameters;


            while (TmpSqlParameters[0] == ' ')
                TmpSqlParameters = TmpSqlParameters.Remove(0, 1);

            while (TmpSqlParameters[TmpSqlParameters.Length - 1] == ' ')
                TmpSqlParameters = TmpSqlParameters.Remove(TmpSqlParameters.Length - 1, 1);


            if (UseLikeSearchProtections)
                TmpSqlParameters = ProtectLikeSearchSqlInjection(TmpSqlParameters);


            TmpSqlParameters = TmpSqlParameters.Replace("\n", "\\n");
            TmpSqlParameters = TmpSqlParameters.Replace("\t", "\\t");
            TmpSqlParameters = TmpSqlParameters.Replace("\b", "\\b");
            TmpSqlParameters = TmpSqlParameters.Replace("\r", "\\r");
            TmpSqlParameters = TmpSqlParameters.Replace("\x00", "\\0");
            TmpSqlParameters = TmpSqlParameters.Replace("\u001A", "\\Z");
            TmpSqlParameters = TmpSqlParameters.Replace("'", null);
            TmpSqlParameters = TmpSqlParameters.Replace("\"", null);
            TmpSqlParameters = TmpSqlParameters.Replace("NULL", null);
            TmpSqlParameters = TmpSqlParameters.Replace(@"\", null);
            TmpSqlParameters = TmpSqlParameters.Replace(";", null);
            TmpSqlParameters = TmpSqlParameters.Replace("--", null);
            TmpSqlParameters = TmpSqlParameters.Replace("xp_", null);
            TmpSqlParameters = TmpSqlParameters.Replace(@"/\*", null);
            TmpSqlParameters = TmpSqlParameters.Replace("*/", null);


            return TmpSqlParameters;
        }

        public string ProtectXssInjection(string Text)
        {
            string TmpText = Text;

            TmpText = HttpContext.Current.Server.HtmlEncode(Text);
            TmpText = System.Text.RegularExpressions.Regex.Replace(Text, @"<[^>]*>", String.Empty);

            return TmpText;
        }

        public bool ExistXssInjection(string Text)
        {
            string TmpText = HttpContext.Current.Server.UrlDecode(Text);

            if (TmpText.Contains("<script") || TmpText.Contains("javascript:") || TmpText.Contains("script>"))
                return true;

            return false;
        }

        public bool ExistHtml(string Text)
        {
            string TmpText = HttpContext.Current.Server.UrlDecode(Text);

            return (System.Text.RegularExpressions.Regex.Matches(TmpText, @"<[^>]*>").Count > 0);
        }

        public bool IsNumber(string Number)
        {
            int TmpInt = 0;

            bool IsNumber = int.TryParse(Number, out TmpInt);

            return IsNumber;
        }

        public static void SetLogError(Exception ex, string GlobalLanguage = "")
        {
            CurrentClientObjectClass ccoc = new CurrentClientObjectClass();

            if (string.IsNullOrEmpty(GlobalLanguage))
                GlobalLanguage = StaticObject.GetCurrentAdminGlobalLanguage();


            if (ElanatConfig.GetNode("security/save_logs_error").Attributes["active"].Value == "true")
            {
                string Ip = Security.GetUserIp();
                string Date = DateAndTime.GetDate("yyyy-MM-dd");
                string Time = DateAndTime.GetTime("HH-mm-ss");
                string Path = HttpContext.Current.Request.Url.AbsolutePath;
                Random rand = new Random();

                List<string> Lines = new List<string>();
                Lines.Add("error_title=" + ex.Message);
                Lines.Add("path=" + Path);
                Lines.Add("date=" + Date);
                Lines.Add("time=" + DateAndTime.GetTime("HH:mm:ss"));
                Lines.Add("user_name=" + ccoc.UserName);
                Lines.Add("ip=" + Ip);
                Lines.Add("error_text=" + ex.GetBaseException());
                Lines.Add("inner_error_title=" + ex.InnerException.Message);
                Lines.Add("inner_error_text=" + ex.InnerException.GetBaseException());


                string FileName = "";
                if (ex.Message.ToFileNameClean().Length > 150)
                    FileName = ex.Message.ToFileNameClean().Substring(0, 149) + " ...";
                else
                    FileName = ex.Message.ToFileNameClean();

                FileName += Date + "_" + Time + "_" + rand.Next(1000000000, 2000000000);

                System.IO.File.WriteAllLines(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/logs/" + FileName + ".log"), Lines);
            }

            if (StaticObject.WriteLogsError)
            {
                string LogsErrorTemplate = Template.GetGlobalTemplate("part/logs_error", GlobalLanguage);
                LogsErrorTemplate = LogsErrorTemplate.Replace("$_logs_error_text;", ex.Message);

                HttpContext.Current.Response.Write(LogsErrorTemplate);
            }
        }

        // Overload
        public static void SetLogError(string ErrorTitle, string ErrorText = "", string GlobalLanguage = "")
        {
            CurrentClientObjectClass ccoc = new CurrentClientObjectClass();

            if (string.IsNullOrEmpty(GlobalLanguage))
                GlobalLanguage = StaticObject.GetCurrentAdminGlobalLanguage();


            if (ElanatConfig.GetNode("security/save_logs_error").Attributes["active"].Value == "true")
            {
                string Ip = Security.GetUserIp();
                string Date = DateAndTime.GetDate("yyyy-MM-dd");
                string Time = DateAndTime.GetTime("HH-mm-ss");
                string Path = HttpContext.Current.Request.Url.AbsolutePath;
                Random rand = new Random();

                List<string> Lines = new List<string>();
                Lines.Add("error_title=" + ErrorTitle);
                Lines.Add("path=" + Path);
                Lines.Add("date=" + Date);
                Lines.Add("time=" + DateAndTime.GetTime("HH:mm:ss"));
                Lines.Add("user_name=" + ccoc.UserName);
                Lines.Add("ip=" + Ip);
                Lines.Add("error_text=" + ErrorText);


                string FileName = "";
                if (ErrorTitle.ToFileNameClean().Length > 150)
                    FileName = ErrorTitle.ToFileNameClean().Substring(0, 149) + " ...";
                else
                    FileName = ErrorTitle.ToFileNameClean();

                FileName += Date + "_" + Time + "_" + rand.Next(1000000000, 2000000000);

                System.IO.File.WriteAllLines(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/logs/" + FileName + ".log"), Lines);
            }

            if (StaticObject.WriteLogsError)
            {
                string LogsErrorTemplate = Template.GetGlobalTemplate("part/logs_error", GlobalLanguage);
                LogsErrorTemplate = LogsErrorTemplate.Replace("$_logs_error_text;", ErrorTitle);

                HttpContext.Current.Response.Write(LogsErrorTemplate);
            }
        }

        public static void SetLogErrorForApplicationStart(Exception ex)
        {
            if (ElanatConfig.GetNode("security/save_logs_error").Attributes["active"].Value == "true")
            {
                string Date = DateAndTime.GetDate("yyyy-MM-dd");
                string Time = DateAndTime.GetTime("HH-mm-ss");
                Random rand = new Random();

                List<string> Lines = new List<string>();
                Lines.Add("error_title=" + ex.Message);
                Lines.Add("path=none");
                Lines.Add("date=" + Date);
                Lines.Add("time=" + Time);
                Lines.Add("user_name=guest");
                Lines.Add("ip=none");
                Lines.Add("error_text=" + ex.GetBaseException());
                Lines.Add("inner_error_title=" + ex.InnerException.Message);
                Lines.Add("inner_error_text=" + ex.InnerException.GetBaseException());


                string FileName = "";
                if (ex.Message.ToFileNameClean().Length > 150)
                    FileName = ex.Message.ToFileNameClean().Substring(0, 149) + " ...";
                else
                    FileName = ex.Message.ToFileNameClean();

                FileName += Date + "_" + Time + "_" + rand.Next(1000000000, 2000000000);

                System.IO.File.WriteAllLines(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/logs/" + FileName + ".log"), Lines);
            }
        }

        public static string GetAuthorizedExtension(string TagPath, int Item = 0, string Attr = null)
        {
            XmlNodeList NodeList = StaticObject.AuthorizedExtensionDocument.SelectNodes("authorized_extension_root/authorized_extension_list/" + TagPath);
            if (Attr != null)
                return NodeList[Item].Attributes[Attr].Value.ToString();
            else
                return NodeList[Item].InnerText;
        }

        public static bool CheckAuthorizedAllExtension()
        {
            if (GetAuthorizedExtension("authorized_extension", 0, "active") == "true")
                return true;
            return false;
        }

        public static bool CheckUnauthorizedAllExtension()
        {
            if (GetAuthorizedExtension("unauthorized_extension", 0, "active") == "true")
                return true;
            return false;
        }

        public static bool CheckAuthorizedExtension(string FileExtension)
        {
            if (string.IsNullOrEmpty(FileExtension))
                return true;

            if (FileExtension[0] == '.')
                FileExtension = FileExtension.Remove(0, 1);
            if (CheckAuthorizedAllExtension())
            {
                List<string> AuthorizedExtensionList = ListClass.GetAuthorizedExtensionList();
                foreach (string Text in AuthorizedExtensionList)
                {
                    if (FileExtension == Text)
                    {
                        return true;
                    }
                }
            }

            if (CheckUnauthorizedAllExtension())
            {
                List<string> UnauthorizedExtensionList = ListClass.GetUnauthorizedExtensionList();
                foreach (string Text in UnauthorizedExtensionList)
                {
                    if (FileExtension == Text)
                        return false;
                }
            }

            if (!CheckAuthorizedAllExtension())
                return true;
            return true;
        }

        public static bool ExtensionIsAuthorizedForUpload(string Extension)
        {
            bool ReturnValue = false;

            if (Extension[0] == '.')
                Extension = Extension.Remove(0, 1);

            // Check Authorized Extension
            if (!StaticObject.AuthorizedExtensionDocument.SelectSingleNode("authorized_extension_root/authorized_extension_list/authorized_extension_for_upload").Attributes["active"].Value.TrueFalseToBoolean())
                ReturnValue = true;
            else
            {
                XmlNodeList AuthorizedExtensionList = StaticObject.AuthorizedExtensionDocument.SelectSingleNode("authorized_extension_root/authorized_extension_list/authorized_extension_for_upload").ChildNodes;

                foreach (XmlNode node in AuthorizedExtensionList)
                {
                    if (node.Attributes["value"].Value == Extension)
                    {
                        ReturnValue = true;
                        break;
                    }
                }
            }


            // Check Unauthorized Extension
            if (!StaticObject.AuthorizedExtensionDocument.SelectSingleNode("authorized_extension_root/authorized_extension_list/unauthorized_extension_for_upload").Attributes["active"].Value.TrueFalseToBoolean())
                ReturnValue = (true && ReturnValue);
            else
            {
                XmlNodeList UnauthorizedExtensionList = StaticObject.AuthorizedExtensionDocument.SelectSingleNode("authorized_extension_root/authorized_extension_list/unauthorized_extension_for_upload").ChildNodes;

                foreach (XmlNode node in UnauthorizedExtensionList)
                {
                    if (node.Attributes["value"].Value == Extension)
                    {
                        ReturnValue = false;
                        break;
                    }
                }
            }

            return ReturnValue;
        }

        public static List<string> LoginIpBlackList = new List<string>();

        public bool ExistLoginIpToBlackListCheck()
        {
            string UserIp = Security.GetUserIp();

            if(LoginIpBlackList.Count > 0)
                foreach (string Ip in LoginIpBlackList)
                    if (Ip == UserIp)
                        return true;

            return false;
        }

        public bool IpIsSecure(string Ip)
        {
            if (ProcessKeeper.ClientIpIsSecure == "true")
                return true;
            else if (ProcessKeeper.ClientIpIsSecure == "false")
                return false;

            StringClass sc = new StringClass();

            // Search In Inaccessible Ip List
            XmlNode IpNodeList = StaticObject.SecurityDocument.SelectSingleNode("security_root/security_list/user_inaccessible_ip_list/user_inaccessible_ip");

            if (IpNodeList.Attributes["active"].Value == "true")
                foreach (XmlNode ip in IpNodeList.ChildNodes)
                {
                    if (Ip == ip.Attributes["value"].Value)
                    {
                        ProcessKeeper.ClientIpIsSecure = "false";
                        return false;
                    }
                }

            ProcessKeeper.ClientIpIsSecure = "true";
            return true;
        }
        
        public string UnauthorizedReason = "";
        public bool IsUnauthorizedValue(string TableName, string ColumnName, string ColumnValue, string Path = "")
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(Path + "App_Data/unauthorized_list/unauthorized.xml"));

            XmlNodeList NodeList = doc.SelectSingleNode("unauthorized_root/unauthorized_list/" + TableName + "/" + ColumnName).ChildNodes;

            foreach (XmlNode node in NodeList)
            {
                if (node.Attributes["exist"].Value == "true")
                    if (ColumnValue.Contains(node.InnerText))
                        goto StartRewritePath;

                if (node.Attributes["start_by"].Value == "true")
                    if (ColumnValue.TextStartMathByValueCheck(node.InnerText))
                        goto StartRewritePath;

                if (node.Attributes["end_by"].Value == "true")
                    if (node.InnerText.Length >= ColumnValue.Length)
                    {
                        string TmpPath = ColumnValue;

                        TmpPath = TmpPath.GetTextBeforeValue("?");

                        if (TmpPath.TextEndMathByValueCheck(node.InnerText))
                            goto StartRewritePath;
                    }

                continue;

                StartRewritePath:

                UnauthorizedReason = Language.GetLanguage(node.Attributes["reason"].Value, StaticObject.GetCurrentAdminGlobalLanguage());
                return true;

            }

            return false;
        }

        public static void ExitUser()
        {
            // Add Reference
            ReferenceClass rc = new ReferenceClass();
            rc.StartEvent("logout", StaticObject.GetCurrentUserId());


            // Delete Session Data File
            if (File.Exists(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/elanat_system_data/session_data/" + HttpContext.Current.Session.SessionID + ".ini.tmp")))
                File.Delete(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/elanat_system_data/session_data/" + HttpContext.Current.Session.SessionID + ".ini.tmp"));


            HttpContext.Current.Session.Clear();

            StaticObject.OnlineUser--;

            HttpContext.Current.Application.Remove("el_user_" + StaticObject.GetCurrentUserId() + ":online");
        }

        public static bool IsSecureLogin()
        {
            CurrentClientObjectClass ccoc = new CurrentClientObjectClass();

            if (ccoc.RoleDominantType == "guest")
                return true;

            CodeSocket code = new CodeSocket();

            if (HttpContext.Current.Request.Cookies["el_current_user-secure_value"] == null)
                return false;

            if (HttpContext.Current.Session["el_current_user:secure_value"] == null)
                return false;

            string CookiesSecureValue = HttpContext.Current.Request.Cookies["el_current_user-secure_value"].Value;
            string SessionSecureValue = HttpContext.Current.Session["el_current_user:secure_value"].ToString();

            if (code.DeCoder(CookiesSecureValue) != SessionSecureValue)
                return false;


            string SessionIpValue = HttpContext.Current.Session["el_current_user:ip"].ToString();

            if (SessionIpValue != Security.GetUserIp())
                return false;

            return true;
        }

        public void SetUserLogin(string UserId)
        {
            SetNewSecureValue();

            HttpContext.Current.Session.Add("el_current_user:ip", Security.GetUserIp());
        }

        public static void SetNewSecureValue()
        {
            // Set New Secure Value
            CodeSocket code = new CodeSocket();
            StringClass sc = new StringClass();

            string RandomText = sc.GetCleanRandomText(int.Parse(ElanatConfig.GetNode("security/user_secure_value_size").Attributes["value"].Value));

            HttpCookie cookie = new HttpCookie("el_current_user-secure_value", code.Coder(RandomText));
            cookie.Expires.AddSeconds(int.Parse(StaticObject.CookiesLifeTime));

            HttpContext.Current.Response.Cookies.Add(cookie);

            HttpContext.Current.Session.Add("el_current_user:secure_value", RandomText);
        }

        public void SetKeepLogin(string CurrentPath, string Alert)
        {
            StringClass sc = new StringClass();
            string RandomText = sc.GetCleanRandomText(int.Parse(ElanatConfig.GetNode("security/user_secure_value_size").Attributes["value"].Value));

            CurrentClientObjectClass ccoc = new CurrentClientObjectClass();

            using (FileStream fs = File.Create(HttpContext.Current.Server.MapPath(StaticObject.SitePath + "App_Data/elanat_system_data/user_data/user_" + ccoc.UserId + "/keep_login/keep_login.ini")))
            {
                string Value = ""; 
                Value = "user_id=" + ccoc.UserId + Environment.NewLine; 
                Value += "user_ip=" + GetUserIp() + Environment.NewLine; 
                Value += "random_text=" + RandomText + Environment.NewLine; 
                Value += "date_and_time_long=" + DateAndTime.GetDateAndTimeLong() + Environment.NewLine;
                Value += "current_path=" + CurrentPath + Environment.NewLine;
                Value += "alert=" + Alert; 

                byte[] Text = new UTF8Encoding(true).GetBytes(Value);
                fs.Write(Text, 0, Text.Length);
            }

            HttpCookie RandomTextCookie = new HttpCookie("el_current_user-keep_login_random_text", RandomText);
            RandomTextCookie.Expires.AddSeconds(60);

            HttpCookie UserIdCookie = new HttpCookie("el_current_user-keep_login_user_id", ccoc.UserId);
            UserIdCookie.Expires.AddSeconds(60);

            HttpContext.Current.Response.Cookies.Add(RandomTextCookie);
            HttpContext.Current.Response.Cookies.Add(UserIdCookie);
        }

        public void AddIpSessionIndexer()
        {
            HttpRequest request = HttpContext.Current.Request;
            string CurrentIp = request.UserHostAddress;

            bool ExistIp = false;
            int i = 0;
            foreach (string ip in StaticObject.IpSessionIndexerKeeperName)
            {
                if (CurrentIp == ip)
                {
                    ExistIp = true;
                    break;
                }

                i++;
            }

            if (ExistIp)
                StaticObject.IpSessionIndexerKeeperValue[i]++;
            else
            {
                StaticObject.IpSessionIndexerKeeperName.Add(CurrentIp);
                StaticObject.IpSessionIndexerKeeperValue.Add(1);
            }

            StaticObject.IpSessionIndexerKeeperName.RemoveAt(i);
            StaticObject.IpSessionIndexerKeeperValue.RemoveAt(i);
        }

        public int GetIpSessionCount()
        {
            HttpRequest request = HttpContext.Current.Request;
            string CurrentIp = request.UserHostAddress;

            int i = 0;
            foreach (string ip in StaticObject.IpSessionIndexerKeeperName)
            {
                if (CurrentIp == ip)
                    return StaticObject.IpSessionIndexerKeeperValue[i];

                i++;
            }

            return 1;
        }

        public void ResetIpSessionIndexer()
        {
            HttpRequest request = HttpContext.Current.Request;
            string CurrentIp = request.UserHostAddress;

            int i = 0;
            foreach (string ip in StaticObject.IpSessionIndexerKeeperName)
            {
                if (CurrentIp == ip)
                {
                    StaticObject.IpSessionIndexerKeeperValue[i] = 1;
                    break;
                }

                i++;
            }
        }
    }
}
