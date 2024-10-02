using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.PayMentService.Config
{
    public class VnPayConfig
    {
        public string vnp_Url {  get; set; }
        public string vnp_Api { get; set; }
        public string vnp_TmnCode { get; set; }
        public string vnp_HashSecret { get; set; }
        public string vnp_Returnurl { get; set; }
        public string Version { get; set; }
        public string CurrCode { get; set; }
        public string Locale { get; set; }
        public string Command {  get; set; }
    }
}
