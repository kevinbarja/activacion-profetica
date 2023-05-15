using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace ActivacionProfetica.Module.Controllers
{
    public class ListViewPagingController : ViewController<ListView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            ((ASPxGridListEditor)View.Editor).Grid.SettingsPager.PageSize = 200;
        }
    }
}
