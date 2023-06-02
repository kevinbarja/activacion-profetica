using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.SystemModule;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class HideExportButtonListviewController : ViewController<ListView>
    {
        private WebExportController controller;
        private WebResetViewSettingsController controllerReset;

        const string deactivateReason = "HiddenInOperationDetail";

        public HideExportButtonListviewController()
        {
            TargetViewId = "Operation_Places_ListView;" + Constants.View.OperationPaymentsListView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            controller = Frame.GetController<WebExportController>();
            if (controller != null)
            {
                controller.ExportAction.Active.SetItemValue(deactivateReason, false);
            }

            controllerReset = Frame.GetController<WebResetViewSettingsController>();
            if (controller != null)
            {
                controllerReset.ResetViewSettingsAction.Active.SetItemValue(deactivateReason, false);
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (controller != null)
            {
                controller.ExportAction.Active.RemoveItem(deactivateReason);
                controller = null;
            }

            if (controllerReset != null)
            {
                controllerReset.ResetViewSettingsAction.Active.RemoveItem(deactivateReason);
                controllerReset = null;
            }
        }

    }
}
