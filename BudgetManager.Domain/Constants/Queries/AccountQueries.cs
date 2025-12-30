namespace BudgetManager.Domain.Constants.Queries;

public static class AccountQueries
{
    public static string InsertAccountQuery = @"
        INSERT INTO accounts (id_user, name, id_account_types, balance, description)
        VALUES (@UserId, @Name, @AccountTypeId, @Balance, @Description);
        SELECT SCOPE_IDENTITY();
    ";

    public static string GetListAccountsQuery = @"
        SELECT * 
        FROM accounts
        WHERE id_user = @UserId
            AND is_closed = 0;
    ";

    public static string GetAccountByIdQuery = @"
        SELECT * 
        FROM accounts
        WHERE id = @Id
            AND id_user = @UserId;
    ";

    public static string UpdateAccountQuery = @"
        UPDATE accounts
        SET name = @Name, 
            id_account_types = @AccountTypeId,
            balance = @Balance, 
            description = @Description 
        WHERE id = @Id
            AND id_user = @UserId;
    ";

    public static string ClosedAccountQuery = @"
        UPDATE accounts
        SET is_closed = 1
        WHERE id = @Id
            AND id_user = @UserId;
    ";

    public static string GetAccountNamesQuery = @"
        SELECT id, name
        FROM accounts
        WHERE id_user = @UserId
            AND is_closed = 0;
    ";
}
