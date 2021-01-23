using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models.Concretes
{
    public class Sirket : IDisposable
    {
        public List<Arac> arac { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Sirket()
        {
            //Customer = new Kullanici();
            aracs = new List<Arac>();
        }

        [Key]
        public int SirketID { get; set; }

        [Required(ErrorMessage = "You must enter an name.")]
        [StringLength(50, MinimumLength = 3)]
        public string SirketAdi { get; set; }

        [Required(ErrorMessage = "You must enter an addres.")]
        [StringLength(50, MinimumLength = 3)]
        public string SirketAdres { get; set; }

        
        public int AracSayisi { get; set; }

        public Decimal SirketPuani { get; set; }

        public virtual List<Arac> aracs { get; set; }

        
    }
}
