using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.BusinessLogic.Concretes
{
    public class LoginAuth
    {
        public bool kontrol(string user , string pass) 
        {
            Kullanici kullanici = new Kullanici();
            if(user!=kullanici.UserName || pass != kullanici.CustomerPasskey)
            {
                return false;
            }
            return true;
        
        }
    }
}
