using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDataService.DataContext.Entities;

namespace UserDataService.DataContext.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasMany(x => x.Messages)
                .WithOne()
                .HasForeignKey(x => x.ChatId);

        }
    }
}
