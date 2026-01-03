namespace BudgetManager.Providers;

public static class DisplayNameMappings
{
    public static readonly Dictionary<string, string> Names = new()
    {
        { "AccountCreateVM.Name", "Nombre de la cuenta" },
        { "AccountCreateVM.Description", "Descripción" },
        { "AccountCreateVM.AccountTypes", "Tipo de cuenta" },
        { "AccountTypesVM.Name", "Nombre del tipo de cuenta" },
        { "CategoryVM.Name", "Nombre de la categoría" },
        { "CategoryVM.OperationTypeId", "Tipo de operación" },
        { "TransactionCreateVM.Account", "Cuenta" },
        { "TransactionCreateVM.OperationTypes", "Tipo de operación" },
        { "TransactionCreateVM.Category", "Categoría" },
        { "TransactionCreateVM.Amount", "Monto" },
        { "TransactionCreateVM.DateTransaction", "Fecha de transacción" },
        { "TransactionCreateVM.Note", "Nota" },
        { "ReportViewModel.Month", "Mes" },
        { "ReportViewModel.AccountId", "Cuenta" },
        { "ReportViewModel.StartDate", "Fecha inicio" },
        { "ReportViewModel.EndDate", "Fecha final" },
        { "UserLoginVM.Email", "Correo electrónico" },
        { "UserLoginVM.Password", "Contraseña" },
        { "UserRegisterVM.Email", "Correo electrónico" },
        { "UserRegisterVM.Password", "Contraseña" },
        { "UserRegisterVM.ConfirmPassword", "Confirmar contraseña" }
    };
}
