namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static Common.EntityValidationConstants.Manager;

    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.WorkPhoneNumber)
                .IsRequired()
                .HasMaxLength(PhoneNumberMaxLength);

            builder
                .Property(m => m.UserId)
                .IsRequired();

            builder
                .HasOne(m => m.User)
                .WithOne()
                .HasForeignKey<Manager>(m => m.UserId);
        }
    }
}
