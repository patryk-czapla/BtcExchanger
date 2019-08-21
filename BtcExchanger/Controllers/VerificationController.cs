using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BtcExchanger.Models;
using System.Dynamic;
namespace BtcExchanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly BTCContext _context;

        public VerificationController(BTCContext context)
        {
            _context = context;
        }
                        
        // POST: api/verification
        [HttpPut]
        public async Task<IActionResult> Put (VerificationItem item) {
            
            var verificationItem = await _context.VerificationItems.SingleOrDefaultAsync(b => b.order_Id == item.order_Id);
            if (verificationItem == null)
            {
                dynamic errors = new ExpandoObject();
                dynamic order_id = new ExpandoObject();
                order_id.database = new string[] {};
                errors.errors = order_id;
                return NotFound(GenerateAnErrorMessag("database","There isn't record with specified id in database."));
            }
            if(verificationItem.verification_code != item.verification_code){
                return BadRequest(GenerateAnErrorMessag("verification","Your verification code is invalid."));
            }
            else{
                 if(verificationItem.verified){                    
                    return BadRequest(GenerateAnErrorMessag("verification","Already verified."));
                }
                verificationItem.verified = true;
                var orderItem = await _context.OrderItems.SingleOrDefaultAsync(b => b.Id == item.order_Id);
                //mock
                orderItem.wallet = "btc_wallet_path";
                await _context.SaveChangesAsync();      

                return Ok(orderItem);                
            }
        }
        public dynamic GenerateAnErrorMessag(string errorClass, string errorMessage){
            
            dynamic error_message = new ExpandoObject();
            var dictionary_second = (IDictionary<string, object>)error_message;
            dictionary_second.Add(errorClass, errorMessage);

            dynamic errors = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)errors;
            dictionary.Add("errors",error_message);

            return errors;
        }

    }
}