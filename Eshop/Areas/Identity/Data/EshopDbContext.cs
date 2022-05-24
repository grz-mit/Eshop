using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data
{
    public class EshopDbContext : IdentityDbContext<AppUser>
    {
        public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<OfferModel>()
                .Property(o => o.Price)
                .HasColumnType("decimal")
                .HasPrecision(18,2);
           
            builder.Entity<AppUser>()
               .Property(a => a.Wallet)
               .HasColumnType("decimal")
               .HasPrecision(18, 2);
        }

        public DbSet<OfferModel> Offers { get; set; }
        public DbSet<GalleryModel> Galleries { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<OfferEndedModel> OffersEnded { get; set; }
        public DbSet<SentMessageModel> SentMessages { get; set; }
        public DbSet<ReceivedMessageModel> ReceivedMessages { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<ReplyComModel> Reply { get; set; }
    }
}
