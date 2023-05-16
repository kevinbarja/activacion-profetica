using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace ActivacionProfetica.Module.Controllers
{
    public class OperationController : ViewController
    {
        public OperationController()
        {
            TargetViewId = "Operation_Places_ListView";
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<NewObjectViewController>().NewObjectAction.Active["Startup"] = false;
        }
    }
}
