namespace AiServices.Domain.Common;

/// <summary>
/// Result nhỏ gọn dùng cho các use case không muốn throw exception cho lỗi nghiệp vụ dự đoán được.
/// </summary>
public sealed class OperationResult<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }

    private OperationResult(bool isSuccess, T? value, string? errorCode, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public static OperationResult<T> Success(T value) => new(true, value, null, null);

    public static OperationResult<T> Fail(string errorCode, string errorMessage)
        => new(false, default, errorCode, errorMessage);
}
