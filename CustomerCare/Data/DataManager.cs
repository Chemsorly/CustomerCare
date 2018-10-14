using CustomerCare.Data.Dataproviders;
using CustomerCare.Data.Repositories;
using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data
{
    public class DataManager : ManagerBase
    {
        KundenRepository Kunden;
        NetworkRepository Networks;
        TelefontarifeRepository Telefontarife;
        DatentarifRepository Datentarife;

        public DataManager()
        {
            //init db
            bool needSeeding = false;
            var dbcontext = new EntityFrameworkContext();
            needSeeding = dbcontext.Database.CreateIfNotExists();

            //init repositories
            Kunden = new KundenRepository(dbcontext);
            Networks = new NetworkRepository(dbcontext);
            Telefontarife = new TelefontarifeRepository(dbcontext);
            Datentarife = new DatentarifRepository(dbcontext);

            //seed if necessary
            if (needSeeding)
            {
                this.Seed();
                Console.WriteLine("database seeded");
            }                    
        }

        public override void Initialize()
        {

        }

        public List<Datentarif> GetDatentarife()
        {
            return this.Datentarife.GetAll();
        }

        public List<Telefontarif> GetTelefontarife()
        {
            return this.Telefontarife.GetAll();
        }

        #region RequestedFunctionality

        public Kunde GetCustomer(int id) { return Kunden.Get(id); }
        public List<Kunde> GetCustomerByName(String pName) { return Kunden.GetByName(pName); }
        public List<Kunde> GetCustomerByVorname(String pName) { return Kunden.GetByVorname(pName); }
        public Kunde GetCustomerByRufnummer(String pRufnummer) { return Kunden.GetByRufnummer(pRufnummer); }

        public void ChangeAdressOfCustomer(String pIssuer, Kunde pCustomer, Adresse pNewAdress)
        {
            try
            {
                //get customer
                var kunde = Kunden.Get(pCustomer.KundenId);
                if (kunde == null)
                    throw new ArgumentException("customer not found");

                //apply change
                if(pNewAdress is Rechnungsadresse)
                    kunde.ChangeValue(pIssuer, nameof(Kunde.Rechnungsadresse), pNewAdress as Rechnungsadresse);
                else if(pNewAdress is Lieferadresse)
                    kunde.ChangeValue(pIssuer, nameof(Kunde.Lieferadresse), pNewAdress as Lieferadresse);
                else
                    throw new Exception($"unknown type of Adresse");

                //submit
                Kunden.Update(kunde);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void ChangeTarifOfMobilfunkvertrag(String pIssuer, Mobilfunkvertrag pMobilfunkvertrag, Tarif pNewTarif)
        {
            try
            {
                //check if telefontarif
                if (pNewTarif is Telefontarif)
                    pMobilfunkvertrag.ChangeValue(pIssuer, nameof(Mobilfunkvertrag.Telefontarif), pNewTarif);
                else if (pNewTarif is Datentarif)
                    pMobilfunkvertrag.ChangeValue(pIssuer, nameof(Mobilfunkvertrag.Datentarif), pNewTarif);
                else
                    throw new Exception("unknown type of Tarif supplied");

                //submit
                Kunden.Update(pMobilfunkvertrag.Kunde);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        private void Seed()
        {
            Networks.Create(new Network()
            {
                Tag = "3G",
                ISO_Code = "ISO-1234"
            });
            Networks.Create(new Network()
            {
                Tag = "4G",
                ISO_Code = "ISO-2345"
            });
            Networks.Create(new Network()
            {
                Tag = "5G",
                ISO_Code = "ISO-3456"
            });

            Telefontarife.Create(new Telefontarif()
            {
                Name = "Phone_Basic",
                AllowedNetworks = new List<Network>()
                {
                    Networks.Get("3G"),
                    Networks.Get("4G")
                }
            });
            Telefontarife.Create(new Telefontarif()
            {
                Name = "Phone_Deluxe",
                AllowedNetworks = new List<Network>()
                {
                    Networks.Get("3G"),
                    Networks.Get("4G"),
                    Networks.Get("5G")
                }
            });
            Datentarife.Create(new Datentarif()
            {
                Name = "Data_Basic",
                MaxAllowedBandwith = 512000,
                AllowedNetworks = new List<Network>()
                {
                    Networks.Get("3G"),
                    Networks.Get("4G"),
                }
            });
            Datentarife.Create(new Datentarif()
            {
                Name = "Data_Deluxe",
                MaxAllowedBandwith = 1024000,
                AllowedNetworks = new List<Network>()
                {
                    Networks.Get("3G"),
                    Networks.Get("4G"),
                    Networks.Get("5G")
                }
            });

            var kunde = new Kunde()
            {
                Vorname = "Hans",
                Name = "Hermann"
            };
            kunde.Rechnungsadresse = new Rechnungsadresse(kunde)
            {
                Straße = "Hanshermannstraße",
                Hausnummer = "22",
                ZipCode = 12345
            };
            kunde.Mobilfunkvertraege = new List<Mobilfunkvertrag>();
            kunde.Mobilfunkvertraege.Add(new Mobilfunkvertrag(kunde)
            {                
                MonatlicherPreis = 25d,
                Rufnummer = "12345678",
                Telefontarif = Telefontarife.Get("Phone_Basic"),
                Datentarif = Datentarife.Get("Data_Basic"),
            });
            kunde.Mobilfunkvertraege.Add(new Mobilfunkvertrag(kunde)
            {
                MonatlicherPreis = 45d,
                Rufnummer = "12345679",
                Telefontarif = Telefontarife.Get("Phone_Basic"),
                Datentarif = Datentarife.Get("Data_Deluxe"),
            });
            Kunden.Create(kunde);

            kunde = new Kunde()
            {
                Vorname = "Manuela",
                Name = "Mustermann",
            };
            kunde.Rechnungsadresse = new Rechnungsadresse(kunde)
            {
                Straße = "Musterstraße",
                Hausnummer = "25",
                ZipCode = 34567
            };
            kunde.Lieferadresse = new Lieferadresse(kunde)
            {
                Straße = "Lieferstraße",
                Hausnummer = "33",
                ZipCode = 12999
            };
            kunde.Mobilfunkvertraege = new List<Mobilfunkvertrag>();
            kunde.Mobilfunkvertraege.Add(new Mobilfunkvertrag(kunde)
            {
                MonatlicherPreis = 25d,
                Rufnummer = "5554443",
                Telefontarif = Telefontarife.Get("Phone_Deluxe"),
                Datentarif = Datentarife.Get("Data_Deluxe"),
            });
            Kunden.Create(kunde);
        }

    }
}
