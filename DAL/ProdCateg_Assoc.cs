namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProdCateg_Assoc
    {
        public int ID { get; set; }

        public int ID_CATEG { get; set; }

        public int ID_PROD { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }
    }
}
