using DevExpress.Web;
using System.Web.UI;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class CheckboxGroupRowContentTemplate : ITemplate
    {
        /// <summary>
        /// Grid id for usng in inner scripts
        /// </summary>
        string gridId = "";

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gridId">Grid id for usng in inner scripts</param>
        public CheckboxGroupRowContentTemplate(string gridId)
        {
            this.gridId = gridId;
        }

        public void InstantiateIn(Control container)
        {
            // Add caption
            ASPxLabel label = new ASPxLabel();
            var gvContainer = (GridViewGroupRowTemplateContainer)container;
            label.Text = GetCaptionText(gvContainer);
            label.EncodeHtml = false;
            //label.ClientSideEvents.Click =
            gvContainer.Grid.ClientSideEvents.RowClick =
             string.Format(
                "function(s, e){{ {0}.PerformCallback(e.visibleIndex) }}",
                gridId,
                gvContainer.VisibleIndex);

            container.Controls.Add(label);
        }

        /// <summary>
        /// Get caption text for container
        /// </summary>
        /// <param name="container">Container</param>
        /// <returns>Caption</returns>
        private string GetCaptionText(GridViewGroupRowTemplateContainer container)
        {
            string captionText = !string.IsNullOrEmpty(container.Column.Caption) ? container.Column.Caption : container.Column.FieldName;

            return " " + string.Format("{1}", captionText, container.GroupText, container.SummaryText);
        }
    }
}
