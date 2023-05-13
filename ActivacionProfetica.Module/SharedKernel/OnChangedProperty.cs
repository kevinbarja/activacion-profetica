using System;

namespace ActivacionProfetica.Module.SharedKernel
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class OnChangedPropertyAttribute : Attribute
    {
        public OnChangedPropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }
}
