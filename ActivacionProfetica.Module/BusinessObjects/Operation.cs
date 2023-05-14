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
    [Caption("Operación")]
    [DefaultProperty(nameof(Id))]
    [Persistent(Schema.Ap + nameof(Operation))]
    public class Operation : BaseEntity
    {
        Customer customer;
        OperationType operationType;

        [Caption("Código")]
        [Appearance("DisableCode", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public new int Id
        {
            get => base.Id;
        }

        [Caption("Persona")]
        [RequiredField]
        [Association("Customer-Operations")]
        [Persistent("Customer_Operations")]
        [ImmediatePostData]
        [ModelDefault("View", "Customer_LookupListView")]
        //[ModelDefault("LookupProperty", "CI")]
        public Customer Customer
        {
            get => customer;
            set => SetPropertyValue(ref customer, value);
        }

        [Caption("Tipo de operación")]
        [RequiredField]
        [Association("OperationType-Operations")]
        [Persistent("OperationType_Operations")]
        [ImmediatePostData]
        public OperationType OperationType
        {
            get => operationType;
            set => SetPropertyValue(ref operationType, value);
        }

        public Operation(Session session) : base(session)
        {
        }
    }
}
