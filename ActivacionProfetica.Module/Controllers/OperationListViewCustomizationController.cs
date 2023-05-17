using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace ActivacionProfetica.Module.Controllers
{
    public class OperationListViewCustomizationController : ViewController<ListView>
    {
        public OperationListViewCustomizationController()
        {
            TargetViewId = "Operation_Places_ListView";
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<NewObjectViewController>().NewObjectAction.Active["Startup"] = false;
            //TODO: Desactivate unlink places when place status is Consignacion and update cuota.
        }
    }
}
