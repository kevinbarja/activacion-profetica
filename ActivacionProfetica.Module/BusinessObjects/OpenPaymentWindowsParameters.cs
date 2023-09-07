using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [DomainComponent]
    public class OpenPaymentWindowsParameters
    {
        public OpenPaymentWindowsParameters() { }

        [RequiredField]
        [DisplayName("Monto a sembrar")]
        [RuleValueComparison(ValueComparisonType.GreaterThan, 0)]
        public int Amount { get; set; }

        [RequiredField]
        [DisplayName("Método de siembra")]
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.None;
    }
}
