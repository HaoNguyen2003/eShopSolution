using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.PayMentService.Model
{
    public class ZaloPayResponseQRCode
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string OrderUrl { get; set; }
        public string ZpTransToken { get; set; }
    }
}
