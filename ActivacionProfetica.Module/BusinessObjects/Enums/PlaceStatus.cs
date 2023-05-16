using DevExpress.ExpressApp.DC;

namespace ActivacionProfetica.Module.BusinessObjects.Enums
{
    public enum PlaceStatus
    {
        [XafDisplayName("Disponible")]
        Available,
        [XafDisplayName("Vendido")]
        Sold,
        [XafDisplayName("Reservado")]
        Reserved,
        [XafDisplayName("En consignación")]
        Consignment,
        [XafDisplayName("Ofrendado")]
        Offered,
    }
}
