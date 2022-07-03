using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }   
        public string Description { get; set; } 
        public string ImageURL { get; set; }
        [Range(1,1000,ErrorMessage = "Price should be between $1 and $1000")]
        public double Price { get; set; }

        // NOTE: Below are two examples of defining a property that is a foreign key
        // The first includes the [ForeignKey("FoodTypeId")] data annotation 
        // The second is an example of how in .NET Core 6 this is not needed, so long
        // as the Id variable has a name that matches the type of the corresponding 
        // object variable.
        //    int CategoryId => Category Category 

        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }
        [ForeignKey("FoodTypeId")]
        public FoodType FoodType { get; set; }
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
