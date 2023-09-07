using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Semillas")]
    [DefaultProperty(nameof(Description))]
    [Persistent(Schema.Rjv + nameof(PaymentPlanDetail))]
    public class PaymentPlanDetail : BaseEntity
    {
        PaymentPlan paymentPlan;
        decimal number;
        string description;
        decimal percentage;
        int amount = 0;
        DateTime? limitDate;

        [Caption("Plan de financiamiento")]
        [Association("PaymentPlan-PaymentPlanDetails")]
        [Persistent("PaymentPlan_PaymentPlanDetails")]
        public PaymentPlan PaymentPlan
        {
            get => paymentPlan;
            set => SetPropertyValue(ref paymentPlan, value);
        }

        [ModelDefault("DisplayFormat", "{0:#}")]
        [Caption("Número")]
        [Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public decimal Number
        {
            get => number;
            set => SetPropertyValue(ref number, value);
        }

        [Caption("Descripción")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(ref description, value);
        }

        [ModelDefault("DisplayFormat", "{0:#}")]
        [Caption("Amount")]
        [Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int Amount
        {
            get => amount;
            set => SetPropertyValue(ref amount, value);
        }

        //[DisplayName("%")]
        [MemberDesignTimeVisibility(false)]
        [ModelDefault("DisplayFormat", "{0:P0}")]
        [RuleRange(DefaultContexts.Save, 0, 1)]
        [Caption("Porcentaje de semilla")]
        public decimal Percentage
        {
            get => percentage;
            set => SetPropertyValue(ref percentage, value);
        }

        [Caption("Fecha límite de siembra")]
        [Nullable(true)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public DateTime? LimitDate
        {
            get => limitDate;
            set => SetPropertyValue(ref limitDate, value);
        }

        [MemberDesignTimeVisibility(false)]
        [Caption("Pago")]
        [Association("PaymentPlanDetail-Payments")]
        public XPCollection<Payment> Payments => GetCollection<Payment>();

        public PaymentPlanDetail(Session session) : base(session)
        {
        }
    }
}
