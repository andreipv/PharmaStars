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
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
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

        public IQueryable<Product> GetAll(double minPrice, double maxPrice)
        {
            return db.Products.Where(a => a.Price <= minPrice && a.Price > maxPrice);
        }

        public IQueryable<Product> GetAll(Manufacturer manufacturer)
        {
            return db.Products.Where(a => a.Manufacturer.Equals(manufacturer));
        }

        public IQueryable<Product> GetAll(Category category)
        {
            return db.Products.Where(a => a.Categories.Equals(category));
        }
    }
}
