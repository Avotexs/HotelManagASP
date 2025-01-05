using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagASP.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chambres",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_Chambre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numero = table.Column<int>(type: "int", nullable: false),
                    Capacite = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chambres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tele = table.Column<int>(type: "int", nullable: false),
                    DateRejoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ChambreId = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    dateArrive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateSortie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prixtotal = table.Column<double>(type: "float", nullable: false),
                    statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationChambreId = table.Column<int>(type: "int", nullable: true),
                    ReservationClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => new { x.ClientId, x.ChambreId });
                    table.ForeignKey(
                        name: "FK_Reservations_Chambres_ChambreId",
                        column: x => x.ChambreId,
                        principalTable: "Chambres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Reservations_ReservationClientId_ReservationChambreId",
                        columns: x => new { x.ReservationClientId, x.ReservationChambreId },
                        principalTable: "Reservations",
                        principalColumns: new[] { "ClientId", "ChambreId" });
                });

            migrationBuilder.InsertData(
                table: "Chambres",
                columns: new[] { "id", "Capacite", "Description", "ImageUrl", "Prix", "numero", "type_Chambre" },
                values: new object[] { 1, 1, "Chambre Simple", "https://www.hotelarmoniparis.com/_novaimg/galleria/1535876.jpg", 100.0, 1, "Simple" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "id", "Adresee", "CIN", "DateRejoin", "Email", "MotDePasse", "Nom", "Prenom", "Tele" },
                values: new object[] { 1, "Rue 1", "A", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "123456", "Hassan", "Hassani", 612345678 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ChambreId", "ClientId", "ReservationChambreId", "ReservationClientId", "dateArrive", "dateSortie", "id", "prixtotal", "statut" },
                values: new object[] { 1, 1, null, null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100.0, "En cours" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ChambreId",
                table: "Reservations",
                column: "ChambreId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationClientId_ReservationChambreId",
                table: "Reservations",
                columns: new[] { "ReservationClientId", "ReservationChambreId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Chambres");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
