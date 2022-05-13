using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdvaniaAPI.Models
{
    public partial class THOLOCONContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public THOLOCONContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public THOLOCONContext()
        {
        }

        public THOLOCONContext(DbContextOptions<THOLOCONContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var authConnectionString = _configuration.GetValue<string>("THoloconEntities");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(authConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<BtsTrackingOrder>(entity =>
            {
                entity.HasKey(e => e.ShipmentId)
                    .HasName("PK_bts_TrackingOrders_1");

                entity.ToTable("bts_TrackingOrders");

                entity.Property(e => e.ShipmentId).HasMaxLength(50);

                entity.Property(e => e.ExternalOrderNumber).HasMaxLength(50);

                entity.Property(e => e.PurchaseOrderNumber).HasMaxLength(50);

                entity.Property(e => e.ShipmentDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.ShipmentTrackingUrl).HasColumnName("ShipmentTrackingURL");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
