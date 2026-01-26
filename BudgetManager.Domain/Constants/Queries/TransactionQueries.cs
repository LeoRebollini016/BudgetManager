namespace BudgetManager.Domain.Constants.Queries;

public static class TransactionQueries
{
    public static string SelectTransactionListQuery = @"
        SELECT 
            t.id,
            t.transaction_date as TransactionDate,
            t.amount as Amount,
            t.id_operations_types as OperationTypeId,
            t.note as Note,
            t.id_category as CategoryId,
            a.name as Account,
            ot.description as OperationType,
            c.name as Category
        FROM transactions t
        JOIN accounts a 
            ON t.id_account = a.id
            AND a.id_user = @UserId
        JOIN categories c 
            ON c.id = t.id_category
            AND c.id_user = @UserId
        JOIN operations_types ot 
            ON ot.id = t.id_operations_types
        WHERE 
            t.id_user = @userId;
    ";
    public static string InsertTransactionQuery = @"
        WITH ValidTransaction AS (
            SELECT
                a.id AS AccountId,
                c.id AS CategoryId
            FROM accounts a
            JOIN categories c
                ON c.id = @CategoryId
            WHERE
                a.id = @AccountId 
                AND a.id_user = @UserId 
                AND a.is_closed = 0 
                AND c.id_user = @UserId
        )
        INSERT INTO transactions
            (id_user, transaction_date, amount, id_operations_types, note, id_account, id_category)
        SELECT
            @UserId, 
            @TransactionDate,
            @Amount,
            @OperationTypeId,
            @Note,
            @AccountId,
            @CategoryId
        FROM ValidTransaction;

        DECLARE @NewId int = SCOPE_IDENTITY();
        
        IF @NewId IS NOT NULL
        BEGIN
            UPDATE accounts
            SET balance = balance + (CASE WHEN @OperationTypeId = 1 THEN @Amount ELSE -@Amount END)
            WHERE id = @AccountId
                AND id_user = @UserId;
        END
        
        SELECT @NewId;
    ";
    public static string UpdateTransactionQuery = @"
        WITH ValidTransaction AS (
            SELECT
                t.id
            FROM transactions t
            JOIN accounts a
                ON a.id = @AccountId
                AND a.id_user = @UserId
            JOIN categories c
                ON c.id = @CategoryId
                AND c.id_user = @UserId
            WHERE
                t.id = @Id
                AND a.is_closed = 0 
                AND t.id_user = @UserId
        )
        UPDATE transactions
        SET 
            transaction_date = @TransactionDate,
            amount = @Amount,
            id_operations_types = @OperationTypeId,
            
            note = @Note,
            id_account = @AccountId,
            id_category = @CategoryId
        WHERE id
            IN (SELECT id FROM ValidTransaction);
    ";
    public static string GetTransactionByIdQuery = @"
        SELECT
            t.id,
            t.transaction_date      AS TransactionDate,
            t.amount                AS Amount,
            t.note                  AS Note,
            t.id_account            AS AccountId,
            t.id_category           AS CategoryId,
            t.id_operations_types   AS OperationTypeId
        FROM transactions t
        WHERE
            t.id = @Id
            AND t.id_user = @UserId;
    ";
    public static string DeleteTransactionByIdQuery = @"
        DELETE
        FROM transactions
        WHERE 
            id = @Id 
            AND id_user = @userId;
    ";
    public const string UpdateAccountBalanceQuery = @"
        UPDATE accounts
        SET balance = balance + (CASE WHEN @OperationTypeId = 1 THEN @Amount ELSE -@Amount END) * @Multiplier
        WHERE id = @AccountId
            AND id_user = @UserId;
    ";
}