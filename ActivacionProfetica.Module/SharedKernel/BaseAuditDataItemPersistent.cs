using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.SharedKernel
{
    [Caption("Auditoría")]
    public class BaseAuditDataItemPersistent : AuditDataItemPersistent
    {
        public BaseAuditDataItemPersistent(Session session) : base(session) { }


        public string PropertyCaption
        {
            get
            {
                int id = AuditedObject.IntId != null ? AuditedObject.IntId.Value : 0;
                string result = string.Empty;

                if (AuditedObject != null)
                {
                    switch (PropertyName)
                    {
                        case "CreateOn":
                            result = "Fecha de registro";
                            break;
                        case "CreateBy":
                            result = "Registrado por";
                            break;
                        case "UpdatedOn":
                            result = "Fecha de actualización";
                            break;
                        case "UpdatedBy":
                            result = "Actualizado por";
                            break;
                        default:
                            result = CaptionHelper.GetMemberCaption(AuditedObject.Target.GetType(), PropertyName);
                            break;
                    }
                }
                if (result == "" || result == null)
                {
                    if (id != 0)
                        result = $"Objeto: {CaptionHelper.GetClassCaption(AuditedObject.DisplayName)} InternalId: {id}";
                    else
                    {
                        result = $"Objeto: {CaptionHelper.GetClassCaption(AuditedObject.DisplayName)}";
                    }
                }
                return result;
            }
        }

        public string OperationTypeCaption
        {
            get
            {
                //Implement a custom logic to obtain a display name based on the OperationType.
                string operationTypeString = string.Empty;
                if (OperationType != null)
                {
                    switch (OperationType.Trim())
                    {
                        case "ObjectCreated":
                            operationTypeString = "Creación";
                            break;
                        case "ObjectChanged":
                            operationTypeString = "Modificación";
                            break;
                        case "ObjectDeleted":
                            operationTypeString = "Eliminación";
                            break;
                        case "AddedToCollection":
                            operationTypeString = "Agregado a la lista";
                            break;
                        case "CollectionObjectChanged":
                            operationTypeString = "Objeto de la lista modificado";
                            break;
                        case "RemovedFromCollection":
                            operationTypeString = "Eliminado de la lista";
                            break;
                        case "InitialValueAssigned":
                            operationTypeString = "Valor inicial asignado";
                            break;
                    }
                }
                return operationTypeString;
            }
        }

        public string UserCaption
        {
            get
            {
                ApplicationUser applicationUser =
                    Session.FindObject<ApplicationUser>(new BinaryOperator(nameof(ApplicationUser.UserName), UserName));
                return applicationUser.FullName;
            }
        }



    }
}
