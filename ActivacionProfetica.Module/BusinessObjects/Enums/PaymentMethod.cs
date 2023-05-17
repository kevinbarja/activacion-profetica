using DevExpress.ExpressApp.DC;

namespace ActivacionProfetica.Module.BusinessObjects.Enums
{
    public enum PaymentMethod
    {
        [XafDisplayName("")]
        None,
        [XafDisplayName("QR")]
        QR,
        [XafDisplayName("Efectivo")]
        Cash
    }
}
