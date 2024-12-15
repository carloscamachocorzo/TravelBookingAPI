using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Infraestructure;

namespace TravelBooking.Infraestructure.DataAccess.Contexts;

public partial class TravelBookingContext : DbContext
{
    public TravelBookingContext(DbContextOptions<TravelBookingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmergencyContacts> EmergencyContacts { get; set; }

    public virtual DbSet<Guests> Guests { get; set; }

    public virtual DbSet<Hotels> Hotels { get; set; }

    public virtual DbSet<Reservations> Reservations { get; set; }

    public virtual DbSet<Rooms> Rooms { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmergencyContacts>(entity =>
        {
            entity.HasKey(e => e.EmergencyContactId).HasName("PK__Emergenc__E8A61D8E6A423BB6");

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            entity.HasOne(d => d.Reservation).WithMany(p => p.EmergencyContacts)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emergency__Reser__38996AB5");
        });

        modelBuilder.Entity<Guests>(entity =>
        {
            entity.HasKey(e => e.GuestId).HasName("PK__Guests__0C423C125E355333");

            entity.Property(e => e.DocumentNumber)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DocumentType)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            entity.HasOne(d => d.Reservation).WithMany(p => p.Guests)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Guests__Reservat__35BCFE0A");
        });

        modelBuilder.Entity<Hotels>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotels__46023BDF208B850D");

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.BaseRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Reservations>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F249EFAFDB2");

            entity.Property(e => e.ReservationDate).HasColumnType("datetime");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__RoomI__31EC6D26");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__UserI__32E0915F");
        });

        modelBuilder.Entity<Rooms>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__328639390ACA89A6");

            entity.Property(e => e.BaseCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__HotelId__2E1BDC42");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C6058573C");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534FEB3E78C").IsUnique();

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
