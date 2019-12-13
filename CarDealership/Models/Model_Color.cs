namespace CarDealership.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Model_Color
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public int ModelFK { get; set; }

        public int ColorFK { get; set; }

        public virtual Color Color { get; set; }

        public virtual Model Model { get; set; }
    }
}
