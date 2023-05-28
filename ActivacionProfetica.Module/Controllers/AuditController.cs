using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;

namespace ActivacionProfetica.Module.Controllers
{
    /// https://docs.devexpress.com/CodeRushForRoslyn/403133/coding-assistance/code-templates/xaf-templates
    public class AuditController : ViewController<DetailView>
    {
        PopupWindowShowAction auditAction;

        public AuditController() : base()
        {
            auditAction = new PopupWindowShowAction(this, "Auditoría", PredefinedCategory.View);
            auditAction.CustomizePopupWindowParams += AuditAction_CustomizePopupWindowParams;
        }

        private void AuditAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            // Set the e.View parameter to a newly created view (https://docs.devexpress.com/eXpressAppFramework/112723/).
            CollectionSource datos = new CollectionSource(Application.CreateObjectSpace(typeof(BaseAuditDataItemPersistent)),
                typeof(BaseAuditDataItemPersistent));
            GroupOperator filter = new GroupOperator(GroupOperatorType.And);
            filter.Operands.Add(new BinaryOperator("AuditedObject.TargetType",
                ((XPObjectSpace)ObjectSpace).Session.GetObjectType(View.CurrentObject)));
            filter.Operands.Add(new BinaryOperator("AuditedObject.TargetKey",
                XPWeakReference.KeyToString(ObjectSpace.GetKeyValue(View.CurrentObject))));
            datos.Criteria["CurrentObject"] = filter;

            ListView auditListView = Application.CreateListView("Audit_ListView", datos, false);
            e.View = auditListView;
        }

        protected override void OnActivated()
        {

            base.OnActivated();
            bool showAction = false;
            if (View.CurrentObject != null)
            {
                showAction = AuditTrailService.Instance.Settings.IsTypeToAudit(View.CurrentObject.GetType())
                    &&
                    !ObjectSpace.IsNewObject(View.CurrentObject)
                    && CurrentUserIsSupervisor();
            }
            auditAction.Active["showAction"] = showAction;
        }

        private bool CurrentUserIsSupervisor()
        {
            var currentUser = SecuritySystem.CurrentUser;
            if (currentUser is ApplicationUser)
            {
                ApplicationUser currentAppUser = currentUser as ApplicationUser;
                return currentAppUser.Roles.Any(r => r.Name == Role.OperationSupervisor);
            }
            {
                return false;
            }
        }
    }
}
