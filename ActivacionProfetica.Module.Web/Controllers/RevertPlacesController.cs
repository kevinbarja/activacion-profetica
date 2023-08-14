using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Linq;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class RevertPlacesController : ViewController<DetailView>
    {
        PopupWindowShowAction revertPlacesAction;


        public RevertPlacesController()
        {
            TargetViewId = Constants.View.OperationDetailView;
            revertPlacesAction = new PopupWindowShowAction(this, "RevertirAsientos", PredefinedCategory.View);
            revertPlacesAction.ImageName = "Undo";
            revertPlacesAction.CustomizePopupWindowParams += RevertPlacesAction_CustomizePopupWindowParams;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            var operation = (View.CurrentObject as Operation);
            ISecurityUserWithRoles currentUser = (ISecurityUserWithRoles)SecuritySystem.CurrentUser;
            revertPlacesAction.Active["Hide"] = !ObjectSpace.IsNewObject(View.CurrentObject) && currentUser.IsUserInRole(Constants.Role.RevertPayments);
        }

        private void RevertPlacesAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace objectSpace =
            (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(ConfirmationWindowParameters));
            ConfirmationWindowParameters parameters =
            objectSpace.CreateObject<ConfirmationWindowParameters>();
            parameters.Mensaje = "¿Está seguro que desea revertir los asientos?. Esta acción no modificará los pagos realizados. Sólo cambiará el estado de los asientos a DISPONIBLE.";
            objectSpace.CommitChanges();
            DetailView confirmationDetailView = Application.CreateDetailView(objectSpace, parameters);
            confirmationDetailView.Caption = "Confirmar reversión de asientos";
            confirmationDetailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
            e.View = confirmationDetailView;
            e.DialogController.Accepting += DialogController_Accepting;
        }

        private void DialogController_Accepting(object sender, DevExpress.ExpressApp.SystemModule.DialogControllerAcceptingEventArgs e)
        {
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
                Message = "Asientos revertidos correctamente",
                Type = InformationType.Success
            };
            parameters.Web.Position = InformationPosition.Top;
            Application.ShowViewStrategy.ShowMessage(parameters);
            //WebWindow.CurrentRequestWindow.RegisterStartupScript("CustomNavigate", "setTimeout(function(){ window.top.location.reload(); }, 1800);");
        }
    }
}
