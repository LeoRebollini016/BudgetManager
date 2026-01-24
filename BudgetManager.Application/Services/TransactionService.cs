using AutoMapper;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class TransactionService(ITransactionRepository transactionRepository): ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task<List<TransactionDetailDto>> GetTransactionListAsync(Guid userId, CancellationToken ct)
        => (List<TransactionDetailDto>)await _transactionRepository.GetTransactionsAsync(userId, ct);
    public async Task InsertTransactionAsync(Guid userId, TransactionCreateDto transactionCreateDto, CancellationToken ct)
        => await _transactionRepository.InsertTransactionAsync(userId, transactionCreateDto, ct);
    public async Task UpdateTransactionAsync(Guid userId, TransactionCreateDto transactionCreateDto, CancellationToken ct)
        => await _transactionRepository.UpdateTransactionAsync(userId, transactionCreateDto, ct);
    public async Task<TransactionDto> GetTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct)
        => await _transactionRepository.GetTransactionById(userId, transactionId, ct);
    public async Task DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct)
        => await _transactionRepository.DeleteTransactionByIdAsync(userId, transactionId, ct);
}