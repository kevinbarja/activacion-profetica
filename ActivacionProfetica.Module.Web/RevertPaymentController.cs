using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using System;

namespace ActivacionProfetica.Module.Web
{
    public class RevertPaymentController : ViewController<DetailView>
    {
        SimpleAction revertPaymentAction;

        public RevertPaymentController()
        {
            TargetViewId = "Payment_DetailView";
            revertPaymentAction = new SimpleAction(this, "revertPaymentAction", PredefinedCategory.Tools)
            {
                //Specify the Action's button caption.
                Caption = "Revertir pago",
                //Specify the confirmation message that pops up when a user executes an Action.
                //ConfirmationMessage = "Are you sure you want to clear the Tasks list?",
                //Specify the icon of the Action's button in the interface.
                //ImageName = "Action_Clear"
            };

            //This event fires when a user clicks the Simple Action control. Handle this event to execute custom code.
            revertPaymentAction.Execute += ClearTasksAction_Execute;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            var currentPayment = View.CurrentObject as Payment;

            revertPaymentAction.Active["Hid"] = !ObjectSpace.IsNewObject(View.CurrentObject) && currentPayment.UsuarioActualEsSupervisor;
        }

        private void ClearTasksAction_Execute(Object sender, SimpleActionExecuteEventArgs e)
        {
            var currentPayment = View.CurrentObject as Payment;
            currentPayment.IsReverted = true;
            currentPayment.PaymentMethod = BusinessObjects.Enums.PaymentMethod.None;
            View.ObjectSpace.CommitChanges();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

        }
    }
}

