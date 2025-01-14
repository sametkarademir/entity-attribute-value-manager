using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SH.EntityAttributeValue.Manager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    IsMultiple = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppAttributeOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAttributeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAttributeOptions_AppAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "AppAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCategoryAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategoryAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCategoryAttributes_AppAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "AppAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCategoryAttributes_AppCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AppCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProducts_AppCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AppCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    AsString = table.Column<string>(type: "text", nullable: true),
                    AsBoolean = table.Column<bool>(type: "boolean", nullable: true),
                    AsDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AsInteger = table.Column<int>(type: "integer", nullable: true),
                    AsDecimal = table.Column<decimal>(type: "numeric", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppValues_AppAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "AppAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppValues_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAttributeOptions_AttributeId",
                table: "AppAttributeOptions",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryAttributes_AttributeId",
                table: "AppCategoryAttributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryAttributes_CategoryId",
                table: "AppCategoryAttributes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_CategoryId",
                table: "AppProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppValues_AttributeId",
                table: "AppValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppValues_ProductId",
                table: "AppValues",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAttributeOptions");

            migrationBuilder.DropTable(
                name: "AppCategoryAttributes");

            migrationBuilder.DropTable(
                name: "AppValues");

            migrationBuilder.DropTable(
                name: "AppAttributes");

            migrationBuilder.DropTable(
                name: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppCategories");
        }
    }
}
