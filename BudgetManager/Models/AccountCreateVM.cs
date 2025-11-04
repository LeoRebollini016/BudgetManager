using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Models;

public class AccountCreateVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Balance { get; set; } = 0.0;
    public int AccountTypeId { get; set; }
    public IEnumerable<SelectListItem> accountTypes { get; set; }
    public string? Description { get; set; }
}
