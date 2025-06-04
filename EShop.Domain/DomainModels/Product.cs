

using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DomainModels
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? ProductDescription { get; set; }
        [Required]
        public int ProductPrice { get; set; }
        [Required]
        public int ProductRating { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
    }
}
