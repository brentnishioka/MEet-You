using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class MEetAndYouDBContext : DbContext
    {
        public MEetAndYouDBContext()
        {
        }

        public MEetAndYouDBContext(DbContextOptions<MEetAndYouDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminAccountRecord> AdminAccountRecords { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventLog> EventLogs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Itinerary> Itineraries { get; set; }
        public virtual DbSet<ItineraryNote> ItineraryNotes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserAccountRecord> UserAccountRecords { get; set; }
        public virtual DbSet<UserEventRating> UserEventRatings { get; set; }
        public virtual DbSet<UserItinerary> UserItineraries { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-RM9387O;Initial Catalog=MEetAndYou-DB;Integrated Security=True");
                //optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["MEetAndYouDatabase"].ConnectionString);
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["MEetAndYouDatabase"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminAccountRecord>(entity => {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK__AdminAcc__719FE4E88D2ED507");

                entity.ToTable("AdminAccountRecords", "MEetAndYou");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminEmail)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Category>(entity => {
                entity.HasKey(e => e.CategoryName)
                    .HasName("category_pk");

                entity.ToTable("Category", "MEetAndYou");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<Event>(entity => {
                entity.ToTable("Events", "MEetAndYou");

                entity.Property(e => e.EventId).HasColumnName("eventID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("eventDate");

                entity.Property(e => e.EventName)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("eventName");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasMany(d => d.CategoryNames)
                    .WithMany(p => p.Events)
                    .UsingEntity<Dictionary<string, object>>(
                        "EventCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("CategoryName").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("category_fk"),
                        r => r.HasOne<Event>().WithMany().HasForeignKey("EventId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("eventID_fk"),
                        j => {
                            j.HasKey("EventId", "CategoryName").HasName("eventCategory_pk");

                            j.ToTable("EventCategory", "MEetAndYou");

                            j.IndexerProperty<int>("EventId").HasColumnName("eventID");

                            j.IndexerProperty<string>("CategoryName").HasMaxLength(50).IsUnicode(false).HasColumnName("categoryName");
                        });
            });

            modelBuilder.Entity<EventLog>(entity => {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__EventLog__5E5486482149FA44");

                entity.ToTable("EventLogs", "MEetAndYou");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.LogLevel)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Image>(entity => {
                entity.ToTable("Images", "MEetAndYou");

                entity.Property(e => e.ImageId).HasColumnName("imageID");

                entity.Property(e => e.ImageExtension)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imageExtension");

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imageName");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("imagePath");

                entity.Property(e => e.ItineraryId).HasColumnName("itineraryID");

                entity.HasOne(d => d.Itinerary)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ItineraryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("imageItineraryID_fk");
            });

            modelBuilder.Entity<Itinerary>(entity => {
                entity.ToTable("Itinerary", "MEetAndYou");

                entity.Property(e => e.ItineraryId).HasColumnName("itineraryID");

                entity.Property(e => e.ItineraryName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("itineraryName");

                entity.Property(e => e.ItineraryOwner).HasColumnName("itineraryOwner");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.ItineraryOwnerNavigation)
                    .WithMany(p => p.Itineraries)
                    .HasForeignKey(d => d.ItineraryOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("itinOwner_fk");

                entity.HasMany(d => d.Events)
                    .WithMany(p => p.Itineraries)
                    .UsingEntity<Dictionary<string, object>>(
                        "ItineraryEvent",
                        l => l.HasOne<Event>().WithMany().HasForeignKey("EventId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("ItinEventE_fk"),
                        r => r.HasOne<Itinerary>().WithMany().HasForeignKey("ItineraryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("ItinEventI_fk"),
                        j => {
                            j.HasKey("ItineraryId", "EventId").HasName("ItinEvent_pk");

                            j.ToTable("ItineraryEvent", "MEetAndYou");

                            j.IndexerProperty<int>("ItineraryId").HasColumnName("itineraryID");

                            j.IndexerProperty<int>("EventId").HasColumnName("eventID");
                        });
            });

            modelBuilder.Entity<ItineraryNote>(entity =>
            {
                entity.HasKey(e => e.ItineraryId)
                    .HasName("itineraryNotes_pk");

                entity.ToTable("ItineraryNotes", "MEetAndYou");

                entity.Property(e => e.ItineraryId)
                    .ValueGeneratedNever()
                    .HasColumnName("itineraryID");

                entity.Property(e => e.NoteContent)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("noteContent");

                entity.HasOne(d => d.Itinerary)
                    .WithOne(p => p.ItineraryNote)
                    .HasForeignKey<ItineraryNote>(d => d.ItineraryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("noteItineraryID_fk");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.PermissionName)
                    .HasName("permission_pk");

                entity.ToTable("Permission", "MEetAndYou");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("permissionName");
            });

            modelBuilder.Entity<Role>(entity => {
                entity.HasKey(e => e.Role1)
                    .HasName("PK__Roles__863D21492235A5A9");

                entity.ToTable("Roles", "MEetAndYou");

                entity.Property(e => e.Role1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<UserAccountRecord>(entity => {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserAcco__1788CCAC45CBEC85");

                entity.ToTable("UserAccountRecords", "MEetAndYou");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.UserPhoneNum)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserRegisterDate)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("Role").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRole__role__46E78A0C"),
                        r => r.HasOne<UserAccountRecord>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRole__UserID__47DBAE45"),
                        j =>
                        {
                            j.HasKey("UserId", "Role").HasName("user_rolePK");

                            j.ToTable("UserRole", "MEetAndYou");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<string>("Role").HasMaxLength(50).IsUnicode(false).HasColumnName("role");
                        });
            });

            modelBuilder.Entity<UserEventRating>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.ItineraryId })
                    .HasName("userEventRatings_pk");

                entity.ToTable("UserEventRatings", "MEetAndYou");

                entity.Property(e => e.EventId).HasColumnName("eventID");

                entity.Property(e => e.ItineraryId).HasColumnName("itineraryID");

                entity.Property(e => e.UserRating).HasColumnName("userRating");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.UserEventRatings)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ratingEventID_fk");

                entity.HasOne(d => d.Itinerary)
                    .WithMany(p => p.UserEventRatings)
                    .HasForeignKey(d => d.ItineraryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ratingItineraryID_fk");
            });

            modelBuilder.Entity<UserItinerary>(entity =>
            {
                entity.HasKey(e => new { e.ItineraryId, e.UserId, e.PermissionName })
                    .HasName("userItinerary_pk");

                entity.ToTable("UserItinerary", "MEetAndYou");

                entity.Property(e => e.ItineraryId).HasColumnName("itineraryID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("permissionName");

                entity.HasOne(d => d.Itinerary)
                    .WithMany(p => p.UserItineraries)
                    .HasForeignKey(d => d.ItineraryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("itineraryID_fk");

                entity.HasOne(d => d.PermissionNameNavigation)
                    .WithMany(p => p.UserItineraries)
                    .HasForeignKey(d => d.PermissionName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("permission_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserItineraries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userIDItinerary_fk");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.Token })
                    .HasName("userRole_PK");

                entity.ToTable("UserToken", "MEetAndYou");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Token)
                    .HasMaxLength(64)
                    .HasColumnName("token")
                    .IsFixedLength();

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("dateCreated");

                entity.Property(e => e.Salt).HasColumnName("salt");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userID_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
