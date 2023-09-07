using ActivacionProfetica.Module.BusinessObjects;
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
            if (View.CurrentObject is Operation && ((Operation)View.CurrentObject).PlaceStatus.InternalId == PlaceStatus.AvailablePlaceStatus)
            {
                MessageOptions parameters2 = new MessageOptions
                {
                    Duration = 5000,
                    Message = "No puede guardar la operación sin previamente indicar si es Reserva, Venta, etc. Presione los botones inferiores.",
                    Type = InformationType.Warning
                };
                parameters2.Web.Position = InformationPosition.Top;
                Application.ShowViewStrategy.ShowMessage(parameters2);
                e.Cancel = true;
            }
            else
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
}
