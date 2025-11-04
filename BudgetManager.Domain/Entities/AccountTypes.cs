using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Domain.Entities;

public class AccountTypes
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public int Order { get; set; }
}
