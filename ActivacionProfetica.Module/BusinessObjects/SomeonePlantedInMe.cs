using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Alguién sembró en mi")]
    [DefaultProperty(nameof(Date))]
    [Persistent(Schema.Ap + nameof(SomeonePlantedInMe))]
    public class SomeonePlantedInMe : BaseEntity
    {
        DateTime date;
        int altarOffering = 0;
        bool isActive = true;

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            date = DateTime.Now;
        }

        [ModelDefault("DisplayFormat", "{0:U}")]
        [Caption("Fecha")]
        [Nullable(false), RequiredField]
        public DateTime Date
        {
            get { return date; }
            set => SetPropertyValue(ref date, value);
        }

        //[ImmediatePostData]
        [ModelDefault("DisplayFormat", "{0:#}")]
        [Caption("Monto total de ofrenda levantada en altar")]
        [Nullable(false), RequiredField]
        public int AltarOffering
        {
            get => altarOffering;
            set => SetPropertyValue(ref altarOffering, value);
        }

        [NonPersistent]
        [Caption("Monto bendecido")]
        public int MontoBendecido
        {
            get
            {
                return Payments.Sum(p => p.SomeonePlantedInMeAmount);
            }
        }

        [NonPersistent]
        [Caption("Monto por bendecir")]
        public int MontoPorBendecir
        {
            get
            {
                return AltarOffering - Payments.Sum(p => p.SomeonePlantedInMeAmount);
            }
        }

        [Caption("Personas bendecidas")]
        [Association("SomeonePlantedInMe-Payments"), Aggregated]
        public XPCollection<Payment> Payments => GetCollection<Payment>();

        public SomeonePlantedInMe(Session session) : base(session)
        {
        }
    }
}
