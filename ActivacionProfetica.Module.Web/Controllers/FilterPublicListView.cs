using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class FilterPublicListView : ViewController<ListView>
    {
        public FilterPublicListView()
        {
            TargetViewId = "PublicOperation_ListView";
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            var currentUser = ObjectSpace.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
            View.CollectionSource.Criteria["FilterCurrentCustomer"] = CriteriaOperator.Parse($"[Customer.CI] = '{currentUser.UserName}'");
        }
    }
}
