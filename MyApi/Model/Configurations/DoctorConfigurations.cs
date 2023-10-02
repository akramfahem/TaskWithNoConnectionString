using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary;

namespace APII.Model.Configurations
{
	public class DoctorConfigurations: IEntityTypeConfiguration<Doctor>
    {

        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d=>d.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FName)
                   .HasColumnType("varchar")
                   .HasMaxLength(255)
                   .IsRequired(true);

            builder.Property(x => x.LName)
                   .HasColumnType("varchar")
                   .HasMaxLength(255)
                   .IsRequired(true);

            builder.HasMany(s => s.Subjects).WithOne(d => d.Doctor);
            
        }
    }
}

