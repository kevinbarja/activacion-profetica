using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Xpo;
using System;
using System.Linq;
using System.Text;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class OperationTypeData : BaseData
    {
        public OperationTypeData(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<OperationType>()
                             select ps).Any();

            if (isEmpty)
            {
                var venta = Updater.ObjectSpace.CreateObject<OperationType>();
                venta.InternalId = OperationType.VentaOperationType;
                venta.Name = "Venta";
                venta.PlaceStatusName = "Vendido";
                StringBuilder descriptionVentaOperationType = new StringBuilder("- Venta al contado.");
                descriptionVentaOperationType.Append(Environment.NewLine);
                descriptionVentaOperationType.AppendLine("- Todos los sectores pueden ser adquiridos al contado.");
                venta.Description = descriptionVentaOperationType.ToString();

                var reserva = Updater.ObjectSpace.CreateObject<OperationType>();
                reserva.InternalId = OperationType.ReservaOperationType;
                reserva.Name = "Reserva";
                reserva.PlaceStatusName = "Reservado";
                StringBuilder descriptionReservaOperationType = new StringBuilder("- Puede reservar los sectores Shofar y Águila.");
                descriptionReservaOperationType.Append(Environment.NewLine);
                descriptionReservaOperationType.AppendLine("- Léon sólo se permite al contado.");
                descriptionReservaOperationType.Append(Environment.NewLine);
                descriptionReservaOperationType.AppendLine("- Antes de Julio: 3 cuotas en 3 meses 40%-30%-30%");
                descriptionReservaOperationType.Append(Environment.NewLine);
                descriptionReservaOperationType.AppendLine("- Posterior a Julio: 2 cuotas en 2 meses 70%-30%");
                reserva.Description = descriptionReservaOperationType.ToString();

                var ofrenda = Updater.ObjectSpace.CreateObject<OperationType>();
                ofrenda.InternalId = OperationType.OfrendaOperationType;
                ofrenda.Name = "Ofrenda";
                ofrenda.PlaceStatusName = "Ofrendado";
                StringBuilder descriptionOfrendaOperationType = new StringBuilder("- En los cultos se ofrendan entradas a costo 0.");
                descriptionOfrendaOperationType.Append(Environment.NewLine);
                descriptionOfrendaOperationType.AppendLine("- Aplica a cualquier sector.");
                ofrenda.Description = descriptionOfrendaOperationType.ToString();

                var consignacion = Updater.ObjectSpace.CreateObject<OperationType>();
                consignacion.InternalId = OperationType.ConsignacionOperationType;
                consignacion.Name = "Consignación";
                consignacion.PlaceStatusName = "En consignación";
                StringBuilder descriptionConsignacionOperationType = new StringBuilder("- Mami Gladis puede acceder a entradas consignadas y luego pagarlas.");
                descriptionConsignacionOperationType.Append(Environment.NewLine);
                descriptionConsignacionOperationType.AppendLine("- Aplica a cualquier sector.");
                consignacion.Description = descriptionConsignacionOperationType.ToString();

                var borrador = Updater.ObjectSpace.CreateObject<OperationType>();
                borrador.InternalId = OperationType.DraftOperationType;
                borrador.Name = "Borrador";
                borrador.PlaceStatusName = "En borrador";
                StringBuilder borradorConsignacionOperationType = new StringBuilder("- ");
                borradorConsignacionOperationType.Append(Environment.NewLine);
                borradorConsignacionOperationType.AppendLine("- ");
                borrador.Description = borradorConsignacionOperationType.ToString();
            }
        }
    }
}