using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexon_Service.Models
{
    public class PurchaseOrder
    {
        public String code { get; set; }
        public String code_material { get; set; }
        public String name_material { get; set; }
        public DateTime date_make { get; set; }
        public int quantity { get; set; }
        public String receiver { get; set; }
        public String note { get; set; }
    }
}
