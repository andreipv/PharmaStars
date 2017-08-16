using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DBModel db = new DBModel();
        private ProductsRepo productsRepo;
        private ManufacturerRepository manufacturerRepo;
        private CategoriesRepository categoriesRepo;
        
        public ProductsRepo ProductsRepo
        {
            get
            {
                if (this.productsRepo == null)
                    this.productsRepo = new ProductsRepo(db);
                return productsRepo;
            }
        }

        public ManufacturerRepository ManufacturerRepo
        {
            get
            {
                if (this.manufacturerRepo == null)
                    this.manufacturerRepo = new ManufacturerRepository(db);
                return manufacturerRepo;
            }
        }

        public CategoriesRepository CategoriesRepo
        {
            get
            {
                if (this.categoriesRepo == null)
                    this.categoriesRepo = new CategoriesRepository(db);
                return categoriesRepo;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
