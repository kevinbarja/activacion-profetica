using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using System;

namespace ActivacionProfetica.Module.Web.Editors
{
    [PropertyEditor(typeof(IAPLookupView), true)]
    public class APLookupPropertyEditor : ASPxGridLookupPropertyEditor
    {
        public APLookupPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {

        }
    }
}
