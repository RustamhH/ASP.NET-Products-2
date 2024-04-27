using Microsoft.AspNetCore.Mvc;
using Products2.Models;
using System.Reflection;

namespace Products2.Controllers
{
    public class ProductController : Controller
    {
        public List<Product> Products { get; set; }=new List<Product>();
        public IActionResult Index()
        {
            return View();
        }

        public ProductController()
        {
            Products = JsonHandling.ReadData<List<Product>>("products");
        }


        public IActionResult GetAllProducts() { return View(Products); }








        [HttpPost]
        public IActionResult GetProductById(int id)
        {
            if(ModelState.IsValid)
            {
                foreach (var item in Products)
                {
                    if (item.Id == id)
                    {
                        return View(item);
                    }
                }
                return View();
            }
            else return View();
        }

        [HttpGet]
        public IActionResult GetProductById()
        {
            return View();
        }
        






        
        [HttpPost]
        public IActionResult GetProductByPrice(decimal price)
        {
            if(ModelState.IsValid)
            {
                var products = new List<Product>();  
                foreach (var item in Products)
                {
                    if (item.Price == price)
                    {
                        products.Add(item);
                    }
                }
                if(products.Count>0) { return View(products); }
                return View();
            }
            else return View();
        }

        [HttpGet]
        public IActionResult GetProductByPrice()
        {
            return View();
        }
        






        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                if (Products.Any(p => p.Id == product.Id))
                {
                    ModelState.AddModelError("Id", "Product ID must be unique");
                    return View(product);
                }
                Products.Add(product);
                JsonHandling.WriteData(Products, "products");
                return RedirectToAction("GetAllProducts");
            }
            else return View(product);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }




        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            if (ModelState.IsValid)
            {

                var product = Products.FirstOrDefault(p => p.Id == id);
                if (product is not null)
                {
                    Products.Remove(product);
                    JsonHandling.WriteData(Products, "products");
                }
                return RedirectToAction("GetAllProducts");
            }
            else return View();
        }

        [HttpGet]
        public IActionResult RemoveProduct()
        {
            return View();
        }

    }
}
