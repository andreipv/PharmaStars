using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductsRepo : IRepository<Product>
    {
        private DBModel db = new DBModel();

        public ProductsRepo(DBModel db)
        {
            this.db = db;
        }

        public Product Get(int id)
        {
                return db.Products.Find(id);
        }

        public Product Get(string name)
        {

            return db.Products.Where(a => a.Name == name).SingleOrDefault();

        }

        public Product Add(Product product)
        {

             db.Products.Add(product);
             db.SaveChanges();

             return product; 

        }

        public Product Delete(Product product)
        {

            Product prod = db.Products.Find(product.ID);

            if (prod == null)
                throw new KeyNotFoundException();

            db.Products.Remove(product);
         
            db.SaveChanges();

            return product;

        }

        public Product Update(int id, Product product)
        {

             Product findProduct = db.Products.Find(id);

             if (findProduct == null)
                  throw new KeyNotFoundException();


             findProduct.Name = product.Name;
             findProduct.ID_MRF = product.ID_MRF;
             findProduct.Price = product.Price;
             findProduct.Quantity = product.Quantity;
             findProduct.Categories = product.Categories;

             db.Entry(findProduct).State = System.Data.Entity.EntityState.Modified;
             db.SaveChanges();


             return findProduct;

        }

        public IQueryable<Product> GetAll()
        {

             return db.Products;

        }

        public IQueryable<Product> GetAll(string search = "")
        {
            return db.Products.Where(a => a.Name.Contains(search));
        }
    }
}
