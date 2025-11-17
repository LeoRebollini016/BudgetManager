namespace BudgetManager.Domain.Constants.Queries;

public static class CategoryQueries
{
    public static string GetCategoriesQuery = @"
        SELECT c.id, c.name, c.id_operations_types as OperationTypeId, c.id_user as UserId, c.id_operations_types as OperationType
        FROM categories c
        LEFT JOIN operations_types ot ON ot.id = c.id_operations_types 
        WHERE c.id_user = @userId AND is_active = 1;";

    public static string GetCategoryByIdQuery = @"
        SELECT id, name, id_operations_types as OperationTypeId, id_user as UserId
        FROM categories
        WHERE id_user = @userId AND id = @id AND is_active = 1;";

    public static string InsertCategoryQuery = @"
        INSERT INTO categories (name, id_operations_types, id_user)
        VALUES (@Name, @OperationTypeId, @userId);
        SELECT SCOPE_IDENTITY();";
    public static string UpdateCategoryQuery = @"
        UPDATE categories
        SET name = @Name, id_operations_types = @OperationTypeId
        WHERE id = @Id;";
    public static string GetCategoryNamesQuery = @"
        SELECT id, name
        FROM categories
        WHERE id_user = @userId AND is_active = 1;
    ";
    public static string GetCategoryDeleteInfoQuery = @"
        SELECT id, name, id_operations_types as OperationType
        FROM categories
        WHERE id_user = @userId AND id = @id;
    ";
    public static string DisableCategoryByIdQuery = @"
        UPDATE categories
        SET is_active = 0        
        WHERE id_user = @userId AND id = @id;
    ";
}
