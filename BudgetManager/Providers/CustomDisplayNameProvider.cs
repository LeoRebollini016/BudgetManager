using BudgetManager.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace BudgetManager.Helpers;

public class CustomDisplayNameProvider : IDisplayMetadataProvider
{
    public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
    {
        if (context.Key.ContainerType == null || context.Key.Name == null)
            return;
        var key = $"{context.Key.ContainerType.Name}.{context.Key.Name}";

        if(Providers.DisplayNameMappings.Names.TryGetValue(key, out var displayName))
            context.DisplayMetadata.DisplayName = () => displayName;

    }
}

        //if (context.Key.Name == nameof(AccountTypesVM.Name))
        //    context.DisplayMetadata.DisplayName = () => "Nombre del tipo de cuenta";
        //if (context.Key.Name == nameof(AccountVM.Name))
        //    context.DisplayMetadata.DisplayName = () => "Nombre de la cuenta";