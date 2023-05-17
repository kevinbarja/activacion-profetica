using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using System;

namespace ActivacionProfetica.Module.Controllers
{
    public class OperationDetailViewController : ViewController<DetailView>
    {
        public OperationDetailViewController()
        {
            TargetViewId = Constants.View.OperationDetailView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        }

        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (e.Object is Place || e.PropertyName == nameof(Operation.PaymentPlan))
            {
                var operation = (!(View?.CurrentObject is IObjectRecord)) ?
                    View?.CurrentObject as Operation
                    : ObjectSpace.GetObject(View?.CurrentObject) as Operation;

                if ((operation.PaymentPlan != null && e.Object is Place) || (operation.PaymentPlan != null && operation.Places.Count > 0))
                {
                    operation.Payments.Reload();
                    if (!(e.NewValue is null && operation.Places.Count == 0))
                    {
                        //Add payments
                        foreach (var paymentPlanDetail in operation.PaymentPlan.PaymentPlanDetails)
                        {
                            var payment = new Payment(((XPObjectSpace)ObjectSpace).Session);
                            payment.PaymentPlanDetail = paymentPlanDetail;
                            if (paymentPlanDetail.LimitDate == null)
                            {
                                payment.PaymentDate = DateTime.Now;
                            }
                            int factor = ((e.NewValue is null && e.Object is Place) || e.NewValue is PaymentPlan) ? 0 : 1;
                            int totalAmount = (operation.Places.Count + factor) * operation.Sector.Amount;
                            payment.Amount = (int)(paymentPlanDetail.Percentage * totalAmount);

                            operation.Payments.Add(payment);
                        }
                    }
                    operation.CallOnChanged(nameof(Operation.Payments));
                }
                else
                {
                    //Clean payments
                    operation.Payments.Reload();
                    operation.CallOnChanged(nameof(Operation.Payments));
                }
                /*
                //var session = ((XPObjectSpace)this.ObjectSpace).Session;

                var operation = View.CurrentObject as Operation;
                //Payments.Reload();
                foreach (var paymentPlanDetail in operation.PaymentPlan.PaymentPlanDetails)
                {
                    var payment = new Payment(operation.Session);
                    payment.PaymentPlanDetail = paymentPlanDetail;
                    if (paymentPlanDetail.LimitDate == null)
                    {
                        payment.PaymentDate = DateTime.Now;
                    }
                    int totalAmount = operation.Places.Count * operation.Sector.Amount;
                    payment.Amount = (int)(paymentPlanDetail.Percentage * totalAmount);

                    operation.Payments.Add(payment);
                }
                //operation.CallOnChanged(nameof(Operation.Payments));
                */
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
        }
    }
}
