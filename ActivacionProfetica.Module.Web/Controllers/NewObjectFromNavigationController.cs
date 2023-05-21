using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class NewObjectFromNavigationController : WindowController
    {
        public NewObjectFromNavigationController()
        {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ShowNavigationItemController showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
        }
        void showNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "NewOperation")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Operation));
                Operation newIssue = objectSpace.CreateObject<Operation>();
                DetailView detailView = Application.CreateDetailView(objectSpace, newIssue);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.ActionArguments.ShowViewParameters.CreatedView = detailView;
                e.Handled = true;
            }
        }
    }
}
