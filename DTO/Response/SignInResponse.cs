namespace food_order_dotnet.DTO.Response
{
    public class SignInDTO
    {
        public int? Id { get; set; }
        public string? Token { get; set; }
        public string? Type { get; set; }
        public string? Username { get; set; }
    }

    public class SignInResponse : MessageResponse
    {
        public SignInDTO? Data { get; set; }
    }
}