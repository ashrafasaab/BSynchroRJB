using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Puplic_API.DTOs;

namespace Puplic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService AccountService;
        private readonly ICustomerService CustomerService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper, ICustomerService customerService)
        {
            AccountService = accountService;
            _mapper = mapper;
            CustomerService = customerService;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await AccountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                var accountDto = _mapper.Map<AccountDto>(account);

                return Ok(accountDto);
            }
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, AccountUpdateDto accountUpdateDto)
        {
            var account = await AccountService.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            else
            {
                account.Description = accountUpdateDto.Desciption;
                await AccountService.UpdateAsync(account);
                return Ok();
            }
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(AccountCreateDto accountCreateDto)
        {
            var customer = await CustomerService.GetByIdAsync(accountCreateDto.CustomerId);
            if (customer == null) return NotFound();

            var account = _mapper.Map<Account>(accountCreateDto);
            var newAccount = await AccountService.CreateAsync(account);

            return Ok(newAccount.ID);
        }
    }
}
