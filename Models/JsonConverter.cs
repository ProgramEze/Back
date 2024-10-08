using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Back.Models
{
    public class StringEnumConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Enum.Parse(objectType, reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }
    }
}