﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lazeez.Service.Infrastructure
{
    public interface IBaseService<M> where M : class
    {
        int Insert(M entity, bool autoSave);
        void Update(M entity);
        void Delete(M entity);
        void Delete(int id);
        void Delete(Expression<Func<M, bool>> whereCondition);
        M GetById(int id);
        List<M> GetAll();
        List<M> GetAll(Expression<Func<M, bool>> whereCondition);
        bool Exists(Expression<Func<M, bool>> whereCondition);
    }
}
