using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace demoprob.Entities2
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "material_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    defect_percentage = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("material_types_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    production_coefficient = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_types_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    supplier_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    supplier_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    inn = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("suppliers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    material_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    material_type_id = table.Column<int>(type: "integer", nullable: false),
                    cost_per_unit = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    current_stock = table.Column<decimal>(type: "numeric(15,4)", precision: 15, scale: 4, nullable: false),
                    min_stock_level = table.Column<decimal>(type: "numeric(15,4)", precision: 15, scale: 4, nullable: false),
                    package_quantity = table.Column<decimal>(type: "numeric(15,4)", precision: 15, scale: 4, nullable: false),
                    unit_of_measure = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("materials_pkey", x => x.id);
                    table.ForeignKey(
                        name: "materials_material_type_id_fkey",
                        column: x => x.material_type_id,
                        principalTable: "material_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    article_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    product_type_id = table.Column<int>(type: "integer", nullable: false),
                    product_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    min_partner_price = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    roll_width = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.article_number);
                    table.ForeignKey(
                        name: "products_product_type_id_fkey",
                        column: x => x.product_type_id,
                        principalTable: "product_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "material_suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    material_id = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("material_suppliers_pkey", x => x.id);
                    table.ForeignKey(
                        name: "material_suppliers_material_id_fkey",
                        column: x => x.material_id,
                        principalTable: "materials",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "material_suppliers_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_materials",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_article = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    material_id = table.Column<int>(type: "integer", nullable: false),
                    material_quantity = table.Column<decimal>(type: "numeric(15,4)", precision: 15, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_materials_pkey", x => x.id);
                    table.ForeignKey(
                        name: "product_materials_material_id_fkey",
                        column: x => x.material_id,
                        principalTable: "materials",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "product_materials_product_article_fkey",
                        column: x => x.product_article,
                        principalTable: "products",
                        principalColumn: "article_number");
                });

            migrationBuilder.CreateIndex(
                name: "idx_materials_suppliers_material",
                table: "material_suppliers",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "idx_materials_suppliers_supplier",
                table: "material_suppliers",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "material_suppliers_material_id_supplier_id_key",
                table: "material_suppliers",
                columns: new[] { "material_id", "supplier_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "material_types_type_name_key",
                table: "material_types",
                column: "type_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_materials_type",
                table: "materials",
                column: "material_type_id");

            migrationBuilder.CreateIndex(
                name: "materials_material_name_key",
                table: "materials",
                column: "material_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_product_materials_material",
                table: "product_materials",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "idx_product_materials_product",
                table: "product_materials",
                column: "product_article");

            migrationBuilder.CreateIndex(
                name: "product_materials_product_article_material_id_key",
                table: "product_materials",
                columns: new[] { "product_article", "material_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "product_types_type_name_key",
                table: "product_types",
                column: "type_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_products_type",
                table: "products",
                column: "product_type_id");

            migrationBuilder.CreateIndex(
                name: "suppliers_inn_key",
                table: "suppliers",
                column: "inn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "suppliers_supplier_name_key",
                table: "suppliers",
                column: "supplier_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "material_suppliers");

            migrationBuilder.DropTable(
                name: "product_materials");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "material_types");

            migrationBuilder.DropTable(
                name: "product_types");
        }
    }
}
