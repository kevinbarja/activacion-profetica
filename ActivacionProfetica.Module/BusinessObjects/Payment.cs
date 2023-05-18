﻿using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Model;
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
        DateTime? paymentDate;
        PaymentMethod paymentMethod;
        //PaymentStatus paymentStatus;
        Operation operation;

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
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
        [NonPersistent]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public PaymentStatus PaymentStatus
        {
            get
            {
                if (PaymentPlanDetail.LimitDate != null && PaymentPlanDetail.LimitDate < DateTime.Now && PaymentMethod == PaymentMethod.None)
                {
                    return PaymentStatus.InArrears;
                }
                else if (PaymentMethod == PaymentMethod.None)
                {
                    return PaymentStatus.Pending;
                }
                else if ((PaymentMethod != PaymentMethod.None) && (PaymentPlanDetail.LimitDate != null && PaymentDate > PaymentPlanDetail.LimitDate))
                {
                    return PaymentStatus.PayedInArrears;
                }
                else
                {
                    return PaymentStatus.PayedOk;
                }
            }
        }

        [Caption("Monto")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int Amount
        {
            get { return amount; }
            set => SetPropertyValue(ref amount, value);
        }

        [ModelDefault("DisplayFormat", "{0:g}")]
        [Caption("Fecha de pago")]
        [Nullable(true)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public DateTime? PaymentDate
        {
            get { return paymentDate; }
            set => SetPropertyValue(ref paymentDate, value);
        }

        [Caption("Método de pago")]
        [ImmediatePostData]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public PaymentMethod PaymentMethod
        {
            get { return paymentMethod; }
            set => SetPropertyValue(ref paymentMethod, value);
        }

        [OnChangedProperty(nameof(PaymentMethod))]
        public void OnChangedPaymentMethod()
        {
            if (PaymentMethod == PaymentMethod.None)
            {
                PaymentDate = null;
            }
            else
            {
                PaymentDate = DateTime.Now;
            }
        }

        public Payment(Session session) : base(session)
        {
        }
    }
}