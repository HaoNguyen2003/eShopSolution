using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateComment
    {
        public int Id { get; set; }
        public string UserID {  get; set; }
        public string Content { get; set; }
    }
}
