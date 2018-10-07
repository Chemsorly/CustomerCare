using CustomerCare.Data.Dataproviders;
using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    internal class NetworkRepository : BaseRepository<Network>
    {
        internal NetworkRepository() : base() { }

        public override void Create(Network item)
        {
            using (var context = CreateContext())
            {
                context.Networks.Add(item);
                context.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            using (var context = CreateContext())
            {
                var entity = context.Networks.FirstOrDefault(t => t.NetworkId == id);
                if (entity != null)
                {
                    context.Networks.Remove(entity);
                    context.SaveChanges();
                }
            }
        }

        public override Network Get(int id)
        {
            using (var context = CreateContext())
                return context.Networks.FirstOrDefault(t => t.NetworkId == id);
        }

        public Network Get(String tag)
        {
            using (var context = CreateContext())
                return context.Networks.FirstOrDefault(t => t.Tag == tag);
        }

        public override void Update(Network item)
        {
            using (var context = CreateContext())
            {
                var entity = context.Networks.FirstOrDefault(t => t.NetworkId == item.NetworkId);
                if (entity != null)
                {
                    context.Entry(entity).CurrentValues.SetValues(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
