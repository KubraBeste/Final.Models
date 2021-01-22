using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Data;
using Final.Commons.Concretes.Logger;
using Final.DataAccess.Abstractions;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Final.DataAccess.Concretes
{

    public class AracRepository : IRepository<Arac>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public AracRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
        public bool Insert(Arac entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Arac] ");
                query.Append("( [AitOlduguSirket] ,[Marka], [Model], [Yıl], [Uygunluk] , [GünlükFiyat]) ");
                query.Append("VALUES ");
                query.Append(
                    "( @AitOlduguSirket, @Marka, @Model, @Yıl, @Uygunluk  @GünlükFiyat ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Arac] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params

                        DBHelper.AddParameter(dbCommand, "@AitOlduguSirket", CsType.Int, ParameterDirection.Input, entity.AitOlduguSirket);
                        DBHelper.AddParameter(dbCommand, "@Marka", CsType.String, ParameterDirection.Input, entity.Marka);
                        DBHelper.AddParameter(dbCommand, "@Model", CsType.String, ParameterDirection.Input, entity.Model);
                        DBHelper.AddParameter(dbCommand, "@Yıl", CsType.Int, ParameterDirection.Input, entity.Yıl);
                        DBHelper.AddParameter(dbCommand, "@Uygunluk", CsType.Boolean, ParameterDirection.Input, entity.Uygunluk);
                        DBHelper.AddParameter(dbCommand, "@GünlükFiyat", CsType.Decimal, ParameterDirection.Input, entity.GünlükFiyat);
                        
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Arac] reported the Database ErrorCode: " + _errorCode);
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
        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_Arac] ");
                query.Append("WHERE ");
                query.Append("[AracID] = @AracID ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Arac] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@AracID", CsType.Int, ParameterDirection.Input, id);

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
                                "Deleting Error for entity [tbl_Arac] reported the Database ErrorCode: " +
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

        public IList<Arac> SelectAll()
        {
           
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Arac> arac = new List<Arac>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    " [AitOlduguSirket] ,[Marka], [Model], [Yıl], [Uygunluk] , [GünlükFiyat]");
                query.Append("FROM [dbo].[tbl_Arac] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Arac] can't be null. ");

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
                                    var entity = new Arac();
                                    entity.AracID = reader.GetInt32(0);
                                    entity.AitOlduguSirket = reader.GetInt32(1);
                                    entity.Marka = reader.GetString(2);
                                    entity.Model = reader.GetString(3);
                                    entity.Yıl = reader.GetInt32(4);
                                    entity.Uygunluk = reader.GetBoolean(5);
                                    entity.GünlükFiyat = reader.GetDecimal(6);
                                    
                                    arac.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Arac] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return arac;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Arac SelectedById(int id)
        {
            
            _errorCode = 0;
            _rowsAffected = 0;

            Arac arac = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    " [AitOlduguSirket] ,[Marka], [Model], [Yıl], [Uygunluk] , [GünlükFiyat]");
                query.Append("FROM [dbo].[tbl_Arac] ");
                query.Append("WHERE ");
                query.Append("[AracID] = @AracID ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Arac] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@AracID", CsType.Int, ParameterDirection.Input, id);

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
                                    var entity = new Arac();
                                    entity.AracID = reader.GetInt32(0);
                                    entity.AitOlduguSirket = reader.GetInt32(1);
                                    entity.Marka = reader.GetString(2);
                                    entity.Model = reader.GetString(3);
                                    entity.Yıl = reader.GetInt32(4);
                                    entity.Uygunluk = reader.GetBoolean(5);
                                    entity.GünlükFiyat = reader.GetDecimal(6);
                                    arac = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Arac] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

               // arac.Kullanici = new AracRepository().SelectAll().Where(x => x.AracMarkası.Equals(Kullanici.KullaniciID) || x.ReceiverAccountNumber.Equals(customer.CustomerID)).ToList();
                //arac.sirket=new SirketRepository().SelectAll().Where(x => x.Ait)
                return arac;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Arac entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tblı_Arac] ");
                query.Append(" SET [AitOlduguSirket] = @AitOlduguSirket, [Marka] = @Marka, [Model] = @Model, [Yıl] = @Yıl, [Uygunluk)] = @Uygunluk, [GünlükFiyat]= @GünlükFiyat");
                query.Append(" WHERE ");
                query.Append(" [AracID] = @AracID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Arac] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@AitOlduguSirket", CsType.String, ParameterDirection.Input, entity.AitOlduguSirket);
                        DBHelper.AddParameter(dbCommand, "@Marka", CsType.String, ParameterDirection.Input, entity.Marka);
                        DBHelper.AddParameter(dbCommand, "@Model", CsType.String, ParameterDirection.Input, entity.Model);
                        DBHelper.AddParameter(dbCommand, "@Yıl", CsType.String, ParameterDirection.Input, entity.Yıl);
                        DBHelper.AddParameter(dbCommand, "@Uygunluk", CsType.String, ParameterDirection.Input, entity.Uygunluk);
                        DBHelper.AddParameter(dbCommand, "@GünlükFiyat", CsType.Decimal, ParameterDirection.Input, entity.GünlükFiyat);
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Arac] reported the Database ErrorCode: " + _errorCode);
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

        
       
    }
}
