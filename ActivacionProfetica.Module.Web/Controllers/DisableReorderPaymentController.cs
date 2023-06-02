using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class DisableReorderPaymentController : ViewController<ListView>
    {
        public DisableReorderPaymentController()
        {
            TargetViewId = Constants.View.OperationPaymentsListView;
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Obtain the List Editor: XAF's abstraction over the UI control.
            ASPxGridListEditor listEditor = View.Editor as ASPxGridListEditor;
            if (listEditor != null)
            {
                // Use the grid control's behavior settings to disable the sort and group functionality.
                listEditor.Grid.SettingsBehavior.AllowSort = false;
                listEditor.Grid.SettingsBehavior.AllowGroup = false;

            }
        }
    }
}
