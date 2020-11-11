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
        private readonly ILogger<Repository<T>> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IList<T> Data { get; set; }
        public Repository(ILogger<Repository<T>> logger, IWebHostEnvironment environment, string filePath)
        {
            _logger = logger;
            _hostingEnvironment = environment;
            Data = LoadDataFromJsonFile(filePath);
        }

        private IList<T> LoadDataFromJsonFile(string filePath)
        {
            var dataFilePath = Path.Combine(_hostingEnvironment.WebRootPath, filePath);
            var jsonString = File.ReadAllText(dataFilePath);
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }

    public class FileRepositoryOptions
    {
        public const string FileRepository = "FileRepository";
        public string Athletes { get; set; }
        public string Shuttles { get; set; }
    }
}