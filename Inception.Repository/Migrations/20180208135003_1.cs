using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Inception.Repository.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteTestOverviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstTestedOn = table.Column<DateTime>(nullable: false),
                    LastTestedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTestOverviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DomainName = table.Column<string>(nullable: true),
                    TestedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTestResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkTestOverviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaximumResponseTime = table.Column<int>(nullable: false),
                    MinimumResponseTime = table.Column<int>(nullable: false),
                    SiteTestOverviewId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkTestOverviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkTestOverviews_SiteTestOverviews_SiteTestOverviewId",
                        column: x => x.SiteTestOverviewId,
                        principalTable: "SiteTestOverviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResponseTime = table.Column<int>(nullable: false),
                    SiteTestResultId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkTestResults_SiteTestResults_SiteTestResultId",
                        column: x => x.SiteTestResultId,
                        principalTable: "SiteTestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkTestOverviews_SiteTestOverviewId",
                table: "LinkTestOverviews",
                column: "SiteTestOverviewId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkTestResults_SiteTestResultId",
                table: "LinkTestResults",
                column: "SiteTestResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkTestOverviews");

            migrationBuilder.DropTable(
                name: "LinkTestResults");

            migrationBuilder.DropTable(
                name: "SiteTestOverviews");

            migrationBuilder.DropTable(
                name: "SiteTestResults");
        }
    }
}
