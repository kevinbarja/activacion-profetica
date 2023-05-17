using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Planes de financiamiento")]
    [DefaultProperty(nameof(Description))]
    [Persistent(Schema.Ap + nameof(PaymentPlan))]
    public class PaymentPlan : BaseEntity
    {
        private string description;
        private Sector sector = Sector.Eagle;
        private DateTime limitDate;


        [Caption("Descripción")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(ref description, value);
        }

        [Caption("Sector")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public Sector Sector
        {
            get => sector;
            set => SetPropertyValue(ref sector, value);
        }

        [Caption("Fecha máxima para acceder a este plan")]
        [RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public DateTime LimitDate
        {
            get => limitDate;
            set => SetPropertyValue(ref limitDate, value);
        }

        [Caption("Cuotas")]
        [Association("PaymentPlan-PaymentPlanDetails"), Aggregated]
        public XPCollection<PaymentPlanDetail> PaymentPlanDetails => GetCollection<PaymentPlanDetail>();

        public PaymentPlan(Session session) : base(session)
        {
        }
    }
}
