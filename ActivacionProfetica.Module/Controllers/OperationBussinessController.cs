using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using System;

namespace ActivacionProfetica.Module.Controllers
{
    public class OperationBussinessController : BusinessObjectViewController<DetailView, Operation>
    {


        public override void OnCurrentObjectChanged(DetailView view, CurrentObjectChangedEventArgs<Operation> args)
        {
            base.OnCurrentObjectChanged(view, args);
            if (operation.PaymentPlan != null && operation.Places.Count > 0)
            {
                //Add payments
                foreach (var paymentPlanDetail in operation.PaymentPlan.PaymentPlanDetails)
                {
                    var payment = ObjectSpace.CreateObject<Payment>();
                    payment.PaymentPlanDetail = paymentPlanDetail;
                    if (paymentPlanDetail.LimitDate == null)
                    {
                        payment.PaymentDate = DateTime.Now;
                    }
                    int totalAmount = operation.Places.Count * operation.Sector.Amount;
                    payment.Amount = (int)(paymentPlanDetail.Percentage * totalAmount);

                    operation.Payments.Add(payment);
                }
                operation.CallOnChanged(nameof(Operation.Payments));
            }
            CurrentObject.Payments
        }
    }
}
