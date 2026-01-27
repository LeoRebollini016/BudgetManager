namespace BudgetManager.Domain.Common;

public class Result
{
    public bool Success { get; }
    public string? Error { get; }
    public string? TargetField { get; }

    protected Result(bool success, string? error = null, string? targetField = null)
    {
        Success = success;
        Error = error;
        TargetField = targetField;
    }
    public static Result Ok() => new Result(true, null);

    public static Result Fail(string error, string? targetField = null) => new Result(false, error, targetField);
}
