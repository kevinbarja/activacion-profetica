using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using System.Linq;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class OpenPaymentController : ViewController<DetailView>
    {
        PopupWindowShowAction openPayementAction;

        public OpenPaymentController()
        {
            TargetViewId = Constants.View.OperationDetailView;

            openPayementAction = new PopupWindowShowAction(this, "SiembraAbierta", PredefinedCategory.View);
            openPayementAction.ImageName = "NewOrder";
            openPayementAction.Caption = "Siembra abierta";

            openPayementAction.Execute += PopupWindow_Execute;
            openPayementAction.CustomizePopupWindowParams += OpenPaymentAction_CustomizePopupWindowParams;
        }

        private void OpenPaymentAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(OpenPaymentWindowsParameters));
            OpenPaymentWindowsParameters parameters = objectSpace.CreateObject<OpenPaymentWindowsParameters>();
            //objectSpace.CommitChanges();

            e.DialogController.SaveOnAccept = false;
            DetailView confirmationDetailView = Application.CreateDetailView(objectSpace, parameters);
            confirmationDetailView.Caption = "Siembra abierta";
            confirmationDetailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.View = confirmationDetailView;
            e.DialogController.Accepting += DialogController_Accepting;
        }

        private void PopupWindow_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var openPayment = e.PopupWindowViewCurrentObject as OpenPaymentWindowsParameters;

            var operation = (!(View?.CurrentObject is IObjectRecord)) ?
                View?.CurrentObject as Operation
                : ObjectSpace.GetObject(View?.CurrentObject) as Operation;

            // Obtener los pagos de manera ordenada
            var payments = operation.Payments
                .Where(p => p.PaymentMethod == PaymentMethod.None)
                .Select(p => p)
                .OrderBy(p => p.Description)
                .ToList();

            var debt = payments.Sum(p => p.Amount);

            // Validate amount
            if (openPayment.Amount > debt)
            {
                throw new UserFriendlyException($"No puede sembrar más de {debt}");
            }

            if (openPayment.PaymentMethod == PaymentMethod.None)
            {
                throw new UserFriendlyException($"Debe seleccionar un método de siembra");
            }

            void localFunction(int creditAux, int paymentsCountAux, ref List<Payment> paymentsAux)
            {
                int i = 0;
                foreach (var paymentAux in paymentsAux)
                {
                    if (creditAux > 0)
                    {
                        if (creditAux <= paymentAux.Amount)
                        {
                            if (creditAux < paymentAux.Amount)
                            {
                                var parcialPaymentsCount = operation.Payments.Where(pa => pa.PaymentPlanDetail.InternalId == paymentAux.PaymentPlanDetail.InternalId).Count();

                                var newPayment = new Payment(((XPObjectSpace)ObjectSpace).Session)
                                {
                                    InternalId = (++i + paymentsCountAux) * -1,
                                    PaymentPlanDetail = paymentAux.PaymentPlanDetail,
                                    Amount = creditAux,
                                    Description = paymentAux.PaymentPlanDetail.Description + $" (Siembra abierta {parcialPaymentsCount})",
                                    Number = 0m,
                                    PaymentMethod = openPayment.PaymentMethod
                                };
                                operation.Payments.Add(newPayment);

                                paymentAux.Description = paymentAux.PaymentPlanDetail.Description + $" (Siembra abierta {++parcialPaymentsCount})";
                                paymentAux.Amount = paymentAux.Amount - creditAux;
                            }
                            else
                            {
                                paymentAux.PaymentMethod = openPayment.PaymentMethod;
                            }
                            creditAux = 0;
                        }
                        else
                        {
                            paymentAux.PaymentMethod = openPayment.PaymentMethod;
                            creditAux = paymentAux.Amount - creditAux;
                            localFunction(creditAux, paymentsCountAux, ref paymentsAux);
                        }
                    }
                }
            }
            var paymentsCount = operation.Payments.Count;
            var credit = openPayment.Amount;
            localFunction(credit, paymentsCount, ref payments);

            operation.CallOnChanged(nameof(Operation.Payments));

            //MessageOptions parametrosMensaje = new MessageOptions
            //{
            //    Duration = 4000,
            //    Message = $"Debe introducir el monto a sembrar",
            //    Type = InformationType.Warning
            //};
            //parametrosMensaje.Web.Position = InformationPosition.Top;
            //Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
            //e.CanCloseWindow = false;
            //return;
        }


        private void DialogController_Accepting(object sender, DevExpress.ExpressApp.SystemModule.DialogControllerAcceptingEventArgs e)
        {
            var operation = (View.CurrentObject as Operation);

            //Validate plan
            if (operation.PaymentPlan is null || operation.Payments is null || operation.Payments.Count() == 0)
            {
                throw new UserFriendlyException($"La operación debe tener asiganado un plan de financiamiento");
            }
        }
    }
}
