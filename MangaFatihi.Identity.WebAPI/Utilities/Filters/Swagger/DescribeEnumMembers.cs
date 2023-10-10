using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MangaFatihi.Management.WebAPI.Utilities.Filters.Swagger
{
    /// <summary>
    /// Swagger schema filter to modify description of enum types so they
    /// show the XML docs attached to each member of the enum.
    /// </summary>
    public class DescribeEnumMembersSchemaFilter : ISchemaFilter
    {
        private readonly XDocument mXmlComments;

        /// <summary>
        /// Initialize schema filter.
        /// </summary>
        /// <param name="argXmlComments">Document containing XML docs for enum members.</param>
        public DescribeEnumMembersSchemaFilter(XDocument argXmlComments)
          => mXmlComments = argXmlComments;

        /// <summary>
        /// Apply this schema filter.
        /// </summary>
        /// <param name="argSchema">Target schema object.</param>
        /// <param name="argContext">Schema filter context.</param>
        public void Apply(OpenApiSchema argSchema, SchemaFilterContext argContext)
        {
            var EnumType = argContext.Type;

            if (!EnumType.IsEnum) return;

            var sb = new StringBuilder(argSchema.Description);

            sb.AppendLine("<p>Possible values:</p>");
            sb.AppendLine("<ul>");

            foreach (var name in Enum.GetValues(EnumType))
            {
                // Allows for large enums
                var value = Convert.ToInt64(name);
                var fullName = $"F:{EnumType.FullName}.{name}";

                var description = mXmlComments.XPathEvaluate(
                    $"normalize-space(//member[@name = '{fullName}']/summary/text())"
                ) as string;

                sb.AppendLine($"<li><b>{value} - {name}</b>: {description}</li>");
            }

            sb.AppendLine("</ul>");

            argSchema.Description = sb.ToString();
        }
    }
}
