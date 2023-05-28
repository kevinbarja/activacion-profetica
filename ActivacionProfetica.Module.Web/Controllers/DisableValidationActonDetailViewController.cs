using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Validation.AllContextsView;
using DevExpress.ExpressApp.Web.SystemModule;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class DisableValidationActonDetailViewController : ViewController<DetailView>
    {
        private RefreshController refreshController;
        private ShowAllContextsController controller;
        private WebDeleteObjectsViewController deleteController;
        private WebRecordsNavigationController navController;
        private WebNewObjectViewController newController;

        const string deactivateReason = "HiddenInDetailView";

        public DisableValidationActonDetailViewController()
        {
            TargetViewId = Constants.View.OperationDetailView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            controller = Frame.GetController<ShowAllContextsController>();
            if (controller != null)
            {
                controller.ValidateAction.Active.SetItemValue(deactivateReason, false);
            }
            deleteController = Frame.GetController<WebDeleteObjectsViewController>();
            if (deleteController != null)
            {
                deleteController.DeleteAction.Active.SetItemValue(deactivateReason, false);
            }

            refreshController = Frame.GetController<RefreshController>();
            if (refreshController != null)
            {
                refreshController.RefreshAction.Active.SetItemValue(deactivateReason, false);
            }

            navController = Frame.GetController<WebRecordsNavigationController>();
            if (refreshController != null)
            {
                navController.NextObjectAction.Active.SetItemValue(deactivateReason, false);
                navController.PreviousObjectAction.Active.SetItemValue(deactivateReason, false);
            }

            newController = Frame.GetController<WebNewObjectViewController>();
            if (newController != null)
            {
                newController.NewObjectAction.Active.SetItemValue(deactivateReason, false);
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (controller != null)
            {
                controller.ValidateAction.Active.RemoveItem(deactivateReason);
                controller = null;
            }
            if (deleteController != null)
            {
                deleteController.DeleteAction.Active.RemoveItem(deactivateReason);
                deleteController = null;
            }
            if (refreshController != null)
            {
                refreshController.RefreshAction.Active.RemoveItem(deactivateReason);
                refreshController = null;
            }
            if (navController != null)
            {
                navController.NextObjectAction.Active.RemoveItem(deactivateReason);
                navController.PreviousObjectAction.Active.RemoveItem(deactivateReason);
                refreshController = null;
            }
            if (newController != null)
            {
                newController.NewObjectAction.Active.RemoveItem(deactivateReason);
                refreshController = null;
            }
        }
    }
}
