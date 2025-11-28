namespace BudgetManager.Domain.Constants.Queries;

public static class ReportQueries
{
    public static string GetReportByDateQuery = @"
        SELECT
			t.transaction_date as Date,
			SUM(CASE WHEN t.id_operations_types = 1 THEN t.amount ELSE 0 END) AS Income,
			SUM(CASE WHEN t.id_operations_types = 2 THEN t.amount ELSE 0 END) AS Expense
		FROM transactions t
		WHERE
			t.transaction_date >= @StartDate AND t.transaction_date < @NextMonth
			AND (@AccountId IS NULL OR t.id_account = @AccountId)
		GROUP BY t.transaction_date
		ORDER BY t.transaction_date;
    ";
	public static string GetReportByRangeDateQuery = @"
		SELECT
			t.transaction_date as Date,
			SUM(CASE WHEN t.id_operations_types = 1 THEN t.amount ELSE 0 END) AS Income,
			SUM(CASE WHEN t.id_operations_types = 2 THEN t.amount ELSE 0 END) AS Expense
		FROM transactions t
		WHERE
			(@StartDate IS NULL OR t.transaction_date >= @StartDate) 
			AND (@EndDate IS NULL OR t.transaction_date <= @EndDate)
			AND (@AccountId IS NULL OR t.id_account = @AccountId)
		GROUP BY t.transaction_date
		ORDER BY t.transaction_date;
	";
	public static string GetReportByCategoryQuery = @"
		SELECT
			c.name AS CategoryName,
			SUM(t.amount) AS Total,
			t.id_operations_types AS OperationType
		FROM transactions t
		JOIN categories c ON c.id = t.id_category
		WHERE
			(@AccountId IS NULL OR t.id_account = @AccountId)
		GROUP BY c.name, t.id_operations_types;
	";
}
