using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3105.Data;
using WebApplication3105.Models;

namespace WebApplication3105.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MVCDbContext mvcDbcontext;

        public CategoryController(MVCDbContext mvcDbcontext)
        {
            this.mvcDbcontext = mvcDbcontext;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var category =  await mvcDbcontext.Category.ToListAsync();
            return View(category);
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category catogoryRequest)
        {
            Category cat= mvcDbcontext.Category.Where(x => x.CategoryName == catogoryRequest.CategoryName).FirstOrDefault();
            if (cat == null)
            {
                var category = new Category()
                {
                    CategoryName = catogoryRequest.CategoryName
                };

                await mvcDbcontext.Category.AddAsync(category);
                await mvcDbcontext.SaveChangesAsync();
                return RedirectToAction("AddProduct", "Product");
            }
            else
            {
                ViewBag.Message = "Exist";
                return View();
            }
        }



        [HttpPost]

        public async Task<IActionResult> Detail(UpdateCategoryModel ucm)
        {
            var category= await mvcDbcontext.Category.FindAsync(ucm.CategoryId);

            if (category != null)
            {
                category.CategoryName=ucm.CategoryName;

                await mvcDbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCategoryModel ucm)
        {
            var category = await mvcDbcontext.Category.FindAsync(ucm.CategoryId);

            if (category != null)
            {
                mvcDbcontext.Category.Remove(category);
                await mvcDbcontext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


    }
}
