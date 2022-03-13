using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FrameworksAndDrivers.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payment");

            migrationBuilder.CreateTable(
                name: "credit-card-status",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credit-card-status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "merchant",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bankaccountdetails = table.Column<string>(name: "bank-account-details", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment-status",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment-status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "credit-card",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    holder_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    holder_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    expiration_month = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    expiration_year = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    cvv = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    status_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credit-card", x => x.id);
                    table.ForeignKey(
                        name: "FK_credit-card_credit-card-status_status_id",
                        column: x => x.status_id,
                        principalSchema: "payment",
                        principalTable: "credit-card-status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                schema: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    merchant_id = table.Column<long>(type: "bigint", maxLength: 255, nullable: false),
                    card_id = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    sale_description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    status_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_payment_credit-card_card_id",
                        column: x => x.card_id,
                        principalSchema: "payment",
                        principalTable: "credit-card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_merchant_merchant_id",
                        column: x => x.merchant_id,
                        principalSchema: "payment",
                        principalTable: "merchant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_payment-status_status_id",
                        column: x => x.status_id,
                        principalSchema: "payment",
                        principalTable: "payment-status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "payment",
                table: "credit-card-status",
                columns: new[] { "id", "status" },
                values: new object[] { (short)1, "Verified" });

            migrationBuilder.InsertData(
                schema: "payment",
                table: "merchant",
                columns: new[] { "id", "bank-account-details" },
                values: new object[] { 1L, "BankAccountDetails" });

            migrationBuilder.InsertData(
                schema: "payment",
                table: "payment-status",
                columns: new[] { "id", "status" },
                values: new object[,]
                {
                    { (short)1, "Received" },
                    { (short)2, "Queued" },
                    { (short)3, "PaymentCommited" },
                    { (short)4, "PaymentRejected" }
                });

            migrationBuilder.InsertData(
                schema: "payment",
                table: "credit-card",
                columns: new[] { "id", "cvv", "expiration_month", "expiration_year", "holder_address", "holder_name", "number", "status_id" },
                values: new object[] { 1L, "323", "12", "2025", "Heroic St. 195", "Antônio J. Penteado", "379354508162306", (short)1 });

            migrationBuilder.InsertData(
                schema: "payment",
                table: "payment",
                columns: new[] { "id", "amount", "card_id", "currency", "merchant_id", "sale_description", "status_id" },
                values: new object[] { new Guid("fc782e65-0117-4c5e-b6d0-afa845effa3e"), 15.99m, 1L, "EUR", 1L, "Final soccer match", (short)1 });

            migrationBuilder.CreateIndex(
                name: "IX_credit-card_status_id",
                schema: "payment",
                table: "credit-card",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_card_id",
                schema: "payment",
                table: "payment",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_merchant_id",
                schema: "payment",
                table: "payment",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_status_id",
                schema: "payment",
                table: "payment",
                column: "status_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "credit-card",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "merchant",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "payment-status",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "credit-card-status",
                schema: "payment");
        }
    }
}
