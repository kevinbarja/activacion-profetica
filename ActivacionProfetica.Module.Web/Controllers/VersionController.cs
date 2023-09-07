using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

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
            return "Versión 1 del 8/09/2023";
            /*
            string enviroment = WebConfigurationManager.AppSettings["Enviroment"];
            string defaultEnviroment = "Ambiente no definido";
            if (enviroment != null)
            {
                return enviroment;
            }
            return defaultEnviroment;
            */
        }
    }
}
