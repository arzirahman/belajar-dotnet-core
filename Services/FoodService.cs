using food_order_dotnet.Data;
using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace food_order_dotnet.Services
{
    public class FoodService(AppDbContext dbContext, JwtService jwtService)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly JwtService _jwtService = jwtService;

        public async Task<FoodListResponse> GetFoods(FoodListRequest param)
        {
            var query = _dbContext.Foods.AsQueryable();
            query = query.Include(f => f.Category);
            if (param.CategoryId.HasValue)
            {
                query = query.Where(f => f.CategoryId == param.CategoryId);
            }
            if (!string.IsNullOrWhiteSpace(param.FoodName))
            {
                var foodName = param.FoodName.ToLower();
                query = query.Where(f => f.FoodName != null && EF.Functions.Like(f.FoodName.ToLower(), $"%{foodName}%"));
            }
            var sortParams = param.SortBy?.Split(",") ?? [];
            if(sortParams[0] == "foodName" && sortParams[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase))
            {
                query = query.OrderByDescending(f => f.FoodName);
            } 
            else
            {
                query = query.OrderBy(f => f.FoodName);
            }
            var totalItems = await query.CountAsync();
            var userId = _jwtService.GetUserData().UserId;
            var foods = await query
                .Skip(((param.PageNumber - 1) * param.PageSize) ?? 0)
                .Take(param.PageSize ?? 10)
                .Select(
                    f => new FoodListDTO
                    {
                        FoodId = f.FoodId,
                        Category = new FoodCategoryDto
                        {
                            CategoryId = f.CategoryId,
                            CategoryName = f.Category != null ? f.Category.CategoryName : null
                        },
                        FoodName = f.FoodName,
                        Price = f.FoodId,
                        ImageFilename = f.ImageFilename,
                        IsCart = _dbContext.Carts.Any(c => c.UserId == userId && c.FoodId == f.FoodId),
                        IsFavorite = _dbContext.FavoriteFoods.Any(ff => 
                            ff.UserId == userId && 
                            ff.FoodId == f.FoodId && 
                            ff.IsFavorite == true
                        ),
                    }
                ).ToListAsync();
            return new FoodListResponse
            {
                Total = totalItems,
                Status = "daw",
                StatusCode = 33,
                Message = "awd",
                Data = foods
            };
        }
    }
}