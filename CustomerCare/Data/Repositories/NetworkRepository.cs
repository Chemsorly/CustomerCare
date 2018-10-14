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
        internal NetworkRepository(BaseContext pContext) : base(pContext) { }

        public override void Create(Network item)
        {
            this.Context.Networks.Add(item);
            this.Context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var entity = this.Context.Networks.FirstOrDefault(t => t.NetworkId == id);
            if (entity != null)
            {
                this.Context.Networks.Remove(entity);
                this.Context.SaveChanges();
            }
        }

        public override Network Get(int id)
        {
            return this.Context.Networks.FirstOrDefault(t => t.NetworkId == id);
        }

        public Network Get(String tag)
        {
            return this.Context.Networks.FirstOrDefault(t => t.Tag == tag);
        }

        public override void Update(Network item)
        {
            var entity = this.Context.Networks.FirstOrDefault(t => t.NetworkId == item.NetworkId);
            if (entity != null)
            {
                this.Context.Entry(entity).CurrentValues.SetValues(item);
                this.Context.SaveChanges();
            }
        }
    }
}
