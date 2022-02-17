using System;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WmdaConnect.Models.MessageBases
{
    /// <summary>
    /// Used to suppress MessageType property when present in *Request objects
    /// But still show them for IMessage objects that derive from *Request objects
    /// See #1637 for more details
    /// </summary>
    public class SwaggerIgnorePropertyOnlyInRequestObjectsSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            if (context.Type.GetInterfaces().Contains(typeof(IMessage)))
            {
                return;
            }

            var excludedProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnorePropertyOnlyInRequestObjectsAttribute>() != null);

            foreach (var excludedProperty in excludedProperties)
            {
                var propertyToRemove = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, excludedProperty.Name, StringComparison.OrdinalIgnoreCase));

                if (propertyToRemove != null)
                {
                    schema.Properties.Remove(propertyToRemove);
                }
            }
        }
    }
}
