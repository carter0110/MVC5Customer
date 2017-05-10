using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskMVC.Startup))]
namespace TaskMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
