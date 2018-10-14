using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerCare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Tests
{
    /// <summary>
    /// Note: Tests should run on a freshly initialized environment (e.g. docker container)
    /// </summary>
    [TestClass()]
    public class DataManagerTests
    {
        [TestMethod()]
        public void GetCustomerTest()
        {
            DataManager dataManager = new DataManager();
            var customer = dataManager.GetCustomer(1);
            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Name == "Hermann");
            Assert.IsTrue(customer.Rechnungsadresse.Hausnummer == "22");
        }

        [TestMethod()]
        public void GetCustomerByNameTest()
        {
            DataManager dataManager = new DataManager();
            var customers = dataManager.GetCustomerByName("Hermann");
            Assert.IsTrue(customers.Count == 1);
            var customer = customers.First();
            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Vorname == "Hans");
            Assert.IsTrue(customer.Rechnungsadresse.Hausnummer == "22");
        }

        [TestMethod()]
        public void GetCustomerByVornameTest()
        {
            DataManager dataManager = new DataManager();
            var customers = dataManager.GetCustomerByVorname("Hans");
            Assert.IsTrue(customers.Count == 1);
            var customer = customers.First();
            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Name == "Hermann");
            Assert.IsTrue(customer.Rechnungsadresse.Hausnummer == "22");
        }

        [TestMethod()]
        public void GetCustomerByRufnummerTest()
        {
            DataManager dataManager = new DataManager();
            var customer = dataManager.GetCustomerByRufnummer("12345678");
            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Name == "Hermann");
            Assert.IsTrue(customer.Rechnungsadresse.Hausnummer == "22");
        }

        [TestMethod()]
        public void ChangeAdressOfCustomerTest()
        {
            DataManager dataManager = new DataManager();
            var customer = dataManager.GetCustomer(1);
            Assert.IsNotNull(customer);

            dataManager.ChangeAdressOfCustomer("unitTester", customer, new Model.Lieferadresse(customer)
            {
                Hausnummer = "100",
                Straße = "unitTestStraße",
                ZipCode = 99999                
            });

            Assert.IsTrue(customer.Lieferadresse.Straße == "unitTestStraße");
            Assert.IsTrue(customer.Lieferadresse.Hausnummer == "100");
            Assert.IsTrue(customer.Lieferadresse.ZipCode == 99999);
        }

        [TestMethod()]
        public void ChangeTarifOfMobilfunkvertragTest()
        {
            DataManager dataManager = new DataManager();
            var customer = dataManager.GetCustomer(1);
            Assert.IsNotNull(customer);

            var mobilfunkvertrag = customer.Mobilfunkvertraege.First();
            var newDatentarif = dataManager.GetDatentarife().First();
            dataManager.ChangeTarifOfMobilfunkvertrag("unitTester", mobilfunkvertrag, newDatentarif);

            Assert.IsTrue(customer.Mobilfunkvertraege.First().Datentarif == newDatentarif);
        }


    }
}