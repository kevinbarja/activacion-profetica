using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System.Collections.Generic;

namespace ActivacionProfetica.Module.Controllers
{
    public class MyFilterController : ViewController
    {
        public MyFilterController()
        {
            TargetObjectType = typeof(Customer);
        }
        private FilterController standardFilterController;
        protected override void OnActivated()
        {
            base.OnActivated();
            standardFilterController = Frame.GetController<FilterController>();
            if (standardFilterController != null)
            {
                standardFilterController.CustomGetFullTextSearchProperties += standardFilterController_CustomGetFullTextSearchProperties;
            }
        }
        void standardFilterController_CustomGetFullTextSearchProperties(object sender,
    CustomGetFullTextSearchPropertiesEventArgs e)
        {
            foreach (string property in GetFullTextSearchProperties())
            {
                e.Properties.Add(property);
            }
            e.Handled = true;
        }
        private List<string> GetFullTextSearchProperties()
        {
            List<string> searchProperties = new List<string>();
            searchProperties.Add("CI");
            return searchProperties;
        }
        protected override void OnDeactivated()
        {
            if (standardFilterController != null)
            {
                standardFilterController.CustomGetFullTextSearchProperties -= standardFilterController_CustomGetFullTextSearchProperties;
            }
            base.OnDeactivated();
        }
    }
}
