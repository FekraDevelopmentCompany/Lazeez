using Lazeez.Repository.Repository;
using Lazeez.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lazeez.Service.Infrastructure
{
    public abstract class BaseService<T, M> : IBaseService<M>
          where T : class, new()
          where M : class, new()
    {
        protected UnitOfWork unitOfWork;
        protected IRepository<T> repository;

        public virtual int Insert(M model, bool autoSave = false)
        {
            Mapper.Instance.CreateMap<M, T>();
            T table = Mapper.Instance.Map<M, T>(model);
            repository.Insert(table);

            if (autoSave)
            {
                unitOfWork.Save();
                return repository.GetPrimaryKeyValue(table);
            }

            return -1;
        }

        public virtual void Update(M model)
        {
            Mapper.Instance.CreateMap<M, T>();
            T table = Mapper.Instance.Map<M, T>(model);
            int id = repository.GetPrimaryKeyValue(table);
            table = repository.GetById(id);
            Mapper.Instance.Map(model, table);
            repository.Update(table);
        }

        public virtual void Delete(M model)
        {
            Mapper.Instance.CreateMap<M, T>();
            T table = Mapper.Instance.Map<M, T>(model);
            repository.Delete(table);
        }

        public virtual void Delete(int id)
        {
            repository.Delete(id);
        }

        public virtual void Delete(Expression<Func<M, bool>> whereCondition)
        {
            Expression<Func<T, bool>> entityWhereCondition = whereCondition.ConvertToExpression<M, T>();
            repository.Delete(entityWhereCondition);
        }

        public M GetById(int id)
        {
            Mapper.Instance.CreateMap<T, M>();
            T table = repository.GetById(id);
            return Mapper.Instance.Map(table, new M());
        }

        public List<M> GetAll()
        {
            Mapper.Instance.CreateMap<T, M>();
            List<T> table = repository.GetAll();
            return Mapper.Instance.MapList<T, M>(table);
        }

        public List<M> GetAll(Expression<Func<M, bool>> whereCondition)
        {
            Mapper.Instance.CreateMap<T, M>();
            Expression<Func<T, bool>> entityWhereCondition = whereCondition.ConvertToExpression<M, T>();
            List<T> table = repository.GetAll(entityWhereCondition).ToList();
            return Mapper.Instance.MapList<T, M>(table);
        }

        public bool Exists(Expression<Func<M, bool>> whereCondition)
        {
            Expression<Func<T, bool>> entityWhereCondition = whereCondition.ConvertToExpression<M, T>();
            return repository.Exists(entityWhereCondition);
        }
    }
}
