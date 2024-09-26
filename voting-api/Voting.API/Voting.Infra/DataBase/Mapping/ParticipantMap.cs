using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voting.API.Domain.Entities;

namespace Voting.API.Voting.Infra.DataBase.Mapping
{
    internal class ParticipantMap : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("participant");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        }
    }
}
