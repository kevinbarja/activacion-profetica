using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System.Linq;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class UserData : BaseData
    {
        public UserData(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<ApplicationUser>()
                             select ps).Any();
            if (isEmpty)
            {
                PermissionPolicyRole adminRole = Updater.ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == Constants.Role.Admin);
                PermissionPolicyRole publicRole = Updater.ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == Constants.Role.Public);

                //User admin
                var userAdmin = Updater.ObjectSpace.CreateObject<ApplicationUser>();
                userAdmin.UserName = "8935712";
                userAdmin.FullName = "Eduardo Barja";
                userAdmin.SetPassword("75632256");

                // The UserLoginInfo object requires a user object InternalId (Oid).
                // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                Updater.ObjectSpace.CommitChanges(); //This line persists created object(s).
                ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, Updater.ObjectSpace.GetKeyValueAsString(userAdmin));
                userAdmin.Roles.Add(adminRole);
            }
        }
    }
}
