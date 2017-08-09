using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    class CategoriesRepository : IRepository<Category>
    {

        public Category Get(int ID)
        {
            {
                using (DBModel db = new DBModel())
                {
                    return db.Categories.Find(ID);
                }
            }
        }

        public Category Get(string name)
        {
            using (DBModel db = new DBModel())
            {
                return db.Categories.Where(a => a.Name == name).SingleOrDefault();
            }
        }

        public IQueryable<Category> GetAll()
        {
            using (DBModel db = new DBModel())
            {
                return db.Categories;
            }
        }
        public Category Add(Category category)
        {
            using (DBModel db = new DBModel())
            {
                db.Categories.Add(category);
                db.SaveChanges();

                return category;
            }
        }

        public Category Update(int ID, Category category)
        {
            using (DBModel db = new DBModel())
            {
                Category findCategory = db.Categories.Find(ID);

                findCategory.ID = category.ID;
                findCategory.Name = category.Name;
                
            
                db.Entry(findCategory).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return category;
            }
        }

        public Category Delete(Category category)
        {
            using (DBModel db = new DBModel())
            {
                db.Categories.Remove(category);
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return category;
            }
        }
        
    }
}

