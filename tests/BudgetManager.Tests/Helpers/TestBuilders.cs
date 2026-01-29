using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Tests.Helpers;

public static class TestBuilders
{
    public static TransactionCreateDto CreateTransactionCreateDto(decimal amount = 100, int operationType = 1)
    {
        return new TransactionCreateDto
        {
            Id = 1,
            TransactionDate = DateTime.UtcNow,
            Amount = amount,
            Note = "Test Transaction",
            AccountId = 1,
            OperationTypeId = operationType,
            CategoryId = 1
        };
    }
    public static Account CreateAccount(decimal balance = 1000, int id = 1)
    {
        return new Account
        {
            Id = id,
            UserId = Guid.NewGuid(),
            Name = "Test Account",
            Balance = balance,
        };
    }
    public static TransactionDto CreateTransactionDto(decimal amount = 100, int operationType = 1)
    {
        return new TransactionDto
        {
            Id = 1,
            TransactionDate = DateTime.UtcNow,
            Amount = amount,
            Note = "Test Transaction",
            AccountId = 1,
            OperationTypeId = operationType,
            CategoryId = 1
        };
    }
}
