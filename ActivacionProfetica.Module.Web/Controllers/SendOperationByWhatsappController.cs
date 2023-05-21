using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.IO;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class SendOperationByWhatsappController : ViewController<DetailView>
    {
        SimpleAction sendMessage;
        public SendOperationByWhatsappController()
        {
            TargetViewId = Constants.View.OperationDetailView;

            sendMessage = new SimpleAction(this, "SendByWhatsappAction", PredefinedCategory.Tools)
            {
                //Specify the Action's button caption.
                Caption = "Enviar por whatsapp",
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

            ReportsModuleV2 moduloReportes = ReportsModuleV2.FindReportsModule(
                        ApplicationReportObjectSpaceProvider.ContextApplication.Modules);

            XtraReport reporte = ReportDataProvider.ReportsStorage.GetReportContainerByHandle(
                ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData)).Report;
            moduloReportes.ReportsDataSourceHelper.SetupBeforePrint(reporte, null, null, false, null, false);

            using (MemoryStream flujo = new MemoryStream())
            {
                var opcionesPdf = new PdfExportOptions();
                opcionesPdf.ShowPrintDialogOnOpen = false;
                reporte.ExportToPdf(flujo, opcionesPdf);

                flujo.Seek(0, SeekOrigin.Begin);
                byte[] contenidoReporte = flujo.ToArray();
                string base64 = Convert.ToBase64String(contenidoReporte);
                //TODO: Send to Leo web service

                string pdfBase64 = "data:application/pdf;" + base64;

                //WebApplication.Redirect("https://api.whatsapp.com/send?phone=59175632256&text=%F0%9F%98%80Lleva%20el%20control%20de%20tu%20plan%2090%20d%C3%ADas%20ingresando%20a%20http%3A%2F%2Flocalhost%3A2064%20tu%20usuario%20es%20tu%20CI%20y%20la%20contrase%C3%B1a%20tu%20n%C3%BAmero%20de%20whatsapp.%20Bendiciones");
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

        }
    }
}
