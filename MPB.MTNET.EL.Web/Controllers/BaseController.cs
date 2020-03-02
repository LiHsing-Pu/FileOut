using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using MPB.MTNET.EL.Web.Helpers;

namespace MPB.MTNET.EL.Web.Controllers
{
    public class BaseController : Controller
    {
        #region 參數屬性區
        public string FunID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["FunID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["FunID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["FunID"] = value; }
        }
        public string AppID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["AppID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["AppID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["AppID"] = value; }
        }
        public string UUID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["UserID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["UserID"] = XSSHelper.EncoderText(value); }
        }
        public string UserCname
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserCname"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["UserCname"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["UserCname"] = value; }
        }
        public string DeptID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["DeptID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["DeptID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["DeptID"] = value; }
        }
        public string DeptCname
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["DeptCname"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["DeptCname"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["DeptCname"] = value; }
        }
        public List<string> RoleID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["RoleID"] == null) { return new List<string>(); }
                else { return (List<string>)System.Web.HttpContext.Current.Session["RoleID"]; }
            }
            set { System.Web.HttpContext.Current.Session["RoleID"] = value; }
        }
        public string OrgID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["OrgID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["OrgID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["OrgID"] = value; }
        }
        public string Token
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Token"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["Token"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["Token"] = XSSHelper.EncoderText(value); }
        }
        public string ClientIP
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["IP"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["IP"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["IP"] = value; }
        }
        public DateTime TokenExpires
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["TokenExpires"] == null) { return DateTime.MinValue; }
                else { return Convert.ToDateTime(System.Web.HttpContext.Current.Session["TokenExpires"]); }
            }
            set { System.Web.HttpContext.Current.Session["TokenExpires"] = value; }
        }
        public string ListClientIP
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["LAST_REMOTE_ADDR"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["LAST_REMOTE_ADDR"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["LAST_REMOTE_ADDR"] = value; }
        }


        public string UserAgent
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["HTTP_USER_AGENT"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["HTTP_USER_AGENT"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["HTTP_USER_AGENT"] = value; }
        }
        public string MtPsSn
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["MtPsSn"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["MtPsSn"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["MtPsSn"] = value; }
        }
        public string SSOKey
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SSOKey"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["SSOKey"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["SSOKey"] = value; }
        }
        public string CpID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["CpID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["CpID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["CpID"] = value; }
        }
        public string CpCname
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["CpCname"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["CpCname"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["CpCname"] = value; }
        }
        public string LoginType
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["LoginType"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["LoginType"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["LoginType"] = value; }
        }
        public string PositionID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["PositionID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["PositionID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["PositionID"] = value; }
        }
        /// <summary>
        /// 審核人員
        /// </summary>
        public string DeptMgrAccCd
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["DeptMgrAccCd"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["DeptMgrAccCd"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["DeptMgrAccCd"] = value; }
        }
        /// <summary>
        /// 航務中心代號
        /// </summary>
        public string CenterDeptID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["CenterDeptID"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["CenterDeptID"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["CenterDeptID"] = value; }
        }
        #endregion
        /// <summary>
        /// Redirects to action.
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action) where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }
        public BaseController()
        {
            //UUID = "admin";
            //UserCname = "林測試";
            //DeptID = "KL";
            //CpID = "11460917";
            //DeptCname = "航港局";
            //MtPsSn = "174195";
            //MtPsSn = "3";

            switch (UUID)
            {
                case "admin":
                    PositionID = "102213";
                    break;
                case "admin10":
                    PositionID = "102191";
                    break;
                default:
                    break;
            }
        }
        public int getPermission()
        {
            if (RoleID.Where(x => x.Contains("ADMIN")).ToList().Count > 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}