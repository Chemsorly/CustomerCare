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
        internal KundenRepository() : base() { }

        public override void Create(Kunde item)
        {
            using (var context = CreateContext())
            {
                context.Kunden.Add(item);
                context.SaveChanges();
            }
        }

        public override void Delete(int id)
        {
            using (var context = CreateContext())
            {
                var entity = context.Kunden.FirstOrDefault(t => t.KundenId == id);
                if (entity != null)
                {
                    context.Kunden.Remove(entity);
                    context.SaveChanges();
                }
            }               
        }

        public override Kunde Get(int id)
        {
            using (var context = CreateContext())
            {
                var kunde = context.Kunden.FirstOrDefault(t => t.KundenId == id);
                if (kunde == null)
                    return null;

                var entry = context.Entry(kunde);
                entry.Reference(t => t.Lieferadresse).Load();
                entry.Reference(t => t.Rechnungsadresse).Load();
                entry.Collection(t => t.Mobilfunkvertraege).Load();
                entry.Entity.Mobilfunkvertraege.ForEach(t => context.Entry(t).Reference(y => y.Datentarif).Load());
                entry.Entity.Mobilfunkvertraege.ForEach(t => context.Entry(t).Reference(y => y.Telefontarif).Load());

                //entry.Collection(t => t.Mobilfunkvertraege.Select(y => y.Datentarif).Load()
                //entry.Collection(t => t.Mobilfunkvertraege.Select(y => y.Telefontarif)).Load();
                return entry.Entity;
            }
               
                //return this.GetAllIncluding(context.Kunden, 
                //    t => t.Lieferadresse,
                //    t => t.Rechnungsadresse,
                //    t => t.Mobilfunkvertraege)
                //    //t => t.Mobilfunkvertraege.Select(y => y.Datentarif),
                //    //t => t.Mobilfunkvertraege.Select(y => y.Telefontarif),
                //    //t => t.Mobilfunkvertraege.Select(y => y.Kunde))
                //    .FirstOrDefault(t => t.KundenId == id);       
        }

        public override void Update(Kunde item)
        {
            using (var context = CreateContext())
            {
                var entity = context.Kunden.FirstOrDefault(t => t.KundenId == item.KundenId); // Entry(item);
                if (entity != null)
                {
                    //context.Kunden.Attach(item);
                    context.Entry(entity).CurrentValues.SetValues(item);
                    //entity.Mobilfunkvertraege = item.Mobilfunkvertraege;
                    //entity.Rechnungsadresse = item.Rechnungsadresse;
                    //entity.Lieferadresse = item.Lieferadresse;

                    this.ApplyChanges(entity, item);
                    context.SaveChanges();
                }
            }  
        }

    }
}
