using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SW.NetCore.DBUtility.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        T GetByGUID(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBySql(string sql, Dictionary<string, object> parameter, CommandType commandType);
        IEnumerable<T> GetByPaging(string sql, Dictionary<string, object> parameters, CommandType commandType);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        string GenerateSql();
    }
}
