using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;

namespace WakeyWakeyBackendAPI.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IEncryptionProvider _encryptionProvider;
        
        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IConfiguration configuration): base(options)
        {
            _encryptionProvider = new GenerateEncryptionProvider(configuration["Database:EncryptionKey"]);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Alarm> Alarms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseEncryption(_encryptionProvider);
        }
    }
}
