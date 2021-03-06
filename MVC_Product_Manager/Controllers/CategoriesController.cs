using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Product_Manager.Data;
using MVC_Product_Manager.Models;
using MVC_Product_Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Product_Manager.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string search, int? pageSizeA, int pg=1)
        {
            if(!pageSizeA.HasValue)
            {
                pageSizeA = 1;
            }
            
            if (pg < 1)
                pg = 1;

            Pagination pagination = null;
            IEnumerable<Category> model = null;

            if (search != null)
            {
                var category = _context.Categories
                    .Where(x => x.CategoryName.Contains(search) || x.Description.Contains(search))
                    .OrderBy(o => o.CategoryName);
                var searchedCategories = await category
                    .ToListAsync();
                int recsCount = searchedCategories.Count();
                pagination = new Pagination(recsCount, pg, pageSizeA.Value);
                int recSkip = (pg - 1) * pageSizeA.Value;
                model = searchedCategories.Skip(recSkip).Take(pagination.PageSize).ToList();
            }
            else
            {
                var allCategories = await _context.Categories
                    .OrderBy(o => o.CategoryName)
                    .ToListAsync();
                int recsCount = allCategories
                    .Count();
                pagination = new Pagination(recsCount, pg, pageSizeA.Value);
                int recSkip = (pg - 1) * pageSizeA.Value;
                model = allCategories.Skip(recSkip).Take(pagination.PageSize).ToList();

            }
            var cat = new CategoryViewModel
            {
                Categories = model,
                Page = pg,
                Pagination = pagination,
                PageSize = pageSizeA.Value,
                Search = search
            };
                return View(cat);
        }

        // GET: Categories
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Categories.ToListAsync());
        //}

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Description,Image, ImageFile")] Category category)
        {
            if (ModelState.IsValid)
            {
                //save and rename file
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if(category.ImageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(category.ImageFile.FileName);
                    string filePath = Path.Combine(path, fileName);
                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await category.ImageFile.CopyToAsync(fileStream);
                    }
                    category.Image = fileName;
                }
                else
                {
                    category.Image = "death-g1dceeb02b_640.jpg";
                }

                //Save to Database
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Description,Image,ImageFile")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var testBild = category.ImageFile;
                    var testimage = category.Image;
                    var testbidl = category.CategoryName;
                    var testbild = category.Id;
                    var testbildd = category.Description;
                    //keep pic if no pic uploaded
                    if(category.ImageFile != null)
                    {
                        //save and rename file
                        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(category.ImageFile.FileName);
                        string filePath = Path.Combine(path, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await category.ImageFile.CopyToAsync(fileStream);
                        }
                        category.Image = fileName;
                    }
                    //save to database
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listOfProductsToRemoveFromCategory = await _context.Products.Where(x => x.Category.Id == id).ToListAsync();

            foreach(var product in listOfProductsToRemoveFromCategory)
            {
                product.Category = null;
                _context.Update(product);
            }
            

            await _context.SaveChangesAsync();

            //TODO: if Category has Products, MySql error, try & catch needed
            var category = await _context.Categories.FindAsync(id);
            //Delete picture from directory
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string fileName = category.Image;
            string filePath = Path.Combine(path, fileName);

            FileInfo file = new FileInfo(filePath);

            file.Delete();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListOfProducts(int id, int pg=1)
        {
            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            var products = await _context.Products.Where(x => x.Category.Id == id).ToListAsync();
            int recsCount = products.Count();
            var pagination = new Pagination(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = products.Skip(recSkip).Take(pagination.PageSize).ToList();
            this.ViewBag.Pagination = pagination;
            return View(model);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
