using System;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace APII.Model.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FName)
                   .HasColumnType("varchar")
                   .HasMaxLength(255)
                   .IsRequired(true);

            builder.Property(x => x.LName)
                   .HasColumnType("varchar")
                   .HasMaxLength(255)
                   .IsRequired(true);

            builder.HasMany(s => s.Subjects).WithMany(st => st.Students).UsingEntity<Enrollment>();

        }
    }
}

