using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ManufacturerRepository:IRepository<Manufacturer>
    {
        private DBModel db;

        public ManufacturerRepository(DBModel db)
        {
            this.db = db;
        }

        public Manufacturer Get(int id)
        {
            using (DBModel db = new DBModel())
            {
                return db.Manufacturers.Find(id);
            }
        }


        public Manufacturer Get(string name)
        {
            using (DBModel db = new DBModel())
            {
                return db.Manufacturers.Where(a => a.Name == name).SingleOrDefault();
            }
        }

        public Manufacturer Add(Manufacturer manufacturer)
        {
            using (DBModel db = new DBModel())
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();

                return manufacturer;
            }
        }


        public Manufacturer Delete(Manufacturer manufacturer)
        {
            using (DBModel db = new DBModel())
            {
                db.Manufacturers.Remove(manufacturer);
                db.Entry(manufacturer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return manufacturer;
            }
        }

        public Manufacturer Update(int id, Manufacturer manufacturer)
        {
            using (DBModel db = new DBModel())
            {
                Manufacturer findManufacturer = db.Manufacturers.Find(id);

                findManufacturer.Name = manufacturer.Name;
                findManufacturer.Adress = manufacturer.Adress;
                

                db.Entry(findManufacturer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return findManufacturer;
            }
        }


        public IQueryable<Manufacturer> GetAll()
        {
            using (DBModel db = new DBModel())
            {
                return db.Manufacturers;
            }
        }





    }
}
