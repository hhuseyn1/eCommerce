using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
using Source.Models.ViewModels;
namespace Source.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public HomeController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddProduct()
    {
        List<Category> categories = _context.Categories.ToList();
        ViewData["Categories"] = categories;

        return View("AddProduct");
    }

    public IActionResult AddCategory()
    {
        return View("AddCategory");
    }

    public IActionResult Categories()
    {
        return View(_context.Categories.ToList());
    }
    public IActionResult Products()
    {
        return View(_context.Products.ToList());
    }
    public IActionResult About()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductViewModel addProduct)
    {
        if (ModelState.IsValid)
        {
            var newProduct = _mapper.Map<Product>(addProduct);
            newProduct = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Price = addProduct.Price,
                CategoryId = addProduct.CategoryId.ToString(),
                ImageUrl = "default"
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Products");
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(AddCategoryViewModel addCategory)
    {
        if (ModelState.IsValid)
        {
            Category category = _mapper.Map<Category>(addCategory);
            category.Id = Guid.NewGuid().ToString();
            category.Name = addCategory.Name;
            category.CreatedDate = DateTime.Now;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Categories");
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(string name)
    {
        List<Category> categories = _context.Categories.ToList();
        ViewData["Categories"] = categories;
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        if (product is not null)
            return View(product);
        return RedirectToAction("Products");
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(AddProductViewModel updatedProduct)
    {
        List<Category> categories = _context.Categories.ToList();
        ViewData["Categories"] = categories;

        if (ModelState.IsValid)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (product is null) return RedirectToAction("Products");
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.CategoryId = updatedProduct.CategoryId.ToString();
            product.Description = updatedProduct.Description;
            product.ImageUrl = updatedProduct.ImageUrl?.ToString();

            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Products");
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(p => p.Name == name);
        if (category is not null)
            return View(category);
        return RedirectToAction("Categories");
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(AddCategoryViewModel updatedCategory)
    {
        if (ModelState.IsValid)
        {
            var category = _context.Categories.FirstOrDefault(p => p.Id == updatedCategory.Id);
            if (category is not null)
                category.Name = updatedCategory.Name;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Categories");
    }



    [HttpPost]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product is not null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Products");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.Id == id);
        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Categories");
    }

}
