using DevExpress.Xpo;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ActivacionProfetica.Module.SharedKernel
{
    [NonPersistent]
    [OptimisticLocking("Version")]
    public abstract class BaseEntity : XPBaseObject
    {
        public BaseEntity(Session session) : base(session) { }

        private int id;

        [Key(true)]
        [MemberDesignTimeVisibility(false)]
        public int Id
        {
            get => id;
            set => SetPropertyValue(ref id, value);
        }

        protected new object GetPropertyValue([CallerMemberName] string propertyName = null)
            => base.GetPropertyValue(propertyName);

        protected new T GetPropertyValue<T>([CallerMemberName] string propertyName = null)
            => base.GetPropertyValue<T>(propertyName);

        protected bool SetPropertyValue<T>(ref T propertyValueHolder, T newValue, [CallerMemberName] string propertyName = null)
            => base.SetPropertyValue<T>(propertyName, ref propertyValueHolder, newValue);

        protected new XPCollection GetCollection([CallerMemberName] string propertyName = null)
            => base.GetCollection(propertyName);

        protected new XPCollection<T> GetCollection<T>([CallerMemberName] string propertyName = null)
            where T : class => base.GetCollection<T>(propertyName);

        protected new T GetDelayedPropertyValue<T>([CallerMemberName] string propertyName = null)
            => base.GetDelayedPropertyValue<T>(propertyName);

        protected bool SetDelayedPropertyValue<T>(T value, [CallerMemberName] string propertyName = null)
            => base.SetDelayedPropertyValue(propertyName, value);

        protected new object EvaluateAlias([CallerMemberName] string propertyName = null)
            => base.EvaluateAlias(propertyName);

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            foreach (MethodInfo infoMetodo in GetType().GetMethods())
            {
                object[] annotations = infoMetodo.GetCustomAttributes(typeof(OnChangedPropertyAttribute), true);
                if (annotations.Length > 0)
                {
                    OnChangedPropertyAttribute atributo = (OnChangedPropertyAttribute)annotations[0];
                    if (atributo.PropertyName == propertyName)
                    {
                        ParameterInfo[] parametros = infoMetodo.GetParameters();
                        infoMetodo.Invoke(this, parametros.Length == 0 ? null : new object[] { oldValue, newValue });
                    }
                }
            }
        }
        public void CallOnChanged(string propertyName)
        {
            OnChanged(propertyName);
        }

        //TODO: CreateBy, CreateAt, UpdateAt, UpdateBy
    }
}
