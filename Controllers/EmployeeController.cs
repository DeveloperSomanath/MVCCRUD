using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly DatabaseContext _context;

        public EmployeeController(ILogger<EmployeeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel model, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                model.Images = "/images/" + fileName;
            }

            var result = await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        [HttpGet("Employee/Delete/{PersonId}")]
        public async Task<IActionResult> Delete(int PersonId)
        {
            var row=await _context.Employees.Where(x=>x.PersonId==PersonId).FirstOrDefaultAsync();
            return View(row);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeModel data)
        {
            var model = await _context.Employees.Where(x=>x.PersonId==data.PersonId).FirstOrDefaultAsync();
            var row =  _context.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet("Employee/Edit/{PersonId}")]
        public async Task<IActionResult> Edit(int PersonId)
        {
            var row=await _context.Employees.Where(x=>x.PersonId==PersonId).FirstOrDefaultAsync();
            return View(row);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel data)
        {
            var row =  _context.Update(data);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
