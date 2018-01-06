using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Lazeez.Repository.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Delete(Expression<Func<T, bool>> whereCondition);
        T GetById(object id);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> whereCondition);
        bool Exists(Expression<Func<T, bool>> whereCondition);
        string GetPrimaryKeyName();
        int GetPrimaryKeyValue(T entity);
        List<T> CallStoredProcedure(string spName);
        List<T> CallStoredProcedure(string spName, List<SqlParameter> parametersList);
        DataTable CallStoredProcedure(object spName, List<SqlParameter> parametersList);
    }
}
