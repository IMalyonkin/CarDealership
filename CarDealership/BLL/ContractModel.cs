using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;

namespace CarDealership.BLL
{
    public class ContractModel
    {
        public Contract contract { get; set; }
        public string model { get; set; }
        public int modelId { get; set; }
        public string kit { get; set; }
        public string client { get; set; }
        public string employee { get; set; }
        public int employeeId { get; set; }
        public string date { get; set; }
    }
}
