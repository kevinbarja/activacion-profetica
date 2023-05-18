using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Reflection;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater
    {
        private const string CreateSchemeScript = "IF NOT EXISTS ( SELECT  * FROM sys.schemas WHERE   Name = N'ap' ) EXEC('CREATE SCHEMA [ap] AUTHORIZATION [dbo]');";

        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            new RolData(this);
            new UserData(this);
            new SectorData(this);
            new PlaseStatusData(this);
            new PlaceData(this);

#if !RELEASE
            //ApplicationUser sampleUser = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "Usuario");
            //if (sampleUser == null)
            //{
            //    sampleUser = ObjectSpace.CreateObject<ApplicationUser>();
            //    sampleUser.UserName = "Usuario";
            //    sampleUser.FullName = "Jonh Doe";
            //    // Set a password if the standard authentication type is used
            //    sampleUser.SetPassword("");

            //    // The UserLoginInfo object requires a user object InternalId (Oid).
            //    // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            //    ObjectSpace.CommitChanges(); //This line persists created object(s).
            //    ((ISecurityUserWithLoginInfo)sampleUser).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser));
            //}
            //PermissionPolicyRole defaultRole = CreateDefaultRole();
            //sampleUser.Roles.Add(defaultRole);

            //ApplicationUser userAdmin = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "75632256");
            //if (userAdmin == null)
            //{
            //    userAdmin = ObjectSpace.CreateObject<ApplicationUser>();
            //    userAdmin.UserName = "75632256";
            //    userAdmin.FullName = "Eduardo Barja";
            //    // Set a password if the standard authentication type is used
            //    userAdmin.SetPassword("");

            //    // The UserLoginInfo object requires a user object InternalId (Oid).
            //    // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            //    ObjectSpace.CommitChanges(); //This line persists created object(s).
            //    ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin));
            //}
            //// If a role with the Administrators singularName doesn't exist in the database, create this role
            //PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administradores");
            //if (adminRole == null)
            //{
            //    adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            //    adminRole.Name = "Administradores";
            //}
            //adminRole.IsAdministrative = true;
            //userAdmin.Roles.Add(adminRole);
            //ObjectSpace.CommitChanges(); //This line persists created object(s).
#endif
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            ExecuteNonQueryCommand(CreateSchemeScript, true);
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }

        #region Method for allow the access to functionality of the base class.  
        public int ExecuteSentence(string sql)
        {
            return ExecuteNonQueryCommand(sql, false);
        }

        public new IObjectSpace ObjectSpace => base.ObjectSpace;

        public Session Session => ((XPObjectSpace)base.ObjectSpace).Session;

        public Assembly Assembly() => System.Reflection.Assembly.GetExecutingAssembly();
        #endregion
    }
}
