namespace BudgetManager.Domain.Constants.Queries;

public static class AccountQueries
{
    public static string InsertAccountQuery = @"
        INSERT INTO accounts (name, id_account_types, balance, description)
        VALUES (@Name, @AccountTypeId, @Balance, @Description);
        SELECT SCOPE_IDENTITY();
    ";

    public static string GetListAccountsQuery = @"
        SELECT * 
        FROM accounts
        WHERE is_closed = 0;
    ";

    public static string GetAccountByIdQuery = @"
        SELECT * 
        FROM accounts
        WHERE id = @id;
    ";

    public static string UpdateAccountQuery = @"
        UPDATE accounts
        SET name = @Name, id_account_types = @AccountTypeId,
            balance = @Balance, description = @Description 
        WHERE id = @Id;
    ";

    public static string ClosedAccountQuery = @"
        UPDATE accounts
        SET is_closed = 1
        WHERE id = @id;
    ";

    public static string GetAccountNamesQuery = @"
        SELECT id, name
        FROM accounts
        WHERE id_user = @userId AND is_closed = 0;
    ";
}
