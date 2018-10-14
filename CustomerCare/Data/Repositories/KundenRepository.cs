using CustomerCare.Data.Dataproviders;
using CustomerCare.Model;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    internal class KundenRepository : BaseRepository<Kunde>
    {
        internal KundenRepository(BaseContext pContext) : base(pContext) { }

        public override void Create(Kunde item)
        {
            this.Context.Kunden.Add(item);
            this.Context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var entity = this.Context.Kunden.FirstOrDefault(t => t.KundenId == id);
            if (entity != null)
            {
                this.Context.Kunden.Remove(entity);
                this.Context.SaveChanges();
            }
        }

        public override Kunde Get(int id)
        {
            return this.Context.Kunden.FirstOrDefault(t => t.KundenId == id);  
        }

        public List<Kunde> GetByName(String pName)
        {
            return this.Context.Kunden.Where(t => t.Name == pName).ToList();
        }

        public List<Kunde> GetByVorname(String pVorname)
        {
            return this.Context.Kunden.Where(t => t.Vorname == pVorname).ToList();
        }

        public Kunde GetByRufnummer(String pRufnummer)
        {
            return this.Context.Kunden.FirstOrDefault(t => t.Mobilfunkvertraege.Any(u => u.Rufnummer == pRufnummer));
        }

        public override void Update(Kunde item)
        {
            var entity = this.Context.Kunden.FirstOrDefault(t => t.KundenId == item.KundenId);
            if (entity != null)
            {
                this.Context.Entry(entity).CurrentValues.SetValues(item);
                this.Context.SaveChanges();
            }

        }

    }
}
