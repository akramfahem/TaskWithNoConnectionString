using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APII.Model.Configurations
{
    public class UniversityConfigurations : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

