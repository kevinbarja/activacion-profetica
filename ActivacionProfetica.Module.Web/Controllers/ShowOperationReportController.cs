using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System;

namespace ActivacionProfetica.Module.Controllers
{
    public class ShowOperationReportController : ViewController<DetailView>
    {
        SimpleAction sendMessage;
        public ShowOperationReportController()
        {
            TargetViewId = Constants.View.OperationDetailView;

            sendMessage = new SimpleAction(this, "ReportAction", PredefinedCategory.Tools)
            {
                //Specify the Action's button caption.
                Caption = "Reporte",
                //Specify the confirmation message that pops up when a user executes an Action.
                //ConfirmationMessage = "Are you sure you want to clear the Tasks list?",
                //Specify the icon of the Action's button in the interface.
                //ImageName = "Action_Clear"
            };

            //This event fires when a user clicks the Simple Action control. Handle this event to execute custom code.
            sendMessage.Execute += ClearTasksAction_Execute;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            sendMessage.Active["Hid"] = !ObjectSpace.IsNewObject(View.CurrentObject);
        }

        private void ClearTasksAction_Execute(Object sender, SimpleActionExecuteEventArgs e)
        {
            var operation = (Operation)View.CurrentObject;

            var reportOsProvider = ReportDataProvider.GetReportObjectSpaceProvider(this.Application.ServiceProvider);
            var reportStorage = ReportDataProvider.GetReportStorage(this.Application.ServiceProvider);
            IObjectSpace objectSpace = reportOsProvider.CreateObjectSpace(typeof(ReportDataV2));
            IReportDataV2 reportData = objectSpace.FirstOrDefault<ReportDataV2>(data => data.DisplayName == "Reporte");
            string handle = reportStorage.GetReportContainerHandle(reportData);
            ReportServiceController controller = Frame.GetController<ReportServiceController>();

            CriteriaOperator objectsCriteria = ((BaseObjectSpace)objectSpace).GetObjectsCriteria(View.ObjectTypeInfo, e.SelectedObjects);
            if (controller != null)
            {
                controller.ShowPreview(handle, objectsCriteria);
            };
            //if (controller != null)
            //{
            //    controller.ShowPreview(handle);
            //};
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

        }
    }
}
