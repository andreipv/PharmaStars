using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Mapping
{
    public static class CategoriesMapper
    {
        public static CategoriesModel EntityToModel(Category entity)
        {
            CategoriesModel model = new CategoriesModel();
            model.ID = entity.ID;
            model.Name = entity.Name;
            return model;
        }
        public static Category ModelToEntity(CategoriesModel model)
        {
            Category entity = new Category();
            entity.ID = model.ID;
            entity.Name = model.Name;
            return entity;

        }
    }
}