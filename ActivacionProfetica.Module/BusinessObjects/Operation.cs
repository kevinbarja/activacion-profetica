using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Appearance("HidePlacesSelection", TargetItems = nameof(Places),
    Visibility = ViewItemVisibility.Hide,
    Criteria = nameof(PlaceStatus) + " is Null")]
    [Appearance("HideOperationCode", TargetItems = nameof(InternalId),
    Visibility = ViewItemVisibility.Hide,
    Criteria = "IsNewObject(this)")]
    [Caption("Operación")]
    [DefaultProperty(nameof(InternalId))]
    [Persistent(Schema.Ap + nameof(Operation))]
    public class Operation : BaseEntity
    {
        Customer customer;
        PlaceStatus placeStatus;

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

        [MemberDesignTimeVisibility(false)]
        [NonPersistent]
        public XPCollection<Place> PlacesFiltered
        {
            get
            {
                List<Place> placesDatasource = new List<Place>();

                XPQuery<Place> places = Session.Query<Place>();
                var placesFiltered = from p in places
                                     where p.Operation == null
                                     select p;

                foreach (var placeFiltered in placesFiltered)
                {
                    if (this.PlaceStatus.InternalId == PlaceStatus.ReservedPlaceStatus)
                    {
                        if (CheckInheritance(placeFiltered, Place.ShofarSector, Place.EagleSector))
                        {
                            placesDatasource.Add(placeFiltered);
                        }
                    }
                    else
                    {
                        placesDatasource.Add(placeFiltered);
                    }
                }

                var result = new XPCollection<Place>(Session, placesDatasource);

                return result;
            }
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

        //[ModelDefault("AllowEdit", "False")]
        [Caption("Selección de asientos")]
        //[DataSourceProperty(nameof(PlacesFiltered), DataSourcePropertyIsNullMode.SelectNothing)]
        [Association("Operation-Places")]
        [ImmediatePostData]
        public XPCollection<Place> Places =>
            GetCollection<Place>(nameof(Places));

        [OnChangedProperty(nameof(PlaceStatus))]
        public void OnChangeOperationType(object currentValue, object newValue)
        {
            // Places.Reload();
        }

        [OnChangedProperty(nameof(Places))]
        public void OnChangePlaces(object currentValue, object newValue)
        {
            // Places.Reload();
        }

        //[RuleFromBoolProperty("ValidateEmptyPlaces",
        //            DefaultContexts.Save,
        //            "Debe seleccionar al menos un asiento.",
        //            UsedProperties = nameof(Places))]
        //[NonPersistent]
        //[MemberDesignTimeVisibility(false)]
        //public bool ValidateEmptyPlaces => Places.Any();

        public Operation(Session session) : base(session)
        {
        }
    }
}
