using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Appearance("GreenInternalId", TargetItems = nameof(InternalId),
        Context = Constants.View.OperationDetailView, FontStyle = System.Drawing.FontStyle.Bold)]

    [Appearance("HidePlacesSelection", TargetItems = nameof(Places),
    Visibility = ViewItemVisibility.Hide,
    Criteria = nameof(PlaceStatus) + " is Null")]

    [Appearance("HideOperationCode", TargetItems = nameof(InternalId) + ";" + nameof(PlaceStatus),
    Visibility = ViewItemVisibility.Hide,
    Criteria = "IsNewObject(this)")]

    [Appearance("DisableSector", TargetItems = "Sector,Customer,PaymentPlan,Places",
    Enabled = false,
    Criteria = "!IsNewObject(this)")]

    [Caption("Operación")]
    [DefaultProperty(nameof(InternalId))]
    [Persistent(Schema.Ap + nameof(Operation))]
    public class Operation : BaseEntity
    {
        Customer customer;
        PlaceStatus placeStatus;
        Sector sector;
        PaymentPlan paymentPlan;

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            PlaceStatus = (from s in Session.Query<PlaceStatus>()
                           where s.InternalId == PlaceStatus.AvailablePlaceStatus
                           select s).Single();
        }

        [Caption("Persona")]
        [RequiredField]
        [Association("Customer-Operations")]
        [Persistent("Customer_Operations")]
        [ImmediatePostData]
        [ModelDefault("LookupProperty", "{0:FullName} ({0:CI})")]
        public Customer Customer
        {
            get => customer;
            set => SetPropertyValue(ref customer, value);
        }

        [Caption("Estado de los asientos seleccionados")]
        [RequiredField]
        [Association("PlaceStatus-Operations")]
        [Persistent("PlaceStatus_Operations")]
        [ImmediatePostData]
        public PlaceStatus PlaceStatus
        {
            get => placeStatus;
            set => SetPropertyValue(ref placeStatus, value);
        }

        [Caption("Sector")]
        [RequiredField]
        [Association("Sector-Operations")]
        [Persistent("Sector_Operations")]
        [ImmediatePostData]
        public Sector Sector
        {
            get => sector;
            set => SetPropertyValue(ref sector, value);
        }

        [Caption("Plan de financiamiento")]
        [RequiredField]
        [ImmediatePostData]
        [Association("PaymentPlan-Operations")]
        [Persistent("PaymentPlan_Operations")]
        public PaymentPlan PaymentPlan
        {
            get => paymentPlan;
            set => SetPropertyValue(ref paymentPlan, value);
        }

        private bool CheckInheritance(Place currentPlace, int parentPlaceId, int otherParentPlaceId)
        {
            if (currentPlace.InternalId == parentPlaceId || currentPlace.InternalId == otherParentPlaceId)
            {
                return true;
            }

            for (Place place = currentPlace.ParentPlace;
                place != null; place = place.ParentPlace)
            {
                if (place.InternalId == parentPlaceId || place.InternalId == otherParentPlaceId)
                {
                    return true;
                }
            }
            return false;
        }

        [Caption("Selección de asientos")]
        [Association("Operation-Places")]
        [ImmediatePostData]
        public XPCollection<Place> Places =>
            GetCollection<Place>(nameof(Places));


        [RuleFromBoolProperty("ValidateEmptyPlaces",
                    DefaultContexts.Save,
                    "Debe seleccionar al menos un asiento.",
                    UsedProperties = nameof(Places))]
        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public bool ValidateEmptyPlaces => Places.Any();

        [Caption("Cuotas")]
        [Association("Operation-Payments"), Aggregated]
        public XPCollection<Payment> Payments => GetCollection<Payment>();


        [OnChangedProperty(nameof(Sector))]
        public void OnChangeOperationType()
        {
            PaymentPlan = null;
            Places.Reload();
            CallOnChanged(nameof(Places));
        }




        [NonPersistent]
        [Caption("¿Existe algún pago en mora?")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool AnyPaymentsInArrears => Payments.Any(p => p.PaymentPlanDetail.LimitDate != null && p.PaymentPlanDetail.LimitDate > DateTime.Now && p.PaymentMethod == PaymentMethod.None);

        [NonPersistent]
        [Caption("Cantidad de asientos")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PlacesQuantity => Places.Count;


        private bool noCreatePayments;

        [NonPersistent]
        [Caption("No crear pagos")]
        [MemberDesignTimeVisibility(false)]
        public bool NoCreatePayments
        {
            get { return noCreatePayments; }
            set { noCreatePayments = value; }
        }

        /// 
        /// NON PERSISTENTS
        /// 

        [NonPersistent]
        [Caption("Asientos")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Asientos
        {
            get
            {
                if (Sector.InternalId == Sector.EagleSectorId)
                {
                    return Places?.Count().ToString() + " " + "(Cantidad de asientos)";
                }
                else
                {
                    return string.Join(",  ", Places.Select(p => p.Path.Replace(p.Sector.Name + "-", string.Empty)));
                }
            }
        }

        [NonPersistent]
        [Caption("TodosLosPagosRealizados")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool TodosLosPagosRealizados => !Payments.Any(p => p.PaymentMethod == PaymentMethod.None) && Payments.Count() > 0;

        [NonPersistent]
        [Caption("EsShofarOEsAguila")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool EsShofarOEsAguila => (Sector.Name == Sector.EagleSectorName || Sector.Name == Sector.ShofarSectorName);

        [NonPersistent]
        [Caption("PrimerCuotaPagada")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool PrimerCuotaPagada
        {
            get
            {
                if (Payments is null || Payments.Count == 0)
                {
                    return false;
                }
                else
                {
                    return Payments.Any(p => p.PaymentStatus != PaymentStatus.Pending && p.PaymentPlanDetail.Number == 1);
                }
            }
        }


        [NonPersistent]
        [Caption("AsientosReservadosPorPersona")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PlacesQuantityOfPerson => Customer.Operations.Where(o => o.PlaceStatus.InternalId == PlaceStatus.ReservedPlaceStatus).Sum(o => o.PlacesQuantity);


        [NonPersistent]
        [Caption("AguilaSoloPuedeReservar1Asiento")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool AguilaSoloPuedeReservar1Asiento
        {
            get
            {
                if (Sector.Name == Sector.EagleSectorName)
                {
                    var quantity = Customer.Operations.Where(o => o.PlaceStatus.InternalId == PlaceStatus.ReservedPlaceStatus && o.Sector.InternalId == Sector.EagleSectorId).Sum(o => o.PlacesQuantity);
                    return (quantity <= 1);
                }
                else
                {
                    return true;
                }
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
        [Caption("LaOperacionNoDebeTenerPagos")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool LaOperacionNoDebeTenerPagos => Payments == null || Payments.Count() == 0;

        [NonPersistent]
        [Caption("LosPagosDebeEstarRevertidos")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool LosPagosDebeEstarRevertidos => Payments.Count() == Payments.Count(p => p.IsReverted);


        public Operation(Session session) : base(session)
        {
        }
    }
}
