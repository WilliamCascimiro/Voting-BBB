using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voting.API.Domain.Entities;

namespace Voting.API.Voting.Infra.DataBase.Mapping
{
    internal class ResultMap : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.ToTable("result");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Count).HasColumnName("count").IsRequired();

            builder.Property(x => x.ParedaoId).HasColumnName("id_paredao").IsRequired();
            builder.HasOne(p => p.Paredao).WithMany().HasForeignKey(p => p.ParedaoId);

            builder.Property(x => x.ParticipantId).HasColumnName("id_participant").IsRequired();
            builder.HasOne(p => p.Participant).WithMany().HasForeignKey(p => p.ParticipantId);
        }
    }
}
