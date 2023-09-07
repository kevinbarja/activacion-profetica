using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Planes de financiamiento")]
    [DefaultProperty(nameof(Description))]
    [Persistent(Schema.Rjv + nameof(PaymentPlan))]
    public class PaymentPlan : BaseEntity
    {
        private string description;
        private Sector sector;
        private DateTime? limitDate;


        [Caption("Descripción")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(ref description, value);
        }

        [ImmediatePostData]
        [Caption("Sector"), RequiredField]
        [Association("Sector-PaymentPlans")]
        [Persistent("Sector_PaymentPlans")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public Sector Sector
        {
            get => sector;
            set => SetPropertyValue(ref sector, value);
        }

        [Caption("Fecha máxima para acceder a este plan")]
        [Nullable(true)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public DateTime? LimitDate
        {
            get => limitDate;
            set => SetPropertyValue(ref limitDate, value);
        }

        [Caption("Semillas")]
        [Association("PaymentPlan-PaymentPlanDetails"), Aggregated]
        public XPCollection<PaymentPlanDetail> PaymentPlanDetails => GetCollection<PaymentPlanDetail>();

        [MemberDesignTimeVisibility(false)]
        [Caption("Operaciones")]
        [Association("PaymentPlan-Operations")]
        public XPCollection<Operation> Operations => GetCollection<Operation>();

        [NonPersistent]
        [Caption("¿Es ejecutivo de ventas?")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool EsEjecutivoDeVentas
        {
            get
            {
                var currentUser = SecuritySystem.CurrentUser;
                if (currentUser is ApplicationUser)
                {
                    ApplicationUser currentAppUser = currentUser as ApplicationUser;
                    return currentAppUser.Roles.Any(r => r.Name == Role.OperationEjecutive);
                }
                {
                    return false;
                }
            }
        }

        public PaymentPlan(Session session) : base(session)
        {
        }
    }
}
