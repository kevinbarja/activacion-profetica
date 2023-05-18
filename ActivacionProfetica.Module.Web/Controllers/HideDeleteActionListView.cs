using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class HideDeleteActionListView : ViewController<ListView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            DeleteObjectsViewController deleteController =
                Frame.GetController<DeleteObjectsViewController>();

            if (deleteController != null)
            {
                deleteController.Active["Hide"] = false;
            }
        }
    }
}
