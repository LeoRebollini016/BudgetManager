namespace BudgetManager.Domain.Dtos.User;

public class RegisterUserResultDto
{
    public bool Success { get; init; }
    public IReadOnlyCollection<string> Errors { get; init; } = new string[0];
}
