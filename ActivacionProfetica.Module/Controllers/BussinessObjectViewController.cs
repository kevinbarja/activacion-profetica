using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ActivacionProfetica.Module.Controllers
{
    /// <summary>
    /// A base class for View Controllers intended for Object Views. 
    /// Ref: https://blog.delegate.at/2018/03/10/xaf-best-practices-2017-03.html#ViewControllers
    /// </summary>
    [ToolboxItem(false)]
    public abstract class BusinessObjectViewController<TView, TObjectType> : ViewController<TView>
        where TObjectType : class
        where TView : ObjectView
    {
        public event EventHandler<CurrentObjectChangingEventArgs<TObjectType>> CurrentObjectChanging;
        public event EventHandler<CurrentObjectChangedEventArgs<TObjectType>> CurrentObjectChanged;

        protected BusinessObjectViewController()
            => TargetObjectType = typeof(TObjectType);

        protected override void OnActivated()
        {
            base.OnActivated();

            UnsubscribeFromViewEvents();
            SubscribeToViewEvents();
        }

        protected override void OnDeactivated()
        {
            UnsubscribeFromViewEvents();

            base.OnDeactivated();
        }

        private void SubscribeToViewEvents()
        {
            if (View != null)
            {
                View.QueryCanChangeCurrentObject += View_QueryCanChangeCurrentObject;
                View.CurrentObjectChanged += View_CurrentObjectChanged;
            }
        }

        private void UnsubscribeFromViewEvents()
        {
            if (View != null)
            {
                View.QueryCanChangeCurrentObject -= View_QueryCanChangeCurrentObject;
                View.CurrentObjectChanged -= View_CurrentObjectChanged;
            }
        }

        void View_QueryCanChangeCurrentObject(object sender, CancelEventArgs e)
        {
            var args = new CurrentObjectChangingEventArgs<TObjectType>(e.Cancel, CurrentObject);
            OnCurrentObjectChanging((TView)sender, args);
            e.Cancel = args.Cancel;
        }

        public virtual void OnCurrentObjectChanging(TView view, CurrentObjectChangingEventArgs<TObjectType> e)
            => CurrentObjectChanging?.Invoke(this, e);

        void View_CurrentObjectChanged(object sender, EventArgs e)
            => OnCurrentObjectChanged((TView)sender, new CurrentObjectChangedEventArgs<TObjectType>(CurrentObject));

        public virtual void OnCurrentObjectChanged(TView view, CurrentObjectChangedEventArgs<TObjectType> args)
            => CurrentObjectChanged?.Invoke(this, args);

        /// <summary>
        ///  Gets the current object (see DevExpress.ExpressApp.View.CurrentObject).
        /// </summary>
        [Browsable(false)]
        public TObjectType CurrentObject
        {
            get
            {
                if (!(View?.CurrentObject is IObjectRecord))
                {
                    return View?.CurrentObject as TObjectType;
                }
                return ObjectSpace.GetObject(View?.CurrentObject) as TObjectType;
            }
        }

        /// <summary>
        /// Gets the list of selected objects (see DevExpress.ExpressApp.View.SelectedObjects).
        /// </summary>
        [Browsable(false)]
        public IList<TObjectType> SelectedObjects
        {
            get
            {
                IList selectedObjects = View.SelectedObjects;
                if (selectedObjects.Count > 0 && selectedObjects[0] is IObjectRecord)
                {
                    IList<TObjectType> list = new List<TObjectType>();
                    {
                        foreach (object item in selectedObjects)
                        {
                            list.Add((TObjectType)base.ObjectSpace.GetObject(item));
                        }
                        return list;
                    }
                }
                return selectedObjects.OfType<TObjectType>().ToList();
            }
        }

        /// <summary>
        ///  Gets the current Session.
        /// </summary>
        [Browsable(false)]
        public Session Session
        {
            get
            {
                return ((XPObjectSpace)ObjectSpace).Session;
            }
        }

        public XPCollection GetCollectionByNameMember(object obj, string nameMember)
        {
            DevExpress.ExpressApp.DC.ITypeInfo threatTypeInfo = Application.TypesInfo.FindTypeInfo(obj.GetType());
            return threatTypeInfo.FindMember(nameMember).GetValue(obj) as XPCollection;
        }

    }

    public class CurrentObjectChangedEventArgs<TObjectType> : EventArgs
        where TObjectType : class
    {
        public readonly TObjectType CurrentObject;

        public CurrentObjectChangedEventArgs(TObjectType obj)
            => CurrentObject = obj;
    }

    public class CurrentObjectChangingEventArgs<TObjectType> : EventArgs
        where TObjectType : class
    {
        public readonly TObjectType CurrentObject;

        public bool Cancel { get; set; }

        public CurrentObjectChangingEventArgs(TObjectType obj) : this(false, obj)
        {
        }

        public CurrentObjectChangingEventArgs(bool cancel, TObjectType obj)
        {
            Cancel = cancel;
            CurrentObject = obj;
        }
    }

}
