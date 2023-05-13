using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Xpo;

namespace ActivacionProfetica.Module.Controllers
{
    /// https://docs.devexpress.com/CodeRushForRoslyn/403133/coding-assistance/code-templates/xaf-templates
    public class AuditController : ViewController<DetailView>
    {
        PopupWindowShowAction auditAction;

        public AuditController() : base()
        {
            auditAction = new PopupWindowShowAction(this, "Auditoría", "View");
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
                showAction = AuditTrailService.Instance.Settings.IsTypeToAudit(View.CurrentObject.GetType());
            }
            auditAction.Active["showAction"] = showAction;
        }
    }
}
