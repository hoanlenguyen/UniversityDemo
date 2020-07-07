using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UniversityDemo.BaseEntities
{
    public class DateTimeOffsetJsonConverter: IsoDateTimeConverter
    {
        public static JsonConverter Instance { get; set; } = new DateTimeOffsetJsonConverter();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTimeOffset date)
                value = date.UtcDateTime;
            base.WriteJson(writer, value, serializer);
        }
    }
}
