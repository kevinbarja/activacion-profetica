using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.Persistent.Validation;
using System;

namespace ActivacionProfetica.Module.Controllers
{
    public partial class MyStateMachineController : ViewController
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
                //Update estado de los asientos
                var operation = (View.CurrentObject as Operation);
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
