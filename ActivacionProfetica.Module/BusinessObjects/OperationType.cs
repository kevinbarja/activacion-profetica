using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Tipo de operación")]
    [DefaultProperty(nameof(Name))]
    [Persistent(Schema.Ap + nameof(OperationType))]
    public class OperationType : BaseEntity, IAPLookupView
    {
        public static int VentaOperationType = 1;
        public static int ReservaOperationType = 2;
        public static int OfrendaOperationType = 3;
        public static int ConsignacionOperationType = 4;

        private string name;
        private string description;

        public OperationType(Session session) : base(session)
        {
        }

        [Caption("Nombre")]
        [Size(StringSize.ShortSringSize), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(ref name, value);
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
        [Association("OperationType-Operations")]
        public XPCollection<Operation> Operations => GetCollection<Operation>(nameof(Operations));
    }
}
