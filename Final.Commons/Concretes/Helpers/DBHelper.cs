
using Final.Commons.Concretes.Data;
using Final.Commons.Concretes.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Commons.Concretes.Helpers
{
    public static class DBHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        // Get connection provider from .config file.
        public static string GetConnectionProvider()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ProviderName;
        }

        // Add parameters by converting them to the right type for incoming DbCommand object.
        public static void AddParameter(DbCommand command, string paramName, CsType csDataType,
            ParameterDirection direction, object value)
        {
            if (command == null)
                throw new ArgumentNullException("command", "The AddParameter's Command value is null.");

            try
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = paramName;
                parameter.DbType = CSharpDbTypeConverter(csDataType);
                parameter.Value = value ?? DBNull.Value;
                parameter.Direction = direction;
                command.Parameters.Add(parameter);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("DBHelper::AddParameter::Error occured.", ex);
            }
        }

        // This method converts C# Data Types to DB Types
        private static DbType CSharpDbTypeConverter(CsType csDataType)
        {
            var dbType = DbType.String;
            switch (csDataType)
            {
                case CsType.String:
                    dbType = DbType.String;
                    break;
                case CsType.Int:
                    dbType = DbType.Int32;
                    break;
                case CsType.Long:
                    dbType = DbType.Int64;
                    break;
                case CsType.Double:
                    dbType = DbType.Double;
                    break;
                case CsType.Decimal:
                    dbType = DbType.Decimal;
                    break;
                case CsType.DateTime:
                    dbType = DbType.DateTime;
                    break;
                case CsType.Boolean:
                    dbType = DbType.Boolean;
                    break;
                case CsType.Short:
                    dbType = DbType.Int16;
                    break;
                case CsType.Guid:
                    dbType = DbType.Guid;
                    break;
                case CsType.ByteArray:
                case CsType.Binary:
                    dbType = DbType.Binary;
                    break;
            }
            return dbType;
        }
    }
}
