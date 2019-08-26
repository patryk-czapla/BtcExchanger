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
    public class transactionController : ControllerBase
    {
        private readonly BTCContext _context;
        private static Random random = new Random();

        public transactionController(BTCContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a transaction if provided data is valid.
        /// </summary>
        /// <remarks>
        /// Sample requests:
        ///
        ///     GET /transaction/1/1234
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="verification_code"></param>
        /// <returns>Returns a item</returns>
        /// <response code="201">Returns the item</response>
        /// <response code="404">If transaction doesn't exist</response>
        /// <response code="400">If verification_code is incorect</response>
        [HttpGet("{id}:{verification_code}", Name = "GetTransaction")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id, string verification_code)
        {
            var transactionItem = await _context.TransactionItems.FindAsync(id);
            if (transactionItem == null)
            {
                return NotFound();
            }
            var verificationItem = await _context.VerificationItems.SingleOrDefaultAsync(b => b.VerificationId == id && b.verification_code == verification_code );
            if (verificationItem == null)
            {
                return BadRequest();
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
                return BadRequest(ErrorHelper.GenerateAnErrorMessage("contact","One contact method should be specified."));
            }
            if ((item.wallet != null ))
            {
                return BadRequest(ErrorHelper.GenerateAnErrorMessage("wallet","You can't provide wallet field."));
            }
            if ((item.status != null ))
            {
                return BadRequest(ErrorHelper.GenerateAnErrorMessage("status","You can't provide status field."));
            }
            item.status = Status.WAITING_FOR_VERIFICATION;
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

            return CreatedAtAction(nameof(GetTransaction), new { id = item.Id, verification_code = vitem.verification_code}, item);
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }        
    }
}