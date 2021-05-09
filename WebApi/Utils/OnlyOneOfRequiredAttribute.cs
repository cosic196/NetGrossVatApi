using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApi.Utils
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class OnlyOneOfRequiredAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Only one of '{0}' required.";

        private readonly object _typeId = new object();

        public string[] RequiredProperties { get; private set; }

        public OnlyOneOfRequiredAttribute(params string[] requiredProperties)
            : base(_defaultErrorMessage)
        {
            RequiredProperties = requiredProperties;
        }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.InvariantCulture, ErrorMessageString,
                string.Join(',', RequiredProperties));
        }

        public override bool IsValid(object value)
        {
            if(RequiredProperties.Length == 0)
            {
                return true;
            }
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            bool foundOne = false;
            foreach (var requiredProperty in RequiredProperties)
            {
                object property = properties.Find(requiredProperty, true).GetValue(value);
                if(property != null)
                {
                    if(foundOne)
                    {
                        return false;
                    }
                    foundOne = true;
                }
            }
            return foundOne;
        }
    }
}
