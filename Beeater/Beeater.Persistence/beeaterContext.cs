using System;
using Beeater.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Beeater.Persistence
{
    public partial class beeaterContext : DbContext
    {
        public beeaterContext()
        {
        }

        public beeaterContext(DbContextOptions<beeaterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authorization> Authorizations { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Preference> Preferences { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Theater> Theaters { get; set; }
        public virtual DbSet<Trailer> Trailers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.ToTable("Authorization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bookingauth).HasColumnName("bookingauth");

                entity.Property(e => e.Movieauth).HasColumnName("movieauth");

                entity.Property(e => e.Sceneauth).HasColumnName("sceneauth");

                entity.Property(e => e.Showingauth).HasColumnName("showingauth");

                entity.Property(e => e.Userauth).HasColumnName("userauth");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SeatId).HasColumnName("seatId");

                entity.Property(e => e.ShowId).HasColumnName("showId");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("FK__Booking__seatId__7E37BEF6");

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Booking__showId__7D439ABD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Booking__userId__1332DBDC");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorizationId).HasColumnName("authorizationId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .HasColumnName("title");

                entity.HasOne(d => d.Authorization)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AuthorizationId)
                    .HasConstraintName("FK__Employee__author__60A75C0F");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.HasIndex(e => e.Name, "UQ__Genre__72E12F1BC8DACA19")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgeRating).HasColumnName("ageRating");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.GenreId).HasColumnName("genreId");

                entity.Property(e => e.MoviePoster).HasColumnName("moviePoster");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("releaseDate");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GenreId).HasColumnName("genreId");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Preferences)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Preferenc__genre__6754599E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Preferences)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Preferenc__userI__123EB7A3");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MovieId).HasColumnName("movieId");

                entity.Property(e => e.Rating1).HasColumnName("rating");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Rating__movieId__6E01572D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Rating__userId__114A936A");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Row).HasColumnName("row");

                entity.Property(e => e.TheaterId).HasColumnName("theaterId");

                entity.HasOne(d => d.Theater)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.TheaterId)
                    .HasConstraintName("FK__Seat__theaterId__797309D9");
            });

            modelBuilder.Entity<Show>(entity =>
            {
                entity.ToTable("Show");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MovieId).HasColumnName("movieId");

                entity.Property(e => e.ShowTime)
                    .HasColumnType("date")
                    .HasColumnName("showTime");

                entity.Property(e => e.TheaterId).HasColumnName("theaterId");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Shows)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Show__movieId__75A278F5");

                entity.HasOne(d => d.Theater)
                    .WithMany(p => p.Shows)
                    .HasForeignKey(d => d.TheaterId)
                    .HasConstraintName("FK__Show__theaterId__76969D2E");
            });

            modelBuilder.Entity<Theater>(entity =>
            {
                entity.ToTable("Theater");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.ToTable("Trailer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Movieid).HasColumnName("movieid");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("path");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Trailers)
                    .HasForeignKey(d => d.Movieid)
                    .HasConstraintName("FK__Trailer__movieid__70DDC3D8");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.AuthorizationId).HasColumnName("authorizationId");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("date")
                    .HasColumnName("birthdate");

                entity.Property(e => e.BonusPoints).HasColumnName("bonusPoints");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.HasOne(d => d.Authorization)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AuthorizationId)
                    .HasConstraintName("FK__User__authorizat__14270015");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
