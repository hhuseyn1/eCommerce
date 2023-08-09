using Microsoft.AspNetCore.Mvc;
using Source.Data;
using Source.Models;
using Source.Models.ViewModels;

namespace Source.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        this._context = context;  
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddProduct()
    {
        return View("AddProduct");
    }

    public IActionResult AddTag()
    {
        return View("AddTag");
    }

    public IActionResult AddCategory()
    {
        return View("AddCategory");
    }

    public IActionResult Tags()
    {
        return View(_context.Tags.ToList());
    }
    public IActionResult Categories()
    {
        return View(_context.Categories.ToList());
    }
    public IActionResult Products()
    {
        return View(_context.Products.ToList());
    }

    [HttpPost]
    public IActionResult AddProduct(AddProductViewModel addProduct)
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddTag(AddTagViewModel addTag)
    {
        if (ModelState.IsValid)
        {
            Tag tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = addTag.Name,
                CreatedDate = DateTime.Now
            };
            _context.Tags.Add(tag);
            _context.SaveChangesAsync();    
        }
            return RedirectToAction("Tags");
    }
    [HttpPost]
    public IActionResult AddCategory(AddCategoryViewModel addCategory)
    {
        if (ModelState.IsValid)
        {
            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = addCategory.Name,
                CreatedDate = DateTime.Now
            };
            _context.Categories.Add(category);
            _context.SaveChangesAsync();
        }
        return RedirectToAction("Categories");
    }

    public IActionResult About()
    {
        return View();
    }

}
