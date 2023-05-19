using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace ActivacionProfetica.Module.Controllers
{
    public class MyController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            AboutInfo.Instance.Version = "Versión 1.0.1";
        }
    }
}
