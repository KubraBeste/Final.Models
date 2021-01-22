using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models.Concretes
{
    public class ManagerLogin
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int ManagerID { get; set; }

        [Required(ErrorMessage = "You must enter an username.")]
        [StringLength(50, MinimumLength = 3)]
        public string ManagerName { get; set; }

        [Required(ErrorMessage = "You must enter an password.")]
        [StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }

        public bool isActive { get; set; }
    }
}
