using System.Web.Mvc;

namespace SGK.Areas.JC
{
    public class JCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "JC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "JC_default",
                "JC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}