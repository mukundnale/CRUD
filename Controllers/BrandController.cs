using CrudOperationsinNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationsinNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;

        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_dbContext.Brandss == null)
            {
                return NotFound();
            }
            return await _dbContext.Brandss.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            if (_dbContext.Brandss == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brandss.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }

        [HttpPost]  
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _dbContext.Brandss.Add(brand);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new {id=brand.ID}, brand);
        }

        [HttpPut]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if(id != brand.ID)
            {
                return BadRequest();
            }
            _dbContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch(DbUpdateConcurrencyException)
            {
                if(!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        
        }

        private bool BrandAvailable(int id)
        {
            return (_dbContext.Brandss?.Any(x=>x.ID== id)).GetValueOrDefault();  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if(_dbContext.Brandss == null)
            {
                return NotFound();
            }

            var brand = await _dbContext.Brandss.FindAsync(id);
            if(brand == null)
            {
                return NotFound();
            }
        
            _dbContext.Brandss.Remove(brand);

            await _dbContext.SaveChangesAsync();
            

            return Ok();
        }
    }
}
