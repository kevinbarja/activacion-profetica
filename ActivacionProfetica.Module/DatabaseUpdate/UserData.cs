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
                var shepar = Updater.ObjectSpace.FirstOrDefault<Sector>(s => s.InternalId == Sector.PastoresSectorId);

                //User admin
                var userAdmin = Updater.ObjectSpace.CreateObject<ApplicationUser>();
                userAdmin.UserName = "Administrador";
                userAdmin.FullName = "Administrador";
                userAdmin.Sector = shepar;
                userAdmin.SetPassword("123");

                // The UserLoginInfo object requires a user object InternalId (Oid).
                // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                Updater.ObjectSpace.CommitChanges(); //This line persists created object(s).
                ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, Updater.ObjectSpace.GetKeyValueAsString(userAdmin));
                userAdmin.Roles.Add(adminRole);
            }
        }
    }
}
