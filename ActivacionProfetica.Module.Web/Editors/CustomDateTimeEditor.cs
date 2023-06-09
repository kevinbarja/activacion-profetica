using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System;
using System.Web.UI.WebControls;

namespace ActivacionProfetica.Module.Web.Editors
{
    [PropertyEditor(typeof(DateTime), true)]
    public class CustomDateTimeEditor : ASPxDateTimePropertyEditor
    {
        public CustomDateTimeEditor(Type objectType, IModelMemberViewItem info) :
            base(objectType, info)
        { }
        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (ViewEditMode == ViewEditMode.Edit)
            {
                ASPxDateEdit dateEdit = (ASPxDateEdit)control;
                dateEdit.TimeSectionProperties.Visible = true;
                dateEdit.UseMaskBehavior = true;
            }
        }
    }
}
