using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.DataLayer.SeedConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
                new AppRole
                {
                    Id = "639de93f-7876-4fff-96ec-37f8db3bf180",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    Description = "The Customer role for the user"
                },
                new AppRole
                {
                    Id = "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f2f",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "The Admin role for the user"
                },
                new AppRole
                {
                    Id = "45deb9d6-c1ae-54a6-a03b-c9a5cfc15d2f",
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    Description = "The Staff role for the user"
                }
            );
        }
    }
}
