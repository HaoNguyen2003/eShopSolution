using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.DataLayer.SeedConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "b4d149a3-b621-4853-81e1-d76b805d21a1",
                    RoleId = "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f2f"
                }
            );
        }
    }
}
