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

namespace Final.DataAccess.Concretes
{
    public class ManageRepository: IRepository<ManagerLogin> , IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public ManageRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }
        public bool DeletedById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
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

        public bool Insert(ManagerLogin entity)
        {

            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_ManagerLogin] ");
                query.Append("( [ManagerName], [Password],  [isActive]  ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @ManagerName, @Password ,  @isActive ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_ManagerLogin] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@ManagerName", CsType.String, ParameterDirection.Input, entity.ManagerName);
                        DBHelper.AddParameter(dbCommand, "@Password", CsType.String, ParameterDirection.Input, entity.Password);
                        DBHelper.AddParameter(dbCommand, "@isActive", CsType.String, ParameterDirection.Input, entity.isActive);
                       

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_ManagerLogin] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("ManageRepository::Insert:Error occured.", ex);
            }
        }

        public IList<ManagerLogin> SelectAll()
        {
            throw new NotImplementedException();
        }

        public ManagerLogin SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            ManagerLogin manager = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[ManagerName], [Password] ,[isActive] ");
                query.Append("FROM [dbo].[tbl_ManagerLogin] ");
                query.Append("WHERE ");
                query.Append("[ManagerID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_ManagerLogin] can't be null. ");

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
                                    var entity = new ManagerLogin();
                                    entity.ManagerID = reader.GetInt32(0);
                                    entity.ManagerName = reader.GetString(1);
                                    entity.Password = reader.GetString(2);
                                    entity.isActive = reader.GetBoolean(3);
                                    manager = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_ManagerLogin] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                
                return manager;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(ManagerLogin entity)
        {
            throw new NotImplementedException();
        }
    }
}
