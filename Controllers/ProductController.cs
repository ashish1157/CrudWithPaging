using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using PagedList.Mvc;
using WebApplication3105.Data;
using WebApplication3105.Models;
using X.PagedList;

namespace WebApplication3105.Controllers
{
    public class ProductController : Controller
    {
        private readonly MVCDbContext dbContext;

        public ProductController(MVCDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int? page)
        {
            List<Product> products = dbContext.Product.ToList();
            List<Category> categories = dbContext.Category.ToList();
            int recordsPerPage = 3;


            var data = from p in products
                       join c in categories on p.CategoryId equals c.CategoryId into table1
                       from c in table1.ToList()
                       select new ProductCategory
                       {
                           product = p,
                           category = c
                       };

            int pageNumber = page ?? 1;
            var pagedList = data.ToPagedList(pageNumber, recordsPerPage);

            return View(pagedList);


        }


        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct(Product productRequest)
        {


            var product = new Product()
            {
                ProductName = productRequest.ProductName,
                CategoryId = productRequest.CategoryId
            };

            await dbContext.Product.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]

        public async Task<IActionResult> Show(int id)
        {
            var product = await dbContext.Product.FirstOrDefaultAsync(x => x.ProductId == id);

            if (product != null)
            {
                var showmodel = new UpdateProductModel()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId
                };
                return View(showmodel);
            }
            return RedirectToAction("Index");
        }



        [HttpPost]

        public async Task<IActionResult> Show(UpdateProductModel upm)
        {
            var product = await dbContext.Product.FindAsync(upm.ProductId);

            if (product != null)
            {
                product.ProductName = upm.ProductName;
                product.CategoryId = upm.CategoryId;

                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductModel upm)
        {
            var product = await dbContext.Product.FindAsync(upm.ProductId);

            if(product!= null)
            {
               dbContext.Product.Remove(product);  
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");   
        }

      

    }
}
