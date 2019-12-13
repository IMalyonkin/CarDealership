namespace CarDealership.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicle-Option")]
    public partial class Vehicle_Option
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int VehicleFK { get; set; }

        public int OptionFK { get; set; }

        public virtual Option Option { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
