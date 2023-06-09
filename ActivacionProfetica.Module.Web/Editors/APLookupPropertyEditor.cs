using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System;
using System.Web.UI.WebControls;

namespace ActivacionProfetica.Module.Web.Editors
{
    [PropertyEditor(typeof(IAPLookupView), true)]
    public class APLookupPropertyEditor : ASPxGridLookupPropertyEditor
    {
        public APLookupPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {

        }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (ViewEditMode == ViewEditMode.Edit)
            {
                ASPxGridLookup gridControl = control as ASPxGridLookup;

                ASPxGridView gridView = gridControl.GridView;
                gridView.Settings.UseFixedTableLayout = true;
                gridView.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
                gridView.Width = Unit.Percentage(100);
                foreach (WebColumnBase column in gridView.Columns)
                {
                    if (column.Caption == "CI")
                    {
                        column.Width = Unit.Percentage(20);
                    }
                    else
                    {
                        column.Width = Unit.Percentage(80);
                    }

                }
            }
        }
    }
}
