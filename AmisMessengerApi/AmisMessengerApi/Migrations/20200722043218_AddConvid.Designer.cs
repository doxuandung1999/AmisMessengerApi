﻿// <auto-generated />
using System;
using AmisMessengerApi.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AmisMessengerApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200722043218_AddConvid")]
    partial class AddConvid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AmisMessengerApi.Entities.File", b =>
                {
                    b.Property<Guid>("fileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("convId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("fileName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("filePath")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("fileType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("fileId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("AmisMessengerApi.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserAvatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserEmail")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
