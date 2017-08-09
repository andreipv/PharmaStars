using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoriesRepository : IRepository<Category>
    {
        private DBModel db;

        public CategoriesRepository(DBModel db)
        {
            this.db = db;
        }

        public Category Get(int ID)
        {
            
                
                    return db.Categories.Find(ID);
                
            
        }

        public Category Get(string name)
        {
            
                return db.Categories.Where(a => a.Name == name).SingleOrDefault();
            
        }

        public IQueryable<Category> GetAll()
        {
           
                return db.Categories;
            
        }
        public Category Add(Category category)
        {
           
                db.Categories.Add(category);
                db.SaveChanges();

                return category;
            
        }

        public Category Update(int ID, Category category)
        {
           
                Category findCategory = db.Categories.Find(ID);

                findCategory.ID = category.ID;
                findCategory.Name = category.Name;
                
            
                db.Entry(findCategory).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return category;
            
        }

        public Category Delete(Category category)
        {
            
                db.Categories.Remove(category);
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return category;
            
        }
        
    }
}

