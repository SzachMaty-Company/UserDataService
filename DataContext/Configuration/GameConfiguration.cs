using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDataService.DataContext.Entities;

namespace UserDataService.DataContext.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasOne(x => x.Chat)
                .WithOne()
                .HasForeignKey<Game>(x => x.ChatId);

            builder.HasMany(x => x.Moves)
                .WithOne()
                .HasForeignKey(x => x.GameId);
        }
    }
}
