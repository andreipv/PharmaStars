using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Mapping
{
    public static class ManufacturerMapper
    {
        public static Manufacturer ModelToEntity(ManufacturerModel mfr)
        {
            var manufacturer = new Manufacturer();
            manufacturer.ID = mfr.ID;
            manufacturer.Name = mfr.Name;
            manufacturer.Adress = mfr.Adress;

            return manufacturer;

        }

        public static ManufacturerModel EntityToModel(Manufacturer manufacturer)
        {
            var mfr = new ManufacturerModel();
            mfr.ID = manufacturer.ID;
            mfr.Name = manufacturer.Name;
            mfr.Adress = manufacturer.Adress;

            return mfr;
        }



    }
}