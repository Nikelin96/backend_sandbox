namespace GrpcBackendService.Models.Mappings;

using Dapper.FluentMap.Conventions;
using System.Text.RegularExpressions;

public sealed class PropertyTransformConvention : Convention
{
    public PropertyTransformConvention()
    {
        Properties().Configure(c => c.Transform(s => Regex.Replace(input: s, pattern: "([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", replacement: "$1$3_$2$4").ToLower()));
    }
}
