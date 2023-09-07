using ActivacionProfetica.Module.BusinessObjects.Enums;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [DomainComponent]
    public class OpenPaymentWindowsParameters
    {
        public OpenPaymentWindowsParameters() { }
        [DisplayName("Monto a sembrar")]
        public int Monto { get; set; }
        [DisplayName("Método de siembra")]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
