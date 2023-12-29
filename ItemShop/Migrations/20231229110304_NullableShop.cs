using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemShop.Migrations
{
    /// <inheritdoc />
    public partial class NullableShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.AlterColumn<int>(
                    name: "ShopId",
                    table: "Items",
                    type: "integer",
                    nullable: true,
                    oldNullable: false,
                    defaultValue: 0);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
