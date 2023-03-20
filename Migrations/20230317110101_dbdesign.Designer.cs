﻿// <auto-generated />
using CSV_FileReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSV_FileReader.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230317110101_dbdesign")]
    partial class dbdesign
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CSV_FileReader.Models.Manufacturer", b =>
                {
                    b.Property<string>("ManufacturerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManufacturerID");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("CSV_FileReader.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegistrationID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("ManufacturerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RegistrationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RegistrationID");

                    b.HasIndex("ManufacturerID");

                    b.HasIndex("TimeID");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("CSV_FileReader.Models.Time", b =>
                {
                    b.Property<string>("TimeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("TimeID");

                    b.ToTable("Times");
                });

            modelBuilder.Entity("CSV_FileReader.Models.Registration", b =>
                {
                    b.HasOne("CSV_FileReader.Models.Manufacturer", "Manufacturer")
                        .WithMany("Registration")
                        .HasForeignKey("ManufacturerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSV_FileReader.Models.Time", "Time")
                        .WithMany("Registration")
                        .HasForeignKey("TimeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("Time");
                });

            modelBuilder.Entity("CSV_FileReader.Models.Manufacturer", b =>
                {
                    b.Navigation("Registration");
                });

            modelBuilder.Entity("CSV_FileReader.Models.Time", b =>
                {
                    b.Navigation("Registration");
                });
#pragma warning restore 612, 618
        }
    }
}
