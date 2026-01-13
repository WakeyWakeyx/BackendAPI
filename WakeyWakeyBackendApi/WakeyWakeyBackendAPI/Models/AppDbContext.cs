using System.Reflection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WakeyWakeyBackendAPI.Models.Attributes;
using WakeyWakeyBackendAPI.Models.Converters;

namespace WakeyWakeyBackendAPI.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IDataProtectionProvider _protectionProvider;
        
        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IDataProtectionProvider protectionProvider): base(options)
        {
            _protectionProvider = protectionProvider;
        }


        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UseFieldEncryption(modelBuilder.Entity<User>());
        }

        private void UseFieldEncryption<TEntity>(EntityTypeBuilder<TEntity> entityBuilder) where TEntity : class
        {
            var encryptedProperties = typeof(TEntity).GetProperties()
                .Where(property => property.GetCustomAttribute<EncryptedAttribute>() != null);
            foreach (var property in encryptedProperties)
            {
                var converter = new EncryptedStringConverter(_protectionProvider);
                entityBuilder.Property(property.PropertyType, property.Name).HasConversion(converter);
            }
        }
    }
}
