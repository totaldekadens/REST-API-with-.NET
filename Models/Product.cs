using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MyFirstAPI.Controllers.Models;

namespace MyFirstAPI.Models
{
    public class Product
    {
        public int Id {get; set;}
        public string Sku { get; set; } = string.Empty;
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public decimal Price {get; set;}
        public bool IsAvailable {get; set;}


        public int CategoryId {get; set;}
        [JsonIgnore] // Will not Serialize (So it will not be an infinite loop)

        public virtual Category Category {get; set;}
    }
}