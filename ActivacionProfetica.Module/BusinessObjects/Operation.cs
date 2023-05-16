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
    Criteria = nameof(OperationType) + " is Null")]
    [Appearance("HideOperationCode", TargetItems = nameof(Id),
    Visibility = ViewItemVisibility.Hide,
    Criteria = "IsNewObject(this)")]
    [Caption("Operación")]
    [DefaultProperty(nameof(Id))]
    [Persistent(Schema.Ap + nameof(Operation))]
    public class Operation : BaseEntity
    {
        Customer customer;
        OperationType operationType;

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            OperationType = (from s in Session.Query<OperationType>()
                             where s.InternalId == OperationType.DraftOperationType
                             select s).Single();
        }

        [Caption("Código")]
        [Appearance("DisableCode", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public new int Id
        {
            get => base.InternalId;
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

        [Caption("Tipo de operación")]
        [RequiredField]
        [Association("OperationType-Operations")]
        [Persistent("OperationType_Operations")]
        [ImmediatePostData]
        public OperationType OperationType
        {
            get => operationType;
            set => SetPropertyValue(ref operationType, value);
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
                    if (this.OperationType.InternalId == OperationType.ReservaOperationType)
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
            if (currentPlace.Id == parentPlaceId || currentPlace.Id == otherParentPlaceId)
            {
                return true;
            }

            for (Place place = currentPlace.ParentPlace;
                place != null; place = place.ParentPlace)
            {
                if (place.Id == parentPlaceId || place.Id == otherParentPlaceId)
                {
                    return true;
                }
            }
            return false;
        }

        [Caption("Selección de asientos")]
        [DataSourceProperty(nameof(PlacesFiltered), DataSourcePropertyIsNullMode.SelectNothing)]
        [Association("Operation-Places")]
        public XPCollection<Place> Places =>
            GetCollection<Place>(nameof(Places));

        [OnChangedProperty(nameof(OperationType))]
        public void OnChangeOperationType(object currentValue, object newValue)
        {
            Places.Reload();
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
