﻿using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
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

        private void ShowSaveButton(bool disable)
        {
            ModificationsController controlador = Frame.GetController<ModificationsController>();
            controlador.SaveAction.Active.SetItemValue("OnlySaveByStateMachine", disable);
            controlador.SaveAndCloseAction.Active.SetItemValue("OnlySaveByStateMachine", disable);
            controlador.SaveAndNewAction.Active.SetItemValue("OnlySaveByStateMachine", disable);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            //Disable save button
            if (ObjectSpace.IsNewObject(View.CurrentObject))
            {
                ShowSaveButton(false);
            }
        }

        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            var operation = (!(View?.CurrentObject is IObjectRecord)) ?
                View?.CurrentObject as Operation
                : ObjectSpace.GetObject(View?.CurrentObject) as Operation;

            if (!operation.NoCreatePayments)
            {

                if (e.Object is Place || e.PropertyName == nameof(Operation.PaymentPlan))
                {

                    if ((operation.PaymentPlan != null && e.Object is Place) || (operation.PaymentPlan != null && operation.Places.Count > 0))
                    {
                        ObjectSpace.Delete(operation.Payments);
                        operation.Payments.Reload();
                        if (!(e.NewValue is null && operation.Places.Count == 0))
                        {
                            //Add payments
                            foreach (var paymentPlanDetail in operation.PaymentPlan.PaymentPlanDetails)
                            {
                                var payment = new Payment(((XPObjectSpace)ObjectSpace).Session);
                                payment.InternalId = paymentPlanDetail.Number * -1;
                                payment.PaymentPlanDetail = paymentPlanDetail;
                                int factor = ((e.NewValue is null && e.Object is Place) || e.NewValue is PaymentPlan) ? 0 : 1;

                                int placeQuantity = (operation.Places.Count + factor);
                                int amount = paymentPlanDetail.Amount * placeQuantity;
                                payment.Amount = amount;
                                if (paymentPlanDetail.Description.Contains("Alguien"))
                                {
                                    payment.PaymentDate = DateTime.Now;
                                    payment.PaymentMethod = BusinessObjects.Enums.PaymentMethod.Cash;
                                }
                                operation.Payments.Add(payment);
                            }
                        }
                        operation.CallOnChanged(nameof(Operation.Payments));
                    }
                    else
                    {
                        //Clean payments
                        ObjectSpace.Delete(operation.Payments);
                        operation.Payments.Reload();
                        operation.CallOnChanged(nameof(Operation.Payments));
                    }
                }
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            ShowSaveButton(true);
            ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
        }
    }
}
