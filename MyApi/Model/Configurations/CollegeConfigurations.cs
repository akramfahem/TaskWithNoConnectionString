using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APII.Model.Configurations
{
    public class CollegeConfigurations : IEntityTypeConfiguration<College>
    {
        public void Configure(EntityTypeBuilder<College> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.University)
                   .WithMany(x => x.Colleges)
                   .HasForeignKey(x => x.UniversityId)
                   .IsRequired(true);

            builder.HasMany(x => x.Students)
                   .WithOne(x => x.College)
                   .HasForeignKey(x => x.CollegeId);

            builder.HasMany(d => d.Doctors).WithOne(c => c.College).HasForeignKey(c=>c.CollegeId);
            builder.HasMany(S => S.Subjects).WithOne(c => c.College).HasForeignKey(c => c.CollegeId);
        }
    }
}

