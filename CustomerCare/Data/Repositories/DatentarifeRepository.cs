using CustomerCare.Data.Dataproviders;
using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    internal class DatentarifRepository : BaseRepository<Datentarif>
    {
        internal DatentarifRepository(BaseContext pContext) : base(pContext) { }

        public override void Create(Datentarif item)
        {
            this.Context.Datentarife.Add(item);
            this.Context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var entity = this.Context.Datentarife.FirstOrDefault(t => t.TarifId == id);
            if (entity != null)
            {
                this.Context.Datentarife.Remove(entity);
                this.Context.SaveChanges();
            }
        }

        public override Datentarif Get(int id)
        {
            return this.Context.Datentarife.FirstOrDefault(t => t.TarifId == id);
        }

        public Datentarif Get(String name)
        {
            return this.Context.Datentarife.FirstOrDefault(t => t.Name == name);
        }

        public List<Datentarif> GetAll()
        {
            return this.Context.Datentarife.ToList();
        }

        public override void Update(Datentarif item)
        {
            var entity = this.Context.Datentarife.FirstOrDefault(t => t.TarifId == item.TarifId);
            if (entity != null)
            {
                this.Context.Entry(entity).CurrentValues.SetValues(item);
                this.Context.SaveChanges();
            }
        }
    }
}
