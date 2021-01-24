using Final.DataAccess.Concretes;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.BusinessLogic.Concretes
{
    public class TeslimBusiness : IDisposable
    {
        private TeslimBusiness _teslimbusiness = new TeslimBusiness();
        private bool _bDisposed;
        private readonly object _lock = new object();
        private Teslim generalTeslim;

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
                    _teslimbusiness = null;
                }

                _bDisposed = true;
            }
        }
        private bool InsertTeslim(Teslim entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new TeslimRepository())
                {
                    isSuccess = repo.Insert(entity);
                    using (var customerRepo = new KullaniciRepository())
                    {
                        Kullanici c = customerRepo.SelectedById(entity.TeslimEdenKisi);
                        generalTeslim = c.Teslim.ElementAt(c.Kiralama.Count - 1);
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:TransactionBusiness::InsertTransaction::Error occured.", ex);
            }
        }

        public List<Teslim> SelectAllTeslim()
        {
            var responseEntities = new List<Teslim>();

            try
            {
                using (var repo = new TeslimRepository())
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
        public TeslimBusiness()
        {
            _teslimbusiness = new TeslimBusiness();
        }
        public bool TeslimEtmek(Teslim teslim)
        {
            AracBusiness aracBusiness = new AracBusiness();
            Kiralama kiralama = new Kiralama();

            var v1 = aracBusiness.SelectAracById(teslim.TeslimEdilenArac);
            var v2 = v1.GünlükFiyat;
            var v3 = kiralama.KiralananArac;
            if (v1.ToString() == v3.ToString())
            {
                var v4 = kiralama.KiralamaTarih;
                var v5 = teslim.TeslimTarihi;
                var v6 = (v4 - v5).TotalDays;
            }
            
            /*
            try
            {
                bool isSuccess = false;
                teslim.isSuccess = true;
                if (sender.Uygunluk == false)
                {
                    lock (_lock)
                        sender.Uygunluk = true;
                    lock (_lock)
                        teslim.TeslimEdenKisi = sender1.CustomerID;
                    lock (_lock)
                        teslim.TeslimEdilenArac = sender.AracID;
                    isSuccess = InsertTeslim(teslim);
                    if (isSuccess != teslim.isSuccess)
                    {
                        teslim = generalTeslim;
                        teslim.isSuccess = isSuccess;
                        //if (UpdateKiralamaInfo(kiralama))
                        //{

                        //}
                    }

                }
                return isSuccess;
            }
            catch (Exception ex)
            {

                throw new Exception("BusinessLogic:TransactionBusiness::Kiralamak::Error occured.", ex);
            }*/
            throw new NotImplementedException();
        }

        private bool UpdateKiralamaInfo(Kiralama kiralama)
        {
            throw new NotImplementedException();
        }


    }
}
