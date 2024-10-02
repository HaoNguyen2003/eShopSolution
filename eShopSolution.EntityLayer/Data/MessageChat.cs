using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Message")]
    public class MessageChat
    {
        [Key]
        public int MessageID { get; set; } 
        public string UserID { get; set; }
        public int ChatRoomID { get; set; }
        public string Content { get; set; } 
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; } 
        public virtual AppUser User { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
