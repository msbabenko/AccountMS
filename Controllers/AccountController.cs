using AccountMS.Models;
using AccountMS.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountController(AccountService accountService)
        {
            _service = accountService;

        }
        // GET: api/<AccountController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<AccountController>/5
        [HttpGet("{accountId}")]
        public async Task<AccountDTO> Get(int accountId)
        {
            return _service.GetAccount(accountId);
        }
        //[Route("GetAccounts")]
        [HttpGet("GetAccounts/{customerid}")]
        public async Task<IList<Account>> Getb(int customerid)
        {
            return _service.GetCustomerAccounts(customerid);
        }

        //[HttpGet("getAccountStatement")]
        //public async Task<IList<AccountStatement>> AccountStatement(StatementDTO statementDTO)
        //{
        //    return _service.GetAccountStatement(statementDTO);
        //}

        [Route("getAccountStatement")]
        [HttpPost]
        public async Task<IList<AccountStatement>> AccountStatement(StatementDTO statementDTO)
        {
            return _service.GetAccountStatement(statementDTO);
        }

        // POST api/<AccountController>
        [Route("CreateAccount")]
        [HttpPost]
        public async Task<string> Post([FromBody] AccountDTO customerId)
        {
            return  _service.CreateAccount(customerId);

        }

        //// PUT api/<AccountController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AccountController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
