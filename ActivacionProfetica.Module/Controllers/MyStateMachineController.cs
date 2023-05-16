using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
using System;

namespace ActivacionProfetica.Module.Controllers
{
    public partial class MyStateMachineController : ViewController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<StateMachineController>().TransitionExecuted += OnTransitionExecuted;
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.GetController<StateMachineController>().TransitionExecuted -= OnTransitionExecuted;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            Frame.GetController<StateMachineController>().ChangeStateAction.Active["showAction"] = true;
            //.RemoveItem("ViewIsInEditMode");
        }
        private void OnTransitionExecuted(object sender, ExecuteTransitionEventArgs e)
        {
            try
            {
                View.ObjectSpace.CommitChanges();
            }
            catch (Exception ex)
            {
                View.ObjectSpace.Rollback();
                //ErrorHandling.Instance.SetPageError(ex);
            }
        }
    }
}
