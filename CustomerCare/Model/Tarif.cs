using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public abstract class Tarif : BaseEntity
    {
        [Key]
        public int TarifId { get; private set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public List<Network> AllowedNetworks { get; set; } = new List<Network>();
    }
}
