namespace CarDealership.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Model_Engine
    {
        public int Id { get; set; }

        public int ModelFK { get; set; }

        public int EngineFK { get; set; }

        public virtual Engine Engine { get; set; }

        public virtual Model Model { get; set; }
    }
}
