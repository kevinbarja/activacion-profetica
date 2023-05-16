using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Xpo;
using System;
using System.Linq;
using System.Text;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class PlaseStatusData : BaseData
    {
        public PlaseStatusData(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<PlaceStatus>()
                             select ps).Any();

            if (isEmpty)
            {
                //AvailablePlaceStatus
                var availablePlaceStatus = Updater.ObjectSpace.CreateObject<PlaceStatus>();
                availablePlaceStatus.InternalId = PlaceStatus.AvailablePlaceStatus;
                availablePlaceStatus.SingularName = "Disponible";
                availablePlaceStatus.PluralName = "Disponibles";
                StringBuilder descriptionAvailablePlaceStatus = new StringBuilder("- Estado inicial de los asientos.");
                descriptionAvailablePlaceStatus.Append(Environment.NewLine);
                descriptionAvailablePlaceStatus.AppendLine("- Puede realizar transición de estado a: 'Vendido', 'Reservado', 'Ofrendado', 'Consignado'.");
                descriptionAvailablePlaceStatus.Append(Environment.NewLine);
                descriptionAvailablePlaceStatus.AppendLine("- Todos los sectores pueden ser adquiridos al contado.");
                descriptionAvailablePlaceStatus.Append(Environment.NewLine);
                descriptionAvailablePlaceStatus.AppendLine("- Shofar y Águila pueden ser reservados.");
                availablePlaceStatus.Description = descriptionAvailablePlaceStatus.ToString();


                //SoldPlaceStatus
                var soldPlaceStatus = Updater.ObjectSpace.CreateObject<PlaceStatus>();
                soldPlaceStatus.InternalId = PlaceStatus.SoldPlaceStatus;
                soldPlaceStatus.SingularName = "Vendido";
                soldPlaceStatus.PluralName = "Vendidos";
                StringBuilder descriptionSoldPlaceStatus = new StringBuilder("- Estados anteriores posibles: 'Disponible', 'Reservado', 'Consignado' ");
                descriptionSoldPlaceStatus.Append(Environment.NewLine);
                descriptionSoldPlaceStatus.AppendLine("- 'Disponible' -> 'Vendido': Pago al contado.");
                descriptionSoldPlaceStatus.Append(Environment.NewLine);
                descriptionSoldPlaceStatus.AppendLine("- 'Reservado' -> 'Vendido': Pago en cuotas.");
                descriptionSoldPlaceStatus.Append(Environment.NewLine);
                descriptionSoldPlaceStatus.AppendLine("- Antes de Julio: 3 cuotas en 3 meses 40%-30%-30%");
                descriptionSoldPlaceStatus.Append(Environment.NewLine);
                descriptionSoldPlaceStatus.AppendLine("- Posterior a Julio: 2 cuotas en 2 meses 70%-30%");
                descriptionSoldPlaceStatus.Append(Environment.NewLine);
                descriptionSoldPlaceStatus.AppendLine("- 'Consignado' -> 'Vendido': Pago al contado.");
                soldPlaceStatus.Description = descriptionSoldPlaceStatus.ToString();

                //ReservedPlaceStatus
                var reservedPlaceStatus = Updater.ObjectSpace.CreateObject<PlaceStatus>();
                reservedPlaceStatus.InternalId = PlaceStatus.ReservedPlaceStatus;
                reservedPlaceStatus.SingularName = "Reservado";
                reservedPlaceStatus.PluralName = "Reservados";
                StringBuilder descriptionReservedPlaceStatus = new StringBuilder("- Estados anterior posible: 'Disponible'");
                descriptionReservedPlaceStatus.Append(Environment.NewLine);
                descriptionReservedPlaceStatus.AppendLine("- 'Disponible' -> 'Reservado': Pago en cuotas.");
                descriptionAvailablePlaceStatus.Append(Environment.NewLine);
                descriptionAvailablePlaceStatus.AppendLine("- Sólo Shofar y Águila pueden ser reservados.");
                descriptionSoldPlaceStatus.Append(Environment.NewLine);
                descriptionSoldPlaceStatus.AppendLine("- Antes de Julio: 3 cuotas en 3 meses 40%-30%-30%");
                descriptionSoldPlaceStatus.Append(Environment.NewLine);
                descriptionSoldPlaceStatus.AppendLine("- Posterior a Julio: 2 cuotas en 2 meses 70%-30%");
                reservedPlaceStatus.Description = descriptionReservedPlaceStatus.ToString();

                //ConsignmentPlaceStatus
                var consignmentPlaceStatus = Updater.ObjectSpace.CreateObject<PlaceStatus>();
                consignmentPlaceStatus.InternalId = PlaceStatus.ConsignmentPlaceStatus;
                consignmentPlaceStatus.SingularName = "Consignado";
                consignmentPlaceStatus.PluralName = "Consignados";
                StringBuilder descriptionConsignmentPlaceStatus = new StringBuilder("- Estados anterior posible: 'Disponible'");
                descriptionConsignmentPlaceStatus.Append(Environment.NewLine);
                descriptionConsignmentPlaceStatus.AppendLine("- 'Disponible' -> 'Consignado': No realiza pago.");
                descriptionConsignmentPlaceStatus.Append(Environment.NewLine);
                descriptionConsignmentPlaceStatus.AppendLine("- Sólo personas autorizadas.");
                descriptionConsignmentPlaceStatus.AppendLine("- 'Consignado' -> 'Disponible' (Total): En caso de que retornen todas las entradas ");
                descriptionConsignmentPlaceStatus.AppendLine("- 'Consignado' -> 'Disponible' (Parcial): Debe desvincular los asientos de la operación");
                descriptionConsignmentPlaceStatus.AppendLine("- 'Consignado' -> 'Vendido' Pago total");
                consignmentPlaceStatus.Description = descriptionReservedPlaceStatus.ToString();

                //ConsignmentPlaceStatus
                var offeringPlaceStatus = Updater.ObjectSpace.CreateObject<PlaceStatus>();
                offeringPlaceStatus.InternalId = PlaceStatus.OfferingPlaceStatus;
                offeringPlaceStatus.SingularName = "Ofrendado";
                offeringPlaceStatus.PluralName = "Ofrendados";
                StringBuilder descriptionOfferingPlaceStatus = new StringBuilder("- Estados anterior posible: 'Disponible'");
                descriptionOfferingPlaceStatus.Append(Environment.NewLine);
                descriptionOfferingPlaceStatus.AppendLine("- 'Disponible' -> 'Ofrendado': No realiza pago.");
                offeringPlaceStatus.Description = descriptionReservedPlaceStatus.ToString();
            }
        }
    }
}