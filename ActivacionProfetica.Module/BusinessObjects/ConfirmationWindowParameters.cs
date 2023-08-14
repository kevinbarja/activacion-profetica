using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;

namespace ActivacionProfetica.Module.BusinessObjects
{

    [DomainComponent]
    public class ConfirmationWindowParameters
    {
        public ConfirmationWindowParameters() { }
        [DisplayName("Mensaje de confirmación")]
        [ModelDefault("AllowEdit", "False")]
        public string Mensaje { get; set; }
    }
}
