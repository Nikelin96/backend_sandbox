namespace GrpcBackendService;

static class EnumExtensions
{
    public static string ToPostgreEnum(this System.Enum pocoEnum)
    {
        return pocoEnum.ToString().ToLower().Replace("_", " ");
    }
}