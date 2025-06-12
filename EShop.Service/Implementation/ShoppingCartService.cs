using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository)
        {
            _shoppingCartRepository=shoppingCartRepository;
            _productInShoppingCartRepository=productInShoppingCartRepository;
        }

        public void DeleteProductFromShoppingCart(Guid productInShoppingCartId)
        {
            var prodictInShoppingCart = _productInShoppingCartRepository.Get(selector: x => x,
                                                                                         predicate: x => x.Id.Equals(productInShoppingCartId));

            if (prodictInShoppingCart == null)
            {
                throw new Exception("Product in shopping cart not found");
            }

            _productInShoppingCartRepository.Delete(prodictInShoppingCart);
        }

        public ShoppingCart? GetByUserId(Guid userId)
        {
            return _shoppingCartRepository.Get(selector: x => x, predicate: x => x.OwnerId.Equals(userId.ToString()));
        }

        public ShoppingCartDTO GetByUserIdWithIncludedPrducts(Guid userId)
        {
            var userCart = _shoppingCartRepository.Get(selector: x => x,
                                                           predicate: x => x.OwnerId.Equals(userId.ToString()),
                                                           include: x => x.Include(z => z.AllProducts).ThenInclude(m => m.Product));

            var allProducts = userCart.AllProducts.ToList();

            var allProductPrices = allProducts.Select(z => new
            {
                ProductPrice = z.Product.ProductPrice,
                Quantity = z.Quantity
            }).ToList();

            double totalPrice = 0.0;

            foreach (var item in allProductPrices)
            {
                totalPrice += item.Quantity * item.ProductPrice;
            }

            ShoppingCartDTO model = new ShoppingCartDTO
            {
                Products = allProducts,
                TotalPrice = totalPrice
            };

            return model;
        }

        public bool OrderProducts(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
