using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ShopContext db;

        public ProductsController(ShopContext context) {
            db = context;

            db.Database.EnsureCreated(); // Gets the database. db is now representing the database.
        }


        /* GET all products */
        /* Route: /api/products */

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            // returns with a status 200 and the list of all products
            return  Ok(await db.Products.ToArrayAsync());
        }



        /* GET product by id */
        /* Route: /api/products/{id} */

        [HttpGet("{id}")]
          public async Task<ActionResult> GetProduct(int id)
        {

            var product = await db.Products.FindAsync(id);

            if (product == null) 
            {
                // Returns with a status 404
                return NotFound();
            }
            // Returns with a status 200 and the product
            return Ok(product);
        }




        /* POST Product */
        /* Route: /api/products */

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Checks if the body pass the validation of the model
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct",
                new {id = product.Id}, // Will automatically create a new Id.
                product
            );
        }



        /* PUT Product */
        /* Route: /api/products/{id} */

        [HttpPut]
        public async Task<ActionResult> PutProduct(int Id, Product product)
        {
            // Checks if the product har the same Id as the Id in the URI
            if(Id != product.Id) 
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                // Checks if the product still exists
                if(!db.Products.Any(p => p.Id == Id))
                {
                    return NotFound();
                }
                else 
                {
                    throw;
                }
            }
            // If the update was succesfull. // comment:  Why not Ok? 
            return NoContent();
        } 




        /* DELETE Product */
        /* Route: /api/products/{id} */

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            // Get product by id
            var product = await db.Products.FindAsync(id);

            // Checks if product exists
            if(product == null) 
            {
                return NotFound();
            }

            // Removes product from database
            db.Products.Remove(product);

            // Saves changes in database
            await db.SaveChangesAsync();
            // Comment: Why not Ok(product)?
            return product;
        }        




        /* DELETE several Products */
        /* Route: /api/products/delete?ids={id}&ids={id} etc. */

        [HttpPost("Delete")]
        public async Task<ActionResult> DeleteMultiple([FromQuery]int[] ids)
        {

            var products = new List<Product>();

            foreach(var id in ids)
            {
                  // Get product by id
                var product = await db.Products.FindAsync(id);

                // Checks if product exists
                if(product == null) 
                {
                    return NotFound();
                }

                products.Add(product);

            }

            // Removes products from database
            db.Products.RemoveRange(products);

            // Saves changes in database
            await db.SaveChangesAsync();

            return Ok(products);
        }   
    }
}
