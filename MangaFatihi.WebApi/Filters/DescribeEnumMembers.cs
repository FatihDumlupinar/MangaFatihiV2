using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MangaFatihi.WebApi.Filters
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

            foreach (var EnumMemberName in Enum.GetNames(EnumType))
            {
                var FullEnumMemberName = $"F:{EnumType.FullName}.{EnumMemberName}";

                var EnumMemberDescription = mXmlComments.XPathEvaluate(
                  $"normalize-space(//member[@name = '{FullEnumMemberName}']/summary/text())"
                ) as string;

                if (string.IsNullOrEmpty(EnumMemberDescription)) continue;

                sb.AppendLine($"<li><b>{EnumMemberName}</b>: {EnumMemberDescription}</li>");
            }

            sb.AppendLine("</ul>");

            argSchema.Description = sb.ToString();
        }
    }
}
