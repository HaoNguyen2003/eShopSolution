using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ChatRoom")]
    public class ChatRoom
    {
        [Key]
        public int ChatRoomID { get; set; } 
        public string Name { get; set; } 
        public virtual ICollection<UserChatRoom> UserChatRooms { get; set; } 
        public virtual ICollection<MessageChat> Messages { get; set; } 
    }
}
