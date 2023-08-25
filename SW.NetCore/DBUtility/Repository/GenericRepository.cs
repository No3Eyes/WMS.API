using Dapper;
using SW.NetCore.DBUtility.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Text;
using System.Reflection;

namespace SW.NetCore.DBUtility.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>  where T : class
    {
        private IUnitOfWork _unitOfWork;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result;

            if (_unitOfWork.Transaction != null)
            {
                result = _unitOfWork.Connection.GetAll<T>(transaction: _unitOfWork.Transaction);
            }
            else
            {
                result = _unitOfWork.Connection.GetAll<T>();
            }

            return result;
        }

        public T GetById(int id)
        {
            T result;

            if (_unitOfWork.Transaction != null)
            {
                result = _unitOfWork.Connection.Get<T>(id, transaction: _unitOfWork.Transaction);
            }
            else
            {
                result = _unitOfWork.Connection.Get<T>(id);
            }

            return result;
        }

        public T GetByGUID(Guid id)
        {
            T result;

            if (_unitOfWork.Transaction != null)
            {
                result = _unitOfWork.Connection.Get<T>(id, transaction: _unitOfWork.Transaction);
            }
            else
            {
                result = _unitOfWork.Connection.Get<T>(id);
            }

            return result;
        }

        public IEnumerable<T> GetByPaging(string sql, Dictionary<string, object> parameters, CommandType commandType)
        {
            //TODO query paging
            return null;
        }

        public IEnumerable<T> GetBySql(string sql, Dictionary<string, object> parameters, CommandType commandType)
        {
            IEnumerable<T> result;

            if (_unitOfWork.Transaction != null)
            {
                result = _unitOfWork.Connection.Query<T>(sql, parameters, transaction: _unitOfWork.Transaction, commandType: commandType);
            }
            else
            {
                result = _unitOfWork.Connection.Query<T>(sql, parameters, commandType: commandType);
            }

            return result;
        }

        public void Insert(T entity)
        {
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.Insert(entity, _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.Insert(entity);
        }

        public void OraInsert(T entity)
        {
            string sql = GetInsertSql();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            var parms = new DynamicParameters();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                parms.Add(propertyInfo.Name, propertyInfo.GetValue(entity,null));
            }

            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.Execute(sql, parms, _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.Execute(sql, parms);
        }

        public void Update(T entity)
        {
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.Update(entity, _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.Update(entity);


        }
        public void Delete(T entity)
        {
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.Delete(entity, _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.Delete(entity);
        }

        /// <summary>
        /// 取TEntity的TableName
        /// </summary>
        protected string GetTableNameMapper()
        {
            dynamic attributeTable = typeof(T).GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType().Name == "TableAttribute");

            return attributeTable != null ? attributeTable.Name : typeof(T).Name;
        }

        public string GenerateSql()
        {
            string strSQL = string.Empty;

            List<string> Fields = new List<string>(); 

            StringBuilder sbWhere = new StringBuilder(" WHERE ");

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Fields.Add(propertyInfo.Name);
            }
            
            string[] arrField = new string[Fields.Count];

            Fields.CopyTo(arrField, 0);

            strSQL = "SELECT " + string.Join(",", arrField) + " FROM " + GetTableNameMapper() + " ";

            return strSQL;
        }

        public string GetInsertSql()
        {
            List<string> Fields = new List<string>();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Fields.Add(propertyInfo.Name);
            }

            string[] arrField = new string[Fields.Count];
            Fields.CopyTo(arrField, 0);

            
            //Create a comma-separated list for 
            //the INSERT INTO part of the clause
            var fields = string.Join(", ", arrField);

            //Create a comma-separated list for
            //the VALUES part of the clause.
            var values = string.Join(", :", arrField);

            return $"INSERT INTO { GetTableNameMapper()}({fields}) " +
                   $"VALUES(:{values}) ";
            
        }

    }
}