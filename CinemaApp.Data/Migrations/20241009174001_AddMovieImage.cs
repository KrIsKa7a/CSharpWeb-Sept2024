using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("1e7cd06d-4624-450f-936e-85224e00afd5"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("66475cb0-ab9e-4296-b54d-f532c2f258d1"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("80a7eecb-710a-416d-91ca-ad7d6d806a87"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("71970c1a-0539-47f3-8ae8-14b6ec72db62"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("c6659795-8bd2-45a6-ab25-f06236ee97db"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                defaultValue: "~/images/no-image.jpg");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("1e7cd06d-4624-450f-936e-85224e00afd5"), "Plovdiv", "Cinema city" },
                    { new Guid("66475cb0-ab9e-4296-b54d-f532c2f258d1"), "Sofia", "Cinema city" },
                    { new Guid("80a7eecb-710a-416d-91ca-ad7d6d806a87"), "Varna", "Cinemax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("71970c1a-0539-47f3-8ae8-14b6ec72db62"), "The Lord of the Rings: The Fellowship of the Ring is a 2001 epic high fantasy adventure film directed by Peter Jackson from a screenplay by Fran Walsh, Philippa Boyens, and Jackson, based on 1954's The Fellowship of the Ring, the first volume of the novel The Lord of the Rings by J. R. R. Tolkien. ", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" },
                    { new Guid("c6659795-8bd2-45a6-ab25-f06236ee97db"), "Harry Potter and the Goblet of Fire is a 2005 fantasy film directed by Mike Newell from a screenplay by Steve Kloves. It is based on the 2000 novel Harry Potter and the Goblet of Fire by J. K. Rowling.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" }
                });
        }
    }
}
