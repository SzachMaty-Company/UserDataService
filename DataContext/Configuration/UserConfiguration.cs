using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDataService.DataContext.Entities;

namespace UserDataService.DataContext.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Games)
                .WithOne()
                .HasForeignKey(x => x.BlackId)
                .HasForeignKey(x => x.WhiteId);

            builder.HasOne(x => x.Statistics)
                .WithOne()
                .HasForeignKey<User>(x => x.StatisticsId);

        }
    }
}
