using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voting.API.Domain.Entities;

namespace Voting.API.Voting.Infra.DataBase.Mapping
{
    internal class VoteMap : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("vote");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.CreateDate).HasColumnName("create_date").IsRequired();

            builder.Property(x => x.ParedaoId).HasColumnName("id_paredao").IsRequired();
            builder.HasOne(p => p.Paredao).WithMany().HasForeignKey(p => p.ParedaoId);

            builder.Property(x => x.ParticipantId).HasColumnName("id_participant").IsRequired();
            builder.HasOne(p => p.Participant).WithMany().HasForeignKey(p => p.ParticipantId);

            builder.Property(x => x.UserId).HasColumnName("id_user").IsRequired();
            builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
        }
    }
}
