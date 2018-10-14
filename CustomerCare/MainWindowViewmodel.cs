using CustomerCare.Model;
using CustomerCare.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomerCare
{
    public class MainWindowViewmodel : ManagerBase
    {
        Core Core;
        Kunde _selectedKunde;
        public Kunde SelectedKunde
        {
            get => _selectedKunde;
            set
            {
                if (value != null)
                {
                    _selectedKunde = value;

                    //load Mobilfunkverträge
                    this.AvailableMobilfunkverträge = _selectedKunde.Mobilfunkvertraege.ToList();

                    NotifyPropertyChanged();
                }
            }
        }

        List<Mobilfunkvertrag> _availableMobilfunkverträge;
        public List<Mobilfunkvertrag> AvailableMobilfunkverträge
        {
            get => _availableMobilfunkverträge;
            set
            {
                if (value != null)
                {
                    _availableMobilfunkverträge = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<Telefontarif> AvailableTelefontarife => Core != null ? Core.Telefontarife : new List<Telefontarif>();
        public Telefontarif SelectedTelefontarif { get; set; }

        public List<Datentarif> AvailableDatentarife => Core != null ? Core.Datentarife : new List<Datentarif>();
        public Datentarif SelectedDatentarif { get; set; }

        Mobilfunkvertrag _selectedVertrag;
        public Mobilfunkvertrag SelectedVertrag
        {
            get => _selectedVertrag;
            set
            {
                if (value != null)
                {
                    _selectedVertrag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public String Username { get; set; } = "dummyUser";

        public ICommand LoadCustomerCommand { get; set; }
        public ICommand AdressänderungRechnungsadresseCommand { get; set; }
        public ICommand AdressänderungLieferadresseCommand { get; set; }
        public ICommand SetTelefontarifCommand { get; set; }
        public ICommand SetDatentarifCommand { get; set; }

        public String LoadCustomerField { get; set; }
        public String NewAddressStraße { get; set; }
        public String NewAddressHausnummer { get; set; }
        public String NewAddressZipCode { get; set; }

        public MainWindowViewmodel()
        {
            LoadCustomerCommand = new RelayCommand<object>(LoadCustomerClicked);
            AdressänderungRechnungsadresseCommand = new RelayCommand<object>(AdressänderungRechnungsadresseClicked);
            AdressänderungLieferadresseCommand = new RelayCommand<object>(AdressänderungLieferadresseClicked);
            SetTelefontarifCommand = new RelayCommand<object>(SetTelefontarifClicked);
            SetDatentarifCommand = new RelayCommand<object>(SetDatentarifClicked);
            Core = new Core();
        }
        public override void Initialize()
        {
            Core.Initialize();

            //refresh all bindings (e.g. commands)
            this.NotifyPropertyChanged(String.Empty);
        }

        void LoadCustomerClicked(object obj)
        {
            //fetch data from LoadCustomerField
            int value;
            if (int.TryParse(LoadCustomerField, out value))
            {
                //is int, check for customer id
                var kunde = Core.GetKunde(value);
                if (kunde != null)
                {
                    this.SelectedKunde = kunde;
                    return;
                }                    
            }

            //check for alternatives: Rufnummer
            Kunde targetKunde = Core.GetKundeByRufnummer(LoadCustomerField);
            if(targetKunde != null)
            {
                this.SelectedKunde = targetKunde;
                return;
            }                

            //check for Name, Vorname
            //returns Collection, for simplicity pick first found element
            var kunden = Core.GetKundeByName(LoadCustomerField);
            if(kunden.Any())
            {
                this.SelectedKunde = kunden.First();
                return;
            }
                
            kunden = Core.GetKundeByVorname(LoadCustomerField);
            if (kunden.Any())
            {
                this.SelectedKunde = kunden.First();
                return;
            }                
        }

        void AdressänderungLieferadresseClicked(object obj)
        {
            AdressänderungClicked(AdressenTyp.Lieferadresse);
        }

        void AdressänderungRechnungsadresseClicked(object obj)
        {
            AdressänderungClicked(AdressenTyp.Rechnungsadresse);
        }

        void AdressänderungClicked(AdressenTyp pTyp)
        {
            //get values
            int zip;

            if (int.TryParse(NewAddressZipCode, out zip))
            {
                Adresse newAdress = null;
                switch(pTyp)
                {
                    case AdressenTyp.Rechnungsadresse:
                        newAdress = new Rechnungsadresse(SelectedKunde)
                        {
                            Straße = NewAddressStraße,
                            Hausnummer = NewAddressHausnummer,
                            ZipCode = zip,
                            LastUpdatedBy = Username,
                        };
                        break;
                    case AdressenTyp.Lieferadresse:
                        newAdress = new Lieferadresse(SelectedKunde)
                        {
                            Straße = NewAddressStraße,
                            Hausnummer = NewAddressHausnummer,
                            ZipCode = zip,
                            LastUpdatedBy = Username,
                        };
                        break;
                }

                Core.ChangeAdressOfCustomer(Username, SelectedKunde, newAdress);               
            }
        }

        void SetTelefontarifClicked(object obj)
        {
            SetTarif(SelectedTelefontarif);
        }

        void SetDatentarifClicked(object obj)
        {
            SetTarif(SelectedDatentarif);
        }

        void SetTarif(Tarif pNewTarif)
        {
            if(!String.IsNullOrWhiteSpace(this.Username) && this.SelectedVertrag != null && pNewTarif != null)
                Core.ChangeTarifOfMobilfunkvertrag(this.Username, this.SelectedVertrag, pNewTarif);
        }
    }
}
