using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Participante")]
    [ImageName("BO_Person")]
    [DefaultProperty(nameof(FullName))]
    [Persistent(Schema.Ap + nameof(Customer))]
    public class Customer : BaseEntity
    {
        int ci;
        string fullName = string.Empty;
        int whatsApp;

        [ModelDefault("DisplayFormat", "{0:#}")]
        [Caption("CI")]
        [ToolTip("CI sin extensión")]
        [Nullable(false)]
        [RequiredField]
        [RuleUniqueValue("ValidateUniqueCI", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, SkipNullOrEmptyValues = true)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int CI
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

        [ModelDefault("DisplayFormat", "{0:#}")]
        [Caption("WhatsApp")]
        [Nullable(false)]
        [RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int WhatsApp
        {
            get => whatsApp;
            set => SetPropertyValue(ref whatsApp, value);
        }

        public Customer(Session session) : base(session)
        {
        }
    }
}
