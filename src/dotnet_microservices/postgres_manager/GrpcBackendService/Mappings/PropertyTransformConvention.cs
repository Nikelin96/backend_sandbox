
using Dapper.FluentMap.Conventions;
using System.Text.RegularExpressions;

namespace GrpcBackendService.Models.Mappings;
public sealed class PropertyTransformConvention : Convention
{
    public PropertyTransformConvention()
    {
        Properties().Configure(configure => configure.Transform(columnName => Regex.Replace(input: columnName, pattern: "([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", replacement: "$1$3_$2$4").ToLower()));
    }
}
