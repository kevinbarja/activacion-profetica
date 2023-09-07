using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System.ComponentModel;

namespace ActivacionProfetica.Module.Web.Controllers
{
    public class SaveController : ViewController<DetailView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            ModificationsController controlador = Frame.GetController<ModificationsController>();
            controlador.SaveAction.Executing += new CancelEventHandler(ValidarGuardar);
            controlador.SaveAndCloseAction.Executing += new CancelEventHandler(ValidarGuardar);
            controlador.SaveAndNewAction.Executing += new CancelEventHandler(ValidarGuardar);
        }

        protected override void OnDeactivated()
        {
            ModificationsController controlador = Frame.GetController<ModificationsController>();
            controlador.SaveAction.Executing -= new CancelEventHandler(ValidarGuardar);
            controlador.SaveAndCloseAction.Executing -= new CancelEventHandler(ValidarGuardar);
            controlador.SaveAndNewAction.Executing -= new CancelEventHandler(ValidarGuardar);

            base.OnDeactivated();
        }

        private void ValidarGuardar(object sender, CancelEventArgs e)
        {
            MessageOptions parameters = new MessageOptions
            {
                Duration = 3000,
                Message = "Guardado correctamente",
                Type = InformationType.Success
            };
            parameters.Web.Position = InformationPosition.Top;
            Application.ShowViewStrategy.ShowMessage(parameters);
        }
    }
}
