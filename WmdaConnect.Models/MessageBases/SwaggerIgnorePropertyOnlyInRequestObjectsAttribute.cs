using System;

namespace WmdaConnect.Models.MessageBases
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerIgnorePropertyOnlyInRequestObjectsAttribute : Attribute
    {
    }
}
