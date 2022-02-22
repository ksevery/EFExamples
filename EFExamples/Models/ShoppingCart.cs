using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExamples.Models
{
    public record ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<ProductShoppingCart>? Products { get; set; }
    }
}
