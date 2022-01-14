using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WmdaConnect.Shared.Converters
{
    public class MultiFormatDateConverter : DateTimeConverterBase
    {
        public IList<string> DateTimeFormats { get; set; } = new[] { "yyyy-MM-dd", "yyyyMMdd" };

        public DateTimeStyles DateTimeStyles { get; set; }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullableType = IsNullableType(objectType);
            if (reader.TokenType == JsonToken.Null && !isNullableType)
            {
                throw new JsonSerializationException(
                    string.Format(CultureInfo.InvariantCulture, "Cannot convert null value to {0}.", objectType));
            }

            var underlyingObjectType = isNullableType ? Nullable.GetUnderlyingType(objectType)! : objectType;
            if (reader.TokenType == JsonToken.Date)
            {
                if (underlyingObjectType == typeof(DateTimeOffset))
                {
                    return reader.Value is not DateTimeOffset
                        ? new DateTimeOffset((DateTime) reader.Value)
                        : reader.Value;
                }

                if (reader.Value is DateTimeOffset offset)
                {
                    return offset.DateTime;
                }

                return reader.Value;
            }

            if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null)
            {
                var errorMessage = string.Format(
                    CultureInfo.InvariantCulture,
                    "Unexpected token parsing date. Expected String, got {0}.",
                    reader.TokenType);
                throw new JsonSerializationException(errorMessage);
            }

            var dateString = (string)reader.Value;
            if (underlyingObjectType == typeof(DateTimeOffset))
            {
                foreach (var format in this.DateTimeFormats)
                {
                    // adjust this as necessary to fit your needs
                    if (DateTimeOffset.TryParseExact(dateString, format, CultureInfo.InvariantCulture, this.DateTimeStyles, out var date))
                    {
                        return date;
                    }
                }
            }

            if (underlyingObjectType == typeof(DateTime))
            {

                foreach (var format in this.DateTimeFormats)
                {
                    // adjust this as necessary to fit your needs
                    if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, this.DateTimeStyles, out var date))
                    {
                        return date;
                    }
                }
            }

            return existingValue; //This allows the invalid format to continue to the API where it fails and shows an output error
            throw new JsonException("Unable to parse \"" + dateString + "\" as a date.");
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public static bool IsNullableType(Type t)
        {
            if (t.IsGenericTypeDefinition || t.IsGenericType)
            {
                return t.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            return false;
        }
    }
}
