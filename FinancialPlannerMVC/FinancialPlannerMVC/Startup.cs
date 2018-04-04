using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinancialPlannerMVC.Startup))]
namespace FinancialPlannerMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
