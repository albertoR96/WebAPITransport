using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPITransport.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly ApplicationDB db;

        public DriverController(ApplicationDB db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<List<model.Driver>> Get()
        {
            return await db.Drivers.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<model.Driver>> Get(int id)
        {
            var driver = await db.Drivers.FirstOrDefaultAsync(x => x.ID == id);
            if (driver == null)
            {
                return NotFound();
            }
            return driver;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<model.Driver>> Get(string name)
        {
            var driver = await db.Drivers.FirstOrDefaultAsync(x => x.Name.Contains(name));
            if (driver == null)
            {
                return NotFound();
            }
            return driver;
        }

        [HttpPost]
        public async Task<ActionResult> Post(model.Driver driver)
        {
            db.Add(driver);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(model.Driver driver, int id)
        {
            if (driver.ID != id)
            {
                return BadRequest("Driver id does not match with the id in the url");
            }
            db.Update(driver);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await db.Drivers.AnyAsync(x => x.ID == id);
            if (!exists)
            {
                return NotFound();
            }
            db.Remove(new model.Driver() { ID = id });
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
