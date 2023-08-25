using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.NetCore.DBUtility.Model
{
    public class ConnInfo
    {
        public ConnInfo(string dbName, string connString, string providerName)
        {
            this.dbName = dbName;
            this.connString = connString;
            this.providerName = providerName;
        }

        public string dbName { get; set; }
        public string connString { get; set; }
        public string providerName { get; set; }
    }
}
