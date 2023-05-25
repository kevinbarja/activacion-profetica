using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System.Web.Configuration;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class MyController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            AboutInfo.Instance.Version = GetEnviroment();
        }

        private string GetEnviroment()
        {
            string enviroment = WebConfigurationManager.AppSettings["Enviroment"];
            string defaultEnviroment = "Ambiente no definido";
            if (enviroment != null)
            {
                return enviroment;
            }
            return defaultEnviroment;
        }
    }
}
