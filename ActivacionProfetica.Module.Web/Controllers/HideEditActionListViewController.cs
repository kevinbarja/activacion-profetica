using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.SystemModule;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class HideEditActionListViewController : ViewController<ListView>
    {
        public HideEditActionListViewController()
        {
            TargetViewId = "Operation_Places_ListView;" + Constants.View.OperationPaymentsListView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<ListViewController>().EditAction.Active["ViewController1"] = false;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.GetController<ListViewController>().EditAction.Active.RemoveItem("ViewController1");
        }
    }

}
