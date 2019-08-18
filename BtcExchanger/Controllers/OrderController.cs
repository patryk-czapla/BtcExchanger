using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BtcExchanger.Models;

namespace BtcExchanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BTCContext _context;

        public OrderController(BTCContext context)
        {
            _context = context;
        }
        // GET: api/order
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
        
        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = item.Id }, item);
        }

        // PUT api/order/5
        [HttpPut ("{id}")]
        public async Task<IActionResult> Put (int id, OrderItem item) {
            
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

    }
}