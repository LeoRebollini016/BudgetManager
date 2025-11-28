namespace BudgetManager.Domain.Dtos.AccountTypes;

public class AccountTypesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int Order { get; set; }
}
