namespace ToDoListAPI.Common
{
    public class Result
    {

        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        public Result() { }

        public static Result Success() => new Result { IsSuccess = true };
        public static Result Failure(string error) => new Result { IsSuccess = false, Error = error };
    }

    public class Result<T> : Result
    {
        public T? Data { get; private set; }

        public static Result<T> Success(T data) => new Result<T> { IsSuccess = true, Data = data };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
    }
}

