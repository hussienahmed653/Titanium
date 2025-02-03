using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titanium2.Domain.Stock;

namespace Titanium2.Infrastructure.StockRepo
{
    public class StockConfig : IEntityTypeConfiguration<StockModel>
    {
        public void Configure(EntityTypeBuilder<StockModel> builder)
        {
            builder.HasKey(s => s.StockId);
            builder.Property(s => s.StockId)
                .ValueGeneratedNever();

        }
    }
}
