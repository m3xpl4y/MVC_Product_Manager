
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Product_Manager.Data;
using MVC_Product_Manager.Models;
using MVC_Product_Manager.ViewModel;

namespace MVC_Product_Manager.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(int pg=1)
        {
            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            var products = await _context.Products
                .Include(x => x.Category)
                .ToListAsync();
            int recsCount = products.Count();
            var pagination = new Pagination(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = products.Skip(recSkip).Take(pagination.PageSize).ToList();
            this.ViewBag.Pagination = pagination;
            return View(model);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create(int? id)
        {

            var viewModel = new ProductViewModel();
            viewModel.CategoryList = new List<Category>();
            //returniert eine list mit kategorien damit beim erstellen einen dropdown mit kategorien zur verfügung steht
            if(id != null)
            {
                var categoryID = await _context.Categories.FindAsync(id); //Id suchen 
                viewModel.CategoryList.Add(categoryID); // ID der liste hinzufügen als ersten Datensatz
                var category = await _context.Categories.ToListAsync(); //alle datensätze setzten in liste
                category.Remove(categoryID); //mitgegebene ID aus der liste entfernen 
                foreach (var item in category)
                {
                    viewModel.CategoryList.Add(item); //restlichen datensätze zur viewModel Liste hinzufügen
                }
            }
            else
            {
                var category = await _context.Categories.ToListAsync();
                foreach (var item in category)
                {
                    viewModel.CategoryList.Add(item);
                }
            }


            return View(viewModel);
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,ProductName,ProductDescription,ArtNr,Brand,Image")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {

                    Category category = await _context.Categories.FindAsync(productViewModel.CategoryId);
                    var product = new Product
                    {
                        Id = productViewModel.Id,
                        ProductName = productViewModel.ProductName,
                        Description = productViewModel.ProductDescription,
                        ArtNr = productViewModel.ArtNr,
                        Brand = productViewModel.Brand,
                        Category = category
                    };
                    await _context.AddAsync(product);


                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productViewModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Products.FindAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,ProductName,Description,ArtNr,Brand,Image")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewModelExists(productViewModel.Id))
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
            return View(productViewModel);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productViewModel = await _context.Products.FindAsync(id);
            _context.Products.Remove(productViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
