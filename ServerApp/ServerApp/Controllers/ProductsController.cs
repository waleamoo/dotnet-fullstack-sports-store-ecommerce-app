using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private DataContext context;
        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }

        // GET:/api/prpducts/{id}
        [HttpGet("{id}")]
        public Product GetProduct(long id)
        {
            Product result = context.Products
                .Include(p => p.Supplier).ThenInclude(s => s.Products)
                .Include(p => p.Ratings)
                .FirstOrDefault(p => p.ProductId == id);
            if (result != null)
            {
                if (result.Supplier != null)
                {
                    result.Supplier.Products = result.Supplier.Products.Select(p =>
                     new Product
                     {
                         ProductId = p.ProductId,
                         Name = p.Name,
                         Category = p.Category,
                         Description = p.Description,
                         Price = p.Price,
                     });
                }
                if (result.Ratings != null)
                {
                    foreach (Rating r in result.Ratings)
                    {
                        r.Product = null;
                    }
                }
            }
            return result;
        }

        // GET: https://localhost:3601/api/products?related=true&category=soccer&search=stadium
        [HttpGet]
        public IEnumerable<Product> GetProducts(string category, string search, bool related = false)
        {
            IQueryable<Product> query = context.Products;

            if (!string.IsNullOrWhiteSpace(category))
            {
                string catLower = category.ToLower();
                query = query.Where(p => p.Category.ToLower().Contains(catLower));
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                string searchLower = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchLower)
                || p.Description.ToLower().Contains(searchLower));
            }

            if (related)
            {
                query = query.Include(p => p.Supplier).Include(p => p.Ratings);
                List<Product> data = query.ToList();
                data.ForEach(p => {
                    if (p.Supplier != null)
                    {
                        p.Supplier.Products = null;
                    }
                    if (p.Ratings != null)
                    {
                        p.Ratings.ForEach(r => r.Product = null);
                    }
                });
                return data;
            }
            else
            {
                return query;
            }
        }


        /*
        
        Product result = context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Ratings)
            .FirstOrDefault(p => p.ProductId == id);
        if (result != null)
        {
            if (result.Supplier != null)
            {
                result.Supplier.Products = null;
            }
            if (result.Ratings != null)
            {
                foreach (Rating r in result.Ratings)
                {
                    r.Product = null;
                }
            }
        }
        return result;

        {
            "productId":1,"name":"Kayak","category":"Watersports",
            "description":"A boat for one person","price":275.00,
            "supplier":{
                "supplierId":1,"name":"Splash Dudes","city":"San Jose","state":"CA","products":null
             },
            "ratings":[
                {"ratingId":1,"stars":4,"product":null},
                {"ratingId":2,"stars":3,"product":null}
            ]
        }

        ----------------------------------------------------------------------

        Include all the products associated with a supplier 
        Product result = context.Products
                .Include(p => p.Supplier).ThenInclude(s => s.Products)
                .Include(p => p.Ratings)
                .FirstOrDefault(p => p.ProductId == id);
            if (result != null)
            {
                if (result.Supplier != null)
                {
                    result.Supplier.Products = result.Supplier.Products.Select(p =>
                     new Product
                     {
                         ProductId = p.ProductId,
                         Name = p.Name,
                         Category = p.Category,
                         Description = p.Description,
                         Price = p.Price,
                     });
                }
                if (result.Ratings != null)
                {
                    foreach (Rating r in result.Ratings)
                    {
                        r.Product = null;
                    }
                }
            }
            return result;

        {
            "productId":1,"name":"Kayak","category":"Watersports",
             "description":"A boat for one person","price":275.00,
             "supplier":{"supplierId":1,"name":"Splash Dudes","city":"San Jose",
             "state":"CA",
             "products":[
                 { "productId":1,"name":"Kayak","category":"Watersports",
                 "description":"A boat for one person", "price":275.00,
                 "supplier":null,"ratings":null},
                 { "productId":2,"name":"Lifejacket","category":"Watersports",
                 "description":"Protective and fashionable","price":48.95,
                 "supplier":null,"ratings":null}]},
            "ratings":[
                {"ratingId":1,"stars":4,"product":null},
                {"ratingId":2,"stars":3,"product":null}
            ]
        }

         */
    }
}
