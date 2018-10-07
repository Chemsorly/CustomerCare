using CustomerCare.Data.Dataproviders;
using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    internal class TelefontarifeRepository : BaseRepository<Telefontarif>
    {
        internal TelefontarifeRepository() : base() { }

        public override void Create(Telefontarif item)
        {
            using (var context = CreateContext())
            {
                context.Telefontarife.Add(item);
                context.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            using (var context = CreateContext())
            {
                var entity = context.Telefontarife.FirstOrDefault(t => t.TarifId == id);
                if (entity != null)
                {
                    context.Telefontarife.Remove(entity);
                    context.SaveChanges();
                }
            }
        }

        public override Telefontarif Get(int id)
        {
            using (var context = CreateContext())
                return context.Telefontarife.FirstOrDefault(t => t.TarifId == id);
        }

        public Telefontarif Get(String name)
        {
            using (var context = CreateContext())
                return context.Telefontarife.FirstOrDefault(t => t.Name == name);
        }

        public override void Update(Telefontarif item)
        {
            using (var context = CreateContext())
            {
                var entity = context.Telefontarife.FirstOrDefault(t => t.TarifId == item.TarifId);
                if (entity != null)
                {
                    context.Entry(entity).CurrentValues.SetValues(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
