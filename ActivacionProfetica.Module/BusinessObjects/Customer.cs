using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{

    [Appearance("HideAdvisorInfo", TargetItems = nameof(ChurchName),
    Visibility = ViewItemVisibility.Hide,
    Criteria = nameof(IsCcecoMember) + "=True")]

    [Caption("Persona")]
    [ImageName("BO_Person")]
    [DefaultProperty(nameof(FullName))]
    [Persistent(Schema.Rjv + nameof(Customer))]
    public class Customer : BaseEntity, IAPLookupView
    {
        string ci = string.Empty;
        string fullName = string.Empty;
        string whatsApp = string.Empty;
        bool isCcecoMember = true;
        string churchName = "Casa de Oración Central";
        int age = 0;
        Gender gender = Gender.Male;

        [RuleRegularExpression(@"(?<!\d)", CustomMessageTemplate = @"´""CI"" debe contener sólo dígitos")]
        [Caption("CI")]
        [ToolTip("CI sin extensión")]
        [Nullable(false)]
        [RequiredField]
        [ImmediatePostData]
        [RuleUniqueValue("ValidateUniqueCI", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, SkipNullOrEmptyValues = true)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string CI
        {
            get => ci;
            set => SetPropertyValue(ref ci, value);
        }

        [Caption("Nombres y apellidos")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string FullName
        {
            get => fullName;
            set => SetPropertyValue(ref fullName, value);
        }

        [RuleRegularExpression(@"(?<!\d)\d{8}(?!\d)", CustomMessageTemplate = @"´""WhatsApp"" debe contener 8 dígitos")]
        [ToolTip("Ejemplo: 75632255")]
        [Caption("WhatsApp")]
        [Nullable(false)]
        [RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string WhatsApp
        {
            get => whatsApp;
            set => SetPropertyValue(ref whatsApp, value);
        }

        [Caption("¿Congrega en Casa de Oración Central?")]
        [Nullable(false)]
        [RequiredField]
        [ImmediatePostData]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsCcecoMember
        {
            get => isCcecoMember;
            set => SetPropertyValue(ref isCcecoMember, value);
        }

        [Caption("Iglesia que congrega")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string ChurchName
        {
            get => churchName;
            set => SetPropertyValue(ref churchName, value);
        }

        [Caption("Edad")]
        [Nullable(true), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Age
        {
            get => age;
            set => SetPropertyValue(ref age, value);
        }

        [Caption("Género")]
        [Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public Gender Gender
        {
            get => gender;
            set => SetPropertyValue(ref gender, value);
        }


        [Caption("Operaciones")]
        [MemberDesignTimeVisibility(false)]
        [Association("Customer-Operations")]
        public XPCollection<Operation> Operations => GetCollection<Operation>(nameof(Operations));

        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        [RuleFromBoolProperty("ChurchNameValidated",
                   DefaultContexts.Save,
                   "\"Iglesia que congrega\" no debe estar vacío.",
                   UsedProperties = nameof(ChurchName),
                   SkipNullOrEmptyValues = true)]
        public bool ChurchNameValidated => (!IsCcecoMember && ChurchName != string.Empty) || (IsCcecoMember);

        [OnChangedProperty(nameof(IsCcecoMember))]
        public void OnChangeIsCcecoMember(object oldValue, object newValue)
        {
            if (((bool)oldValue) && !((bool)newValue))
            {
                ChurchName = string.Empty;
            }
            else
            {
                ChurchName = "Casa de Oración Central";
            }

        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (IsCcecoMember)
            {
                ChurchName = "Casa de Oración Central";
            }
        }

        public Customer(Session session) : base(session)
        {
        }


    }
}
