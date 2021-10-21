using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class addMaxDepositeWithdrawAppSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "Wallets",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxDeposit",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxWithDraw",
                table: "AppSettings",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MaxDeposit",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "MaxWithDraw",
                table: "AppSettings");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Wallets",
                type: "float",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
