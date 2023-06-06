using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using System;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class RevertPaymentOperationController : ViewController<DetailView>
    {
        SimpleAction revertPaymentAction;

        public RevertPaymentOperationController()
        {
            TargetViewId = Constants.View.OperationDetailView;
            revertPaymentAction = new SimpleAction(this, "revertPaymentActionOnOperation", PredefinedCategory.Tools)
            {
                Caption = "Revertir pagos",
            };
            revertPaymentAction.Execute += ClearTasksAction_Execute;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            var operation = (View.CurrentObject as Operation);
            revertPaymentAction.Active["Hid"] = !ObjectSpace.IsNewObject(View.CurrentObject) && operation.UsuarioActualEsSupervisor;
        }

        private void ClearTasksAction_Execute(Object sender, SimpleActionExecuteEventArgs e)
        {
            //Update estado de los asientos
            var operation = (View.CurrentObject as Operation);
            operation.NoCreatePayments = true;
            foreach (var currentPayment in operation.Payments)
            {
                currentPayment.IsReverted = true;
                currentPayment.PaymentMethod = BusinessObjects.Enums.PaymentMethod.None;
            }
            View.ObjectSpace.CommitChanges();
        }

    }
}
