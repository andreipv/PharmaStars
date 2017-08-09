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
            
                return db.Manufacturers.Find(id);
            
        }


        public Manufacturer Get(string name)
        {
            
                return db.Manufacturers.Where(a => a.Name == name).SingleOrDefault();
            
        }

        public Manufacturer Add(Manufacturer manufacturer)
        {
            
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();

                return manufacturer;
            
        }


        public Manufacturer Delete(Manufacturer manufacturer)
        {
            
                db.Manufacturers.Remove(manufacturer);
                db.Entry(manufacturer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return manufacturer;
            
        }

        public Manufacturer Update(int id, Manufacturer manufacturer)
        {
            
                Manufacturer findManufacturer = db.Manufacturers.Find(id);

                findManufacturer.Name = manufacturer.Name;
                findManufacturer.Adress = manufacturer.Adress;
                

                db.Entry(findManufacturer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return findManufacturer;
            
        }


        public IQueryable<Manufacturer> GetAll()
        {
            
                return db.Manufacturers;
            
        }





    }
}
