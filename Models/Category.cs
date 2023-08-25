using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers.Models
{
    public class Category
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty; // Cannot be null

        public virtual List<Product> Products {get; set;} 
    }
}