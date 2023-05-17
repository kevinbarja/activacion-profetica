using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Sectores")]
    [DefaultProperty(nameof(Name))]
    [Persistent(Schema.Ap + nameof(Sector))]
    public class Sector : BaseEntity
    {
        public static int LionSectorId = 1;
        public static string LionSectorName = "León";
        public static int ShofarSectorId = 2;
        public static string ShofarSectorName = "Shofar";
        public static int EagleSectorId = 3;
        public static string EagleSectorName = "Águila";

        private string name;

        [Caption("Nombre")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(ref name, value);
        }

        [MemberDesignTimeVisibility(false)]
        [Association("Sector-Places")]
        public XPCollection<Place> Places => GetCollection<Place>();

        [MemberDesignTimeVisibility(false)]
        [Association("Sector-PaymentPlans")]
        public XPCollection<PaymentPlan> PaymentPlans => GetCollection<PaymentPlan>();

        [MemberDesignTimeVisibility(false)]
        [Association("Sector-Operations")]
        public XPCollection<Operation> Operations => GetCollection<Operation>();


        public Sector(Session session) : base(session)
        {
        }


    }
}
