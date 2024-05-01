using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDataService.DataContext.Entities;

namespace UserDataService.DataContext.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.Statistics)
                .WithOne()
                .HasForeignKey<Statistics>(x => x.UserId);
        }
    }
}
