using Final.Commons.Concretes.Data;
using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Logger;
using Final.DataAccess.Abstractions;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Final.DataAccess.Concretes
{
    public class SirketRepository : IRepository<Sirket>, IDisposable
    {

        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public SirketRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }
        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_Sirket] ");
                query.Append("WHERE ");
                query.Append("[SirketID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();
                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception(
                                "Deleting Error for entity [tbl_Sirket] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Insert(Sirket entity)
        {

            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Sirket] ");
                query.Append("( [SirketAdi] ,[SirketAdres], [AracSayisi], [SirketPuani]) ");
                query.Append("VALUES ");
                query.Append(
                    "( @SirketAdi, @SirketAdres, @AracSayisi, @SirketPuani ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@SirketAdi", CsType.String, ParameterDirection.Input, entity.SirketAdi);
                        DBHelper.AddParameter(dbCommand, "@SirketAdres", CsType.String, ParameterDirection.Input, entity.SirketAdres);
                        DBHelper.AddParameter(dbCommand, "@AracSayisi", CsType.Int, ParameterDirection.Input, entity.AracSayisi);
                        DBHelper.AddParameter(dbCommand, "@SirketPuani", CsType.Decimal, ParameterDirection.Input, entity.SirketPuani);
                      
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Sirket> SelectAll()
        {

            _errorCode = 0;
            _rowsAffected = 0;

            IList<Sirket> sirkets = new List<Sirket>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[SirketID], [SirketAdi], [SirketAdres], [AracSayisi], [SirketPuani] ");
                query.Append("FROM [dbo].[tbl_Sirket] ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int,
                            ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Sirket();
                                    entity.SirketID = reader.GetInt32(0);
                                    entity.SirketAdi = reader.GetString(1);
                                    entity.SirketAdres = reader.GetString(2);
                                    entity.AracSayisi = reader.GetInt32(3);
                                    entity.SirketPuani = reader.GetDecimal(4);

                                    sirkets.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return sirkets;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Sirket SelectedById(int id)
        {

            _errorCode = 0;
            _rowsAffected = 0;

            Sirket sirket = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[SirketID],[SirketAdi] ,[SirketAdres], [AracSayisi], [SirketPuani]");
                query.Append("FROM [dbo].[tbl_Sirket] ");
                query.Append("WHERE ");
                query.Append("[SirketID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Sirket();
                                    entity.SirketID = reader.GetInt32(0);
                                    entity.SirketAdi = reader.GetString(1);
                                    entity.SirketAdres = reader.GetString(2);
                                    entity.AracSayisi = reader.GetInt32(3);
                                    entity.SirketPuani = reader.GetDecimal(4);
                                    sirket = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                //kullanici.Kir = new TranscationsRepository().SelectAll().Where(x => x.TransactorAccountNumber.Equals(customer.CustomerID) || x.ReceiverAccountNumber.Equals(customer.CustomerID)).ToList();
                sirket.aracs = new AracRepository().SelectAll().Where(x => x.AitOlduguSirket.Equals(sirket.SirketID)).ToList();
                return sirket;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Sirket entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Sirket] ");
                query.Append(" SET [SirketAdres] = @SirketAdres, [AracSayisi] = @AracSayisi, [SirketPuani] = @SirketPuanı");
                query.Append(" WHERE ");
                query.Append(" [SirketID] = @SirketID ");
                query.Append(" SELECT @intErrorCode = @@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@SirketAdi", CsType.String, ParameterDirection.Input, entity.SirketAdi);
                        DBHelper.AddParameter(dbCommand, "@SirketAdres", CsType.String, ParameterDirection.Input, entity.SirketAdres);
                        DBHelper.AddParameter(dbCommand, "@AracSayisi", CsType.Int, ParameterDirection.Input, entity.AracSayisi);
                        DBHelper.AddParameter(dbCommand, "@SirketPuani", CsType.Decimal, ParameterDirection.Input, entity.SirketPuani);


                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Update:Error occured.", ex);
            }
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }

        public IEnumerable<SelectListItem> GetAll()
        {
            var sirket = new Sirket();
            IEnumerable<SelectListItem> objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in sirket.SirketAdi
                                  select new SelectListItem()
                                  {
                                      Text = sirket.SirketAdi,
                                      Value = sirket.SirketID.ToString(),
                                      Selected =true
                                  }).ToList();
            return objSelectListItems;
        }
    }
}
