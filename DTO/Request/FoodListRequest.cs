namespace food_order_dotnet.DTO.Request
{
    public class FoodListRequest : PaginationRequest
    {
        public string? FoodName { get; set; }
        public int? CategoryId { get; set; }
    }
}