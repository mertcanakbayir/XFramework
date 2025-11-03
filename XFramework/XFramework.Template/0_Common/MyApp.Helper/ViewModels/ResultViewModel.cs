namespace MyApp.Helper.ViewModels
{
    public class ResultViewModel<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }

        public int StatusCode { get; set; }


        public static ResultViewModel<T> Success(string message = "", int statusCode = 200)
        {
            return new ResultViewModel<T>
            {
                IsSuccess = true,
                Message = message,
                Errors = null,
                StatusCode = statusCode
            };
        }

        public static ResultViewModel<T> Success(T data,
            string message = "", int statusCode = 200)
        {
            return new ResultViewModel<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                Errors = null,
                StatusCode = statusCode
            };
        }
        public static ResultViewModel<T> Failure(string message,
            List<string> errors = null, int statusCode = 400)
        {
            return new ResultViewModel<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>(),
                StatusCode = statusCode
            };
        }
    }
}
