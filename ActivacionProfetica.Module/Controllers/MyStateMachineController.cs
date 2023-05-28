using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;

namespace ActivacionProfetica.Module.Controllers
{
    public partial class MyStateMachineController : ViewController<DetailView>
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
            //Frame.GetController<StateMachineController>().ChangeStateAction.Active["showAction"] = true;
            //.RemoveItem("ViewIsInEditMode");
        }
        private void OnTransitionExecuted(object sender, ExecuteTransitionEventArgs e)
        {
            try
            {
                //TODO: Validate no reservar asientos en León
                //TODO: Validate concurrence on other operation
                //Update estado de los asientos
                var operation = (View.CurrentObject as Operation);
                operation.NoCreatePayments = true;
                foreach (var place in operation.Places)
                {
                    place.Status = operation.PlaceStatus;
                }
                View.ObjectSpace.CommitChanges();
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

                //View.ObjectSpace.CommitChanges();
            }
            catch (Exception ex)
            {
                //View.ObjectSpace.Rollback();
                //ErrorHandling.Instance.SetPageError(ex);
            }
        }
    }
}
