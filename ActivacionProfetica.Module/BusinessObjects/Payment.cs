using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Appearance("RedTextPayment", TargetItems = "*", Context = "LookupListView;ListView", Criteria = "[IsReverted] = True", FontStyle = FontStyle.Strikeout, FontColor = "253, 125, 125")]
    [Appearance("ResidualRiskLow", Enabled = false, TargetItems = "*",
        Criteria = "[PaymentMethod] != ##Enum#ActivacionProfetica.Module.BusinessObjects.Enums.PaymentMethod,None# And IsNewObject(This) = False And UsuarioActualPuedeRevertirPagos = False",
        Context = Constants.View.OperationPaymentsListView, BackColor = "240, 240, 240")]
    [Appearance("DisablePayment", Enabled = false, TargetItems = "PaymentDate, UpdatedBy",
        Criteria = "UsuarioActualPuedeRevertirPagos = False",
        Context = "LookupListView;ListView")]
    //[Appearance("BlueOnCreation", TargetItems = "PaymentMethod", Context = "LookupListView;ListView", Criteria = "IsNewObject(This) Or [PaymentMethod] == ##Enum#ActivacionProfetica.Module.BusinessObjects.Enums.PaymentMethod,None#", FontStyle = FontStyle.Bold, BackColor = "163, 219, 247")]
    [Caption("Pagos")]
    [Persistent(Schema.Ap + nameof(Payment))]

    public class Payment : BaseEntity
    {
        PaymentPlanDetail paymentPlanDetail;
        int amount;
        int someonePlantedInMeAmount;
        DateTime? paymentDate;
        PaymentMethod paymentMethod;
        //PaymentStatus paymentStatus;
        Operation operation;
        SomeonePlantedInMe someonePlantedInMe;

        bool isReverted = false;

        [Caption("Alguién sembró en mi")]
        [Association("SomeonePlantedInMe-Payments")]
        [Persistent("SomeonePlantedInMe_Payments")]
        [ModelDefault("AllowNew", "False")]
        public SomeonePlantedInMe SomeonePlantedInMe
        {
            get => someonePlantedInMe;
            set => SetPropertyValue(ref someonePlantedInMe, value);
        }

        [Caption("¿Pago revertido?")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsReverted
        {
            get { return isReverted; }
            set => SetPropertyValue(ref isReverted, value);
        }

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

        [ImmediatePostData]
        [Caption("Monto (Alguién sembró en mi)")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int SomeonePlantedInMeAmount
        {
            get { return someonePlantedInMeAmount; }
            set => SetPropertyValue(ref someonePlantedInMeAmount, value);
        }

        //[NonPersistent]
        //[MemberDesignTimeVisibility(false)]
        //[RuleFromBoolProperty("SomeonePlantedInMeAmountValidated",
        //           DefaultContexts.Save,
        //           "\"Monto (Alguién sembró en mi)\" no debe estar vacío o cero cuando selecciona Alguien sembró en mi",
        //           UsedProperties = nameof(SomeonePlantedInMeAmount),
        //           SkipNullOrEmptyValues = true)]
        //public bool SomeonePlantedInMeAmountValidated => (SomeonePlantedInMe is null && someonePlantedInMeAmount == 0) || (SomeonePlantedInMe != null && someonePlantedInMeAmount > 0);


        //[NonPersistent]
        //[MemberDesignTimeVisibility(false)]
        //[RuleFromBoolProperty("MaxSomeonePlantedInMeAmountValidated",
        //                   DefaultContexts.Save,
        //                   "\"Monto (Alguién sembró en mi)\" excede el monto disponible para bendecir, favor consulte con su supervisor",
        //                   UsedProperties = nameof(SomeonePlantedInMeAmount),
        //                   SkipNullOrEmptyValues = true)]
        //public bool MaxSomeonePlantedInMeAmountValidated => (SomeonePlantedInMe != null && SomeonePlantedInMe.MontoPorBendecir > someonePlantedInMeAmount);


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
            if (!UsuarioActualPuedeRevertirPagos)
            {
                UpdatedBy = GetCurrentUser();
            }

            if (PaymentMethod == PaymentMethod.None)
            {
                PaymentDate = null;
            }
            else
            {
                PaymentDate = DateTime.Now;
            }
        }

        [NonPersistent]
        [Caption("UsuarioActualEsSupervisor")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool UsuarioActualEsSupervisor
        {
            get
            {
                var currentUser = SecuritySystem.CurrentUser;
                if (currentUser is ApplicationUser)
                {
                    ApplicationUser currentAppUser = currentUser as ApplicationUser;
                    return currentAppUser.Roles.Any(r => r.Name == Role.OperationSupervisor);
                }
                {
                    return false;
                }
            }
        }

        [NonPersistent]
        [Caption("UsuarioActualPuedeRevertirPagos")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool UsuarioActualPuedeRevertirPagos
        {
            get
            {
                var currentUser = SecuritySystem.CurrentUser;
                if (currentUser is ApplicationUser)
                {
                    ApplicationUser currentAppUser = currentUser as ApplicationUser;
                    return currentAppUser.Roles.Any(r => r.Name == Role.RevertPayments);
                }
                {
                    return false;
                }
            }
        }

        [NonPersistent]
        [Caption("Monto QR")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmountQR
        {
            get
            {
                if (paymentMethod == PaymentMethod.QR)
                {
                    if (someonePlantedInMeAmount > 0)
                        return Amount - someonePlantedInMeAmount;
                    return Amount;
                }
                return 0;
            }
        }

        [NonPersistent]
        [Caption("Monto efectivo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmountCash
        {
            get
            {
                if (paymentMethod == PaymentMethod.Cash)
                {
                    if (someonePlantedInMeAmount > 0)
                        return Amount - someonePlantedInMeAmount;
                    return Amount;
                }
                return 0;
            }
        }


        [NonPersistent]
        [Caption("Monto a pagar")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int AmountToPay
        {
            get
            {
                return Amount - SomeonePlantedInMeAmount;
            }
        }


        [NonPersistent]
        [Caption("PaymentMethodCaption")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PaymentMethodCaption
        {
            get
            {
                switch (PaymentMethod)
                {
                    case PaymentMethod.None:
                        return "Pendiente";
                    case PaymentMethod.QR:
                        return "QR";
                    case PaymentMethod.Cash:
                        return "Efectivo";
                    default:
                        return string.Empty;
                }
            }
        }

        [NonPersistent]
        [Caption("PaymentDescription")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string PaymentDescription
        {
            get
            {
                if (PaymentPlanDetail.Description.Contains("sembró"))
                {
                    return PaymentPlanDetail.Description;
                }

                if (someonePlantedInMeAmount > 0)
                {
                    var amountPayed = Amount - someonePlantedInMeAmount;
                    if (amountPayed == 0)
                    {
                        return $"Alguién sembró en mi Bs.{someonePlantedInMeAmount}";
                    }
                    else
                    {
                        return $"Alguién sembró en mi Bs.{someonePlantedInMeAmount} + mi siembra de Bs.{amountPayed}";
                    }
                }
                return PaymentPlanDetail.Description;
            }
        }

        //[NonPersistent]
        //[MemberDesignTimeVisibility(false)]
        //[RuleFromBoolProperty("PaymentDateValidated",
        //           DefaultContexts.Save,
        //           "\"Fecha de pago\" no debe estar vacío.",
        //           UsedProperties = nameof(PaymentMethod),
        //           SkipNullOrEmptyValues = false)]
        //public bool PaymentDateValidated => (PaymentMethod == PaymentMethod.None && PaymentDate is null) || (PaymentMethod != PaymentMethod.None && PaymentDate != null);

        protected override void OnSaving()
        {
            UpdatedOn = DateTime.Now;
            if (UpdatedBy is null)
            {
                UpdatedBy = GetCurrentUser();
            }
            //base.OnSaving();
            if (PaymentMethod != PaymentMethod.None && PaymentDate is null)
            {
                PaymentDate = DateTime.Now;
            }
        }
        public Payment(Session session) : base(session)
        {
        }
    }
}
