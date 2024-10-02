using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.AddModel
{
    public class ReplyComment
    {
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int? ParentCommentID { get; set; } = null;
        public string Content { get; set; }
    }
}
