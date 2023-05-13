using DevExpress.Persistent.Validation;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ActivacionProfetica.Module.SharedKernel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredFieldAttribute : RuleRequiredFieldAttribute
    {
        public RequiredFieldAttribute([CallerMemberName] string propertyName = null,
            [CallerFilePath] string filePath = null)
            : base(ConvertToConvention(propertyName, filePath),
                  DefaultContexts.Save, "\"{TargetPropertyName}\" no debe estar vacío.")
        { }

        private static string ConvertToConvention(string propertyName = null, string filePath = null)
        {
            string className = propertyName is null ? string.Empty : Path.GetFileName(filePath).Replace(".cs", "");
            string id = className + "RequiredField" + (propertyName ?? "") + "Empty";
            return id;
        }

        protected override Type RuleType => typeof(RequiredField);

        public class RequiredField : RuleRequiredField
        {
            public RequiredField() : base() { }

            public RequiredField(IRuleRequiredFieldProperties properties) : base(properties) { }

            protected override bool IsValueValid(object value, out string errorMessage)
            {
                bool result = base.IsValueValid(value, out errorMessage);
                if (result)
                {
                    if (value is string)
                    {
                        //Check if all chars are blank.
                        result = value.ToString().Trim() != string.Empty;
                    }
                    else if (value.GetType().IsEnum)
                    {
                        //Check if is null enum.
                        string val = value.ToString();
                        result = val != "None";
                    }

                }


                return result;
            }
        }

        public interface IRequiredFieldProperties : IRuleRequiredFieldProperties { }
    }
}
