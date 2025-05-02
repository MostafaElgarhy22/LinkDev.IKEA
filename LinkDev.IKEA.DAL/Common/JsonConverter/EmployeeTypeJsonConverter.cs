using System.Text.Json;
using System.Text.Json.Serialization;
using LinkDev.IKEA.DAL.Common.Enumss;

namespace LinkDev.IKEA.DAL.Common.JsonConverter
{
    internal class EmployeeTypeJsonConverter : JsonConverter<EmployeeType>
    {
        public override EmployeeType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var employeeTypeAsString = reader.GetString()!;
            return employeeTypeAsString?.ToLower() switch
            {
                "fulltime" => EmployeeType.FullTime,
                "parttime" => EmployeeType.PartTime,
                "internship" => EmployeeType.Intern,
                _ => EmployeeType.FullTime
            };
        }

        public override void Write(Utf8JsonWriter writer, EmployeeType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}
