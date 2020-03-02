
using MPB.MTNET.EL.Web.DataContainer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute, IActionFilter, IResultFilter
    {
        public UserAction userAction { get; set; }
        private string UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] == null) { return ""; }
                else { return HttpContext.Current.Session["UserID"].ToString(); }
            }
        }
        public AuthorizeFilterAttribute()
        {

        }
        /// <summary>
        /// 在執行 Action 之前執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!FunContainer.usersFun.ContainsKey(UserId))
            {
                ContentResult Content = new ContentResult();
                Content.Content = string.Format("<script type='text/javascript'>alert('請先登錄！');window.location.href='{0}';</script>", "/Home/Logout");
                filterContext.Result = Content;
            }
            else
            {
                System.Collections.Generic.List<MICS.MES.SSO.AccFunRes> list_AccFunRes = new System.Collections.Generic.List<MICS.MES.SSO.AccFunRes>();
                if (FunContainer.usersFun.TryGetValue(UserId, out list_AccFunRes))
                {
                    System.Web.Routing.RouteData rd = filterContext.RequestContext.RouteData;
                    string currentController = rd.GetRequiredString("controller");
                    if (list_AccFunRes.Where(o => o.FunUrl != null && o.FunUrl.Contains(currentController)).Count() == 0)
                    {
                        ContentResult Content = new ContentResult();
                        Content.Content = string.Format("<script type='text/javascript'>alert('本帳號無使用此功能的權限，請重新登入！');window.location.href='{0}';</script>", "/Home/Logout");
                        filterContext.Result = Content;
                    }
                }
                else
                {
                    ContentResult Content = new ContentResult();
                    Content.Content = string.Format("<script type='text/javascript'>alert('本帳號無使用此功能的權限，請重新登入！');window.location.href='{0}';</script>", "/Home/Logout");
                    filterContext.Result = Content;
                }
            }
            //if (!HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    //ContentResult Content = new ContentResult();
            //    ////Content.Content = string.Format("<script type='text/javascript'>alert('請先登錄！');window.location.href='{0}';</script>", FormsAuthentication.LoginUrl);
            //    //Content.Content = string.Format("<script type='text/javascript'>alert('請先登錄！');</script>", FormsAuthentication.LoginUrl);
            //    //filterContext.Result = Content;
            //    if (FunContainer.usersFun.ContainsKey(UserId))
            //    {
            //        //FunContainer.usersFun[user_id]
            //    }
            //}
        }
        /// <summary>
        /// 在執行 Action 之後執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        /// <summary>
        /// 在執行 Action Result 之前執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }
        /// <summary>
        /// 在執行 Action Result 之後執行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
    }
}