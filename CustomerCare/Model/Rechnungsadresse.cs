using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public class Rechnungsadresse : Adresse
    {
        protected Rechnungsadresse() { }
        public Rechnungsadresse(Kunde pKunde) : base(pKunde) { }

        [Key, ForeignKey(nameof(Kunde))]
        public int RechnungsadresseId { get; set; }
    }
}
