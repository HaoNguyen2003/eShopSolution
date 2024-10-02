using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class CommentModel
    {
        public int Id { get; set; }
        public AppUserModel appUserModel { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ParentCommentId { get; set; }
        public int? HasChild { get; set;}
        public List<CommentModel> Replies { get; set; }
    }
}
