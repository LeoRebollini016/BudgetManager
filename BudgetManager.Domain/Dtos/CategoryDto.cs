namespace BudgetManager.Domain.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int operationTypeId { get; set; }
    public int userId { get; set; }
}
