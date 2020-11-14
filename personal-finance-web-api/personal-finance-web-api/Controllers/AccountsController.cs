using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using personal_finance_web_api.Models;
using personal_finance_web_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace personal_finance_web_api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsRepository _accountsRepository;

        public AccountsController(AccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<AccountDTO>> ReadAccounts()
        {
            var account = await _accountsRepository.ReadAccounts();

            return Ok(account);
        }

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<AccountDTO>> ReadAccount(string accountId)
        {
            var account = await _accountsRepository.ReadAccount(accountId);

            return Ok(account);
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<AccountDTO>> ReadAccountsByUser(string userId)
        {
            var account = await _accountsRepository.ReadAccountsByUserId(userId);

            return Ok(account);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> CreateUser([FromBody] Account account)
        {
            var created = await _accountsRepository.CreateAccount(account);

            return Ok(created);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> UpdateUser([FromBody] Account account)
        {
            var updated = await _accountsRepository.UpdateAccount(account);

            return Ok(updated);
        }

        [HttpDelete("{accountId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> DeleteUser(string accountId)
        {
            var deleted = await _accountsRepository.DeleteAccount(accountId);

            return Ok(deleted);
        }
    }
}
