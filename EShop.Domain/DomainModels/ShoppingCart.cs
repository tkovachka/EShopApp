﻿
using EShop.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public virtual ICollection<ProductInShoppingCart>? AllProducts { get; set; }
    }
}
