using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models.Concretes
{
    public class Arac :IDisposable
    {
        private Sirket sirket;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Arac()
        {
            sirket = new Sirket();
            Kiralamas =  new List<Kiralama>();
            Teslims = new List<Teslim>();
        }

        public int AracID { get; set; }

        [Required(ErrorMessage = "You must enter an marka.")]
        public string Marka { get; set; }

        [Required(ErrorMessage = "You must enter an model.")]
        public string Model{ get; set; }

        public int Yıl { get; set; }

        public bool Uygunluk { get; set; }
        public decimal GünlükFiyat { get; set; }

        [Required(ErrorMessage = "You must enter an sirketId")]
        public int AitOlduguSirket { get; set; }
        public Sirket sirkets { get; set; }

        public virtual List<Kiralama> Kiralamas { get; set; }

        public virtual List<Teslim> Teslims { get; set; }

    }
}
