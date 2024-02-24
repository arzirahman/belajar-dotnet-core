using food_order_dotnet.Data;
using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace food_order_dotnet.Controllers 
{

    [ApiController]
    [Route("food-order")]
    public class FoodController(AppDbContext dbContext, FoodService foodService) : ApiController 
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly FoodService _foodService = foodService;

        [HttpGet("foods")]
        [Authorize]
        public async Task<ActionResult<FoodListResponse>> GetFoods([FromQuery] FoodListRequest param)
        {
            return await ExecuteAction(async () => await _foodService.GetFoods(param));
        }
    }
}