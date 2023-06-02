using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class PlaceLookupListViewController : ViewController<ListView>
    {
        public PlaceLookupListViewController()
        {
            TargetViewId = "Operation_Places_LookupListView";
        }

        private ASPxGridView Grid
        {
            get
            {
                ASPxGridListEditor editor = View != null ? (View as ListView).Editor as ASPxGridListEditor : null;
                if (editor != null)
                    return editor.Grid;

                return null;
            }
        }

        public const string keyFieldName = "SaleItemId";
        public const string gridId = "PurchaseSummaryListViewGrid";

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (Grid != null)
            {
                // Add checkboxes to group rows
                Grid.ClientInstanceName = gridId;
                Grid.Templates.GroupRowContent = new CheckboxGroupRowContentTemplate(gridId);
                Grid.CustomCallback += Grid_CustomCallback;
                //Grid.HtmlRowPrepared += Grid_HtmlRowPrepared;
            }
        }

        protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] parameters = e.Parameters.Split(';');

            int index = int.Parse(parameters[0]);
            if (Grid.IsGroupRow(index))
            {
                if (Grid.IsRowExpanded(index))
                {
                    Grid.CollapseRow(index, false);
                }
                else
                {
                    Grid.ExpandRow(index, false);
                }
            }

        }
    }
}
