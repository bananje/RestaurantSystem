using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    CourierId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CurrentDeliveringOrder = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.CourierId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    OrdersCount = table.Column<int>(type: "integer", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ProductImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BaseDiscount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    MaxDiscount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PriceWithDiscount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    WeightUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentStatus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PaymentStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsCourierAssigned = table.Column<bool>(type: "boolean", nullable: false),
                    CourierId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    ClosedWithStatus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OrderStatusChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CourierInfoCourierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "CourierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Couriers_CourierInfoCourierId",
                        column: x => x.CourierInfoCourierId,
                        principalTable: "Couriers",
                        principalColumn: "CourierId");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    OrderLineId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ReadyStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.OrderLineId);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ProductId",
                table: "OrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourierId",
                table: "Orders",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourierInfoCourierId",
                table: "Orders",
                column: "CourierInfoCourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            // Вставка данных в таблицу Couriers
            var courierIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            migrationBuilder.InsertData(
                table: "Couriers",
                columns: new[] { "CourierId", "FirstName", "MiddleName", "LastName", "Email", "Phone", "Status", "CurrentDeliveringOrder", "Version" },
                values: new object[,]
                {
        { courierIds[0], "John", "A.", "Doe", "john.doe@example.com", "1234567890", "Active", Guid.NewGuid(), 1L },
        { courierIds[1], "Jane", "B.", "Doe", "jane.doe@example.com", "0987654321", "Active", Guid.NewGuid(), 1L },
        { courierIds[2], "Jim", "C.", "Beam", "jim.beam@example.com", "5555555555", "Inactive", Guid.NewGuid(), 1L },
        { courierIds[3], "Jack", "D.", "Daniels", "jack.daniels@example.com", "4444444444", "Active", Guid.NewGuid(), 1L },
        { courierIds[4], "Jill", "D.", "Valentine", "jill.valentine@example.com", "3333333333", "Inactive", Guid.NewGuid(), 1L }
                });

            // Вставка данных в таблицу Customers
            var customerIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "FirstName", "MiddleName", "LastName", "Email", "Phone", "OrdersCount", "DeliveryAddress", "Version" },
                values: new object[,]
                {
        { customerIds[0], "Alice", "M.", "Johnson", "alice.johnson@example.com", "2222222222", 5, "123 Main St", 1L },
        { customerIds[1], "Bob", "N.", "Brown", "bob.brown@example.com", "1111111111", 3, "456 Elm St", 1L },
        { customerIds[2], "Charlie", "O.", "Davis", "charlie.davis@example.com", "6666666666", 7, "789 Oak St", 1L },
        { customerIds[3], "Dave", "P.", "Miller", "dave.miller@example.com", "7777777777", 2, "101 Pine St", 1L },
        { customerIds[4], "Eve", "Q.", "Wilson", "eve.wilson@example.com", "8888888888", 4, "202 Birch St", 1L }
                });

            // Вставка данных в таблицу Products
            var productIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Title", "Description", "ProductImageUrl", "Status", "ShortDescription", "Price", "Discount", "BaseDiscount", "MaxDiscount", "PriceWithDiscount", "Weight", "WeightUnit" },
                values: new object[,]
                {
        { productIds[0], "ProductStruct 1", "Description 1", "http://example.com/product1.jpg", "Available", "Short Description 1", 100m, 10m, 5m, 15m, 90m, 1.0, "kg" },
        { productIds[1], "ProductStruct 2", "Description 2", "http://example.com/product2.jpg", "Available", "Short Description 2", 200m, 20m, 10m, 25m, 180m, 1.5, "kg" },
        { productIds[2], "ProductStruct 3", "Description 3", "http://example.com/product3.jpg", "Out of Stock", "Short Description 3", 300m, 30m, 15m, 35m, 270m, 2.0, "kg" },
        { productIds[3], "ProductStruct 4", "Description 4", "http://example.com/product4.jpg", "Available", "Short Description 4", 400m, 40m, 20m, 45m, 360m, 2.5, "kg" },
        { productIds[4], "ProductStruct 5", "Description 5", "http://example.com/product5.jpg", "Available", "Short Description 5", 500m, 50m, 25m, 55m, 450m, 3.0, "kg" }
                });

            // Вставка данных в таблицу Orders
            var orderIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CurrentStatus", "PaymentStatus", "IsCourierAssigned", "CourierId", "CustomerId", "TotalPrice", "DeliveryAddress", "IsClosed", "ClosedWithStatus", "OrderStatusChangedAt", "Version", "CourierInfoCourierId" },
                values: new object[,]
                {
        { orderIds[0], "Processing", "Paid", true, courierIds[0], customerIds[0], 150m, "123 Main St", false, "", DateTime.UtcNow, 1L, null },
        { orderIds[1], "Shipped", "Paid", true, courierIds[1], customerIds[1], 250m, "456 Elm St", false, "", DateTime.UtcNow, 1L, null },
        { orderIds[2], "Delivered", "Paid", true, courierIds[2], customerIds[2], 350m, "789 Oak St", true, "Completed", DateTime.UtcNow, 1L, null },
        { orderIds[3], "Cancelled", "Refunded", false, courierIds[3], customerIds[3], 450m, "101 Pine St", true, "Cancelled", DateTime.UtcNow, 1L, null },
        { orderIds[4], "Pending", "Pending", false, courierIds[4], customerIds[4], 550m, "202 Birch St", false, "", DateTime.UtcNow, 1L, null }
                });

            // Вставка данных в таблицу OrderLines
            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "OrderLineId", "OrderId", "ProductId", "Quantity", "Price", "Discount", "ReadyStatus" },
                values: new object[,]
                {
        { Guid.NewGuid(), orderIds[0], productIds[0], 2, 100m, 10m, "Ready" },
        { Guid.NewGuid(), orderIds[1], productIds[1], 1, 200m, 20m, "Not Ready" },
        { Guid.NewGuid(), orderIds[2], productIds[2], 4, 150m, 15m, "Ready" },
        { Guid.NewGuid(), orderIds[3], productIds[3], 3, 300m, 30m, "Ready" },
        { Guid.NewGuid(), orderIds[4], productIds[4], 5, 250m, 25m, "Not Ready" }
                });
        
    }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
