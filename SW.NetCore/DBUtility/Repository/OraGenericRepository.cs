using Dapper;
using SW.NetCore.DBUtility.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SW.DapperExtensions;
using System.Text;
using System.Reflection;
using SW.DapperExtensions.Oracle;

namespace SW.NetCore.DBUtility.Repository
{
    public abstract class OraGenericRepository<T> : IOraGenericRepository<T> where T : class
    {
        private IUnitOfWork _unitOfWork;
        public OraGenericRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result;

            if (_unitOfWork.Transaction != null)
            {
                result = _unitOfWork.Connection.GetAll<T>(tran:_unitOfWork.Transaction);
            }
            else
            {
                result = _unitOfWork.Connection.GetAll<T>();
            }

            return result;
        }

        public IEnumerable<T> GetByWhere(string where, object parms, string returnFields = null, string orderBy = null)
        {
            IEnumerable<T> result;

            if (_unitOfWork.Transaction != null)
            {
                result = _unitOfWork.Connection.GetByWhere<T>(where, param: parms, returnFields,orderBy, tran: _unitOfWork.Transaction);
            }
            else  
            {
                result = _unitOfWork.Connection.GetByWhere<T>(where, param: parms, returnFields, orderBy);
            }

            return result;
        }

        public T GetById(string id)
        {
            T result;

            if (_unitOfWork.Transaction != null)
            {
                result = _unitOfWork.Connection.GetById<T>(id, tran: _unitOfWork.Transaction);
            }
            else
            {
                result = _unitOfWork.Connection.GetById<T>(id);
            }

            return result;
        }

        public void Insert(T entity)
        {
            
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.Insert(entity, tran: _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.Insert(entity);
            
            
        }

        public void Update(T entity)
        {
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.Update(entity, tran :_unitOfWork.Transaction);
            else
                _unitOfWork.Connection.Update(entity);


        }

        public void UpdateByWhere(T entity, string where, string updateFields)
        {
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.UpdateByWhere(entity, where, updateFields, tran: _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.UpdateByWhere(entity, where, updateFields);
        }


        public void DeleteById(string id)
        {
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.DeleteByIds<T>(id, tran : _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.DeleteByIds<T>(id);
        }

        public void DeleteByWhere(string where,object parms)
        {
            if (_unitOfWork.Transaction != null)
                _unitOfWork.Connection.DeleteByWhere<T>(where, parms, tran: _unitOfWork.Transaction);
            else
                _unitOfWork.Connection.DeleteByWhere<T>(where, parms);
        }

        /// <summary>
        /// 取TEntity的TableName
        /// </summary>
        protected string GetTableNameMapper()
        {
            //dynamic attributeTable = typeof(T).GetCustomAttributes(false)
            //    .SingleOrDefault(attr => attr.GetType().Name == "TableName");
            dynamic attributeTable = typeof(T).GetCustomAttributes(false)
                .SingleOrDefault();

            return attributeTable != null ? attributeTable.TableName : typeof(T).Name;
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
    }
}