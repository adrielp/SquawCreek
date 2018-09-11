using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SquawCreekFinal.Startup))]
namespace SquawCreekFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
