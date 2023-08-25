using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;

namespace HPlusSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ShopContext db;

        public ProductsController(ShopContext context) {
            db = context;

            db.Database.EnsureCreated(); // Runs the seeding. Gets the database
        }


        [HttpGet] // Route: /api/products
        public async Task<ActionResult> GetAllProducts()
        {
            // returns with a status 200 and the list of all products
            return  Ok(await db.Products.ToArrayAsync());
        }

        [HttpGet("{id}")] // Route: /api/products/{id}

          public async Task<ActionResult> GetProduct(int id)
        {

            var product = await db.Products.FindAsync(id);

            if (product == null) 
            {
                // returns with a status 404
                return NotFound();
            }
            // returns with a status 200 and the product
            return Ok(product);
        }
    }
}
