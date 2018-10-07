using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public class Network : BaseEntity
    {
        [Key]
        public int NetworkId { get; private set; }

        public String Tag { get; set; }
        public String ISO_Code { get; set; }
        public double Frequency { get; }
    }
}
