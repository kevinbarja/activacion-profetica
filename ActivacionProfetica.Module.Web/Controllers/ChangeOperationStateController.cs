using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;

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
                    Duration = 4000,
                    Message = "Operación exitosa!",
                    Type = InformationType.Success
                };
                parameters.Web.Position = InformationPosition.Top;
                Application.ShowViewStrategy.ShowMessage(parameters);

                if (View.CurrentObject is Operation && ((Operation)View.CurrentObject).PlaceStatus.InternalId == PlaceStatus.ReservedPlaceStatus)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("CustomNavigate", "setTimeout(function(){ window.top.location.reload(); }, 1800);");
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
                        Duration = 4000,
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
                                    currentUserIsSupervisor = currentAppUser.Roles.Any(r => r.Name == Role.OperationSupervisor);
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
