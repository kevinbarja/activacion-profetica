using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Xpo;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Venta")]
    //[DefaultProperty(nameof(Id+FullName))]
    [Persistent(Schema.Ap + nameof(Sale))]
    public class Sale : BaseEntity
    {
        public Sale(Session session) : base(session)
        {
        }
    }
}
