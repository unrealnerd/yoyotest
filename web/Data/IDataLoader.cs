using System.Collections;
using System.Collections.Generic;

namespace web.Data
{
    public interface IDataLoader<T> where T: class
    {
        IList<T> LoadData();
    }
}