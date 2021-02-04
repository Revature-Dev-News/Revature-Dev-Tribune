using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreApi.Service.Migrations
{
    public partial class migration_0_setup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    AuthorEntityId = table.Column<long>(type: "bigint", nullable: true),
                    TopicEntityId = table.Column<long>(type: "bigint", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_Articles_Authors_AuthorEntityId",
                        column: x => x.AuthorEntityId,
                        principalTable: "Authors",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_Topics_TopicEntityId",
                        column: x => x.TopicEntityId,
                        principalTable: "Topics",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReaderTopics",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaderEntityId = table.Column<long>(type: "bigint", nullable: true),
                    TopicEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaderTopics", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_ReaderTopics_Readers_ReaderEntityId",
                        column: x => x.ReaderEntityId,
                        principalTable: "Readers",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReaderTopics_Topics_TopicEntityId",
                        column: x => x.TopicEntityId,
                        principalTable: "Topics",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "EntityId", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1L, "cc@aol.com", "Chedro Cardenas", "12345" },
                    { 2L, "el@aol.com", "Elliott Lockwood", "12345" },
                    { 3L, "jg@aol.com", "Joshwin Greene", "12345" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "EntityId", "Name" },
                values: new object[,]
                {
                    { 1L, "Startups" },
                    { 2L, "DevOps" },
                    { 3L, "Testing" },
                    { 4L, "Big Data" },
                    { 5L, "Machine Learning" },
                    { 6L, "FAANG" },
                    { 7L, "Languages" },
                    { 8L, "Hacker News" },
                    { 9L, "Databases" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorEntityId",
                table: "Articles",
                column: "AuthorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TopicEntityId",
                table: "Articles",
                column: "TopicEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReaderTopics_ReaderEntityId",
                table: "ReaderTopics",
                column: "ReaderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReaderTopics_TopicEntityId",
                table: "ReaderTopics",
                column: "TopicEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ReaderTopics");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
