namespace BudgetManager.Constants.Queries;

public static class AccountTypesQueries
{
    public static string CreateAccTypesQuery = @"
        INSERT INTO account_types (name, id_user, [order]) 
        SELECT @Name, @UserId, ISNULL(MAX([order]), 0) + 1
        FROM account_types
        WHERE id_user = @UserId;
        SELECT SCOPE_IDENTITY();
    ";

    public static string ExistAccTypesByUserIDQuery = @"
        SELECT 1 FROM account_types 
        WHERE name = @name AND id_user = @userId;
    ";

    public static string GetListAccTypesQuery =
        @"SELECT id, name, [order]
        FROM account_types
        WHERE id_user = @userId
        ORDER BY [order];
    ";
    public static string UpdateAccTypesQuery =
        @"UPDATE account_types
        SET Name = @name
        WHERE id = @Id
            AND id_user = @UserId;
    ";
    public static string GetAccTypesByIdQuery = @"
        SELECT id, name, [order]
        FROM account_types
        WHERE id = @id AND id_user = @userId;
    ";
    public static string DeleteAccTypesByIdQuery = @"
        DELETE
        FROM account_types
        WHERE id = @id
            AND id_user = @UserId;
    ";
    public static string SortAccTypesQuery = @"
        UPDATE account_types 
        SET [order] = @Order 
        WHERE Id = @Id
            AND id_user = @UserId;
    ";
    public static string GetAccountTypesNamesQuery = @"
        SELECT id, name
        FROM account_types
        WHERE id_user = @userId;
    ";
    public static string HasRelatedAccountsQuery = @"
        SELECT TOP 1 1
        FROM accounts
        WHERE id_account_types = @id
            AND id_user = @userId;
    ";
}
