using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Threading;

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

        private PaymentMethod _paymentMethd;

        [RequiredField]
        [DisplayName("Método de siembra")]
        [ImmediatePostData]
        public PaymentMethod PaymentMethod
        {
            get { return _paymentMethd; }
            set
            {
                //Thread.Sleep(10000);
                //Amount = 100;
                _paymentMethd = value;
            }
        }

        [OnChangedProperty(nameof(PaymentMethod))]
        public void OnChangedPaymentMethod()
        {
            Thread.Sleep(10000);
            //
        }
    }
}
