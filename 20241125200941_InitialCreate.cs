using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KuaforYonetim.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salon",
                columns: table => new
                {
                    SalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salon", x => x.SalonId);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uzmanlik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslangicSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.CalisanId);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Salon_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salon",
                        principalColumn: "SalonId");
                });

            migrationBuilder.CreateTable(
                name: "Hizmetler",
                columns: table => new
                {
                    HizmetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sure = table.Column<double>(type: "float", nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    SalonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetler", x => x.HizmetId);
                    table.ForeignKey(
                        name: "FK_Hizmetler_Salon_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salon",
                        principalColumn: "SalonId");
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaslangicSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ucret = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    CalisanId = table.Column<int>(type: "int", nullable: true),
                    HizmetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuId);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId");
                    table.ForeignKey(
                        name: "FK_Randevular_Hizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmetler",
                        principalColumn: "HizmetId");
                });

            migrationBuilder.InsertData(
                table: "Calisanlar",
                columns: new[] { "CalisanId", "AdSoyad", "BaslangicSaati", "BitisSaati", "SalonId", "Uzmanlik" },
                values: new object[,]
                {
                    { 1, "Ayşe Yılmaz", null, null, null, "Saç Kesimi" },
                    { 2, "Mehmet Can", null, null, null, "Boyama" },
                    { 3, "Elif Demir", null, null, null, "Manikür" }
                });

            migrationBuilder.InsertData(
                table: "Hizmetler",
                columns: new[] { "HizmetId", "HizmetAdi", "SalonId", "Sure", "Ucret" },
                values: new object[,]
                {
                    { 1, "Kısa Saç Kesimi", null, 30.0, 100m },
                    { 2, "Saç Boyama", null, 90.0, 200m },
                    { 3, "Manikür", null, 45.0, 150m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisanlar",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_SalonId",
                table: "Hizmetler",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HizmetId",
                table: "Randevular",
                column: "HizmetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Hizmetler");

            migrationBuilder.DropTable(
                name: "Salon");
        }
    }
}
