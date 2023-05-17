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
    [Caption("Cuotas")]
    [DefaultProperty(nameof(Description))]
    [Persistent(Schema.Ap + nameof(PaymentPlanDetail))]
    public class PaymentPlanDetail : BaseEntity
    {
        PaymentPlan paymentPlan;
        private int number;
        private string description;
        private decimal percentage;
        private DateTime limitDate;

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
        public int Number
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

        //[DisplayName("%")]
        [ModelDefault("DisplayFormat", "{0:P0}")]
        [RuleRange(DefaultContexts.Save, 1, 100)]
        [Caption("Porcentaje")]
        public decimal Percentage
        {
            get => percentage;
            set => SetPropertyValue(ref percentage, value);
        }

        [Caption("Fecha límite de pago")]
        [RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public DateTime LimitDate
        {
            get => limitDate;
            set => SetPropertyValue(ref limitDate, value);
        }

        public PaymentPlanDetail(Session session) : base(session)
        {
        }
    }
}
