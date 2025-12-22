using AutoMapper;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class TransactionService(ITransactionRepository transactionRepository, IUserService userService, IMapper mapper): ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<TransactionDetailDto>> GetTransactionListAsync(CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        return (List<TransactionDetailDto>)await _transactionRepository.GetTransactionsAsync(userId, ct);
    }
    public async Task InsertTransactionAsync(TransactionCreateDto transactionCreateDto, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        transactionCreateDto.UserId = userId;
        await _transactionRepository.InsertTransactionAsync(transactionCreateDto, ct);
    }
    public async Task UpdateTransactionAsync(TransactionCreateDto transactionCreateDto, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        transactionCreateDto.UserId = userId;
        await _transactionRepository.UpdateTransactionAsync(transactionCreateDto, ct);
    }
    public async Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(int transactionId, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        return await _transactionRepository.GetTransactionDeleteInfoByIdAsync(transactionId, userId, ct);
    }
    public async Task DeleteTransactionByIdAsync(int transactionId, CancellationToken ct)
    {
        var userId = _userService.GetUserId();
        await _transactionRepository.DeleteTransactionByIdAsync(transactionId, userId, ct);
    }
}