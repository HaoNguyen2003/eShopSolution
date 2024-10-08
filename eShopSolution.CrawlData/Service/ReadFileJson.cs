using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace eShopSolution.CrawlData.Service
{
    public class ReadFileJson
    {
        public void FucntionReadFileJson(IFormFile formFile)
        {
            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }
            using (var stream = new StreamReader(formFile.OpenReadStream()))
            {
                string json = stream.ReadToEnd();
                var jsonData = JsonConvert.DeserializeObject<dynamic>(json);
                Console.WriteLine($"{jsonData}");
            }
        }

    }
}
