using System.Net;
using food_order_dotnet.Data;
using food_order_dotnet.DTO.Request;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Models;
using food_order_dotnet.Resources;
using Microsoft.AspNetCore.WebUtilities;
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
            if(sortParams.Length > 1 && sortParams[0] == "foodName" && sortParams[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase))
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
                Message = ValidationMessages.GetFoodSuccess,
                StatusCode = (int) HttpStatusCode.OK,
                Status = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.OK),
                Data = foods
            };
        }

        public async Task<CartResponse> AddCart(CartRequest request)
        {
            if (!await _dbContext.Foods.AnyAsync(f => f.FoodId == request.FoodId))
            {
                throw new Exception(ValidationMessages.FoodIdNotFound);
            }
            var userId = _jwtService.GetUserData().UserId;
            var IsAdded = await _dbContext.Carts.AnyAsync(c => c.FoodId == request.FoodId && c.UserId == userId);
            if (!IsAdded)
            {
                var cart = new Cart
                {
                    FoodId = request.FoodId,
                    UserId = userId
                };
                _dbContext.Carts.Add(cart);
            }
            await _dbContext.SaveChangesAsync();
            var food = await GetFoodDTO(request.FoodId, userId);
            return new CartResponse
            {
                Total = 1,
                Message = string.Format(ValidationMessages.AddCartSuccess ?? "", food?.FoodName),
                StatusCode = (int) HttpStatusCode.OK,
                Status = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.OK),
                Data = food
            };
        }

        public async Task<CartResponse> DeleteCart(int? foodId)
        {
            if (!await _dbContext.Foods.AnyAsync(f => f.FoodId == foodId))
            {
                throw new Exception(ValidationMessages.FoodIdNotFound);
            }
            var userId = _jwtService.GetUserData().UserId;
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.FoodId == foodId && c.UserId == userId);
            if (cart != null)
            {
                _dbContext.Carts.Remove(cart);
                await _dbContext.SaveChangesAsync();
            }
            var food = await GetFoodDTO(foodId, userId);
            return new CartResponse
            {
                Total = 1,
                Message = string.Format(ValidationMessages.DeleteCartSuccess ?? "", food?.FoodName),
                StatusCode = (int) HttpStatusCode.OK,
                Status = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.OK),
                Data = food
            };
        }

        public async Task<CartResponse> ToggleFavorite(int? foodId)
        {
            if (!await _dbContext.Foods.AnyAsync(f => f.FoodId == foodId))
            {
                throw new Exception(ValidationMessages.FoodIdNotFound);
            }
            var userId = _jwtService.GetUserData().UserId;
            var favorite = await _dbContext.FavoriteFoods.FirstOrDefaultAsync(ff => ff.FoodId == foodId && ff.UserId == userId);
            if (favorite == null)
            {
                favorite = new FavoriteFood
                {
                    FoodId = foodId,
                    UserId = userId,
                    IsFavorite = true
                };
                _dbContext.FavoriteFoods.Add(favorite);
            } 
            else
            {
                favorite.IsFavorite = !favorite.IsFavorite;
                _dbContext.FavoriteFoods.Update(favorite);
            }
            Console.WriteLine(favorite);
            await _dbContext.SaveChangesAsync();
            var food = await GetFoodDTO(foodId, userId);
            return new CartResponse
            {
                Total = 1,
                Message = string.Format((food?.IsFavorite == true 
                    ? ValidationMessages.AddFavoriteSuccess 
                    : ValidationMessages.DeleteFavoriteSuccess) ?? "", food?.FoodName),
                StatusCode = (int) HttpStatusCode.OK,
                Status = ReasonPhrases.GetReasonPhrase((int) HttpStatusCode.OK),
                Data = food
            };
        }

        private async Task<FoodListDTO?> GetFoodDTO(int? foodId, int? userId)
        {
            return await _dbContext.Foods
                .Where(f => f.FoodId == foodId)
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
            ).FirstOrDefaultAsync();
        }
    }
}