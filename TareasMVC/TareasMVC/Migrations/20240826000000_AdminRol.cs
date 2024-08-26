using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF NOT EXISTS(Select Id FROM AspNetRoles WHERE Id = '8464827d-2afe-471b-9e7e-b3b393016402')
                BEGIN
	            INSERT AspNetRoles (Id, [Name], [NormalizedName])
	            VALUES ('8464827d-2afe-471b-9e7e-b3b393016402', 'admin', 'ADMIN')
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AspNetRoles WHERE Id = '8464827d-2afe-471b-9e7e-b3b393016402'");
        }
    }
}
