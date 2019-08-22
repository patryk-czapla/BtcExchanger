using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BtcExchanger.Models;
using System;

namespace BtcExchanger.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BTCContext _context;
        private static Random random = new Random();

        public TransactionController(BTCContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get full list of transactions.
        /// </summary> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionItems()
        {
            return await _context.TransactionItems.ToListAsync();
        }

        // GET: api/transaction/5
        [HttpGet("{id}", Name = "GetTransaction")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
            var transactionItem = await _context.TransactionItems.FindAsync(id);

            if (transactionItem == null)
            {
                return NotFound();
            }

            return transactionItem;
        }
        
        /// <summary>
        /// Create a new transaction.
        /// </summary>
        /// <remarks>
        /// Sample requests:
        ///
        ///     POST /transaction
        ///     {
        ///         btc_quantity = 0.00001
        ///         account_number = "0000000000000000"
        ///         email = "batman@gotham.ct"         
        ///     }
        ///
        ///     POST /transaction
        ///     {
        ///         btc_quantity = 0.00001
        ///         account_number = "0000000000000000"
        ///         phone_number = 777777777         
        ///     }
        ///        
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>Created a new item</returns>
        /// <response code="201">Returns the created item</response>
        /// <response code="400">If some datum is incorect</response>
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransactionItem(Transaction item)
        {
            if ((item.email == null && item.phone_number == null)||(item.email != null && item.phone_number != null))
            {
                
                return BadRequest(ErrorHelper.GenerateAnErrorMessag("contact","One contact method should be specified."));
            }
            _context.TransactionItems.Add(item);
            await _context.SaveChangesAsync(); 
            //mock for tests
            var vitem = new Verification {  TransactionId = item.Id, verification_code = "1234", verified = false };

            //for deployment
            //var vitem = new VerificationItem{ Id = item.Id, verification_code = RandomString(10), verified = false };

            _context.VerificationItems.Add(vitem);
            await _context.SaveChangesAsync(); 

            //for deployment
            //generate an email or send an SMS

            return CreatedAtAction(nameof(GetTransaction), new { id = item.Id }, item);
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        

    }
}