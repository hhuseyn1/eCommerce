using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Source.Data;
using Source.Models;
using Source.Models.ViewModels;

namespace Source.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AdminController(AppDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
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
    public IActionResult About()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddProduct(AddProductViewModel addProduct)
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddTag(AddTagViewModel addTag)
    {

        if (ModelState.IsValid)
        {
            Tag tag = _mapper.Map<Tag>(addTag);
            tag.Id = Guid.NewGuid();
            tag.Name = addTag.Name;
            tag.CreatedDate = DateTime.Now;
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Tags");
    }


    [HttpPost]
    public async Task<IActionResult> AddCategory(AddCategoryViewModel addCategory)
    {
        if (ModelState.IsValid)
        {
            Category category = _mapper.Map<Category>(addCategory);
            category.Id = Guid.NewGuid();
            category.Name = addCategory.Name;
            category.CreatedDate = DateTime.Now;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Categories");
    }

    [HttpGet]
    public IActionResult EditProduct(Guid productId)
    {
        return View();
    }

    [HttpPost]
    public IActionResult EditProduct(AddProductViewModel updatedProduct)
    {
       
        return View();
    }

    [HttpGet]
    public IActionResult EditCategory(string name)
    {
        var category = _context.Categories.FirstOrDefault(p => p.Name == name);
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

    [HttpGet]
    public IActionResult EditTag(string name)
    {
        var tag = _context.Tags.FirstOrDefault(p => p.Name == name);
        if (tag is not null)
            return View(tag);
        return RedirectToAction("Tags");
    }

    [HttpPost]
    public IActionResult EditTag(AddTagViewModel updatedTag)
    {
        if (ModelState.IsValid)
        {
            var tag = _context.Tags.FirstOrDefault(p => p.Id == updatedTag.Id);
            if (tag is not null)
                tag.Name = updatedTag.Name;
        }
        return RedirectToAction("Tags");
    }


    [HttpPost]
    public async Task<IActionResult> DeleteProduct(Product deleteProduct)
    {

        return RedirectToAction("Products");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTag(Tag deleteTag)
    {

        return RedirectToAction("Tags");
    }


    [HttpPost]
    public async Task<IActionResult> DeleteCategory(Category deleteCategory)
    {

        return RedirectToAction("Categories");
    }




}
