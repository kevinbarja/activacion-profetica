using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using System;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class SendByWhatsappController : ViewController<DetailView>
    {
        SimpleAction sendMessage;
        public SendByWhatsappController()
        {
            TargetViewId = Constants.View.OperationDetailView;

            sendMessage = new SimpleAction(this, "Enviar por WhatsApp", PredefinedCategory.Tools)
            {
                //Specify the Action's button caption.
                Caption = "Enviar por WhatsApp",
                //Specify the confirmation message that pops up when a user executes an Action.
                //ConfirmationMessage = "Are you sure you want to clear the Tasks list?",
                //Specify the icon of the Action's button in the interface.
                //ImageName = "Action_Clear"
            };

            //This event fires when a user clicks the Simple Action control. Handle this event to execute custom code.
            sendMessage.Execute += ClearTasksAction_Execute;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            sendMessage.Active["Hid"] = !ObjectSpace.IsNewObject(View.CurrentObject);
        }

        private void ClearTasksAction_Execute(Object sender, SimpleActionExecuteEventArgs e)
        {
            var operation = (Operation)View.CurrentObject;

            ApplicationUser userPublic = ObjectSpace.FindObject<ApplicationUser>(new BinaryOperator("UserName", "Sam"));

            if (userPublic == null)
            {
                PermissionPolicyRole publicRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == Constants.Role.Public);
                userPublic = ObjectSpace.CreateObject<ApplicationUser>();
                userPublic.UserName = operation.Customer.CI;
                userPublic.FullName = operation.Customer.FullName;
                userPublic.SetPassword(operation.Customer.WhatsApp);
                ObjectSpace.CommitChanges();
                ((ISecurityUserWithLoginInfo)userPublic).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userPublic));
                userPublic.Roles.Add(publicRole);
                ObjectSpace.CommitChanges();
            }


            WebApplication.Redirect("https://api.whatsapp.com/send?phone=59175632256&text=%F0%9F%98%80Lleva%20el%20control%20de%20tu%20plan%2090%20d%C3%ADas%20ingresando%20a%20http%3A%2F%2Flocalhost%3A2064%20tu%20usuario%20es%20tu%20CI%20y%20la%20contrase%C3%B1a%20tu%20n%C3%BAmero%20de%20whatsapp.%20Bendiciones");
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

        }
    }
}
