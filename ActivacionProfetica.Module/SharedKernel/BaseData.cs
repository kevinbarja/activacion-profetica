using ActivacionProfetica.Module.DatabaseUpdate;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace ActivacionProfetica.Module.SharedKernel
{
    public abstract class BaseData
    {
        public Updater Updater { get; set; }

        public BaseData(Updater updater)
        {
            Updater = updater;
            Execute();
            SaveChanges();
        }

        public abstract void Execute();

        public ObjectType GetObjectByKey<ObjectType>(object key)
        {
            return Updater.ObjectSpace.GetObjectByKey<ObjectType>(key);
        }
        public ObjectType FindObject<ObjectType>(CriteriaOperator criteria)
        {
            return Updater.ObjectSpace.FindObject<ObjectType>(criteria);
        }

        public ObjectType CreateObject<ObjectType>()
        {
            return Updater.ObjectSpace.CreateObject<ObjectType>();
        }
        public XPQuery<T> Query<T>()
        {
            return Updater.Session.Query<T>();
        }

        public void SaveChanges()
        {
            Updater.ObjectSpace.CommitChanges();
        }
    }
}
