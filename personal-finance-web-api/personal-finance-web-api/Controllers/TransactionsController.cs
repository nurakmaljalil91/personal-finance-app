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
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsRepository _transactionsRepository;


        public TransactionsController(TransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;

        }

        [HttpGet]
        public async Task<ActionResult<Transaction>> ReadTransactions()
        {
            var account = await _transactionsRepository.ReadTransactions();

            return Ok(account);
        }

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<Transaction>> ReadTransactionsByAccountId(string accountId)
        {
            var account = await _transactionsRepository.ReadTransactionsByAccountId(accountId);

            return Ok(account);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> CreateTransaction([FromBody] Transaction transaction)
        {
            var created = await _transactionsRepository.CreateTransaction(transaction);
            return Ok(created);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> UpdateTransaction([FromBody] Transaction transaction)
        {
            var updated = await _transactionsRepository.UpdateTransaction(transaction);

            return Ok(updated);
        }

        [HttpDelete("{transactionId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> DeleteTransaction(string transactionId)
        {
            var deleted = await _transactionsRepository.DeleteTransaction(transactionId);

            return Ok(deleted);
        }
    }
}
