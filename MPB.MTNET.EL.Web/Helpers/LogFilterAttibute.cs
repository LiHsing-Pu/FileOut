using MPB.MTNET.EL.Web.DataContainer;
using MPB.MTNET.Library;
using MPB.MTNET.MICS.MES.Core;
using MPB.MTNET.MICS.MES.SAL;
using MPB.MTNET.MICS.MES.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MPB.MTNET.MICS.MES.RCode;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class LogFilterAttibute : ActionFilterAttribute, IActionFilter, IResultFilter
    {
        private static string strAPId = ConfigurationManager.AppSettings["APId"].ToString();
        private static string strAPKey = ConfigurationManager.AppSettings["APKey"].ToString();
        private static string stApiGateWay = ConfigurationManager.AppSettings["ApiGateWay"].ToString();

        public UserAction userAction { get; set; }

        #region Base參數
        private string UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] == null) { return ""; }
                else { return HttpContext.Current.Session["UserID"].ToString(); }
            }
        }
        private string ClientIP
        {
            get
            {
                if (HttpContext.Current.Session["IP"] == null) { return ""; }
                else { return HttpContext.Current.Session["IP"].ToString(); }
            }
        }
        public string Token
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Token"] == null) { return ""; }
                else { return System.Web.HttpContext.Current.Session["Token"].ToString(); }
            }
            set { System.Web.HttpContext.Current.Session["Token"] = value; }
        }
        #endregion

        private DateTime sDate { get; set; }
        private DateTime eDate { get; set; }

        /// <summary>
        /// 建構子
        /// </summary>
        public LogFilterAttibute()
        {

        }
        /// <summary>
        /// 在執行 Action 之前執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            sDate = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// 在執行 Action 之後執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            dynamic ViewModel = filterContext.Controller.ViewData["ViewModel"];
            //dynamic obj = DynamicObj.ViewModel(controllerName);



            #region 從Web抓取的訊息

            AccFunRes accFunRes = FunContainer.usersFun[UserId].Where(o => o.FunUrl == null ? false : o.FunUrl.Contains(controllerName)).FirstOrDefault();
            var warningType = filterContext.Controller.ViewData["WarningType"];
            var ex = filterContext.Controller.ViewData["Exception"];

            string M2appFunAction = "";
            string M2appContent = "";
            WarningType warningTypeInfo = new WarningType();
            warningType = WarningType.Info;
            if (warningType != null)
            {
                warningTypeInfo = (WarningType)warningType;
                switch (userAction)
                {
                    case UserAction.Create:
                        M2appFunAction = "C";
                        if (warningTypeInfo == WarningType.Info)
                        {
                            M2appContent = RCodes.DataInsertSuccess.Message;
                        }
                        else
                        {
                            M2appContent = RCodes.DataInsertFail.Message;
                        }
                        break;
                    case UserAction.Read:
                        M2appFunAction = "R";
                        if (warningTypeInfo == WarningType.Info)
                        {
                            M2appContent = RCodes.DataQuerySuccess.Message;
                        }
                        else
                        {
                            M2appContent = RCodes.DataQueryFail.Message;
                        }
                        break;
                    case UserAction.Update:
                        M2appFunAction = "U";
                        if (warningTypeInfo == WarningType.Info)
                        {
                            M2appContent = RCodes.DataUpdataSuccess.Message;
                        }
                        else
                        {
                            M2appContent = RCodes.DataUpdataFail.Message;
                        }
                        break;
                    case UserAction.Delete:
                        M2appFunAction = "D";
                        if (warningTypeInfo == WarningType.Info)
                        {
                            M2appContent = RCodes.DataDeleteSuccess.Message;
                        }
                        else
                        {
                            M2appContent = RCodes.DataDeleteFail.Message;
                        }
                        break;
                    case UserAction.Export:
                        M2appFunAction = "E";
                        if (warningTypeInfo == WarningType.Info)
                        {
                            M2appContent = RCodes.DataExportSuccess.Message;
                        }
                        else
                        {
                            M2appContent = RCodes.DataExportFail.Message;
                        }
                        break;
                    case UserAction.Printer:
                        M2appFunAction = "P";
                        if (warningTypeInfo == WarningType.Info)
                        {
                            M2appContent = RCodes.DataPrinterSuccess.Message;
                        }
                        else
                        {
                            M2appContent = RCodes.DataPrinterFail.Message;
                        }
                        break;
                    default:
                        break;
                }

                if (ex != null)
                {
                    M2appContent += "-";
                    M2appContent += ((Exception)ex).Message;
                }
            }
            else
            {
                warningTypeInfo = WarningType.Warning;
                M2appContent = filterContext.Controller.GetType().Name + "-" + filterContext.ActionDescriptor.ActionName + "，尚無紀錄設定";
            }
            #endregion

            MESInfo SAL_info = new MESInfo()
            {
                ApId = strAPId,
                SvcApiId = "LogAppMessage",
                AppAuthKey = strAPKey,
                ServiceMsgID = SerialNumbers.GetRandom,
                SrcId = strAPId,
                DstId = "SAL",
                ClientIp = ClientIP,
                ReqDT = sDate,
                FinDT = DateTime.Now,
                AccCd = UserId,
                Token = Token
            };

            //這邊會用web.config的LogServiceMessage參數值作為API的URL。
            bool SAL_result = SALService.SaveLogAppMessage(SAL_info, M2appContent, warningTypeInfo, M2appFunAction, accFunRes.FunCd == null ? "" : accFunRes.FunCd, strAPId);


            base.OnActionExecuted(filterContext);
        }
        /// <summary>
        /// 在執行 Action Result 之前執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        /// <summary>
        /// 在執行 Action Result 之後執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string drinks = this.userAction.ToString();

        }
    }
    public enum UserAction
    {
        Create,
        Read,
        Update,
        Delete,
        Export,
        Printer
    }
}