namespace food_order_dotnet.DTO.Request
{
    public class PaginationRequest
    {
        public string? _sortBy;
        private int? _pageSize;
        private int? _pageNumber;

        public string? SortBy
        {
            get => _sortBy;
            set => _sortBy = (value != null && value.Split(",").Length > 1) ? value : "foodName,asc";
        }

        public int? PageSize
        {
            get => _pageSize;
            set => _pageSize = (value != null && value > 0) ? value : 10;
        }

        public int? PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (value != null && value > 0) ? value : 1;
        }
    }
}