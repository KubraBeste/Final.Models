﻿using Final.Commons.Concretes.Data;
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
    public class TeslimRepository : IRepository<Teslim>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public TeslimRepository()
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
                query.Append("FROM [dbo].[tbl_Teslim] ");
                query.Append("WHERE ");
                query.Append("[ID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Teslim] can't be null. ");

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
                                "Deleting Error for entity [tbl_Teslim] reported the Database ErrorCode: " +
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

        public bool Insert(Teslim entity)
        {


            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Teslim] ");
                query.Append("([TeslimTarihi], [Odeme], [TeslimEdenKisi], [TeslimEdilenArac] ) ");
                query.Append("VALUES ");
                query.Append(
                    "( @TeslimTarihi, @Odeme , @TeslimEdenKisi, @TeslimEdilenArac ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Teslim] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params

                        DBHelper.AddParameter(dbCommand, "@TeslimTarihi", CsType.DateTime, ParameterDirection.Input, entity.TeslimTarihi);
                        DBHelper.AddParameter(dbCommand, "@Odeme", CsType.Int, ParameterDirection.Input, entity.Odeme);
                        DBHelper.AddParameter(dbCommand, "@TeslimEdenKisi", CsType.Int, ParameterDirection.Input, entity.TeslimEdenKisi);
                        DBHelper.AddParameter(dbCommand, "@TeslimEdilenArac", CsType.Int, ParameterDirection.Input, entity.TeslimEdilenArac);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Teslim] reported the Database ErrorCode: " + _errorCode);
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

        public IList<Teslim> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Teslim> teslim = new List<Teslim>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[TeslimTarihi], [Odeme], [TeslimEdenKisi], [TeslimEdilenArac] ");
                query.Append("FROM [dbo].[tbl_Teslim] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Teslim can't be null. ");

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
                                    var entity = new Teslim();
                                    entity.ID = reader.GetInt32(0);
                                    entity.TeslimTarihi = reader.GetDateTime(1);
                                    entity.Odeme = reader.GetDecimal(2);
                                    entity.TeslimEdenKisi = reader.GetInt32(3);
                                    entity.TeslimEdilenArac = reader.GetInt32(4);
                                    teslim.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Teslim] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return teslim;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("TransactionsRepository::SelectAll:Error occured.", ex);
            }
        
        }

        public Teslim SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Teslim teslim = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("([TeslimTarihi], [Odeme], [TeslimEdenKisi], [TeslimEdilenArac] ) ");
                query.Append("FROM [dbo].[tbl_Teslim] ");
                query.Append("WHERE ");
                query.Append("[ID] = @id ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Teslim] can't be null. ");

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
                                    var entity = new Teslim();
                                    entity.ID = reader.GetInt32(0);
                                    entity.TeslimTarihi = reader.GetDateTime(1);
                                    entity.Odeme = reader.GetDecimal(2);
                                    entity.TeslimEdenKisi = reader.GetInt32(3);
                                    entity.TeslimEdilenArac = reader.GetInt32(4);
                                    teslim = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Teslim] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

               
                return teslim;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Teslim entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("UPDATE [dbo].[tbl_Teslim] ");
                query.Append("SET [TeslimTarihi] = @TeslimTarihi, [Odeme] = @Odeme, [TeslimEdenKisi] =  @TeslimEdenKisi, [TeslimEdilenArac] = @TeslimEdilenArac ");
                query.Append("WHERE ");
                query.Append(" [ID] = @ID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Teslim] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@TeslimTarihi", CsType.DateTime, ParameterDirection.Input, entity.TeslimTarihi);
                        DBHelper.AddParameter(dbCommand, "@Odeme", CsType.Int, ParameterDirection.Input, entity.Odeme);
                        DBHelper.AddParameter(dbCommand, "@TeslimEdenKisi", CsType.Int, ParameterDirection.Input, entity.TeslimEdenKisi);
                        DBHelper.AddParameter(dbCommand, "@TeslimEdilenArac", CsType.Int, ParameterDirection.Input, entity.TeslimEdilenArac);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Teslim] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("TransactionsRepository::Update:Error occured.", ex);
            }
        }
    }
}
