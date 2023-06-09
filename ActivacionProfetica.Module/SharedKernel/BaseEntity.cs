using ActivacionProfetica.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.SharedKernel
{
    [NonPersistent]
    [OptimisticLocking("Version")]
    public abstract class BaseEntity : XPBaseObject
    {
        public BaseEntity(Session session) : base(session) { }

        private int id;

        [Caption("Código")]
        [Key(true)]
        //[MemberDesignTimeVisibility(false)]
        public int InternalId
        {
            get => id;
            set => SetPropertyValue(ref id, value);
        }

        ApplicationUser GetCurrentUser()
        {
            if (Session != null)
            {
                return Session.FindObject<ApplicationUser>(CriteriaOperator.Parse("Oid=CurrentUserId()"));  // In non-XAF apps where SecuritySystem.Instance is unavailable (v20.1.7+).
            }
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreatedOn = DateTime.Now;
            CreatedBy = GetCurrentUser();
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            UpdatedOn = DateTime.Now;
            UpdatedBy = GetCurrentUser();
        }
        ApplicationUser createdBy;
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Caption("Registrado por")]
        [ModelDefault("AllowEdit", "False")]
        public ApplicationUser CreatedBy
        {
            get { return createdBy; }
            set { SetPropertyValue("CreatedBy", ref createdBy, value); }
        }

        [ModelDefault("EditMask", "dd.MM.yyyy hh:mm:ss")]
        DateTime createdOn;
        [Caption("Fecha de registro")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "G")]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { SetPropertyValue("CreatedOn", ref createdOn, value); }
        }

        ApplicationUser updatedBy;
        [Caption("Actualizado por")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False")]
        public ApplicationUser UpdatedBy
        {
            get { return updatedBy; }
            set { SetPropertyValue("UpdatedBy", ref updatedBy, value); }
        }

        [ModelDefault("EditMask", "dd.MM.yyyy hh:mm:ss")]
        DateTime updatedOn;
        [Caption("Fecha actualización")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "G")]
        public DateTime UpdatedOn
        {
            get { return updatedOn; }
            set { SetPropertyValue("UpdatedOn", ref updatedOn, value); }
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


        protected override void OnDeleting()
        {
            base.OnDeleting();
            if (Session.CollectReferencingObjects(this).Count > 0)
            {
                string usedby = "Usado por: " + Environment.NewLine;
                int i = 0;

                foreach (var obj in Session.CollectReferencingObjects(this))
                {
                    i++;
                    usedby += obj.GetType().Name + Environment.NewLine;
                    if (i > 2)
                    {
                        usedby += "entre otros...";
                        break;
                    }
                }
                throw new Exception(usedby);
            }
        }
    }
}
