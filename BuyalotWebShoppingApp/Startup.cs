using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuyalotWebShoppingApp.Startup))]
namespace BuyalotWebShoppingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
