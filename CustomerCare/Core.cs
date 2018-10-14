using CustomerCare.Data;
using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare
{
    public class Core : ManagerBase
    {
        DataManager DataManager;

        public List<Telefontarif> Telefontarife => this.DataManager != null ? this.DataManager.GetTelefontarife() : new List<Telefontarif>();
        public List<Datentarif> Datentarife => this.DataManager != null ? this.DataManager.GetDatentarife() : new List<Datentarif>();

        public override void Initialize()
        {
            DataManager = new DataManager();
        }

        public Kunde GetKunde(int id) { return this.DataManager.GetCustomer(id); }
        public List<Kunde> GetKundeByName(String pName) { return DataManager.GetCustomerByName(pName); }
        public List<Kunde> GetKundeByVorname(String pName) { return DataManager.GetCustomerByVorname(pName); }
        public Kunde GetKundeByRufnummer(String pRufnummer) { return DataManager.GetCustomerByRufnummer(pRufnummer); }
        
        public void ChangeAdressOfCustomer(String pIssuer, Kunde pCustomer, Adresse pNewAdress)
        {
            this.DataManager.ChangeAdressOfCustomer(pIssuer, pCustomer, pNewAdress);        
        }
        public void ChangeTarifOfMobilfunkvertrag(String pIssuer, Mobilfunkvertrag pMobilfunkvertrag, Tarif pNewTarif)
        {
            this.DataManager.ChangeTarifOfMobilfunkvertrag(pIssuer, pMobilfunkvertrag, pNewTarif);
        }
    }
}
