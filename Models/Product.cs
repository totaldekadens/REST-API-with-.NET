using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MyFirstAPI.Controllers.Models;

namespace MyFirstAPI.Models
{
    public class Product
    {
        public int Id {get; set;}
        [Required]
        public string Sku { get; set; } = string.Empty;
        [Required]
        public string Name {get; set;} = string.Empty;
        [Required]
        public string Description {get; set;} = string.Empty;
        public decimal Price {get; set;}
        public bool IsAvailable {get; set;}


        [Required] 
        public int CategoryId {get; set;}

        [JsonIgnore] // Will not Serialize (So it will not be an infinite loop)
        public virtual Category? Category {get; set;}
    }
}