using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;

namespace CarDealership.BLL
{
    public class VehicleModel
    {
        public Vehicle vehicle { get; set; }
        public string model { get; set; }
        public byte[] image { get; set; }
        public string engineName { get; set; }
        public string engineType { get; set; }
        public decimal? enginePower { get; set; }
        public string kit { get; set; }
        public string color { get; set; }
        public string totalPrice { get; set; }
        public List<Option> options { get; set; }
    }
}
