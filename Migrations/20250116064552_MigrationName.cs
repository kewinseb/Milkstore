using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilkStore.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    productImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    stockQuantity = table.Column<int>(type: "int", nullable: false),
                    estimatedDelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__2D10D16ACCDF16A4", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    emailId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    state = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    pinCode = table.Column<int>(type: "int", nullable: true),
                    phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "user"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__87355E72966BE19D", x => x.emailId);
                });

            migrationBuilder.CreateTable(
                name: "CardDetails",
                columns: table => new
                {
                    cardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_emailId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    cardNumber = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    cvv = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CardDeta__4D5BC491844A3D3D", x => x.cardId);
                    table.ForeignKey(
                        name: "FK__CardDetai__User___534D60F1",
                        column: x => x.User_emailId,
                        principalTable: "Users",
                        principalColumn: "emailId");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_emailId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Product_productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__415B03B88746E28E", x => x.cartId);
                    table.ForeignKey(
                        name: "FK__Cart__Product_pr__4316F928",
                        column: x => x.Product_productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "FK__Cart__User_email__4222D4EF",
                        column: x => x.User_emailId,
                        principalTable: "Users",
                        principalColumn: "emailId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_emailId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingNumber = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__0809335DA27F7321", x => x.orderId);
                    table.ForeignKey(
                        name: "FK__Orders__User_ema__47DBAE45",
                        column: x => x.User_emailId,
                        principalTable: "Users",
                        principalColumn: "emailId");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_emailId = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    transactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totalAmountPaid = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    paymentMode = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    paymentStatus = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__9B57CF7229E9D5AC", x => x.transactionId);
                    table.ForeignKey(
                        name: "FK__Transacti__User___5070F446",
                        column: x => x.User_emailId,
                        principalTable: "Users",
                        principalColumn: "emailId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    orderDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orders_orderId = table.Column<int>(type: "int", nullable: false),
                    Product_productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    priceAtOrder = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    trackingNumber = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__5EEE527321D87DB5", x => x.orderDetailsId);
                    table.ForeignKey(
                        name: "FK__OrderItem__Order__4CA06362",
                        column: x => x.Orders_orderId,
                        principalTable: "Orders",
                        principalColumn: "orderId");
                    table.ForeignKey(
                        name: "FK__OrderItem__Produ__4D94879B",
                        column: x => x.Product_productId,
                        principalTable: "Product",
                        principalColumn: "productId");
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDetails",
                columns: table => new
                {
                    shipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shippingAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    deliveryStatus = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    deliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Orders_orderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shipment__472178019705953B", x => x.shipmentId);
                    table.ForeignKey(
                        name: "FK__ShipmentD__Order__5629CD9C",
                        column: x => x.Orders_orderId,
                        principalTable: "Orders",
                        principalColumn: "orderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardDetails_User_emailId",
                table: "CardDetails",
                column: "User_emailId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Product_productId",
                table: "Cart",
                column: "Product_productId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_User_emailId",
                table: "Cart",
                column: "User_emailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Orders_orderId",
                table: "OrderItems",
                column: "Orders_orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Product_productId",
                table: "OrderItems",
                column: "Product_productId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_User_emailId",
                table: "Orders",
                column: "User_emailId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_Orders_orderId",
                table: "ShipmentDetails",
                column: "Orders_orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_User_emailId",
                table: "Transactions",
                column: "User_emailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDetails");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ShipmentDetails");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
