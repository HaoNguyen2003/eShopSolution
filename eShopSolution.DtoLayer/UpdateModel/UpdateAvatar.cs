using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateAvatar
    {
        public string ID { get; set; }
        public IFormFile? formFile { get; set; }
    }
}
