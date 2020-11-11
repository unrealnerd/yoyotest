using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace web.Data
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