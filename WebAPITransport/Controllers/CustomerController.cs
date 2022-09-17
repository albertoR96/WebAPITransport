using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPITransport.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDB db;

        public CustomerController(ApplicationDB db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<List<model.Customer>> Get()
        {
            return await db.Customers.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(model.Customer customer)
        {
            db.Add(customer);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(model.Customer customer, int id)
        {
            if (customer.ID != id)
            {
                return BadRequest("Customer id does not match with the id in the url");
            }
            db.Update(customer);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await db.Customers.AnyAsync(x => x.ID == id);
            if (!exists)
            {
                return NotFound();
            }
            db.Remove(new model.Customer() { ID = id });
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
