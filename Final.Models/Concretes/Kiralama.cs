using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models.Concretes
{
    public class Kiralama : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Kiralama()
        {

            kullanici = new Kullanici();
            arac = new Arac();
        }

        public int ID { get; set; }

        public Kullanici kullanici { get; set; }
        public int KiralayanKisi { get; set; }

        public Arac arac { get; set; }
        public int KiralananArac { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KiralamaTarih { get; set; }
        

        
        public bool isSuccess { get; set; }
    }
}
