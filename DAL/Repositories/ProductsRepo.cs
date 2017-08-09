using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductsRepo : IRepository<Product>
    {
        private DBModel db;

        public ProductsRepo(DBModel db)
        {
            this.db = db;
        }

        public Product Get(int id)
        {
            using (DBModel db = new DBModel())
            {
                return db.Products.Find(id);
            }
        }

        public Product Get(string name)
        {
            using (DBModel db = new DBModel())
            {
                return db.Products.Where(a => a.Name == name).SingleOrDefault();
            }
        }

        public Product Add(Product product)
        {
            using (DBModel db = new DBModel())
            {
                db.Products.Add(product);
                db.SaveChanges();

                return product;
            }
        }

        public Product Delete(Product product)
        {
            using (DBModel db = new DBModel())
            {
                db.Products.Remove(product);
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return product;
            }
        }

        public Product Update(int id, Product product)
        {
            using (DBModel db = new DBModel())
            {
                Product findProduct = db.Products.Find(id);

                findProduct.Name = product.Name;
                findProduct.ID_MRF = product.ID_MRF;
                findProduct.Price = product.Price;
                findProduct.Quantity = product.Quantity;
                findProduct.ProdCateg_Assoc = product.ProdCateg_Assoc;

                db.Entry(findProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return findProduct;
            }
        }

        public IQueryable<Product> GetAll()
        {
            using (DBModel db = new DBModel())
            {
                return db.Products;
            }
        }
    }
}
