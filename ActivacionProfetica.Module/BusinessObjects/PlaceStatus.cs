using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Estados de los asientos")]
    [Appearance("WhiteText", TargetItems = nameof(InternalId), Context = "ListView", FontStyle = FontStyle.Bold, FontColor = "255,255,255")]
    [DefaultProperty(nameof(SingularName))]
    [Persistent(Schema.Ap + nameof(PlaceStatus))]
    public class PlaceStatus : BaseEntity//, IAPLookupView
    {
        public static int AvailablePlaceStatus = 1;
        public static int SoldPlaceStatus = 2;
        public static int ReservedPlaceStatus = 3;
        public static int ConsignmentPlaceStatus = 4;
        public static int OfferingPlaceStatus = 5;

        private string singularName;
        private string pluralName;
        private string description;

        public PlaceStatus(Session session) : base(session)
        {
        }

        [Caption("Nombre en singular")]
        [Size(StringSize.ShortSringSize), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string SingularName
        {
            get => singularName;
            set => SetPropertyValue(ref singularName, value);
        }

        [Caption("Nombre en plural")]
        [Size(StringSize.ShortSringSize), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string PluralName
        {
            get => pluralName;
            set => SetPropertyValue(ref pluralName, value);
        }

        [Caption("Descripción")]
        [Size(StringSize.LargeSringSize), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(ref description, value);
        }

        [Caption("Operaciones")]
        [MemberDesignTimeVisibility(false)]
        [Association("PlaceStatus-Operations")]
        public XPCollection<Operation> Operations => GetCollection<Operation>(nameof(Operations));

        [Caption("Operaciones")]
        [MemberDesignTimeVisibility(false)]
        [Association("PlaceStatus-Places")]
        public XPCollection<Place> Places => GetCollection<Place>(nameof(Places));
    }
}
