using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;
using BtcExchanger.Models;

namespace BtcExchanger.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly BTCContext _context;

        public VerificationController(BTCContext context)
        {
            _context = context;
        }
                        
        /// <summary>
        /// Delivers a verification of contact.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /verification
        ///     {
        ///        "TransactionId": 1,
        ///        "verification_code": "some code",
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>Updated item with wallet field</returns>
        /// <response code="201">Returns the updated item with wallet field</response>
        /// <response code="404">If there is no transaction with a specified id</response>
        /// <response code="400">If code is invalid or it was verificated earlier</response>
        [HttpPut]
        public async Task<IActionResult> Put (Verification item) {
            
            var verificationItem = await _context.VerificationItems.SingleOrDefaultAsync(b => b.TransactionId == item.TransactionId);
            if (verificationItem == null)
            {
                return NotFound(ErrorHelper.GenerateAnErrorMessag("database","There isn't record with specified id in database."));
            }
            if(verificationItem.verification_code != item.verification_code){
                return BadRequest(ErrorHelper.GenerateAnErrorMessag("verification","Your verification code is invalid."));
            }
            else{
                 if(verificationItem.verified){                    
                    return BadRequest(ErrorHelper.GenerateAnErrorMessag("verification","Already verified."));
                }
                verificationItem.verified = true;

                var transactionItem = await _context.TransactionItems.SingleOrDefaultAsync(b => b.Id == item.TransactionId);
                //mock
                transactionItem.wallet="btc_wallet_path";

                await _context.SaveChangesAsync();      

                return Ok(transactionItem);                
            }
        }        

    }
}