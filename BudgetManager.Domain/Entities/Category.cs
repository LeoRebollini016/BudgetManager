namespace BudgetManager.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int operationTypeId { get; set; }
    public int userId { get; set; }
}
