﻿using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
using static ActivacionProfetica.Module.BusinessObjects.Place;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    //[Appearance("RiskAcceptanceAcceptedHide", Visibility = ViewItemVisibility.Hide, TargetItems = "Accepted;", Criteria = "[Risk].[AttachFile] Is Null", Context = "Risk_RiskAcceptances_ListView")]
    [Appearance("FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "Operation_Places_LookupListView", Criteria = "Operation is not null", FontStyle = FontStyle.Strikeout, BackColor = "253, 125, 125")]
    [Caption("Asiento")]
    [DefaultProperty(nameof(Path))]
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
        [ImmediatePostData]
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

        private string path;

        [Caption("Nombre completo")]
        [Size(StringSize.ShortSringSize)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Path
        {
            get
            {
                if (path == null)
                {
                    path = CalculatePath(this);
                }
                return path;
            }
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && (propertyName == "Parent" || propertyName == "Name") && oldValue != newValue)
            {
                path = CalculatePath(this);
                OnChanged("Path");
            }
        }
        private string CalculatePath(ITreeNode node)
        {
            string result = node.Name;
            if (node.Parent != null)
            {
                result = CalculatePath(node.Parent) + " - " + result;
            }
            return result;
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