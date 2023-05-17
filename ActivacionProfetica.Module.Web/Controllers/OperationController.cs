using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Web.SystemModule;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class OperationController : WebLinkUnlinkController
    {
        public OperationController()
        {
            TargetViewId = Constants.View.OperationDetailView;
        }

        protected override void Link(PopupWindowShowActionExecuteEventArgs args)
        {
            base.Link(args);
            //TODO
        }

        protected override void Unlink(SimpleActionExecuteEventArgs args)
        {
            base.Unlink(args);

            foreach (object item in args.SelectedObjects)
            {
                Place places = item as Place;

            }
        }
    }
}
