using Microsoft.Extensions.Configuration;
using SW.NetCore.DBUtility.Interface;
using SW.NetCore.DBUtility.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.NetCore.DBUtility.Context
{
    public class SqlContext : IContext
    {
        private readonly List<ConnInfo> _connInfo;
        private string _connString;
        private string _providerName;
        private AssemblyProvider _assemblyProvider = null;

        public SqlContext(List<ConnInfo> connInfo)
        {
            _connInfo = connInfo;
        }

        public SqlContext(string connString,string providerName)
        {
            _providerName = providerName;
            _connString = connString;
            _assemblyProvider = new AssemblyProvider(_providerName);
        }

        public List<ConnInfo> GetConnInfo()
        {
            return _connInfo;
        }

        public IDbConnection GetConnect(string db)
        {
            if (_connInfo.Any(a => a.dbName == db))
            {
                ConnInfo conn = _connInfo.Where(w => w.dbName == db).FirstOrDefault();
                _providerName = conn.providerName;
                _connString = conn.connString;
                _assemblyProvider = new AssemblyProvider(_providerName);

                IDbConnection connection = _assemblyProvider.Factory.CreateConnection();
                connection.ConnectionString = _connString;

                return connection;
            }
            else
                return null;

        }

        public IDbConnection GetConnect()
        {
            IDbConnection connection = _assemblyProvider.Factory.CreateConnection();
            connection.ConnectionString = _connString;

            return connection;
        }

        
    }
}
