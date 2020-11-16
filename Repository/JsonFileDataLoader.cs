using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Repository
{
    public class JsonFileDataLoader<T> : IDataLoader<T> where T : class
    {
        private readonly string _filePath;

        public JsonFileDataLoader(string filePath)
        {
            _filePath = filePath;
        }

        public IList<T> LoadData()
        {
            var jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }
}
