using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        IList<T> Data { get; set; }
    }
}