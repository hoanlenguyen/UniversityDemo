using System;
using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UniversityDemo.BaseEntities
{
    internal class CoordinatesToPointConverter : JsonConverter
    {
        public static CoordinatesToPointConverter Instance { get; set; } = new CoordinatesToPointConverter();

        public override bool CanConvert(Type objectType)
        {
            //return objectType == typeof(Coordinates) || objectType == typeof(Point);
            return objectType== typeof(Point);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.Load(reader);

            if (jToken.Type == JTokenType.Object)
            {
                var point = jToken.ToObject<Point>(serializer);

                //return new Coordinates(point.Position.Longitude, point.Position.Latitude);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //if (value is Coordinates coordinates)
            //    JToken.FromObject(new Point(coordinates.Longitude, coordinates.Latitude)).WriteTo(writer);
            //else
                JValue.CreateNull().WriteTo(writer);
        }
    }
}
