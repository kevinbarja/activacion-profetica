using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using static ActivacionProfetica.Module.BusinessObjects.Place;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Lugar")]
    [Persistent(Schema.Ap + nameof(Place))]
    public class Place : BaseEntity, IPlace, ITreeNode
    {
        public static int LeonSector = 1;
        public static int ShofarSector = 2;
        public static int AguilaSector = 3;

        private Place parentPlace;
        private string name;
        Operation operation;

        [Caption("Nombre")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(ref name, value);
        }

        //[ModelDefault("View", "CuentaContable_CuentaMayor_LookupListView")]
        [Caption("Lugar superior")]
        [Persistent("ParentPlace_Place")]
        [Association("ParentPlace-ChildrenPlace")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ImmediatePostData]
        public Place ParentPlace
        {
            get => parentPlace;
            set => SetPropertyValue(ref parentPlace, value);
        }

        [MemberDesignTimeVisibility(false)]
        [Association("ParentPlace-ChildrenPlace")]
        public XPCollection<Place> ChildrenPlace
        {
            get => GetCollection<Place>(nameof(ChildrenPlace));
        }

        [Caption("Operación")]
        [Association("Operation-Places")]
        [Persistent("Operation_Places")]
        //[ModelDefault("LookupProperty", "Name")]
        [ImmediatePostData]
        //[DataSourceCriteria("Squad=='@This.Squad'")]
        [VisibleInLookupListView(true), VisibleInListView(true)]
        public Operation Operation
        {
            get => operation;
            set => SetPropertyValue(ref operation, value);
        }

        public Place(Session session) : base(session)
        {
        }


        [RuleFromBoolProperty("PlaceCircularReferenceValidation",
            DefaultContexts.Save,
            "Referencia circular detectada. Para corregir este error, " +
            "cambie la propiedad \"Lugar superior\" por otro valor.",
            UsedProperties = nameof(ParentPlace))]
        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public bool CircularReferenceValidation
        {
            get
            {
                for (Place place = ParentPlace;
                    place != null; place = place.ParentPlace)
                {
                    if (place == this)
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        /// <summary>
        /// Solapa la propiedad Parent de ITreeNode para 
        /// agregar el modificador (set) y permitir recursividad. 
        /// </summary>
        public interface IPlace : ITreeNode
        {
            new ITreeNode Parent { get; set; }
        }

        #region IPlace, ITreeNode
        ITreeNode IPlace.Parent
        {
            get => ParentPlace;
            set => ParentPlace = (value as Place);
        }

        IBindingList ITreeNode.Children => ChildrenPlace;

        ITreeNode ITreeNode.Parent => ParentPlace;

        //[Browsable(false)]
        //public string Name => string.Empty;
        #endregion
    }
}
