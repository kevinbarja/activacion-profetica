using DevExpress.ExpressApp.DC;

namespace ActivacionProfetica.Module.BusinessObjects.Enums
{
    public enum PaymentStatus
    {
        [XafDisplayName("Pendiente")]
        Pending,
        [XafDisplayName("En mora")]
        InArrears,
        [XafDisplayName("Pagado en fecha")]
        PayedOk,
        [XafDisplayName("Pagado en mora")]
        PayedInArrears
    }
}
