using BudgetManager.Domain.Dtos.Report;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models
{
    public class ReportViewModel
    {
        public string ReportType { get; set; }
        public DateTime? Month { get; set; } = DateTime.Now;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } = DateTime.Now;
        [Range(1, int.MaxValue, ErrorMessage = "Seleccioné un tipo de cuenta válida.")]
        public int? AccountId { get; set; }

        public List<ReportCategoryDto> ByCategory { get; set; } = [];
        public List<ReportTimeSeriesDto> TimeSeries { get; set; } = [];
        public IEnumerable<SelectListItem> SelectOptions { get; set; } = [];
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }
}
