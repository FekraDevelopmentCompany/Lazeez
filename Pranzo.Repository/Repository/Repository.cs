using Lazeez.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Lazeez.Repository.Repository
{
    class Repository<T> : IRepository<T> where T : class
    {
        private readonly EFContext _context;
        private readonly IDbSet<T> _dbSet;

        public Repository(EFContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> Table
        {
            get { return _dbSet; }
        }

        public void Insert(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            //_dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(Expression<Func<T, bool>> whereCondition)
        {
            var lstEntities = _dbSet.Where(whereCondition).ToList();

            foreach (var entity in lstEntities)
                Delete(entity);
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition).AsNoTracking().ToList();
        }

        public bool Exists(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.Any(whereCondition);
        }

        public string GetPrimaryKeyName()
        {
            ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            ObjectSet<T> set = objectContext.CreateObjectSet<T>();
            IEnumerable<string> keyNames = set.EntitySet.ElementType
                .KeyMembers
                .Select(k => k.Name);
            return keyNames.First();
        }

        public int GetPrimaryKeyValue(T entity)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            ObjectSet<T> set = objectContext.CreateObjectSet<T>();
            string keyName = set.EntitySet.ElementType
                .KeyMembers
                .Select(k => k.Name)
                .First();

            Type type = typeof(T);
            return int.Parse(type.GetProperty(keyName).GetValue(entity, null).ToString());
        }

        public List<T> CallStoredProcedure(string spName)
        {
            try
            {
                DbRawSqlQuery<T> result = _context.Database.SqlQuery<T>(spName);
                return result.ToList();
            }
            catch { return new List<T>(); }
        }

        public List<T> CallStoredProcedure(string spName, List<SqlParameter> parametersList)
        {
            spName = spName + " " + string.Join(",", parametersList);
            DbRawSqlQuery<T> result = _context.Database.SqlQuery<T>(spName, parametersList.ToArray());
            return result.ToList();
        }

        public DataTable CallStoredProcedure(object spName, List<SqlParameter> parametersList)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_context.Database.Connection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(spName.ToString(), conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 80000;

                if (parametersList != null)
                    for (int i = 0; i < parametersList.Count; i++)
                    {
                        cmd.Parameters.Add(parametersList[i]);
                    }

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
            }
            return dt;
        }
    }
}