using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Profit.Models;

namespace Profit.Controllers
{
    public class DataController : Controller
    {
        private ProfitContext _context;
        public DataController(ProfitContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchTitle, int page = 1, int pageSize = 10)
        {
            ViewBag.SearchTitle = searchTitle;
            ViewBag.PageSize = pageSize;
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTitle))
            {
                query = query.Where(p => p.Title.Contains(searchTitle.Trim()));
                ViewBag.ProductCount = query.Count();
            }
            else
            {
                ViewBag.ProductCount = await _context.Products.CountAsync();
            }
            var products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewBag.CurretPage = page;
            return View(products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _context.Add(product);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
