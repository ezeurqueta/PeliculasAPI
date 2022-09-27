using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PeliculasAPI.Migrations
{
    public partial class SalaDeCineData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SalasDeCine",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 4, "Cine Hoytz", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-64.205367 -31.41262)") });

            migrationBuilder.InsertData(
                table: "SalasDeCine",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 5, "Cine Dinosaurio Mall", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-64.220056 -31.366713)") });

            migrationBuilder.InsertData(
                table: "SalasDeCine",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[] { 6, "Cine Rivera Indarte", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-64.290905 -31.334729)") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
