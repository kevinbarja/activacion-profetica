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
                venta.Id = OperationType.VentaOperationType;
                venta.Name = "Venta";
                StringBuilder descriptionVentaOperationType = new StringBuilder("- Venta al contado.");
                descriptionVentaOperationType.Append(Environment.NewLine);
                descriptionVentaOperationType.AppendLine("- Todos los sectores pueden ser adquiridos al contado.");
                venta.Description = descriptionVentaOperationType.ToString();

                var reserva = Updater.ObjectSpace.CreateObject<OperationType>();
                reserva.Id = OperationType.ReservaOperationType;
                reserva.Name = "Reserva";
                StringBuilder descriptionReservaOperationType = new StringBuilder("- Puede reservar los sectores Shofar y Águila.");
                descriptionReservaOperationType.Append(Environment.NewLine);
                descriptionReservaOperationType.AppendLine("- Léon sólo se permite al contado.");
                reserva.Description = descriptionReservaOperationType.ToString();

                var ofrenda = Updater.ObjectSpace.CreateObject<OperationType>();
                ofrenda.Id = OperationType.OfrendaOperationType;
                ofrenda.Name = "Ofrenda";
                StringBuilder descriptionOfrendaOperationType = new StringBuilder("- En los cultos se ofrendas entradas.");
                descriptionOfrendaOperationType.Append(Environment.NewLine);
                descriptionOfrendaOperationType.AppendLine("- Aplica a cualquier sector.");
                ofrenda.Description = descriptionOfrendaOperationType.ToString();

                var consignacion = Updater.ObjectSpace.CreateObject<OperationType>();
                consignacion.Id = OperationType.ConsignacionOperationType;
                consignacion.Name = "Consignación";
                StringBuilder descriptionConsignacionOperationType = new StringBuilder("- Mami Gladis puede acceder a entradas consignadas y luego pagarlas.");
                descriptionConsignacionOperationType.Append(Environment.NewLine);
                descriptionConsignacionOperationType.AppendLine("- Aplica a cualquier sector.");
                consignacion.Description = descriptionConsignacionOperationType.ToString();
            }
        }
    }
}