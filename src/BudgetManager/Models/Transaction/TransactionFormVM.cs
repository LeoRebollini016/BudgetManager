using BudgetManager.Domain.Constants.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models.Transaction;

public class TransactionFormVM
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime TransactionDate { get; set; }
    [Required(ErrorMessage = "Debe ingresar un monto.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public double Amount { get; set; }
    [StringLength(200, ErrorMessage = "la nota no puede superar los 200 caracteres.")]
    public string? Note { get; set; }
    [Required(ErrorMessage = "Debe seleccionar una cuenta.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioné una cuenta válida.")]
    public int AccountId { get; set; }
    public IEnumerable<SelectListItem>? Account { get; set; }
    [Required(ErrorMessage = "Debe seleccionar un tipo de operación.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioné un tipo de operación válido.")]
    public int OperationTypeId { get; set; }
    public OperationTypeEnum OperationTypes { get; set; } = OperationTypeEnum.Ingreso;
    [Required(ErrorMessage = "Debe seleccionar una categoría.")]
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioné una categoría válida.")]
    public int CategoryId { get; set; }
    public IEnumerable<SelectListItem>? Category { get; set; }
}
