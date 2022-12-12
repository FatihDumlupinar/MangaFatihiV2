using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaFatihi.Persistence.Migrations
{
    public partial class SeriesEpisode_AddTeamId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeriesEpisodes_StaticSeriesEpisodeTypes_StaticSeriesEpisodeTypesId",
                table: "SeriesEpisodes");

            migrationBuilder.RenameColumn(
                name: "StaticSeriesEpisodeTypesId",
                table: "SeriesEpisodes",
                newName: "StaticSeriesEpisodeTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SeriesEpisodes_StaticSeriesEpisodeTypesId",
                table: "SeriesEpisodes",
                newName: "IX_SeriesEpisodes_StaticSeriesEpisodeTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "SeriesEpisodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeriesEpisodes_TeamId",
                table: "SeriesEpisodes",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesEpisodes_StaticSeriesEpisodeTypes_StaticSeriesEpisodeTypeId",
                table: "SeriesEpisodes",
                column: "StaticSeriesEpisodeTypeId",
                principalTable: "StaticSeriesEpisodeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesEpisodes_Teams_TeamId",
                table: "SeriesEpisodes",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeriesEpisodes_StaticSeriesEpisodeTypes_StaticSeriesEpisodeTypeId",
                table: "SeriesEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SeriesEpisodes_Teams_TeamId",
                table: "SeriesEpisodes");

            migrationBuilder.DropIndex(
                name: "IX_SeriesEpisodes_TeamId",
                table: "SeriesEpisodes");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "SeriesEpisodes");

            migrationBuilder.RenameColumn(
                name: "StaticSeriesEpisodeTypeId",
                table: "SeriesEpisodes",
                newName: "StaticSeriesEpisodeTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_SeriesEpisodes_StaticSeriesEpisodeTypeId",
                table: "SeriesEpisodes",
                newName: "IX_SeriesEpisodes_StaticSeriesEpisodeTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesEpisodes_StaticSeriesEpisodeTypes_StaticSeriesEpisodeTypesId",
                table: "SeriesEpisodes",
                column: "StaticSeriesEpisodeTypesId",
                principalTable: "StaticSeriesEpisodeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
