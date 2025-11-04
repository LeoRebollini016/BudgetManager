namespace BudgetManager.Domain.Constants.Queries;

public static class CategoryQueries
{
    public static string GetCategoriesQuery = @"
        SELECT *
        FROM categories
        WHERE id_user = @userId;";

    public static string GetCategoryByIdQuery = @"
        SELECT *
        FROM categories
        WHERE id_user = @userId AND id = @id;";

    public static string InsertCategoryQuery = @"
        INSERT INTO categories (name, id_operations_types, id_user)
        VALUES (@Name, @operationTypeId, @userId);
        SELECT SCOPE_IDENTITY();";
}
