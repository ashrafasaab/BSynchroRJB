using Application.Core.Entities;
using NuGet.Protocol;
using Puplic_API.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace IntegrationTesting.AccountEndpoints
{
    public class CreateAccount : IClassFixture<ApiTestFixture>
    {
        public HttpClient Client { get; }

        public CreateAccount(ApiTestFixture factory)
        {
            Client = factory.CreateClient();
        }
        [Fact]
        public async void PostAccount_CreateAccountWithInitialCredit_NewAccountWithNewTransaction()
        {
            // Arrange

            var newCustomerjsonContent = GetValidNewCustomerJson();

            var newCustomerResponse = await Client.PostAsync("api/Customers", newCustomerjsonContent);
            newCustomerResponse.EnsureSuccessStatusCode();
            var customerId = Convert.ToInt32(await newCustomerResponse.Content.ReadAsStringAsync());

            var newAccountjsonContent = GetValidNewAccountCreateDtoJson(customerId);

            // Act

            var newAccountResponse = await Client.PostAsync($"api/Accounts", newAccountjsonContent);
            newAccountResponse.EnsureSuccessStatusCode();
            var accountId = await newAccountResponse.Content.ReadAsStringAsync();

            var getNewAccountResponse = await Client.GetAsync($"api/Accounts/{accountId}");
            getNewAccountResponse.EnsureSuccessStatusCode();
            var newAccountResponseAsString = await getNewAccountResponse.Content.ReadAsStringAsync();
            var accountModel = newAccountResponseAsString.FromJson<AccountDto>();

            var newTransactionResponse = await Client.GetAsync($"api/Transactions/Account/{accountId}");
            newTransactionResponse.EnsureSuccessStatusCode();
            var newTransactionResponseAsString = await newTransactionResponse.Content.ReadAsStringAsync();
            var transactionModel = newTransactionResponseAsString.FromJson<List<TransactionDto>>();

            // Assert

            Assert.Equal(10, accountModel.CreditBalance);
            Assert.Equal(10, transactionModel[0].CreditValue);
        }

        private StringContent GetValidNewCustomerJson()
        {
            var request = new Customer("Foo", "Bar");
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }

        private StringContent GetValidNewAccountCreateDtoJson(int customerId)
        {
            var request = new AccountCreateDto()
            {
                Desciption = "Test",
                InitialCredit = 10,
                CustomerId = customerId
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            return jsonContent;
        }
    }
}
