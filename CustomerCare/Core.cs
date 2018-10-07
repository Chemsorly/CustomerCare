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

        public override void Initialize()
        {
            DataManager = new DataManager();
        }

        public Kunde GetKunde(int id) { return this.DataManager.GetCustomer(id); }

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
