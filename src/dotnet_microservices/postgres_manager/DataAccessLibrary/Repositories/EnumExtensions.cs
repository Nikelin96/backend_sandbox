using System.Text.RegularExpressions;

namespace DataAccessLibrary.Repositories;

static class EnumExtensions
{
    public static string ToPostgreEnum(this Enum pocoEnum) => Regex.Replace(input: pocoEnum.ToString(), pattern: "([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", replacement: "$1$3 $2$4").ToLower();
}