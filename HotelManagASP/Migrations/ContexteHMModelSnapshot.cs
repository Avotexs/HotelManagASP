﻿// <auto-generated />
using System;
using HotelManagASP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelManagASP.Migrations
{
    [DbContext(typeof(ContexteHM))]
    partial class ContexteHMModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelManagASP.Models.Chambre", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Capacite")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prix")
                        .HasColumnType("float");

                    b.Property<int>("numero")
                        .HasColumnType("int");

                    b.Property<string>("type_Chambre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Chambres");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Capacite = 1,
                            Description = "Chambre Simple",
                            ImageUrl = "https://www.hotelarmoniparis.com/_novaimg/galleria/1535876.jpg",
                            Prix = 100.0,
                            numero = 1,
                            type_Chambre = "Simple"
                        });
                });

            modelBuilder.Entity("HotelManagASP.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Adresee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateRejoin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tele")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Adresee = "Rue 1",
                            CIN = "A",
                            DateRejoin = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            MotDePasse = "123456",
                            Nom = "Hassan",
                            Prenom = "Hassani",
                            Tele = 612345678
                        });
                });

            modelBuilder.Entity("HotelManagASP.Models.Reservation", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ChambreId")
                        .HasColumnType("int");

                    b.Property<int?>("ReservationChambreId")
                        .HasColumnType("int");

                    b.Property<int?>("ReservationClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateArrive")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateSortie")
                        .HasColumnType("datetime2");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<double>("prixtotal")
                        .HasColumnType("float");

                    b.Property<string>("statut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId", "ChambreId");

                    b.HasIndex("ChambreId");

                    b.HasIndex("ReservationClientId", "ReservationChambreId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            ChambreId = 1,
                            dateArrive = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            dateSortie = new DateTime(2021, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            id = 1,
                            prixtotal = 100.0,
                            statut = "En cours"
                        });
                });

            modelBuilder.Entity("HotelManagASP.Models.Reservation", b =>
                {
                    b.HasOne("HotelManagASP.Models.Chambre", "Chambre")
                        .WithMany("Reservations")
                        .HasForeignKey("ChambreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelManagASP.Models.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelManagASP.Models.Reservation", null)
                        .WithMany("Reservations")
                        .HasForeignKey("ReservationClientId", "ReservationChambreId");

                    b.Navigation("Chambre");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("HotelManagASP.Models.Chambre", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelManagASP.Models.Client", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("HotelManagASP.Models.Reservation", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
