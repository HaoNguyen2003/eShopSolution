using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Comments")]
    public class Comments
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public DateTime? CommentDate { get; set; }
        public DateTime? CommentUpdate { get; set; }
        public string Content { get; set; }
        public int? ParentCommentID { get; set; }
        public int? DisplayCommentLevelID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Comments? ParentComment { get; set; }
        public virtual AppUser AppUser { get; set; }
        public ICollection<Comments> Replies { get; set; }
    }
}
