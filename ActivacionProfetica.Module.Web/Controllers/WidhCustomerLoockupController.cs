using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class WidhCustomerLoockupController : ViewController<ListView>
    {
        public WidhCustomerLoockupController()
        {
            TargetViewId = "Customer_LookupListView";
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            ASPxGridListEditor gridListEditor = View.Editor as ASPxGridListEditor;
            if (gridListEditor != null)
            {
                ASPxGridView gridView = gridListEditor.Grid;
                gridView.Settings.UseFixedTableLayout = true;
                gridView.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
                gridView.Width = Unit.Percentage(100);
                foreach (WebColumnBase column in gridView.Columns)
                {
                    IColumnInfo columnInfo = ((IDataItemTemplateInfoProvider)gridListEditor).GetColumnInfo(column);
                    if (columnInfo != null)
                    {
                        IModelColumn modelColumn = (IModelColumn)columnInfo.Model;
                        column.Width = Unit.Pixel(modelColumn.Width);
                    }
                }
            }
        }
    }
}
