using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Model
{
    public class Kunde : BaseEntity
    {
        [Key]
        public int KundenId { get; set; }

        public virtual ICollection<Mobilfunkvertrag> Mobilfunkvertraege { get; set; }

        String _vorname;
        [Required]
        public String Vorname
        {
            get => _vorname;
            set
            {
                if (_vorname != value)
                {
                    _vorname = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        String _name;
        [Required]
        public String Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        Rechnungsadresse _rechnungsadresse;
        [Required]
        public virtual Rechnungsadresse Rechnungsadresse
        {
            get => _rechnungsadresse;
            set
            {
                if (_rechnungsadresse != value)
                {
                    _rechnungsadresse = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        Lieferadresse _lieferadresse;
        public virtual Lieferadresse Lieferadresse
        {
            get => _lieferadresse;
            set
            {
                if (_lieferadresse != value)
                {
                    _lieferadresse = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
    }
}
