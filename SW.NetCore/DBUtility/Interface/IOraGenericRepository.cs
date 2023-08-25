using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SW.NetCore.DBUtility.Interface
{
    public interface IOraGenericRepository<T> where T : class
    {
        T GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByWhere(string where, object parms, string returnFields, string orderBy);
        void Insert(T entity);
        void DeleteById(string id);
        void DeleteByWhere(string where, object parms);
        void Update(T entity);
        void UpdateByWhere(T entity, string where, string updateFields);
        string GenerateSql();
    }
}
