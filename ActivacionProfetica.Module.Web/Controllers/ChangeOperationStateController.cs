using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ActivacionProfetica.Module.Controllers
{
    public partial class ChangeOperationStateController : ViewController<DetailView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<StateMachineController>().TransitionExecuting += OnTransitionExecuting;
            Frame.GetController<StateMachineController>().TransitionExecuted += OnTransitionExecuted;
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.GetController<StateMachineController>().TransitionExecuted -= OnTransitionExecuted;
            Frame.GetController<StateMachineController>().TransitionExecuting -= OnTransitionExecuting;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            //Frame.GetController<StateMachineController>().ChangeStateAction.Active["showAction"] = false;
            Frame.GetController<StateMachineController>().ChangeStateActionItemsMode = ChangeStateActionItemsMode.GroupByStateMachine;
            //.RemoveItem("ViewIsInEditMode");
        }
        private void OnTransitionExecuted(object sender, ExecuteTransitionEventArgs e)
        {
            try
            {
                var isReserved = View.CurrentObject is Operation && ((Operation)View.CurrentObject).PlaceStatus.InternalId == PlaceStatus.ReservedPlaceStatus;

                //Update estado de los asientos
                var operation = (View.CurrentObject as Operation);
                operation.NoCreatePayments = true;
                foreach (var place in operation.Places)
                {
                    place.Status = operation.PlaceStatus;
                }
                View.ObjectSpace.CommitChanges();
                MessageOptions parameters = new MessageOptions
                {
                    Duration = 3000,
                    Message = (isReserved) ? "Whatsapp enviado!" : "Operación exitosa!",
                    Type = InformationType.Success
                };
                parameters.Web.Position = InformationPosition.Top;
                Application.ShowViewStrategy.ShowMessage(parameters);

                if (isReserved)
                {
                    string number_whatsapp = "591" + operation.Customer.WhatsApp + "@c.us";
                    HttpClient httpClient = new HttpClient();
                    var reportOsProvider = ReportDataProvider.GetReportObjectSpaceProvider(this.Application.ServiceProvider);
                    var reportStorage = ReportDataProvider.GetReportStorage(this.Application.ServiceProvider);
                    IObjectSpace objectSpace = reportOsProvider.CreateObjectSpace(typeof(ReportDataV2));
                    IReportDataV2 reportData = objectSpace.FirstOrDefault<ReportDataV2>(data => data.DisplayName == "Reporte");

                    ReportsModuleV2 moduloReportes = ReportsModuleV2.FindReportsModule(
                                ApplicationReportObjectSpaceProvider.ContextApplication.Modules);

                    XtraReport reporte = ReportDataProvider.ReportsStorage.GetReportContainerByHandle(
                        ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData)).Report;
                    reporte.FilterString = "[InternalId] = ?paramInternalId";
                    reporte.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter
                    {
                        Name = "paramInternalId",
                        Value = operation.InternalId
                    });
                    moduloReportes.ReportsDataSourceHelper.SetupBeforePrint(reporte, null, null, false, null, false);

                    using (MemoryStream flujo = new MemoryStream())
                    {
                        var exportOptions = new ImageExportOptions();
                        exportOptions.ExportMode = ImageExportMode.SingleFile;
                        exportOptions.Format = DXImageFormat.Jpeg;
                        exportOptions.Resolution = 96; //dpi
                        reporte.ExportToImage(flujo, exportOptions);
                        flujo.Seek(0, SeekOrigin.Begin);
                        byte[] contenidoReporte = flujo.ToArray();
                        string base64 = Convert.ToBase64String(contenidoReporte);
                        string json = "{\"from_number\": \"" + number_whatsapp + "\", \"message\": \"operación " + operation.InternalId.ToString() + "\", \"imagen\": \"" + base64 + "\"}";
                        StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        httpClient.PostAsync("http://localhost:3000/webhook", httpContent);
                    }
                    //WebWindow.CurrentRequestWindow.RegisterStartupScript("CustomNavigate", "setTimeout(function(){ window.top.location.reload(); }, 1800);");
                }

            }
            catch (Exception ex)
            {
                View.ObjectSpace.Rollback();
                //ErrorHandling.Instance.SetPageError(ex);
            }
        }

        private void OnTransitionExecuting(object sender, ExecuteTransitionEventArgs e)
        {
            try
            {
                RuleSetValidationResult validationResult = Validator.RuleSet.ValidateTarget(View.ObjectSpace, View.CurrentObject, DefaultContexts.Save);
                if (validationResult.State != ValidationState.Valid)
                {
                    MessageOptions parametrosMensaje = new MessageOptions
                    {
                        Duration = 6000,
                        Message = $"Formulario llenado incorrectamente, por favor verifique los datos e intente nuevamente.",
                        Type = InformationType.Warning
                    };
                    parametrosMensaje.Web.Position = InformationPosition.Top;
                    Application.ShowViewStrategy.ShowMessage(parametrosMensaje);

                    e.Cancel = true;
                }
                else
                {

                    var operation = (View.CurrentObject as Operation);
                    //Validate si está en otra operación y esa operación no es yo mismo o la otra op está no está disponible.
                    foreach (Place placeSelected in operation.Places)
                    {
                        using (UnitOfWork uow = new UnitOfWork(placeSelected.Session.DataLayer))
                        {
                            var currentplaceSelected = uow.GetObjectByKey<Place>(placeSelected.InternalId);


                            //Validate if current place is other sector of operation sector
                            var sectorId = operation.Sector.InternalId;
                            if (currentplaceSelected.Sector.InternalId != sectorId)
                            {
                                MessageOptions parametrosMensaje = new MessageOptions
                                {
                                    Duration = 4000,
                                    Message = $"La operación está en un sector distinto al del asiento '{placeSelected.Path}'",
                                    Type = InformationType.Warning
                                };
                                parametrosMensaje.Web.Position = InformationPosition.Top;
                                Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
                                e.Cancel = true;
                                return;
                            }

                            if (currentplaceSelected.ChildrenPlace != null && currentplaceSelected.ChildrenPlace.Any())
                            {
                                MessageOptions parametrosMensaje = new MessageOptions
                                {
                                    Duration = 4000,
                                    Message = $"El lugar '{placeSelected.Path}' no es un asiento, revise e intente nuevamente.",
                                    Type = InformationType.Warning
                                };
                                parametrosMensaje.Web.Position = InformationPosition.Top;
                                Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
                                e.Cancel = true;
                                return;
                            }


                            if (currentplaceSelected.Operation != null)
                            {
                                var statusId = currentplaceSelected.Operation.PlaceStatus.InternalId;
                                bool currentUserIsSupervisor = false;
                                var currentUser = SecuritySystem.CurrentUser;
                                if (currentUser is ApplicationUser)
                                {
                                    ApplicationUser currentAppUser = currentUser as ApplicationUser;
                                    currentUserIsSupervisor = currentAppUser.Roles.Any(r => r.Name == SharedKernel.Constants.Role.OperationSupervisor);
                                }

                                if (currentUserIsSupervisor)
                                {
                                    if (currentplaceSelected.Operation.InternalId != operation.InternalId && !(statusId != PlaceStatus.SoldPlaceStatus && statusId != PlaceStatus.ReservedPlaceStatus && statusId != PlaceStatus.CortecyPlaceStatus && statusId != PlaceStatus.NoAvailablePlaceStatus))
                                    {
                                        MessageOptions parametrosMensaje = new MessageOptions
                                        {
                                            Duration = 4000,
                                            Message = $"El asiento '{placeSelected.Path}' no está disponbile, está '{currentplaceSelected.Operation.PlaceStatus.SingularName}'. Contáctese con el supervisor.",
                                            Type = InformationType.Warning
                                        };
                                        parametrosMensaje.Web.Position = InformationPosition.Top;
                                        Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    if (currentplaceSelected.Operation.InternalId != operation.InternalId && statusId != PlaceStatus.AvailablePlaceStatus)
                                    {
                                        MessageOptions parametrosMensaje = new MessageOptions
                                        {
                                            Duration = 4000,
                                            Message = $"El asiento '{placeSelected.Path}' no está disponbile, está '{currentplaceSelected.Operation.PlaceStatus.SingularName}'",
                                            Type = InformationType.Warning
                                        };
                                        parametrosMensaje.Web.Position = InformationPosition.Top;
                                        Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //View.ObjectSpace.Rollback();
                //ErrorHandling.Instance.SetPageError(ex);
            }
        }
    }
}
