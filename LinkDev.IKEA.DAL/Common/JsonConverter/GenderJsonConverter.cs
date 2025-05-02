using System.Text.Json;
using System.Text.Json.Serialization;
using LinkDev.IKEA.DAL.Common.Enumss;

namespace LinkDev.IKEA.DAL.Common.JsonConverter
{
    internal class GenderJsonConverter : JsonConverter<Gender>
    {
        public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var genderAsString = reader.GetString()!;

            return genderAsString?.ToLower() switch
            {
                "male" => Gender.Male,
                "female" => Gender.Female,
                _ => Gender.Male
            };
        }

        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
