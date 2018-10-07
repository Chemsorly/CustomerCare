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
        internal DatentarifRepository() : base() { }

        public override void Create(Datentarif item)
        {
            using (var context = CreateContext())
            {
                context.Datentarife.Add(item);
                context.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            using (var context = CreateContext())
            {
                var entity = context.Datentarife.FirstOrDefault(t => t.TarifId == id);
                if (entity != null)
                {
                    context.Datentarife.Remove(entity);
                    context.SaveChanges();
                }
            }
        }

        public override Datentarif Get(int id)
        {
            using (var context = CreateContext())
                return context.Datentarife.FirstOrDefault(t => t.TarifId == id);
        }

        public Datentarif Get(String name)
        {
            using (var context = CreateContext())
                return context.Datentarife.FirstOrDefault(t => t.Name == name);
        }

        public override void Update(Datentarif item)
        {
            using (var context = CreateContext())
            {
                var entity = context.Datentarife.FirstOrDefault(t => t.TarifId == item.TarifId);
                if (entity != null)
                {
                    context.Entry(entity).CurrentValues.SetValues(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
