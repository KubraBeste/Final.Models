using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models.Concretes
{
    public class Arac :IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Arac()
        {
            sirket = new Sirket();
        }

        public int AracID { get; set; }

        public int AitOlduguSirket { get; set; }

        [Required(ErrorMessage = "You must enter an marka.")]
        [StringLength(50, MinimumLength = 3)]
        public string Marka { get; set; }

        [Required(ErrorMessage = "You must enter an model.")]
        [StringLength(50, MinimumLength = 3)]
        public string Model{ get; set; }

        public int Yıl { get; set; }

        public bool Uygunluk { get; set; }


        public decimal GünlükFiyat { get; set; }

        public Sirket sirket { get; set; }

    }
}
