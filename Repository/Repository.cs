using System;
using System.Collections.Generic;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IList<T> Data { get; set; }
        public Repository(IDataLoader<T> dataLoader)
        {
            Data = dataLoader.LoadData();
        }
    }
}
