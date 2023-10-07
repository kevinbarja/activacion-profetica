using DevExpress.ExpressApp.DC;

namespace ActivacionProfetica.Module.BusinessObjects.Enums
{
    public enum Gender
    {
        [XafDisplayName("Campo vacío")]
        WithoutValue,
        [XafDisplayName("Masculino")]
        Male,
        [XafDisplayName("Femenino")]
        Female
    }
}
