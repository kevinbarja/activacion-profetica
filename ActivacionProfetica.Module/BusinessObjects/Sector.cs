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
    [Persistent(Schema.Rjv + nameof(Sector))]
    public class Sector : BaseEntity
    {
        public static int LionSectorId = 1;
        public static string LionSectorName = "León";
        public static int ShofarSectorId = 2;
        public static string ShofarSectorName = "Shofar";
        public static int EagleSectorId = 3;
        public static string EagleSectorName = "Águila";
        //
        public static int PastoresSectorId = 1;
        public static string PastoresSectorName = "Pastores";
        public static int MaestrosSectorId = 2;
        public static string MaestrosSectorName = "Maestros";
        public static int ApostolesSectorId = 3;
        public static string ApostolesSectorName = "Apóstoles";
        public static int ProfetasSectorId = 4;
        public static string ProfetasSectorName = "Profétas";
        public static int EvangelistasSectorId = 5;
        public static string EvangelistasSectorName = "Evangelistas";

        private string name;
        int amount;

        [Caption("Nombre")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(ref name, value);
        }

        [Caption("Ofrenda")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
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

        [MemberDesignTimeVisibility(false)]
        [Association("Sector-Users")]
        public XPCollection<ApplicationUser> Users => GetCollection<ApplicationUser>();

        public Sector(Session session) : base(session)
        {
        }


    }
}
