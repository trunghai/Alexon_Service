using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alexon_Service.Models
{
    public class MaterialType
    {
        public int id { get; set; }
        [Required]
        public String code { get; set; }
        [Required]
        public String name { get; set; }
    }
}
