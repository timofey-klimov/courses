namespace Wep.App.Dto.Responses
{
    public class ApiResponse
    {
        public int Code { get; }

        public bool IsSuccess { get; }

        public string ErrorMessage { get; }

        public ApiResponse(int code, bool isSuccess, string errorMessage)
        {
            Code = code;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage ?? string.Empty;
        }

        public static ApiResponse Ok() => new ApiResponse(0, true, string.Empty);

        public static ApiResponse Fail(int code, string message) => new ApiResponse(code, false, message);
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; }

        public ApiResponse(int code, bool isSuccess, string errorMessage, T data) 
            : base(code, isSuccess, errorMessage)
        {
            Data = data;
        }

        public static ApiResponse<T> Ok(T data) => new ApiResponse<T>(0, true, string.Empty, data);

        public static ApiResponse<T> Fail(int code, string message = "") => new ApiResponse<T>(code, false, message, default(T));

    }
}
