namespace MyApp.Helper.ViewModels
{
    public class PagedResultViewModel<T> : ResultViewModel<List<T>>
    {
        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
        public static PagedResultViewModel<T> Success(List<T> data, int totalCount, int pageNumber, int pageSize, string message = "", int statusCode = 200)
        {
            return new PagedResultViewModel<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Errors = null,
                StatusCode = statusCode
            };
        }
        public static PagedResultViewModel<T> Failure(string message, List<string> errors = null, int statusCode = 400)
        {
            return new PagedResultViewModel<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>(),
                StatusCode = statusCode
            };
        }
    }

}
