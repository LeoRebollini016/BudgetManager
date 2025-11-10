using BudgetManager.Domain.Constants.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetManager.Models;

public class TransactionCreateVM
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime TransactionDate { get; set; }
    public double Amount { get; set; }
    public string? Note { get; set; }
    public int AccountId { get; set; }
    public IEnumerable<SelectListItem> Account { get; set; }
    public int OperationTypeId { get; set; }
    public OperationTypeEnum OperationTypes { get; set; } = OperationTypeEnum.Ingreso;
    public int CategoryId { get; set; }
    public IEnumerable<SelectListItem> Category { get; set; }
}
