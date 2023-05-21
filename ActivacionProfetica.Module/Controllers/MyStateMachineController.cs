using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;

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
                        Message = $"Por favor llene todos los campos obligatorios denotados con el asterísco (*)",
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
                        //TODO: Apply this validation on save controller
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
                            }
                            else if (currentplaceSelected.Operation != null
                                && (currentplaceSelected.Operation.InternalId != operation.InternalId && currentplaceSelected.Operation.PlaceStatus.InternalId != PlaceStatus.AvailablePlaceStatus))
                            {
                                MessageOptions parametrosMensaje = new MessageOptions
                                {
                                    Duration = 4000,
                                    Message = $"El asiento '{placeSelected.Path}' no está disponible",
                                    Type = InformationType.Warning
                                };
                                parametrosMensaje.Web.Position = InformationPosition.Top;
                                Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
                                e.Cancel = true;
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
