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
    public class OrderController : ControllerBase
    {
        private readonly BTCContext _context;
        private static Random random = new Random();

        public OrderController(BTCContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get full list of transactions.
        /// </summary> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }

        // GET: api/order/5
        [HttpGet("{id}", Name = "GetOrderItem")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(long id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return orderItem;
        }
        
        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <remarks>
        /// Sample requests:
        ///
        ///     POST /order
        ///     {
        ///         btc_quantity = 0.00001
        ///         account_number = "0000000000000000"
        ///         email = "batman@gotham.ct"         
        ///     }
        ///
        ///     POST /order
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
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem item)
        {
            if ((item.email == null && item.phone_number == null)||(item.email != null && item.phone_number != null))
            {
                return BadRequest("{errors:{contact:[\"One contact method should be specified.\"]}}");
            }
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync(); 

            //mock for tests
            var vitem = new VerificationItem { order_Id = item.Id, verification_code = "1234", verified = false };

            //for deployment
            //var vitem = new VerificationItem{ order_Id = item.Id, verification_code = RandomString(10), verified = false };

            _context.VerificationItems.Add(vitem);
            await _context.SaveChangesAsync(); 

            //for deployment
            //generate an email or send an SMS

            return CreatedAtAction(nameof(GetOrderItem), new { id = item.Id }, item);
        }
        
        // PUT api/order/5
        [HttpPut ("{id}")]
        public async Task<IActionResult> Put (int id, OrderItem item) {
            
            if ((item.email == null && item.phone_number == null) || (item.email != null && item.phone_number != null))
            {
                return BadRequest("{errors:{contact:[\"One contact method should be specified.\"]}}");
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

             return Ok(item);
        }

        // DELETE api/cinema/5
        [HttpDelete ("{id}")]
        public async Task<ActionResult<OrderItem>> Delete (long id) {

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            _context.OrderItems.Remove (orderItem);

            await _context.SaveChangesAsync();

            return orderItem;
        }
       
        private bool OrderItemExists(long id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}