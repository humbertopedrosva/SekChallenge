using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SekChallenge.API.Entities;

namespace SekChallenge.API.Infra.Mappings
{
    public class ScanMapping : IEntityTypeConfiguration<Scan>
    {
        public void Configure(EntityTypeBuilder<Scan> builder)
        {
            builder.Property(x => x.ResponseCode).IsRequired();
            builder.Property(x => x.ScanId).IsRequired();
            builder.Property(x => x.Resource).IsRequired();
            builder.Property(x => x.Finished).IsRequired().HasDefaultValue(false);
        }
    }
}
