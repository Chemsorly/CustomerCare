using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public class Datentarif : Tarif
    {
        [Required]
        public double MaxAllowedBandwith { get; set; }
        
    }
}
