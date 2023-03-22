using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCash.Data.Migrations
{
    public partial class SecondMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRatesForUSD",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UZS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RUB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EUR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRatesForUSD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exposes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    WalletId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exposes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exposes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exposes_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    WalletId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8786), "I got my salary", "Salary", 0, null },
                    { 2L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8789), "gas", "bills", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8684), "komron2052@gmail.com", "Komron", "Rakhmonov", "12345678", null },
                    { 2L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8690), "ahmadboy@gmail.com", "Ahmad", "jurayev", "87654321", null },
                    { 3L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8691), "shaxmatboyboy@gmail.com", "Shaxmat", "Shashka", "54689135", null }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Amount", "CreatedAt", "Currency", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1L, 10000m, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8720), 1, "MyWallet", null, 2L });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Amount", "CreatedAt", "Currency", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2L, 50000m, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8722), 2, "MyWalletNomber2", null, 2L });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Amount", "CreatedAt", "Currency", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3L, 75000m, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8724), 2, "MyWallet", null, 1L });

            migrationBuilder.InsertData(
                table: "Exposes",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedAt", "Description", "UpdatedAt", "WalletId" },
                values: new object[,]
                {
                    { 1L, 1000m, 2L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8767), "Furniture", null, 1L },
                    { 2L, 2000m, 2L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8769), "Salary", null, 2L },
                    { 3L, 2000m, 2L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8770), "Price", null, 2L }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedAt", "Description", "UpdatedAt", "WalletId" },
                values: new object[,]
                {
                    { 1L, 1000m, 1L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8745), "Salary", null, 1L },
                    { 2L, 2000m, 1L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8747), "Salary", null, 2L },
                    { 3L, 2000m, 1L, new DateTime(2023, 3, 22, 15, 54, 51, 890, DateTimeKind.Local).AddTicks(8749), "Price", null, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exposes_CategoryId",
                table: "Exposes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Exposes_WalletId",
                table: "Exposes",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_CategoryId",
                table: "Incomes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_WalletId",
                table: "Incomes",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRatesForUSD");

            migrationBuilder.DropTable(
                name: "Exposes");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
