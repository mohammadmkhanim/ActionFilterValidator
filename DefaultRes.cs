namespace ActionFilterValidator;

public enum ResultMessages
{
    Ok,
    Created,
    Updated,
    Deleted,
    ValidationError
}

public class DefaultRes
{
    public bool IsSuccess { get; set; }
    public required string Message { get; set; }
    public List<string>? Errors { get; set; }

    public static DefaultRes Success(ResultMessages message)
    {
        return new DefaultRes
        {
            IsSuccess = true,
            Message = message.ToString()
        };
    }

    public static DefaultRes Success(string message)
    {
        return new DefaultRes
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static DefaultRes Fail(string message, List<string>? errors = null)
    {
        return new DefaultRes
        {
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
    }
}

public class DefaultRes<TData> : DefaultRes
{
    public required TData Data { get; set; }

    public static DefaultRes<TData> Success(ResultMessages message, TData data)
    {
        return new DefaultRes<TData>
        {
            IsSuccess = true,
            Message = message.ToString(),
            Data = data
        };
    }

    public static DefaultRes<TData> Success(string message, TData data)
    {
        return new DefaultRes<TData>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }
}