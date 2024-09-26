using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voting.API.Domain.Entities;

namespace Voting.API.Voting.Infra.DataBase.Mapping
{
    internal class ParedaoMap : IEntityTypeConfiguration<Paredao>
    {
        public void Configure(EntityTypeBuilder<Paredao> builder)
        {
            builder.ToTable("paredao");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.StartTime).HasColumnName("start_time").IsRequired();
            builder.Property(x => x.EndTime).HasColumnName("end_time").IsRequired();
            builder.Property(x => x.IsOpen).HasColumnName("is_open").IsRequired();
        }
    }
}
