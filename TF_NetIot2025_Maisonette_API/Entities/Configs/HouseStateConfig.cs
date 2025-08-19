using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TF_NetIot2025_Maisonette_API.Entities.Configs
{
    public class HouseStateConfig : IEntityTypeConfiguration<HouseState>
    {
        public void Configure(EntityTypeBuilder<HouseState> builder)
        {
            builder.HasKey(hs => hs.Id);
            builder.Property(hs => hs.Id).ValueGeneratedOnAdd();    
        }
    }
}
