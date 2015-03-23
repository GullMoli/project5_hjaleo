using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vefforittun_20151_Project5_Solution.Startup))]
namespace Vefforittun_20151_Project5_Solution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
