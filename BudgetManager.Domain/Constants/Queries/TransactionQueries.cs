namespace BudgetManager.Domain.Constants.Queries;

public static class TransactionQueries
{
    public static string SelectTransactionListQuery = @"
        SELECT t.id, t.id_user as UserId, t.transaction_date as TransactionDate, t.amount as Amount, t.id_operations_types as OperationTypeId, 
            t.note as Note, t.id_category as CategoryId, a.name as Account, ot.description as OperationType, c.name as Category
        FROM transactions t
        INNER JOIN accounts a ON t.id_account = a.id
        INNER JOIN operations_types ot ON ot.id = t.id_operations_types
        INNER JOIN categories c ON c.id = t.id_category
        WHERE t.id_user = @userId;
    ";
    public static string InsertTransactionQuery = @"
        INSERT INTO transactions
            (id_user, transaction_date, amount, id_operations_types, note, id_account, id_category)
        VALUES
            (@UserId, @TransactionDate, @Amount, @OperationTypeId, @Note, @AccountId, @CategoryId);
        SELECT CAST(SCOPE_IDENTITY() as int);
    ";
    public static string UpdateTransactionQuery = @"
        UPDATE transactions
        SET transaction_date = @TransactionDate, amount = @Amount, id_operations_types = @OperationTypeId,
            note = @Note, id_account = @AccountId, id_category = @CategoryId
        WHERE id = @Id AND id_user = @UserId;";
    public static string GetTransactionByIdQuery = @"
        SELECT id, transaction_date as TransactionDate, amount, note
        FROM transactions
        WHERE id = @Id AND id_user = @userId;
    ";
    public static string DeleteTransactionByIdQuery = @"
        DELETE
        FROM transactions
        WHERE id = @Id AND id_user = @userId;
    ";
}