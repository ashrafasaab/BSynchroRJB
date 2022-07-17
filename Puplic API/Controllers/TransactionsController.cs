using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using Application.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Puplic_API.DTOs;

namespace Puplic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService TransactionService;
        private readonly IAccountService AccountService;
        private readonly IMapper _mapper;

        public TransactionsController(ApplicationDbContext context, ITransactionService transactionService, IAccountService accountService, IMapper mapper)
        {
            TransactionService = transactionService;
            AccountService = accountService;
            _mapper = mapper;
        }

        // GET: api/Transactions/Account/5
        [HttpGet("Account/{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionByAccountId(int id)
        {
            var account = await AccountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            var transactions = await TransactionService.GetByAccountIdAsync(id);
            var transactionsDto = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionDto>>(transactions);
            return Ok(transactionsDto);
        }


        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionCreateDto transactionCreateDto)
        {
            var account = await AccountService.GetByIdAsync(transactionCreateDto.accountId);

            if (account == null)
            {
                return NotFound();
            }

            var transaction = _mapper.Map<Transaction>(transactionCreateDto);
            transaction.CreationDate = DateTime.UtcNow;

            var newTransaction = await TransactionService.CreateAsync(transaction);

            return Ok(newTransaction.ID);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await TransactionService.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }
            else
            {
                await TransactionService.DeleteAsync(transaction);
            }

            return Ok();
        }
    }
}
