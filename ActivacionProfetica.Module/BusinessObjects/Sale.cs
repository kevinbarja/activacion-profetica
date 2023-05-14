using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Venta")]
    [DefaultProperty(nameof(Id))]
    [Persistent(Schema.Ap + nameof(Sale))]
    public class Sale : BaseEntity
    {
        Customer customer;

        [Caption("Código")]
        [Appearance("DisableCode", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public new int Id
        {
            get => base.Id;
        }

        [Caption("Persona")]
        [RequiredField]
        [Association("Customer-Sales")]
        [Persistent("Customer_Sales")]
        [ImmediatePostData]
        [ModelDefault("View", "Customer_LookupListView")]
        //[ModelDefault("LookupProperty", "CI")]
        public Customer Customer
        {
            get => customer;
            set => SetPropertyValue(ref customer, value);
        }

        public Sale(Session session) : base(session)
        {
        }
    }
}
