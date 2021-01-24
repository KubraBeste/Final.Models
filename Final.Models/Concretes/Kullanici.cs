using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Final.Models.Concretes
{
    public class Kullanici : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Kullanici()
        {
            Kiralama = new List<Kiralama>();
            Teslim = new List<Teslim>();
        }
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "You must enter an name.")]
        [StringLength(50,MinimumLength = 3)]

        public string CustomerName { get; set; }

        [Required(ErrorMessage = "You must enter an surname.")]
        [StringLength(50, MinimumLength = 3)]

        public string CustomerSurname { get; set; }

        [Required(ErrorMessage = "You must enter an password.")]
        [DataType(DataType.Password)]
        public string CustomerPasskey { get; set; }
        public int CustomerTC { get; set; }

        [Required(ErrorMessage = "You must enter an Username.")]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public virtual List<Kullanici> Kullanicis { get; set; }
        public virtual List<Kiralama> Kiralama { get; set; }

        public virtual List<Teslim> Teslim { get; set; }
        
        public virtual List<UserLogin> Users { get; set; }
    }
}
