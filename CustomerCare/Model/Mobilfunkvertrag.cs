using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public class Mobilfunkvertrag : BaseEntity
    {
        /// <summary>
        /// EF ctor
        /// </summary>
        Mobilfunkvertrag() { }

        public Mobilfunkvertrag(Kunde pKunde)
        {
            this.Kunde = pKunde;
        }

        [Key]
        public int MobilfunkvertragId { get; set; }

        [Required]
        public virtual Kunde Kunde { get; set; }

        double _monatlicherPreis; 
        [Required]
        public double MonatlicherPreis
        {
            get => _monatlicherPreis;
            set
            {
                if (_monatlicherPreis != value)
                {
                    _monatlicherPreis = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        Datentarif _datentarif;
        [Required]
        public virtual Datentarif Datentarif
        {
            get => _datentarif;
            set
            {
                if (_datentarif != value)
                {
                    _datentarif = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        Telefontarif _telefontarif;
        [Required]
        public virtual Telefontarif Telefontarif
        {
            get => _telefontarif;
            set
            {
                if (_telefontarif != value)
                {
                    _telefontarif = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        String _rufnummer;
        [Required]
        public String Rufnummer
        {
            get => _rufnummer;
            set
            {
                if (_rufnummer != value)
                {
                    _rufnummer = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

    }
}
