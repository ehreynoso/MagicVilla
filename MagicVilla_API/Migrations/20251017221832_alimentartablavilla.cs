using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class alimentartablavilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenURL", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalles de la Villa ...", new DateTime(2025, 10, 17, 18, 18, 32, 556, DateTimeKind.Local).AddTicks(9517), new DateTime(2025, 10, 17, 18, 18, 32, 556, DateTimeKind.Local).AddTicks(9456), "", 50, "Villa real", 5, 200.0 },
                    { 2, "", "Detalles de la Villa ...", new DateTime(2025, 10, 17, 18, 18, 32, 556, DateTimeKind.Local).AddTicks(9525), new DateTime(2025, 10, 17, 18, 18, 32, 556, DateTimeKind.Local).AddTicks(9522), "", 40, "Premium Vista a la piscina", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
