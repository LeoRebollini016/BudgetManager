using System.Data;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
