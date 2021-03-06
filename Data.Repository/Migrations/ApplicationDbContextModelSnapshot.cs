﻿// <auto-generated />
using System;
using Data.Repository.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Model.Events.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("Duration");

                    b.Property<int>("EventManagerId");

                    b.Property<int?>("LocationId");

                    b.Property<int>("MusicianId");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("RoleGroupId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Domain.Model.Genres.GenrerGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DefaultName");

                    b.HasKey("Id");

                    b.ToTable("GroupsOfGenrers");
                });

            modelBuilder.Entity("Domain.Model.Genres.LanguageGenrer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GenrerGroupId");

                    b.Property<string>("Language");

                    b.Property<int>("LanguageGroupId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GenrerGroupId");

                    b.ToTable("LanguageGenres");
                });

            modelBuilder.Entity("Domain.Model.Images.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Extension");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Model.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<DateTime>("Expires");

                    b.Property<string>("RemoteIpAddress");

                    b.Property<string>("Token");

                    b.Property<string>("UserEmail");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Domain.Model.Reviews.EventReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<int?>("EventReviewPhotoId");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("ReviewDate");

                    b.Property<int>("ReviewerId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("EventReviewPhotoId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("EventReviews");
                });

            modelBuilder.Entity("Domain.Model.Roles.LanguageRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Language");

                    b.Property<string>("Name");

                    b.Property<int>("RoleGroupId");

                    b.HasKey("Id");

                    b.HasIndex("RoleGroupId");

                    b.ToTable("LanguageRoles");
                });

            modelBuilder.Entity("Domain.Model.Roles.RoleGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DefaultName");

                    b.HasKey("Id");

                    b.ToTable("GroupsOfRoles");
                });

            modelBuilder.Entity("Domain.Model.SocialMedia.SocialNetwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("SocialNetworks");
                });

            modelBuilder.Entity("Domain.Model.Tracks.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtistName");

                    b.Property<int?>("CustomerId");

                    b.Property<int>("DurationInSeconds");

                    b.Property<string>("Extension");

                    b.Property<string>("FileName");

                    b.Property<string>("TrackAudioLocation");

                    b.Property<string>("TrackName");

                    b.Property<int?>("TrackPhotoId");

                    b.Property<string>("TrackPhotoLocation");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TrackPhotoId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Domain.Model.Tracks.TrackLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("TrackId");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackLikes");
                });

            modelBuilder.Entity("Domain.Model.Tracks.TrackPlay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("TrackId");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackPlays");
                });

            modelBuilder.Entity("Domain.Model.Users.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerType");

                    b.Property<string>("Description");

                    b.Property<int>("FollowersCount");

                    b.Property<string>("Gender");

                    b.Property<int?>("HeaderPhotoId");

                    b.Property<string>("IdentityId");

                    b.Property<string>("Locale");

                    b.Property<int?>("LocationId");

                    b.Property<decimal>("Price");

                    b.Property<string>("ProfileName");

                    b.Property<int?>("ProfilePhotoId");

                    b.Property<decimal>("Rating");

                    b.Property<int>("ReviewsCount");

                    b.HasKey("Id");

                    b.HasIndex("HeaderPhotoId");

                    b.HasIndex("IdentityId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProfilePhotoId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Age", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("Ages");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<int>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerCountries");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.CountryGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DefaultName");

                    b.HasKey("Id");

                    b.ToTable("GroupsOfCountries");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<int>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerGenres");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.LanguageCountry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryGroupId");

                    b.Property<string>("Language");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryGroupId");

                    b.ToTable("LanguageCountries");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<int>("CountryGroupId");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.Property<int?>("customerId");

                    b.HasKey("Id");

                    b.HasIndex("customerId");

                    b.ToTable("CustomerRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Model.Users.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<long?>("FacebookId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PictureUrl");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("Domain.Model.Events.Event", b =>
                {
                    b.HasOne("Domain.Model.ValueObjects.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Domain.Model.Genres.LanguageGenrer", b =>
                {
                    b.HasOne("Domain.Model.Genres.GenrerGroup", "GenrerGroup")
                        .WithMany()
                        .HasForeignKey("GenrerGroupId");
                });

            modelBuilder.Entity("Domain.Model.RefreshToken", b =>
                {
                    b.HasOne("Domain.Model.Users.Customer")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Domain.Model.Reviews.EventReview", b =>
                {
                    b.HasOne("Domain.Model.Events.Event", "Event")
                        .WithMany("Reviews")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Model.Images.Image", "EventReviewPhoto")
                        .WithMany()
                        .HasForeignKey("EventReviewPhotoId");

                    b.HasOne("Domain.Model.Users.Customer", "Reviewer")
                        .WithMany()
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Roles.LanguageRole", b =>
                {
                    b.HasOne("Domain.Model.Roles.RoleGroup", "Group")
                        .WithMany()
                        .HasForeignKey("RoleGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.SocialMedia.SocialNetwork", b =>
                {
                    b.HasOne("Domain.Model.Users.Customer", "Customer")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Domain.Model.Tracks.Track", b =>
                {
                    b.HasOne("Domain.Model.Users.Customer")
                        .WithMany("Tracks")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Domain.Model.Images.Image", "TrackPhoto")
                        .WithMany()
                        .HasForeignKey("TrackPhotoId");
                });

            modelBuilder.Entity("Domain.Model.Tracks.TrackLike", b =>
                {
                    b.HasOne("Domain.Model.Tracks.Track")
                        .WithMany("Likes")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Tracks.TrackPlay", b =>
                {
                    b.HasOne("Domain.Model.Tracks.Track")
                        .WithMany("Plays")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.Users.Customer", b =>
                {
                    b.HasOne("Domain.Model.Images.Image", "HeaderPhoto")
                        .WithMany()
                        .HasForeignKey("HeaderPhotoId");

                    b.HasOne("Domain.Model.Users.AppUser", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");

                    b.HasOne("Domain.Model.ValueObjects.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Domain.Model.Images.Image", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoId");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.City", b =>
                {
                    b.HasOne("Domain.Model.ValueObjects.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Country", b =>
                {
                    b.HasOne("Domain.Model.Users.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Gender", b =>
                {
                    b.HasOne("Domain.Model.Users.Customer", "Customer")
                        .WithMany("Genders")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.LanguageCountry", b =>
                {
                    b.HasOne("Domain.Model.ValueObjects.CountryGroup", "Group")
                        .WithMany()
                        .HasForeignKey("CountryGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Model.ValueObjects.Role", b =>
                {
                    b.HasOne("Domain.Model.Users.Customer", "customer")
                        .WithMany("Roles")
                        .HasForeignKey("customerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
