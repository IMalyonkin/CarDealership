namespace CarDealership.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kit-Option")]
    public partial class Kit_Option
    {
        public int Id { get; set; }

        public int KitFK { get; set; }

        public int OptionFK { get; set; }

        public virtual Kit Kit { get; set; }

        public virtual Option Option { get; set; }
    }
}
