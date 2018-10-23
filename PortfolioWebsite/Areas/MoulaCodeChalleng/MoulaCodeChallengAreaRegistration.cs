using System.Web.Mvc;

namespace MelbourneData.Areas.MoulaCodeChalleng
{
    public class MoulaCodeChallengAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MoulaCodeChalleng";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MoulaCodeChalleng_default",
                "MoulaCodeChalleng/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}