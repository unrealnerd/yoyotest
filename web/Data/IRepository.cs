using System.Collections.Generic;

namespace web.Data
{
    public interface IRepository<T> where T : class
    {
        IList<T> Data { get; set; }
    }
}