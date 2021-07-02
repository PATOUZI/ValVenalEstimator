﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ValVenalEstimator.Api.Data;

namespace ValVenalEstimator.Api.Migrations
{
    [DbContext(typeof(ValVenalEstimatorDbContext))]
    [Migration("20210701104039_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("ValVenalEstimator.Api.Models.Place", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("ZoneId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ZoneId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("ValVenalEstimator.Api.Models.Prefecture", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Prefectures");
                });

            modelBuilder.Entity("ValVenalEstimator.Api.Models.Zone", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("PrefectureId")
                        .HasColumnType("bigint");

                    b.Property<double>("PricePerMeterSquare")
                        .HasColumnType("double");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("ZoneNum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrefectureId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("ValVenalEstimator.Api.Models.Place", b =>
                {
                    b.HasOne("ValVenalEstimator.Api.Models.Zone", "Zone")
                        .WithMany("Places")
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("ValVenalEstimator.Api.Models.Zone", b =>
                {
                    b.HasOne("ValVenalEstimator.Api.Models.Prefecture", "Prefecture")
                        .WithMany("Zones")
                        .HasForeignKey("PrefectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prefecture");
                });

            modelBuilder.Entity("ValVenalEstimator.Api.Models.Prefecture", b =>
                {
                    b.Navigation("Zones");
                });

            modelBuilder.Entity("ValVenalEstimator.Api.Models.Zone", b =>
                {
                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}
