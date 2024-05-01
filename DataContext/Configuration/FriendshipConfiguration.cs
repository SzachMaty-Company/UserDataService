using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDataService.DataContext.Entities;

namespace UserDataService.DataContext.Configuration
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Friend)
                .WithMany()
                .HasForeignKey(x => x.FriendId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
