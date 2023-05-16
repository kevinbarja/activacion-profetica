using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Linq;

namespace ActivacionProfetica.Module.Controllers
{
    public class OperationController : ViewController<ListView>
    {
        private DialogController controladorDialogo;

        /// <summary>
        /// Establecer el contexto del controlador a la vista lookup de las cuentas contables.
        /// </summary>
        public OperationController()
        {
            TargetViewId = "None";// Constants.View.OperationPlacesLookupListView;
        }

        /// <summary>
        /// Suscribirse al evento relacionado a la acción aceptar o click
        /// del lookup.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
            controladorDialogo = Frame.GetController<DialogController>();

            if (controladorDialogo != null)
            {
                controladorDialogo.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(ValidarSeleccion);
            }

            //Simular click en el botón buscar con el fin de que carge la información automáticamente.
            Frame.GetController<FilterController>().FullTextFilterAction.DoExecute("");
        }

        private void ValidarSeleccion(object sender, DialogControllerAcceptingEventArgs e)
        {
            ArrayList selectedPlaces = new ArrayList();
            if ((View.SelectedObjects.Count > 0) && (View.SelectedObjects[0] is IObjectRecord))
            {
                foreach (var selectedPlace in View.SelectedObjects)
                {
                    selectedPlaces.Add((Place)ObjectSpace.GetObject(selectedPlace));
                }
            }
            else
            {
                selectedPlaces = (ArrayList)View.SelectedObjects;
            }
            foreach (Place placeSelected in selectedPlaces)
            {
                using (UnitOfWork uow = new UnitOfWork(placeSelected.Session.DataLayer))
                {
                    var currentplaceSelected = uow.GetObjectByKey<Place>(placeSelected.Id);

                    if (currentplaceSelected.ChildrenPlace != null && currentplaceSelected.ChildrenPlace.Any())
                    {
                        MessageOptions parametrosMensaje = new MessageOptions
                        {
                            Duration = 4000,
                            Message = $"El lugar '{placeSelected.Path}' no es un asiento, revise e intente nuevamente.",
                            Type = InformationType.Warning
                        };
                        parametrosMensaje.Web.Position = InformationPosition.Top;
                        Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
                        e.Cancel = true;
                    }
                    else if (currentplaceSelected.Operation != null && currentplaceSelected.Operation.InternalId != 0)
                    {
                        MessageOptions parametrosMensaje = new MessageOptions
                        {
                            Duration = 4000,
                            Message = $"El asiento '{placeSelected.Path}' no está disponible",
                            Type = InformationType.Warning
                        };
                        parametrosMensaje.Web.Position = InformationPosition.Top;
                        Application.ShowViewStrategy.ShowMessage(parametrosMensaje);
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// De-suscribirse a los eventos, liberar referencias y recursos.
        /// </summary>
        protected override void OnDeactivated()
        {
            if (controladorDialogo != null)
            {
                controladorDialogo.Accepting -= new EventHandler<DialogControllerAcceptingEventArgs>(ValidarSeleccion);
            }
            base.OnDeactivated();
        }
    }
}
