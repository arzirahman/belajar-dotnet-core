namespace food_order_dotnet.DTO.Response
{
    public class CartResponse : MessageResponse
    {
        public int? Total { get; set; }
        public FoodListDTO? Data { get; set; }
    }
}