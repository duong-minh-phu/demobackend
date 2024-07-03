using ClassLibrary1.Interface;
using ClassLibrary1.Models;
using ClassLibrary1.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IUnitOfWork _repository = new UnitOfWork();
        // GET: api/<CategoryController>
        

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var products = _repository.CategoryRepository.GetByID(id);
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

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult PostCategory(Category product)
        {
            _repository.CategoryRepository.Insert(product);
            _repository.Save();
            return Ok("Create category success");
        }

        // PUT api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var pmt = _repository.CategoryRepository.GetByID(id);
            if (pmt == null) { return NotFound(); };
            _repository.CategoryRepository.Delete(pmt);
            _repository.Save();
            return Ok("Delete category success");
        }

        // DELETE api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category product)
        {
            if (_repository.CategoryRepository.GetByID(id) != null)
            {
                var _product = _repository.CategoryRepository.GetByID(id);
                if (product.Name != null)
                {
                    _product.Name = product.Name;
                }


                _repository.CategoryRepository.Update(_product);
                _repository.Save();
                return Ok("Update category success");
            }
            return NotFound();

        }
    }
}
