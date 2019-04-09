namespace Prototype.OpenTelematics.Api.Security
{
    /// <summary>
    /// API Will use Basic Authentication of Form "Authorization" "Basic Base64(UserId:Password)"
    /// </summary>
    public static class BasicAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Basic";
    }
}