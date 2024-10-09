using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMissedDbSetApplicationUserMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_Movies_MovieId",
                table: "ApplicationUserMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserMovie",
                table: "ApplicationUserMovie");

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("1820001f-68fd-47a8-999a-bfdafbfce155"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("9d8213e7-72a7-4da6-b688-968330e1ec70"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("a49fc398-e209-49eb-b118-48f7ef7125a5"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("02919429-0b71-417f-a4bb-8ad758237bcf"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("101eaeb1-68d5-48e7-90fc-1cd2737b1c5a"));

            migrationBuilder.RenameTable(
                name: "ApplicationUserMovie",
                newName: "UsersMovies");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserMovie_MovieId",
                table: "UsersMovies",
                newName: "IX_UsersMovies_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies",
                columns: new[] { "ApplicationUserId", "MovieId" });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("1ffa06e3-b259-48dd-a4c0-4509599db8ef"), "Sofia", "Cinema city" },
                    { new Guid("5951eb22-2b75-4788-ba94-12cd63d6cf1c"), "Plovdiv", "Cinema city" },
                    { new Guid("97ffaa13-9276-4688-85bf-9d0f52407ebc"), "Varna", "Cinemax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("13045f2d-d369-477c-b441-e7d74da4726b"), "The Lord of the Rings: The Fellowship of the Ring is a 2001 epic high fantasy adventure film directed by Peter Jackson from a screenplay by Fran Walsh, Philippa Boyens, and Jackson, based on 1954's The Fellowship of the Ring, the first volume of the novel The Lord of the Rings by J. R. R. Tolkien. ", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" },
                    { new Guid("7fa03c77-18e6-47c6-957d-b0cbb0ae0283"), "Harry Potter and the Goblet of Fire is a 2005 fantasy film directed by Mike Newell from a screenplay by Steve Kloves. It is based on the 2000 novel Harry Potter and the Goblet of Fire by J. K. Rowling.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_AspNetUsers_ApplicationUserId",
                table: "UsersMovies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_Movies_MovieId",
                table: "UsersMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_AspNetUsers_ApplicationUserId",
                table: "UsersMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_Movies_MovieId",
                table: "UsersMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies");

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("1ffa06e3-b259-48dd-a4c0-4509599db8ef"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("5951eb22-2b75-4788-ba94-12cd63d6cf1c"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("97ffaa13-9276-4688-85bf-9d0f52407ebc"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("13045f2d-d369-477c-b441-e7d74da4726b"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("7fa03c77-18e6-47c6-957d-b0cbb0ae0283"));

            migrationBuilder.RenameTable(
                name: "UsersMovies",
                newName: "ApplicationUserMovie");

            migrationBuilder.RenameIndex(
                name: "IX_UsersMovies_MovieId",
                table: "ApplicationUserMovie",
                newName: "IX_ApplicationUserMovie_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserMovie",
                table: "ApplicationUserMovie",
                columns: new[] { "ApplicationUserId", "MovieId" });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("1820001f-68fd-47a8-999a-bfdafbfce155"), "Varna", "Cinemax" },
                    { new Guid("9d8213e7-72a7-4da6-b688-968330e1ec70"), "Plovdiv", "Cinema city" },
                    { new Guid("a49fc398-e209-49eb-b118-48f7ef7125a5"), "Sofia", "Cinema city" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("02919429-0b71-417f-a4bb-8ad758237bcf"), "Harry Potter and the Goblet of Fire is a 2005 fantasy film directed by Mike Newell from a screenplay by Steve Kloves. It is based on the 2000 novel Harry Potter and the Goblet of Fire by J. K. Rowling.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" },
                    { new Guid("101eaeb1-68d5-48e7-90fc-1cd2737b1c5a"), "The Lord of the Rings: The Fellowship of the Ring is a 2001 epic high fantasy adventure film directed by Peter Jackson from a screenplay by Fran Walsh, Philippa Boyens, and Jackson, based on 1954's The Fellowship of the Ring, the first volume of the novel The Lord of the Rings by J. R. R. Tolkien. ", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMovie",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_Movies_MovieId",
                table: "ApplicationUserMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
