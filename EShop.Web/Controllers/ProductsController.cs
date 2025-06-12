using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    //TODO: fix product image in create
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService=productService;
        }


        // GET: Products
        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName,ProductDescription,ProductPrice,ProductRating")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Insert(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ProductName,ProductDescription,ProductPrice,ProductRating")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            _productService.Update(product);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _productService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddProductToCard(Guid id)
        {
            AddToCartDTO model = _productService.GetSelectedShoppingCartProduct(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductToCard(AddToCartDTO model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _productService.AddProductToSoppingCart(model.SelectedProductId, Guid.Parse(userId), model.Quantity);
            return RedirectToAction(nameof(Index));
        }

    }
}
