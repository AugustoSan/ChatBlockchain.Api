using ChatBlockchain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatBlockchain.Infraestructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacion entre User y Contact
            modelBuilder.Entity<ContactModel>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relacion entre User y KeyTransfer
            modelBuilder.Entity<KeyTransfer>()
                .HasOne(k => k.User)
                .WithMany(u => u.KeyTransfers)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Opcional: definir índices o restricciones adicionales
            modelBuilder.Entity<ContactModel>()
                .HasIndex(c => c.UserId);
            
            modelBuilder.Entity<KeyTransfer>()
                .HasIndex(kt => kt.UserId);
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<KeyTransfer> KeyTransfers { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
    }
}