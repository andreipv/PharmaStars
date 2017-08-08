namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProdCateg_Assoc = new HashSet<ProdCateg_Assoc>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? ID_MRF { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdCateg_Assoc> ProdCateg_Assoc { get; set; }
    }
}
