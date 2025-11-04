using BudgetManager.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace BudgetManager.Helpers;

public class CustomDisplayNameProvider : IDisplayMetadataProvider
{
    public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
    {
        if (context.Key.Name == nameof(AccountTypesVM.Name))
            context.DisplayMetadata.DisplayName = () => "Nombre del tipo de cuenta";
        if (context.Key.Name == nameof(AccountVM.Name))
            context.DisplayMetadata.DisplayName = () => "Nombre de la cuenta";
    }
}
