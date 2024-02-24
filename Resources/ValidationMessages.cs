using System.Resources;

namespace food_order_dotnet.Resources
{
    public static class ValidationMessages
    {
        private static readonly ResourceManager _resourceManager = new(typeof(ValidationMessages));

        public static string? UsernameRequired => _resourceManager.GetString(nameof(UsernameRequired));
        public static string? UsernameExist => _resourceManager.GetString(nameof(UsernameExist));
        public static string? FullnameRequired => _resourceManager.GetString(nameof(FullnameRequired));
        public static string? PasswordRequired => _resourceManager.GetString(nameof(PasswordRequired));
        public static string? RetypePasswordRequired => _resourceManager.GetString(nameof(RetypePasswordRequired));
        public static string? PasswordLength => _resourceManager.GetString(nameof(PasswordLength));
        public static string? PasswordNotMatch => _resourceManager.GetString(nameof(PasswordNotMatch));
        public static string? LoginSuccess => _resourceManager.GetString(nameof(LoginSuccess));
        public static string? RegisterSuccess => _resourceManager.GetString(nameof(RegisterSuccess));
        public static string? ServerError => _resourceManager.GetString(nameof(ServerError));
    }
}
