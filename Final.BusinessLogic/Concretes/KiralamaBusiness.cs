using Final.DataAccess.Concretes;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.BusinessLogic.Concretes
{
    public class KiralamaBusiness : IDisposable
    {
        private KullaniciBusiness _customerbusiness = new KullaniciBusiness();
        private AracBusiness _aracbusiness = new AracBusiness();
        private bool _bDisposed;
        private readonly object _lock = new object();
        private Kiralama generalKiralama;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _customerbusiness = null;
                }

                _bDisposed = true;
            }
        }
        private bool InsertKiralama(Kiralama entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new KiralamaRepository())
                {
                    isSuccess = repo.Insert(entity);
                    using (var customerRepo = new KullaniciRepository())
                    {
                        Kullanici c = customerRepo.SelectedById(entity.KiralayanKisi);
                        generalKiralama = c.Kiralama.ElementAt(c.Kiralama.Count - 1);
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:TransactionBusiness::InsertTransaction::Error occured.", ex);
            }
        }

        public List<Kiralama> SelectAllKiralama()
        {
            var responseEntities = new List<Kiralama>();

            try
            {
                using (var repo = new KiralamaRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public KiralamaBusiness()
        {
            _customerbusiness = new KullaniciBusiness();
            _aracbusiness = new AracBusiness();
        }
        public bool Kiralamak(Kiralama kiralama )
        {

            Kullanici kullanici = new Kullanici();
            Arac arac = new Arac();
            try
            {
                bool isSuccess = false;
                kiralama.isSuccess = false;
               
                    
                    lock (_lock)
                        kiralama.KiralananArac = arac.AracID;
                    lock (_lock)
                        kiralama.KiralayanKisi = kullanici.CustomerID;
                    lock (_lock)
                        kiralama.KiralamaTarih = kiralama.KiralamaTarih.Date;
                        
                    
                    isSuccess = InsertKiralama(kiralama);
                    if(isSuccess != kiralama.isSuccess)
                    {
                        kiralama = generalKiralama;
                        kiralama.isSuccess = isSuccess;
                        if (UpdateKiralamaInfo(kiralama))
                        {
                            lock (_lock)
                                _customerbusiness.UpdateCustomer(kullanici);
                            lock (_lock)
                                _aracbusiness.UpdateArac(arac);
                                
                        }
                    }
                
                return isSuccess;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:TransactionBusiness::MakeTransaction::Error occured.", ex);
            }

           
        }

        private bool UpdateKiralamaInfo(Kiralama kiralama)
        {
            try
            {
                bool isSuccess;
                using (var repo = new KiralamaRepository())
                {
                    isSuccess = repo.Update(kiralama);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:TransactionBusiness::UpdateTransaction::Error occured.", ex);
            }
        }
    }
}
