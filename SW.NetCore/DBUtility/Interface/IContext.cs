using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SW.NetCore.DBUtility.Model;

namespace SW.NetCore.DBUtility.Interface
{
    public interface IContext
    {
        List<ConnInfo> GetConnInfo();
        IDbConnection GetConnect();
        IDbConnection GetConnect(string db);
    }
}