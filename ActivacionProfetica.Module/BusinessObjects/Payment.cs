using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Pagos")]
    [Persistent(Schema.Ap + nameof(Payment))]
    public class Payment : BaseEntity
    {
        PaymentPlanDetail paymentPlanDetail;
        int amount;
        DateTime paymentDate;
        PaymentMethod paymentMethod;
        PaymentStatus paymentStatus;
        Operation operation;

        [MemberDesignTimeVisibility(false)]
        [Caption("Operación")]
        [Association("Operation-Payments")]
        [Persistent("Operation_Payments")]
        public Operation Operation
        {
            get => operation;
            set => SetPropertyValue(ref operation, value);
        }

        [Caption("Cuota")]
        [Association("PaymentPlanDetail-Payments")]
        [Persistent("PaymentPlanDetail_Payments")]
        public PaymentPlanDetail PaymentPlanDetail
        {
            get => paymentPlanDetail;
            set => SetPropertyValue(ref paymentPlanDetail, value);
        }

        [Caption("Estado")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public PaymentStatus PaymentStatus
        {
            get { return paymentStatus; }
            set { paymentStatus = value; }
        }

        [Caption("Monto")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [Caption("Fecha de pago")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }

        [Caption("Método de pago")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public PaymentMethod PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }

        public Payment(Session session) : base(session)
        {
        }
    }
}
