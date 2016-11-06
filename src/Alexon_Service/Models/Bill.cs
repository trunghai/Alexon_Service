using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexon_Service.Models
{
    public class Bill
    {
        public String code { get; set; }
        public String code_material { get; set; }
        public String name_material { get; set; }
        public String code_unit { get; set; }
        public String name_unit { get; set; }
        public String date_make { get; set; }
        public Decimal quantity { get; set; }
        public String receiver { get; set; }
        public String note { get; set; }
        
    }
}
