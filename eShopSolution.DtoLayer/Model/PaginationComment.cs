using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class PaginationComment
    {
        public List<CommentModel>comments { get; set; }
        public int pageNumber {  get; set; }
        public int pageSize {  get; set; }
        public int Remaining { get; set; }
    }
}
