using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.SwaggerExamples
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum)
                return;

            schema.Enum.Clear();

            foreach (Enum e in Enum.GetValues(context.Type))
                schema.Enum.Add(new OpenApiString($"{Convert.ToInt32(e)} - {e.ToString()}"));
        }
    }
}
