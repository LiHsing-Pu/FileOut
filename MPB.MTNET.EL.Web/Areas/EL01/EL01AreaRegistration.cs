using System.Web.Mvc;

namespace MPB.MTNET.EL.Web.Areas.EL01
{
    public class EL01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EL01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EL01_default",
                "EL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}