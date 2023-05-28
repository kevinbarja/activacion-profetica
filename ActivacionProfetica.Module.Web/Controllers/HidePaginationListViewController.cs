using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class HidePaginationListViewController : ViewController<ListView>
    {
        public HidePaginationListViewController()
        {
            TargetViewId = "Operation_Places_ListView;Operation_Payments_ListView";
        }

        private void grid_Load(object sender, EventArgs e)
        {
            ASPxGridView gv = sender as ASPxGridView;
            gv.SettingsPager.Visible = false;
            gv.SettingsPager.ShowDisabledButtons = true;
            gv.SettingsPager.PageSize = 150;
            gv.SettingsPager.PageSizeItemSettings.Visible = true;
            gv.Settings.ShowColumnHeaders = true;
            gv.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;
            //gv.DetailRows.ExpandAllRows();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            var listView = (ListView)View;
            var grid = ((ASPxGridListEditor)listView.Editor).Grid;
            if (grid != null)
            {
                grid.Load += grid_Load;
                grid.DetailRows.ExpandAllRows();
            }

            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            var listView = (ListView)View;
            var grid = ((ASPxGridListEditor)listView.Editor).Grid;
            if (grid != null)
                grid.Load -= grid_Load;
            base.OnDeactivated();
        }
    }
}
