using BudgetManager.Application.Services;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Tests.Helpers;
using FluentAssertions;
using Moq;

namespace BudgetManager.Tests.Application.Services;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _transactionRepoMock;
    private readonly Mock<IAccountRepository> _accountRepoMock;
    private readonly Mock<ICategoryRepository> _categoryRepoMock;

    private readonly TransactionService _service;

    private readonly Guid _userId = Guid.NewGuid();

    public TransactionServiceTests()
    {
        _transactionRepoMock = new Mock<ITransactionRepository>();
        _accountRepoMock = new Mock<IAccountRepository>();
        _categoryRepoMock = new Mock<ICategoryRepository>();

        _service = new TransactionService(
            _transactionRepoMock.Object,
            _accountRepoMock.Object,
            _categoryRepoMock.Object
        );

        SetupHappyPath();
    }

    #region Tests para Insertar Transacciones
    [Fact]
    public async Task InsertTransaction_Fail_WhenAccountDoesNotExist()
    {
        // Arrenge
        var dto = TestBuilders.CreateTransactionCreateDto();

        _accountRepoMock
            .Setup(x => x.GetAccountByIdAsync(_userId, dto.AccountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _service.InsertTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("La cuenta no es válida.");
        result.TargetField.Should().Be("AccountId");
    }
    [Fact]
    public async Task InsertTransaction_Fail_WhenCategoryDoesNotExist()
    {
        // Arrenge
        var dto = TestBuilders.CreateTransactionCreateDto();

        _categoryRepoMock
            .Setup(x => x.GetCategoryByIdAsync(_userId, dto.CategoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category?)null);

        // Act
        var result = await _service.InsertTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("La categoría no es válida.");
    }
    [Theory]
    [InlineData(50, 500, 2, false)]
    [InlineData(50,400, 1, true)]
    public async Task InsertTransaction_ValidateBalance_GivenVariousAmounts(decimal balance, decimal amount, int operationType, bool expectedSucceed)
    {
        // Arrenge
        var dto = TestBuilders.CreateTransactionCreateDto(amount: amount, operationType: operationType);
        var poorAccount = TestBuilders.CreateAccount(balance: balance);

        _accountRepoMock
            .Setup(x => x.GetAccountByIdAsync(_userId, dto.AccountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(poorAccount);

        // Act
        var result = await _service.InsertTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        if (expectedSucceed)
        {
            result.Success.Should().BeTrue();
            _transactionRepoMock.Verify(x => x.InsertTransactionAsync(_userId, dto, It.IsAny<CancellationToken>()), Times.Once);
        }
        else
        {
            result.Success.Should().BeFalse();
            result.Error.Should().Be("Fondos insuficientes en la cuenta.");
            result.TargetField.Should().Be("Amount");
            _transactionRepoMock.Verify(x => x.InsertTransactionAsync(_userId, dto, It.IsAny<CancellationToken>()), Times.Never);
        }
    }
    [Fact]
    public async Task InsertTransaction_Fail_WhenRepositoryFailsToInsert()
    {
        // Arrenge
        var dto = TestBuilders.CreateTransactionCreateDto();

        _transactionRepoMock
            .Setup(x => x.InsertTransactionAsync(_userId, dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync((int?)null);
        // Act
        var result = await _service.InsertTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("No se pudo insertar la transacción.");
    }
    [Fact]
    public async Task InsertTransaction_Fail_WhenExceptionOccurs()
    {
        // Arrenge
        var dto = TestBuilders.CreateTransactionCreateDto();

        _transactionRepoMock
            .Setup(x => x.InsertTransactionAsync(_userId, dto, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));
        // Act
        var result = await _service.InsertTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("Error procesando la transacción.");
    }
    #endregion

    #region Tests para Actualizar Transacciones
    [Fact]
    public async Task UpdateTransaction_Fail_WhenAccountDoesNotExist()
    {
        // Arrange
        var dto = TestBuilders.CreateTransactionCreateDto();

        _accountRepoMock
            .Setup(x => x.GetAccountByIdAsync(_userId, dto.AccountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _service.UpdateTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("La cuenta no es válida.");
    }
    [Fact]
    public async Task UpdateTransaction_Fail_WhenCategoryDoesNotExist()
    {
        // Arrange
        var dto = TestBuilders.CreateTransactionCreateDto();

        _categoryRepoMock
            .Setup(x => x.GetCategoryByIdAsync(_userId, dto.CategoryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Category?)null);

        // Act
        var result = await _service.UpdateTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("La categoría no es válida.");
    }
    [Fact]
    public async Task Update_Fail_WhenTransactionNotFound()
    {
        // Arrange
        var dto = TestBuilders.CreateTransactionCreateDto();
        _transactionRepoMock.Setup(x => x.GetTransactionById(_userId, dto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TransactionDto?)null);

        // Act
        var result = await _service.UpdateTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("La transacción no existe o no tienes permisos.");
    }

    [Theory]
    [InlineData(100, 150, 40, false)]
    [InlineData(100, 150, 60, true)]
    public async Task UpdateTransaction_ValidateBalance_GivenVariousAmounts(
        decimal oldAmount, decimal newAmount, decimal currentBalance, bool expectedSuccess)
    {
        // Arrenge
        var dto = TestBuilders.CreateTransactionCreateDto(amount: newAmount, operationType: 2);
        var oldTx = TestBuilders.CreateTransactionDto(amount: oldAmount, operationType: 2);
        var currentAccount = TestBuilders.CreateAccount(balance: currentBalance);

        _transactionRepoMock.Setup(x => x.GetTransactionById(_userId, dto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(oldTx);

        _accountRepoMock.Setup(x => x.GetAccountByIdAsync(_userId, dto.AccountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(currentAccount);

        if (expectedSuccess)
        {
            _transactionRepoMock.Setup(x => x.UpdateTransactionAsync(It.IsAny<Guid>(), dto, oldAmount, 2, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
        }
        // Act
        var result = await _service.UpdateTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().Be(expectedSuccess);
        if(expectedSuccess)
            _transactionRepoMock.Verify(x => x.UpdateTransactionAsync(
                _userId, dto, oldTx.Amount, oldTx.OperationTypeId, It.IsAny<CancellationToken>()), Times.Once);
        
        if (!expectedSuccess)
        {
            result.Error.Should().Be("Fondos insuficientes en la cuenta.");
            _transactionRepoMock.Verify(x => x.UpdateTransactionAsync(
                It.IsAny<Guid>(), It.IsAny<TransactionCreateDto>(), It.IsAny<decimal>(), It.IsAny<int>(), 
                It.IsAny<CancellationToken>()), Times.Never);
        }
    }

    [Fact]
    public async Task Update_Fail_WhenExceptionOccurs()
    {
        // Arrange
        var dto = TestBuilders.CreateTransactionCreateDto();
        var oldTx = TestBuilders.CreateTransactionDto();

        _transactionRepoMock.Setup(x => x.GetTransactionById(_userId, dto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(oldTx);

        _transactionRepoMock.Setup(x => x.UpdateTransactionAsync(It.IsAny<Guid>(), It.IsAny<TransactionCreateDto>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _service.UpdateTransactionAsync(_userId, dto, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("Error procesando la transacción.");
    }
    #endregion
    #region Tests para Eliminar Transacciones
    [Fact]
    public async Task DeleteTransaction_Fail_WhenTransactionNotFound()
    {
        // Arrange
        var transactionId = 99;
        _transactionRepoMock
            .Setup(x => x.GetTransactionById(_userId, transactionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TransactionDto?)null);

        // Act
        var result = await _service.DeleteTransactionByIdAsync(_userId, transactionId, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be("La transacción no existe o no tienes permisos.");
        _transactionRepoMock.Verify(x => x.DeleteTransactionByIdAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task DeleteTransaction_ExpectedResult_WhenRepositoryExecutes(bool deleteResult)
    {
        // Arrange
        var dto = TestBuilders.CreateTransactionDto();

        _transactionRepoMock
            .Setup(x => x.GetTransactionById(_userId, dto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);
        _transactionRepoMock
            .Setup(x => x.DeleteTransactionByIdAsync(_userId, dto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(deleteResult);

        // Act
        var result = await _service.DeleteTransactionByIdAsync(_userId, dto.Id, CancellationToken.None);

        // Assert

        result.Success.Should().Be(deleteResult);

        if (!deleteResult)
            result.Error.Should().Be("No se pudo eliminar la transacción.");

        _transactionRepoMock.Verify(x => x.DeleteTransactionByIdAsync(It.IsAny<Guid>(), It.IsAny<int>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
    #endregion
    private void SetupHappyPath()
    {
        var defaultAccount = TestBuilders.CreateAccount();
        var defaultCategory = new Category
        {
            Id = 1,
            UserId = Guid.NewGuid(),
            Name = "Test Category",
        };

        _accountRepoMock
            .Setup(x => x.GetAccountByIdAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(defaultAccount);
        _categoryRepoMock
            .Setup(x => x.GetCategoryByIdAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(defaultCategory);
        _transactionRepoMock
            .Setup(x => x.InsertTransactionAsync(It.IsAny<Guid>(), It.IsAny<TransactionCreateDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        _transactionRepoMock
            .Setup(x => x.UpdateTransactionAsync(_userId, It.IsAny<TransactionCreateDto>(), It.IsAny<decimal>(), 1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _transactionRepoMock
            .Setup(x => x.DeleteTransactionByIdAsync(_userId, It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }
}