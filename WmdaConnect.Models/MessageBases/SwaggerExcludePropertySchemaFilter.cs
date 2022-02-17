using System;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WmdaConnect.Models.MessageBases
{
    public class SwaggerExcludePropertySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            if (!context.Type.Name.EndsWith("Request"))
                return;

            switch (context.Type.Name)
            {
                case "SampleRequest":
                case "TypingRequest":
                case "CordBloodUnitReportRequest":
                case "ReservationRequest":
                case "InfectiousDiseaseMarkerRequest":
                    return;
            }

            var excludedProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnorePropertyAttribute>() != null);

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
