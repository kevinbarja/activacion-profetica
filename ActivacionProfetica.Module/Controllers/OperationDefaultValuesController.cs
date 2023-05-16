using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace ActivacionProfetica.Module.Controllers
{
    public class OperationDefaultValuesController : ViewController
    {
        private NewObjectViewController controller;
        protected override void OnActivated()
        {
            base.OnActivated();
            controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                controller.ObjectCreated += controller_ObjectCreated;
            }
        }
        void controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            //if (e.CreatedObject is Task)
            //{
            //    ((Task)e.CreatedObject).StartDate = DateTime.Now;
            //}
        }
        protected override void OnDeactivated()
        {
            if (controller != null)
            {
                controller.ObjectCreated -= controller_ObjectCreated;
            }
            base.OnDeactivated();
        }
    }
}
