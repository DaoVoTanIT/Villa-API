using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApi.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillasNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 54, 16, 186, DateTimeKind.Local).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 54, 16, 186, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 54, 16, 186, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 54, 16, 186, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 54, 16, 186, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumbers_VillaID",
                table: "VillasNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumbers_Villas_VillaID",
                table: "VillasNumbers",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumbers_Villas_VillaID",
                table: "VillasNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumbers_VillaID",
                table: "VillasNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillasNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 53, 20, 802, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 53, 20, 802, DateTimeKind.Local).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 53, 20, 802, DateTimeKind.Local).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 53, 20, 802, DateTimeKind.Local).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 13, 53, 20, 802, DateTimeKind.Local).AddTicks(4110));
        }
    }
}
