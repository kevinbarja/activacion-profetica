using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System.Linq;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class RolData : BaseData
    {
        public RolData(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<PermissionPolicyRole>()
                             select ps).Any();
            if (isEmpty)
            {
                //Public access role
                //var publicAccessRole = Updater.ObjectSpace.CreateObject<PermissionPolicyRole>();
                //publicAccessRole.Name = Constants.Role.Public;
                //publicAccessRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                //publicAccessRole.AddNavigationPermission("Application/NavigationItems/Items/Consultas", SecurityPermissionState.Allow);
                //publicAccessRole.AddNavigationPermission("Application/NavigationItems/Items/ActivacionProfetica", SecurityPermissionState.Deny);
                //publicAccessRole.AddNavigationPermission("Application/NavigationItems/Items/Parametrizaciones", SecurityPermissionState.Deny);
                //publicAccessRole.AddNavigationPermission("Application/NavigationItems/Items/Reportes", SecurityPermissionState.Deny);
                //publicAccessRole.AddNavigationPermission("Application/NavigationItems/Items/Default", SecurityPermissionState.Deny);

                //publicAccessRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                //publicAccessRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                //publicAccessRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                //publicAccessRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "StoredPassword", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                //publicAccessRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                //publicAccessRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                //publicAccessRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                //publicAccessRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                //publicAccessRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Admin role
                var adminRole = Updater.ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = Constants.Role.Admin;
                adminRole.IsAdministrative = true;

                //This section move to internal section:
                PermissionPolicyRole operationEjecutive = Updater.ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == Constants.Role.OperationEjecutive);
                if (operationEjecutive is null)
                {
                    operationEjecutive = Updater.ObjectSpace.CreateObject<PermissionPolicyRole>();
                    operationEjecutive.Name = Constants.Role.OperationEjecutive;
                    operationEjecutive.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                    operationEjecutive.AddNavigationPermission("Application/NavigationItems/Items/Consultas", SecurityPermissionState.Deny);
                    operationEjecutive.AddNavigationPermission("Application/NavigationItems/Items/ActivacionProfetica", SecurityPermissionState.Allow);
                    operationEjecutive.AddNavigationPermission("Application/NavigationItems/Items/ActivacionProfetica/Items/OperationNoAdmin", SecurityPermissionState.Deny);
                    operationEjecutive.AddNavigationPermission("Application/NavigationItems/Items/Parametrizaciones", SecurityPermissionState.Deny);
                    operationEjecutive.AddNavigationPermission("Application/NavigationItems/Items/Reportes", SecurityPermissionState.Deny);
                    operationEjecutive.AddNavigationPermission("Application/NavigationItems/Items/Default", SecurityPermissionState.Deny);
                }

                PermissionPolicyRole operationSupervisor = Updater.ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == Constants.Role.OperationSupervisor);
                if (operationSupervisor is null)
                {
                    operationSupervisor = Updater.ObjectSpace.CreateObject<PermissionPolicyRole>();
                    operationSupervisor.Name = Constants.Role.OperationSupervisor;
                    operationSupervisor.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                    operationSupervisor.AddNavigationPermission("Application/NavigationItems/Items/Consultas", SecurityPermissionState.Deny);
                    operationSupervisor.AddNavigationPermission("Application/NavigationItems/Items/ActivacionProfetica", SecurityPermissionState.Allow);
                    operationSupervisor.AddNavigationPermission("Application/NavigationItems/Items/Parametrizaciones", SecurityPermissionState.Deny);
                    operationSupervisor.AddNavigationPermission("Application/NavigationItems/Items/Reportes", SecurityPermissionState.Allow);
                    operationSupervisor.AddNavigationPermission("Application/NavigationItems/Items/Default", SecurityPermissionState.Deny);
                }
            }



        }
    }
}
