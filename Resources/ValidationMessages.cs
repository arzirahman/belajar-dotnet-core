using System.Resources;

namespace food_order_dotnet.Resources
{
    public static class ValidationMessages
    {
        private static readonly ResourceManager _resourceManager = new(typeof(ValidationMessages));

        public static string? UsernameRequired => _resourceManager.GetString(nameof(UsernameRequired));
        public static string? PasswordRequired => _resourceManager.GetString(nameof(PasswordRequired));
        public static string? PasswordLength => _resourceManager.GetString(nameof(PasswordLength));
        public static string? LoginSuccess => _resourceManager.GetString(nameof(LoginSuccess));
    }
}
