using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomsController.Migrations
{
    /// <inheritdoc />
    public partial class change_var_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isEUEU",
                table: "Countries",
                newName: "isEUCU");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isEUCU",
                table: "Countries",
                newName: "isEUEU");
        }
    }
}
