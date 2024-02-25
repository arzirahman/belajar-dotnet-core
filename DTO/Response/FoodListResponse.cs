namespace food_order_dotnet.DTO.Response
{
    public class FoodListResponse : MessageResponse
    {
        public int? Total { get; set; }
        public List<FoodListDTO>? Data { get; set; }
    }
}