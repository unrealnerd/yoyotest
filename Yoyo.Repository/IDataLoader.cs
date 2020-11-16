using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IDataLoader<T> where T : class
    {
        IList<T> LoadData();
    }
}
