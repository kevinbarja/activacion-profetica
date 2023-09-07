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
        private const string CreateSchemeScript = "IF NOT EXISTS ( SELECT  * FROM sys.schemas WHERE   Name = N'rjv' ) EXEC('CREATE SCHEMA [rjv] AUTHORIZATION [dbo]');";

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
            new PlaceStatusData(this);
            new PlaceData(this);
            new PaymentPlanData(this);
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
