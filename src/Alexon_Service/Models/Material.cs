﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexon_Service.Models
{
    public class Material
    {
        public int id { get; set; }
        public String code { get; set; }
        public String name { get; set; }
        public String code_material_type { get; set; }
        public String code_unit { get; set;}
        public String unit { get; set; }
        public String production_countries { get; set; }
        public String code_symbols { get; set; }
        public String number { get; set; }
        public String capacity { get; set; }
        public String in_use { get; set; }
        public String position { get; set; }
        public String moi_dat_de { get; set; }
        public String note { get; set; }
        public Decimal original_price { get; set; }
        public Int16 status { get; set; }
        public String source { get; set; }
        public Decimal quantity { get; set; }
        



    }
}
