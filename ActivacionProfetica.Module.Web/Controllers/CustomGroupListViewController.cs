using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using System;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class CustomGroupListViewController : ViewController<ListView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender += CurrentRequestWindow_PagePreRender;
            }
        }
        private void CurrentRequestWindow_PagePreRender(object sender, EventArgs e)
        {
            ASPxGridListEditor gridListEditor = View.Editor as ASPxGridListEditor;
            if (gridListEditor != null)
            {
                string afterGroupRowClickScipt = string.Empty;
                if (gridListEditor.Grid != null)
                {
                    afterGroupRowClickScipt = String.IsNullOrEmpty(gridListEditor.Grid.ClientSideEvents.RowClick) ? "" :
                 String.Format(" else {{ ({0})(s, e); }}", gridListEditor.Grid.ClientSideEvents.RowClick);
                }
                string rowClickHandler = @"function(s, e) {
                  if (s.IsGroupRow(e.visibleIndex)) {
                    s.IsGroupRowExpanded(e.visibleIndex) ? s.CollapseRow(e.visibleIndex) : s.ExpandRow(e.visibleIndex);
                  }" + afterGroupRowClickScipt + @" 
                }";

                if (gridListEditor.Grid != null)
                {
                    gridListEditor.Grid.ClientSideEvents.RowClick = rowClickHandler;
                }
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender -= CurrentRequestWindow_PagePreRender;
            }
        }
    }
}
