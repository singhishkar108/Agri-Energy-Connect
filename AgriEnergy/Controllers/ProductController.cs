using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergy.Models;

namespace AgriEnergy.Controllers
{
    public class ProductController : Controller
    {
        private readonly AgriEnergyContext _context;

        public ProductController(AgriEnergyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, DateOnly? startDate, DateOnly? endDate)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            else
            {
                IQueryable<Product> products = _context.Products
                                                    .Include(p => p.Category)
                                                    .Include(p => p.Farmer);

                if (!String.IsNullOrEmpty(searchString))
                {
                    products = products.Where(p => p.ProductName.Contains(searchString) || p.Category.CategoryName.Contains(searchString) || p.Farmer.Name.Contains(searchString));
                }

                if (startDate.HasValue && endDate.HasValue)
                {
                    products = products.Where(p => p.ProductionDate >= startDate.Value && p.ProductionDate <= endDate.Value);
                }

                ViewData["CurrentFilter"] = searchString;
                ViewData["CurrentStartDate"] = startDate?.ToString("MM-dd-yyyy");
                ViewData["CurrentEndDate"] = endDate?.ToString("MM-dd-yyyy");

                return View(await products.ToListAsync());
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Farmer)
                    .FirstOrDefaultAsync(m => m.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        public async Task<IActionResult> FarmerDetails(int? id, string searchString, DateOnly? startDate, DateOnly? endDate, string categoryName)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null)
            {
                return RedirectToAction("Index", "Unauthorized");
            }

            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Users
                .Include(u => u.Products)
                    .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (farmer == null)
            {
                return NotFound();
            }

            // Apply filtering on the farmer's products
            var filteredProducts = farmer.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    p.Category.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.ProductionDate >= startDate.Value && p.ProductionDate <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(categoryName))
            {
                filteredProducts = filteredProducts.Where(p =>
                    p.Category.CategoryName == categoryName);
            }

            // Replace the original list with the filtered list
            farmer.Products = filteredProducts.ToList();

            // Populate categories for the dropdown
            var categories = await _context.Categories
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
            ViewData["Categories"] = categories;

            // Set filter values to maintain state in the view
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStartDate"] = startDate?.ToString("yyyy-MM-dd"); // Adjusted format for HTML date input
            ViewData["CurrentEndDate"] = endDate?.ToString("yyyy-MM-dd"); // Adjusted format for HTML date input
            ViewData["CurrentCategory"] = categoryName;

            return View(farmer);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                // ViewData["FarmerId"] = new SelectList(_context.Users, "UserId", "Name");
                return View();
            }
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,Price,Quantity,Availability,ProductionDate,FarmerId,CategoryId,ImageUrl")] Product product)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(product);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
        //     ViewData["FarmerId"] = new SelectList(_context.Users, "UserId", "Name", product.FarmerId);
        //     return View(product);
        // }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,Price,Quantity,Availability,ProductionDate,CategoryId,ImageUrl")] Product product)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("Role");

            // if (userId == null || userRole != "FARMER")
            if (userId == null)
            {
                return RedirectToAction("Index", "Unauthorized");
            }

            product.FarmerId = userId.Value; // Auto-assign

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }



        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole.Equals("EMPLOYEE"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
                ViewData["FarmerId"] = new SelectList(_context.Users, "UserId", "UserId", product.FarmerId);
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Unauthorized");
            }
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDescription,Price,Quantity,Availability,ProductionDate,FarmerId,CategoryId,ImageUrl")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["FarmerId"] = new SelectList(_context.Users, "UserId", "UserId", product.FarmerId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole.Equals("EMPLOYEE"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Farmer)
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Unauthorized");
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }



    }
}
