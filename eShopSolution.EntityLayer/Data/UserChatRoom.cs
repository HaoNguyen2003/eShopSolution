using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("UserChatRoom")]
    public class UserChatRoom
    {
        [Key]
        public int UserChatRoomID { get; set; } 
        public string UserID { get; set; } 
        public int ChatRoomID { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
