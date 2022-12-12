﻿// <auto-generated />
using System;
using MangaFatihi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MangaFatihi.Persistence.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20221212190839_SeriesEpisode_AddTeamId")]
    partial class SeriesEpisode_AddTeamId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MangaFatihi.Domain.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.Identity.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.Series", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BroadcastStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSlyder")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpToDate")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDateOnPage")
                        .HasColumnType("datetime2");

                    b.Property<int>("StaticSeriesStatusId")
                        .HasColumnType("int");

                    b.Property<int>("StaticSeriesTypesId")
                        .HasColumnType("int");

                    b.Property<string>("Story")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleAlternative")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StaticSeriesStatusId");

                    b.HasIndex("StaticSeriesTypesId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAndSeriesArtist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("SeriesArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SeriesArtistId");

                    b.HasIndex("SeriesId");

                    b.ToTable("SeriesAndSeriesArtists");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAndSeriesAuthor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("SeriesAuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SeriesAuthorId");

                    b.HasIndex("SeriesId");

                    b.ToTable("SeriesAndSeriesAuthors");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAndSeriesCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("SeriesCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SeriesCategoryId");

                    b.HasIndex("SeriesId");

                    b.ToTable("SeriesAndSeriesCategories");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesArtist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("SeriesArtists");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAuthor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("SeriesAuthors");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("SeriesCategories");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesEpisode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EditorUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EpisodeNo")
                        .HasColumnType("int");

                    b.Property<string>("FileSizeMb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOnAir")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StaticSeriesEpisodeTypeId")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TranslatorUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("ViewsCount")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EditorUserId");

                    b.HasIndex("SeriesId");

                    b.HasIndex("StaticSeriesEpisodeTypeId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TranslatorUserId");

                    b.ToTable("SeriesEpisodes");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesEpisodesPage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageNo")
                        .HasColumnType("int");

                    b.Property<Guid>("SeriesEpisodesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SeriesEpisodesId");

                    b.ToTable("SeriesEpisodesPages");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.StaticSeriesEpisodeType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaticSeriesEpisodeTypes");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.StaticSeriesStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaticSeriesStatus");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.StaticSeriesType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaticSeriesTypes");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BackgroundImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WebSiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.TeamAndAppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamsAndAppUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.Identity.RefreshToken", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", "AppUser")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.Series", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.StaticSeriesStatus", "StaticSeriesStatus")
                        .WithMany("Series")
                        .HasForeignKey("StaticSeriesStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.StaticSeriesType", "StaticSeriesTypes")
                        .WithMany("Series")
                        .HasForeignKey("StaticSeriesTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StaticSeriesStatus");

                    b.Navigation("StaticSeriesTypes");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAndSeriesArtist", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.SeriesArtist", "SeriesArtist")
                        .WithMany("SeriesAndSeriesArtists")
                        .HasForeignKey("SeriesArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.Series", "Series")
                        .WithMany("SeriesAndSeriesArtists")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");

                    b.Navigation("SeriesArtist");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAndSeriesAuthor", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.SeriesAuthor", "SeriesAuthor")
                        .WithMany("SeriesAndSeriesAuthors")
                        .HasForeignKey("SeriesAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.Series", "Series")
                        .WithMany("SeriesAndSeriesAuthor")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");

                    b.Navigation("SeriesAuthor");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAndSeriesCategory", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.SeriesCategory", "SeriesCategory")
                        .WithMany("SeriesAndSeriesCategories")
                        .HasForeignKey("SeriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.Series", "Series")
                        .WithMany("SeriesAndSeriesCategories")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");

                    b.Navigation("SeriesCategory");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesEpisode", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", "EditorUser")
                        .WithMany()
                        .HasForeignKey("EditorUserId");

                    b.HasOne("MangaFatihi.Domain.Entities.Series", "Series")
                        .WithMany("SeriesEpisodes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.StaticSeriesEpisodeType", "StaticSeriesEpisodeType")
                        .WithMany("SeriesEpisodes")
                        .HasForeignKey("StaticSeriesEpisodeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", "TranslatorUser")
                        .WithMany()
                        .HasForeignKey("TranslatorUserId");

                    b.Navigation("EditorUser");

                    b.Navigation("Series");

                    b.Navigation("StaticSeriesEpisodeType");

                    b.Navigation("Team");

                    b.Navigation("TranslatorUser");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesEpisodesPage", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.SeriesEpisode", "SeriesEpisodes")
                        .WithMany("SeriesEpisodesPages")
                        .HasForeignKey("SeriesEpisodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeriesEpisodes");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.TeamAndAppUser", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", "AppUser")
                        .WithMany("Teams")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.Team", "Team")
                        .WithMany("AppUser")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("MangaFatihi.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.AppUser", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.Series", b =>
                {
                    b.Navigation("SeriesAndSeriesArtists");

                    b.Navigation("SeriesAndSeriesAuthor");

                    b.Navigation("SeriesAndSeriesCategories");

                    b.Navigation("SeriesEpisodes");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesArtist", b =>
                {
                    b.Navigation("SeriesAndSeriesArtists");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesAuthor", b =>
                {
                    b.Navigation("SeriesAndSeriesAuthors");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesCategory", b =>
                {
                    b.Navigation("SeriesAndSeriesCategories");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.SeriesEpisode", b =>
                {
                    b.Navigation("SeriesEpisodesPages");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.StaticSeriesEpisodeType", b =>
                {
                    b.Navigation("SeriesEpisodes");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.StaticSeriesStatus", b =>
                {
                    b.Navigation("Series");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.StaticSeriesType", b =>
                {
                    b.Navigation("Series");
                });

            modelBuilder.Entity("MangaFatihi.Domain.Entities.Team", b =>
                {
                    b.Navigation("AppUser");
                });
#pragma warning restore 612, 618
        }
    }
}
