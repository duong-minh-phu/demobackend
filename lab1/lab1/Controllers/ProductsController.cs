using ClassLibrary1.Interface;
using ClassLibrary1.Models;
using ClassLibrary1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IUnitOfWork _repository = new UnitOfWork();

        [HttpGet]
        public IActionResult GetProduct(String? orderBy, int pageIndex = 1, int pageSize = 4, double? minPrice = null, double? maxPrice = null, int? categoryId = null)
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderByExpression;
            switch (orderBy)
            {
                case "priceasc":
                    orderByExpression = q => q.OrderBy(p => p.UnitPrice);
                    break;

                case "pricedesc":
                    orderByExpression = q => q.OrderByDescending(p => p.UnitPrice);
                    break;

                case "nameasc":
                    orderByExpression = q => q.OrderBy(p => p.Name);
                    break;

                case "namedesc":
                    orderByExpression = q => q.OrderByDescending(p => p.Name);
                    break;

                default:
                    // Assign a default value to orderByExpression
                    orderByExpression = q => q.OrderBy(p => p.Id);
                    break;
            }

            var products = _repository.ProductRepository.Get(filter: p => (minPrice == null || p.UnitPrice >= minPrice)
                                                                    && (p.UnitPrice <= maxPrice || maxPrice == null)
                                                                    && (categoryId == null || p.CategoryId == categoryId),
                                                            orderBy: orderByExpression,
                                                            includeProperties: "Category",
                                                            pageIndex: pageIndex, pageSize: pageSize);
            if (products.Count() > 0)
            {
                return Ok(products);
            }
            return Ok("Product null");
        }

        [HttpGet("{id}")]
        public IActionResult GetEquipmentById(int id)
        {
            var products = _repository.ProductRepository.GetByID(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (products != null)
            {
                return Ok(products);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            _repository.ProductRepository.Insert(product);
            _repository.Save();
            return Ok("Create product success"); 
        }
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (_repository.ProductRepository.GetByID(id) != null)
            {
                var _product = _repository.ProductRepository.GetByID(id);
                if (product.Name != null)
                {
                    _product.Name = product.Name;
                }
                if (product.UnitPrice != null)
                {
                    _product.UnitPrice = product.UnitPrice;
                }
                if (product.UnitInStock != null)
                {
                    _product.UnitInStock = product.UnitInStock;
                }

                _repository.ProductRepository.Update(_product);
                _repository.Save();
                return Ok("Update product success");
            }
            return NotFound();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var pmt = _repository.ProductRepository.GetByID(id);
            if (pmt == null) { return NotFound(); };
            _repository.ProductRepository.Delete(pmt);
            _repository.Save();
            return Ok("Delete product success");
        }

    }
}
