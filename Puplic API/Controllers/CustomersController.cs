using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puplic_API.DTOs;

namespace Puplic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService CustomerService;
        private readonly IAccountService AccountService;
        private readonly ITransactionService TransactionService;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper, IAccountService accountService, ITransactionService transactionService)
        {
            CustomerService = customerService;
            AccountService = accountService;
            TransactionService = transactionService;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await CustomerService.GetAllAsync();
            var customersDto = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
            return Ok(customersDto);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await CustomerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                var customersDto = _mapper.Map<CustomerDto>(customer);
                return Ok(customersDto);
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<Customer>(customerDto);

            try
            {
                await CustomerService.UpdateAsync(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            var newCustomer = await CustomerService.CreateAsync(customer);

            return Ok(newCustomer.ID);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await CustomerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                await CustomerService.DeleteAsync(customer);
                return Ok();
            }
        }

        // GET: api/Customers/5/FullInformation
        [HttpGet("{id}/FullInformation")]
        public async Task<ActionResult<Customer>> GetCustomerFullInformation(int id)
        {
            var customer = await CustomerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerFullInformation = _mapper.Map<CustomerFullInformationDto>(customer);

            var customerAccount = await AccountService.GetByCustomerIdAsync(id);
            if (customerAccount != null)
            {
                customerFullInformation.AccountId = customerAccount.ID;
                customerFullInformation.Balance = customerAccount.CreditBalance;
                var transactions = await TransactionService.GetByAccountIdAsync(customerAccount.ID);
                customerFullInformation.Transactions = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionDto>>(transactions);
            }

            return Ok(customerFullInformation);
        }
    }
}
