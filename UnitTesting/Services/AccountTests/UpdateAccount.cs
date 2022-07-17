using Application.Core.Entities;
using Application.Core.Interfaces.Reposotories;
using Application.Core.Services;
using Moq;
using Xunit;

namespace UnitTesting.Services.AccountTests
{
    public class UpdateAccount
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly AccountService AccountService;

        public UpdateAccount()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            AccountService = new AccountService(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Accounts.GetByIdAsync(1, default))
                .ReturnsAsync(new Account(1)
                {
                    ID = 1,
                    CreationDate = System.DateTime.UtcNow,
                    CreditBalance = 100,
                    Description = "Test Desc"
                });
        }

        [Fact]
        public async void Update_AddingToAccountBalance_ReturnNewAccountBalance()
        {
            //Arrange
            var accountId = 1;
            decimal value = 10;

            //Act
            var newCreditBalance = await AccountService.AddToCreditBalance(accountId, value);

            //Assert
            Assert.Equal(110, newCreditBalance);
            _unitOfWork.Verify(x => x.Accounts.UpdateAsync(It.IsAny<Account>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChanges(default), Times.Once);
        }
    }
}
