﻿using CustomerCare.Data.Dataproviders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected BaseContext Context;

        protected BaseRepository(BaseContext pContext) { this.Context = pContext; }

        public abstract void Create(T item);

        public abstract void Delete(int id);

        public abstract T Get(int id);

        public abstract void Update(T item);

    }
}
