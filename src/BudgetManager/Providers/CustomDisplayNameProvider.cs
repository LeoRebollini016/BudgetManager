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