using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Dataproviders
{
    abstract class BaseContext : DbContext
    {
        public virtual DbSet<Kunde> Kunden { get; set; }
        public virtual DbSet<Network> Networks { get; set; }
        public virtual DbSet<Telefontarif> Telefontarife { get; set; }
        public virtual DbSet<Datentarif> Datentarife { get; set; }
        public virtual DbSet<Lieferadresse> Lieferadressen { get; set; }
        public virtual DbSet<Rechnungsadresse> Rechnungsadressen { get; set; }

        public BaseContext(String pConnectionString) : base(pConnectionString)
        {

        }
    }
}
