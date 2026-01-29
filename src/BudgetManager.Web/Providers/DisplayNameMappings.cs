namespace BudgetManager.Web.Providers;

public static class DisplayNameMappings
{
    public static readonly Dictionary<string, string> Names = new()
    {
        { "AccountFormVM.Name", "Nombre de la cuenta" },
        { "AccountFormVM.Description", "Descripción" },
        { "AccountFormVM.AccountTypes", "Tipo de cuenta" },
        { "AccountTypesFormVM.Name", "Nombre del tipo de cuenta" },
        { "CategoryFormVM.Name", "Nombre de la categoría" },
        { "CategoryFormVM.OperationTypeId", "Tipo de operación" },
        { "TransactionFormVM.Account", "Cuenta" },
        { "TransactionFormVM.OperationTypes", "Tipo de operación" },
        { "TransactionFormVM.Category", "Categoría" },
        { "TransactionFormVM.Amount", "Monto" },
        { "TransactionFormVM.TransactionDate", "Fecha de transacción" },
        { "TransactionFormVM.Note", "Nota" },
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
