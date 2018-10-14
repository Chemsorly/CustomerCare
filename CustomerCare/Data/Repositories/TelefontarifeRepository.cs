using CustomerCare.Data.Dataproviders;
using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    internal class TelefontarifeRepository : BaseRepository<Telefontarif>
    {
        internal TelefontarifeRepository(BaseContext pContext) : base(pContext) { }

        public override void Create(Telefontarif item)
        {
            this.Context.Telefontarife.Add(item);
            this.Context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var entity = this.Context.Telefontarife.FirstOrDefault(t => t.TarifId == id);
            if (entity != null)
            {
                this.Context.Telefontarife.Remove(entity);
                this.Context.SaveChanges();
            }
        }

        public override Telefontarif Get(int id)
        {
            return this.Context.Telefontarife.FirstOrDefault(t => t.TarifId == id);
        }

        public Telefontarif Get(String name)
        {
            return this.Context.Telefontarife.FirstOrDefault(t => t.Name == name);
        }

        public List<Telefontarif> GetAll()
        {
            return this.Context.Telefontarife.ToList();
        }

        public override void Update(Telefontarif item)
        {
            var entity = this.Context.Telefontarife.FirstOrDefault(t => t.TarifId == item.TarifId);
            if (entity != null)
            {
                this.Context.Entry(entity).CurrentValues.SetValues(item);
                this.Context.SaveChanges();
            }
        }
    }
}
