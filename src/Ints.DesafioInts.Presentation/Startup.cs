using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ints.DesafioInts.Presentation.Startup))]
namespace Ints.DesafioInts.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
