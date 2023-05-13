using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Web;
using System.Web.UI;

namespace ActivacionProfetica.Module.Controllers
{
    public class ExportActionController : ViewController
    {
        SimpleAction exportAction;

        public ExportActionController()
        {
            TargetViewId = Constants.View.AuditListView;
            exportAction = new SimpleAction(this, "Exportar", PredefinedCategory.FullTextSearch);
            exportAction.Execute += new SimpleActionExecuteEventHandler(ExportAction_Execute);
        }

        void ExportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ASPxGridViewExporter exporter = new ASPxGridViewExporter();
            ((Control)View.Control).Page.Controls.Add(exporter);
            ASPxGridView gridView = (ASPxGridView)((ListView)View).Editor.Control;
            exporter.GridViewID = gridView.ID;
            exporter.WriteCsvToResponse("Auditoría");
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            exportAction.Active["showAction"] = true;
        }

    }
}
