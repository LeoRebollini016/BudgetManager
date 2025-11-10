namespace BudgetManager.Domain.Constants.Queries;

public static class AccountQueries
{
    public static string InsertAccountQuery = @"
        INSERT INTO accounts (name, id_account_types, balance, description)
        VALUES (@Name, @AccountTypeId, @Balance, @Description);
        SELECT SCOPE_IDENTITY();";

    public static string GetListAccountsQuery = @"
        SELECT * 
        FROM accounts;";

    public static string GetAccountByIdQuery = @"
        SELECT * 
        FROM accounts
        WHERE id = @id;";

    public static string UpdateAccountQuery = @"
        UPDATE accounts
        SET name = @Name, id_account_types = @AccountTypeId,
            balance = @Balance, description = @Description 
        WHERE id = @Id;";

    public static string DeleteAccountQuery = @"
        DELETE 
        FROM accounts
        WHERE id = @id;";
    public static string GetAccountNamesQuery =
        @"SELECT id, name
        FROM accounts
        WHERE id_user = @userId";
}
