using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Data.Sqlite;
using System.Data.Odbc;
using System.Data.OleDb;
using Npgsql;

namespace SW.NetCore.DBUtility.Context
{
    /// <summary>
    /// A Singlton class which provides and loads the necessary assembly
    /// </summary>
    internal class AssemblyProvider
    {
      
        private string _providerName = string.Empty;

        public AssemblyProvider(string providerName)
        {
            _providerName = providerName;
        }


        #region Refactored Code
        internal DbProviderFactory Factory
        {
            get
            {
                DbProviderFactory factory = null;

                if (_providerName == "System.Data.SqlClient")
                {
                    DbProviderFactories.RegisterFactory(_providerName, SqlClientFactory.Instance);
                    factory = DbProviderFactories.GetFactory(_providerName);
                }
                else if(_providerName == "Oracle.ManagedDataAccess.Client")
                {
                    DbProviderFactories.RegisterFactory(_providerName, OracleClientFactory.Instance);
                    factory = DbProviderFactories.GetFactory(_providerName);
                }
                else if (_providerName == "MySql.Data.MySqlClient")
                {
                    DbProviderFactories.RegisterFactory(_providerName, MySqlClientFactory.Instance);
                    factory = DbProviderFactories.GetFactory(_providerName);
                }
                else if (_providerName == "Npgsql")
                {
                    DbProviderFactories.RegisterFactory(_providerName, NpgsqlFactory.Instance);
                    factory = DbProviderFactories.GetFactory(_providerName);
                }
                else if (_providerName == "Microsoft.Data.Sqlite")
                {
                    DbProviderFactories.RegisterFactory(_providerName, SqliteFactory.Instance);
                    factory = DbProviderFactories.GetFactory(_providerName);
                }
                else if (_providerName == "System.Data.Odbc")
                {
                    DbProviderFactories.RegisterFactory(_providerName, OdbcFactory.Instance);
                    factory = DbProviderFactories.GetFactory(_providerName);
                }
                else if (_providerName == "System.Data.OleDb")
                {
                    DbProviderFactories.RegisterFactory(_providerName, OleDbFactory.Instance);
                    factory = DbProviderFactories.GetFactory(_providerName);
                }

                return factory;
            }
        }

        #endregion
    }
}
