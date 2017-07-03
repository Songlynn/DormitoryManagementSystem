using System.Web.Mvc;

namespace SGK.Areas.TB
{
    public class TBAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TB";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TB_default",
                "TB/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}