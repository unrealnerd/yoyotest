using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace web.Data
{
    public class JsonFileDataLoader<T> : IDataLoader<T> where T : class
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _filePath;

        public JsonFileDataLoader(IWebHostEnvironment environment, string filePath)
        {
            _hostingEnvironment = environment;
            _filePath = filePath;
        }

        public IList<T> LoadData()
        {
            var dataFilePath = Path.Combine(_hostingEnvironment.WebRootPath, _filePath);
            var jsonString = File.ReadAllText(dataFilePath);
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }
}
