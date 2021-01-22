using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models.Concretes
{
    public class Teslim : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public Teslim()
        {
            arac = new Arac();
            kullanici = new Kullanici();
        }
        public int ID { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public decimal Odeme { get; set; }
        public int TeslimEdenKisi { get; set; }
        public int TeslimEdilenArac { get; set; }

        public Arac arac { get; set; }

        public Kullanici kullanici { get; set; }
        public bool isSuccess { get; set; }
    }
}
