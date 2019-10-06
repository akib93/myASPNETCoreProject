using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using OnlineShopingManangment.Models;
using OnlineShopingManangment.ViewModels;


using OnlineShopingManangment.Data.interfaces;

namespace OnlineShopingManangment.Controllers
{
    public class MedicineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment hostingEnvironment;
      

        public MedicineController(ApplicationDbContext context,
            IFileProvider fileprovider, IHostingEnvironment env)
        {
            _context = context;
            fileProvider = fileprovider;
            hostingEnvironment = env;
        }
       

        // GET: Medicine
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var trainees = from s in _context.Medicines
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                trainees = trainees.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    trainees = trainees.OrderByDescending(s => s.Name);
                    break;
                default:
                    trainees = trainees.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Medicine>.CreateAsync(trainees.AsNoTracking(), pageNumber ?? 1, pageSize));


            //var applicationDbContext = _context.Medicines.Include(m => m.Category);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Medicine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MedicineId == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // GET: Medicine/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicineId,Name,ShortDescription,LongDescription,Price,ImageUrl,IsPreferredMedicine,InStock,CategoryId")] Medicine  medicine, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicine);
                await _context.SaveChangesAsync();

                // Code to upload image if not null
                if (file != null || file.Length != 0)
                {
                    // Create a File Info 
                    FileInfo fi = new FileInfo(file.FileName);

                    // This code creates a unique file name to prevent duplications 
                    // stored at the file location
                    var newFilename = medicine.MedicineId + "_" + String.Format("{0:d}",
                                      (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                    var webPath = hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\ImageFiles\" + newFilename);

                    // IMPORTANT: The pathToSave variable will be save on the column in the database
                    var pathToSave = @"/ImageFiles/" + newFilename;

                    // This stream the physical file to the allocate wwwroot/ImageFiles folder
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // This save the path to the record
                    medicine.ImageThumbnailUrl = pathToSave;
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }


        // GET: Medicine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", medicine.CategoryId);
            return View(medicine);
        }

        // POST: Medicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicineId,Name,ShortDescription,LongDescription,Price,ImageUrl,ImageThumbnailUrl,IsPreferredMedicine,InStock,CategoryId")] Medicine medicine)
        {
            if (id != medicine.MedicineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.MedicineId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", medicine.CategoryId);
            return View(medicine);
        }

        // GET: Medicine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MedicineId == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // POST: Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.MedicineId == id);
        }

        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Medicine> medicines;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                medicines = _context.Medicines.OrderBy(p => p.MedicineId);
                currentCategory = "All medicines";
            }
            else
            {
                if (string.Equals("Tablet", _category, StringComparison.OrdinalIgnoreCase))
                    medicines = _context.Medicines.Where(p => p.Category.CategoryName.Equals("Tablet")).OrderBy(p => p.Name);
                else
                    medicines = _context.Medicines.Where(p => p.Category.CategoryName.Equals("Syrup")).OrderBy(p => p.Name);

                currentCategory = _category;
            }

            return View(new MedicineListViewModel
            {
                Medicines = medicines,
                CurrentCategory = currentCategory
            });
        }

        public ViewResult All(int medicineId)
        {
            var medicine = _context.Medicines.FirstOrDefault(d => d.MedicineId == medicineId);
            if (medicine == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(medicine);
        }
    }
}
