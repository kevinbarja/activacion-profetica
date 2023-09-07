using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
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
            openPayementAction.ImageName = "Undo";
            openPayementAction.CustomizePopupWindowParams += OpenPaymentAction_CustomizePopupWindowParams;
        }

        private void OpenPaymentAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(OpenPaymentWindowsParameters));
            OpenPaymentWindowsParameters parameters = objectSpace.CreateObject<OpenPaymentWindowsParameters>();
            //parameters.Mensaje = "¿Está seguro que desea liberar los asientos?. Esta acción no modificará los pagos realizados. Sólo cambiará el estado de los asientos a DISPONIBLE.";
            //objectSpace.CommitChanges();
            DetailView confirmationDetailView = Application.CreateDetailView(objectSpace, parameters);
            confirmationDetailView.Caption = "Siembra Abierta";
            confirmationDetailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.View = confirmationDetailView;
            e.DialogController.Accepting += DialogController_Accepting;
        }

        private void DialogController_Accepting(object sender, DevExpress.ExpressApp.SystemModule.DialogControllerAcceptingEventArgs e)
        {
            var operation = (View.CurrentObject as Operation);
            var availablePlaceStatus = (from s in ((XPObjectSpace)View.ObjectSpace).Session.Query<PlaceStatus>()
                                        where s.InternalId == PlaceStatus.AvailablePlaceStatus
                                        select s).Single();

            operation.PlaceStatus = availablePlaceStatus;
            View.ObjectSpace.CommitChanges();
            /*
            var operation = (View.CurrentObject as Operation);
            operation.NoCreatePayments = true;
            var availablePlaceStatus = (from s in ((XPObjectSpace)View.ObjectSpace).Session.Query<PlaceStatus>()
                                        where s.InternalId == PlaceStatus.AvailablePlaceStatus
                                        select s).Single();

            operation.PlaceStatus = availablePlaceStatus;
            foreach (var place in operation.Places)
            {
                place.Status = availablePlaceStatus;
                place.PlacesIsReverted = true;
            }

            foreach (var currentPayment in operation.Payments)
            {
                currentPayment.PlacesIsReverted = true;
            }
            View.ObjectSpace.CommitChanges();
            MessageOptions parameters = new MessageOptions
            {
                Duration = 3000,
                Message = "Asientos liberados correctamente",
                Type = InformationType.Success
            };
            parameters.Web.Position = InformationPosition.Top;
            Application.ShowViewStrategy.ShowMessage(parameters);
            */
            //WebWindow.CurrentRequestWindow.RegisterStartupScript("CustomNavigate", "setTimeout(function(){ window.top.location.reload(); }, 1800);");
        }

    }
}
