using Microsoft.AspNetCore.Identity;

namespace eShopSolution.EntityLayer.Data
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageURL { get; set; }
        public string? PublicID {  get; set; }
        public DateTime? LastLogin { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Return> Returns { get; set; }
        public virtual ICollection<UserChatRoom> UserChatRooms { get; set; }
        public virtual ICollection<MessageChat> Messages { get; set; }
    }
}
