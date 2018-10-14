using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public abstract class Adresse : BaseEntity
    {
        /// <summary>
        /// EF ctor
        /// </summary>
        protected Adresse() { }

        public Adresse(Kunde pKunde)
        {
            this.Kunde = pKunde;
        }

        [Required]
        public virtual Kunde Kunde { get; set; }

        String _straße;
        [Required]
        public String Straße
        {
            get => _straße;
            set
            {
                if (_straße != value)
                {
                    _straße = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        String _hausnummer;
        [Required]
        public String Hausnummer
        {
            get => _hausnummer;
            set
            {
                if (_hausnummer != value)
                {
                    _hausnummer = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        int _zipCode;
        [Required]
        public int ZipCode
        {
            get => _zipCode;
            set
            {
                if (_zipCode != value)
                {
                    _zipCode = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
    }

    public enum AdressenTyp { Rechnungsadresse, Lieferadresse }
}
