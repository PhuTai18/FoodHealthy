using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodHealthy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionCat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageCategories = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A2B23BC690D", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesIngredients",
                columns: table => new
                {
                    CategoriesIngredientsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionCat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageCategories = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__5917E4CCA5D1DF67", x => x.CategoriesIngredientsID);
                });

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    ComboID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComboName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionCombo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ImageCombo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Combos__DD42580E43A2F6F4", x => x.ComboID);
                });

            migrationBuilder.CreateTable(
                name: "CourierServices",
                columns: table => new
                {
                    CourierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BaseFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FeePerKm = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    EstimatedTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CoverageArea = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourierS__CDAE76F61022AF67", x => x.CourierID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64B8D608C4EF", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DescriptionPayMethod = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__DC31C1F3DB23A67F", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    PromotionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DescriptionPromotion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MinOrderAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: true, defaultValue: 0m),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__52C42F2FC38CE775", x => x.PromotionID);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "Việt Nam"),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    GooglePlaceID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: true, defaultValue: 0m),
                    DateJoined = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stores__3B82F0E1DBB96533", x => x.StoreID);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    VoucherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionVou = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    MinOrderAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: true, defaultValue: 0m),
                    MaxDiscountAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    MaxUsage = table.Column<int>(type: "int", nullable: true),
                    UsedCount = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    PerCustomerLimit = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsStackable = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Vouchers__3AEE79C1633A1F8B", x => x.VoucherID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionProduct = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Calories = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Carbs = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Fat = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ImageProduct = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__B40CC6ED19B2B168", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Products__Catego__6FE99F9F",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoriesIngredientsID = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Calories = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Carbs = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Fat = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ImageIngredients = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingredie__BEAEB27A3C3AB668", x => x.IngredientID);
                    table.ForeignKey(
                        name: "FK__Ingredien__Categ__6383C8BA",
                        column: x => x.CategoriesIngredientsID,
                        principalTable: "CategoriesIngredients",
                        principalColumn: "CategoriesIngredientsID");
                });

            migrationBuilder.CreateTable(
                name: "Bowls",
                columns: table => new
                {
                    BowlID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    BowlName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BaseCalories = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TotalProtein = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TotalCarbs = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TotalFat = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bowls__C923F6C95B09A909", x => x.BowlID);
                    table.ForeignKey(
                        name: "FK__Bowls__CustomerI__08B54D69",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "Việt Nam"),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    GooglePlaceID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Nhà riêng"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__091C2A1BFA4F5092", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK__CustomerA__Custo__403A8C7D",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "LoginHistory",
                columns: table => new
                {
                    LoginID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LogoutTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StatusLog = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoginHis__4DDA283879EEE1C9", x => x.LoginID);
                    table.ForeignKey(
                        name: "FK__LoginHist__Custo__4E88ABD4",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Revoked = table.Column<DateTime>(type: "datetime", nullable: true),
                    RevokedById = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RefreshT__3214EC075843640F", x => x.Id);
                    table.ForeignKey(
                        name: "FK__RefreshTo__Custo__3D5E1FD2",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "UserOTPs",
                columns: table => new
                {
                    OTPID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    OTPCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserOTPs__5C2EC5620DB993F9", x => x.OTPID);
                    table.ForeignKey(
                        name: "FK__UserOTPs__Custom__49C3F6B7",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    PreferenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PreferredCategoryID = table.Column<int>(type: "int", nullable: true),
                    DietaryPreference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LanguageMode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValue: "vi"),
                    ThemeMode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValue: "light"),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserPref__E228490F38211464", x => x.PreferenceID);
                    table.ForeignKey(
                        name: "FK__UserPrefe__Custo__12FDD1B2",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__UserPrefe__Prefe__13F1F5EB",
                        column: x => x.PreferredCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "SavedPaymentMethods",
                columns: table => new
                {
                    SavedPaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardNumberMasked = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExpiryMonth = table.Column<int>(type: "int", nullable: true),
                    ExpiryYear = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SavedPay__446C8635E878C13E", x => x.SavedPaymentID);
                    table.ForeignKey(
                        name: "FK__SavedPaym__Custo__6BE40491",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__SavedPaym__Payme__6CD828CA",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID");
                });

            migrationBuilder.CreateTable(
                name: "PromotionCategories",
                columns: table => new
                {
                    PromotionCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__0D44A889B6D6EFCD", x => x.PromotionCategoryID);
                    table.ForeignKey(
                        name: "FK__Promotion__Categ__2180FB33",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__Promotion__Promo__208CD6FA",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "PromotionID");
                });

            migrationBuilder.CreateTable(
                name: "PromotionStores",
                columns: table => new
                {
                    PromotionStoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__E040054ED4130362", x => x.PromotionStoreID);
                    table.ForeignKey(
                        name: "FK__Promotion__Promo__17036CC0",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "PromotionID");
                    table.ForeignKey(
                        name: "FK__Promotion__Store__17F790F9",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                });

            migrationBuilder.CreateTable(
                name: "Revenue",
                columns: table => new
                {
                    RevenueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    RevenueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalSales = table.Column<decimal>(type: "decimal(15,2)", nullable: true, defaultValue: 0m),
                    TotalOrders = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    RevenueType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Revenue__275F173DFD322F4B", x => x.RevenueID);
                    table.ForeignKey(
                        name: "FK__Revenue__StoreID__7C1A6C5A",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoleStaff = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__96D4AAF753EA21A8", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK__Staff__StoreID__5AEE82B9",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    VoucherID = table.Column<int>(type: "int", nullable: true),
                    PromotionID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DiscountApplied = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    StatusOrder = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, defaultValue: "Pending"),
                    InventoryDeducted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OrderNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Shipping")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BAFE1EE4AC0", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Orders__Customer__3B40CD36",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Orders__Promotio__3E1D39E1",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "PromotionID");
                    table.ForeignKey(
                        name: "FK__Orders__StoreID__3C34F16F",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                    table.ForeignKey(
                        name: "FK__Orders__VoucherI__3D2915A8",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "VoucherCategories",
                columns: table => new
                {
                    VoucherCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VoucherC__9EED8B15CBEC1DA6", x => x.VoucherCategoryID);
                    table.ForeignKey(
                        name: "FK__VoucherCa__Categ__3864608B",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__VoucherCa__Vouch__37703C52",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "VoucherStores",
                columns: table => new
                {
                    VoucherStoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VoucherS__7931D51527A25469", x => x.VoucherStoreID);
                    table.ForeignKey(
                        name: "FK__VoucherSt__Store__2EDAF651",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                    table.ForeignKey(
                        name: "FK__VoucherSt__Vouch__2DE6D218",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "ComboItems",
                columns: table => new
                {
                    ComboItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComboID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ComboIte__EE32F8E5651968F2", x => x.ComboItemID);
                    table.ForeignKey(
                        name: "FK__ComboItem__Combo__03F0984C",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "ComboID");
                    table.ForeignKey(
                        name: "FK__ComboItem__Produ__04E4BC85",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ComboID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDF61AEFD90B", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Feedbacks__Combo__76619304",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "ComboID");
                    table.ForeignKey(
                        name: "FK__Feedbacks__Custo__74794A92",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Feedbacks__Produ__756D6ECB",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "PromotionProducts",
                columns: table => new
                {
                    PromotionProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__C7B85D3CA5995C61", x => x.PromotionProductID);
                    table.ForeignKey(
                        name: "FK__Promotion__Produ__1CBC4616",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK__Promotion__Promo__1BC821DD",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "PromotionID");
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    StoreProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StorePro__629CD72859B407C9", x => x.StoreProductID);
                    table.ForeignKey(
                        name: "FK__StoreProd__Produ__7B5B524B",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK__StoreProd__Store__7A672E12",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                });

            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionTime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Metadata = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserActi__FFE3F4B9669CC59C", x => x.ActionID);
                    table.ForeignKey(
                        name: "FK__UserActio__Custo__0E391C95",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__UserActio__Produ__0F2D40CE",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "VoucherProducts",
                columns: table => new
                {
                    VoucherProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VoucherP__6235909FAADBFD2B", x => x.VoucherProductID);
                    table.ForeignKey(
                        name: "FK__VoucherPr__Produ__339FAB6E",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK__VoucherPr__Vouch__32AB8735",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransactions",
                columns: table => new
                {
                    InventoryTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    ChangeQuantity = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    BeforeQuantity = table.Column<decimal>(type: "decimal(12,4)", nullable: true),
                    AfterQuantity = table.Column<decimal>(type: "decimal(12,4)", nullable: true),
                    ReferenceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventor__0863F868CD878D42", x => x.InventoryTransactionID);
                    table.ForeignKey(
                        name: "FK__Inventory__Ingre__0697FACD",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__Inventory__Store__05A3D694",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                });

            migrationBuilder.CreateTable(
                name: "ProductIngredients",
                columns: table => new
                {
                    ProductIngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(12,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__07480704B53930A4", x => x.ProductIngredientID);
                    table.ForeignKey(
                        name: "FK__ProductIn__Ingre__76969D2E",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__ProductIn__Produ__75A278F5",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "StoreInventory",
                columns: table => new
                {
                    StoreIngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(12,4)", nullable: true, defaultValue: 0m),
                    ReorderLevel = table.Column<decimal>(type: "decimal(12,4)", nullable: true, defaultValue: 0m),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StoreInv__566F018727645774", x => x.StoreIngredientID);
                    table.ForeignKey(
                        name: "FK__StoreInve__Ingre__6A30C649",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__StoreInve__Store__693CA210",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                });

            migrationBuilder.CreateTable(
                name: "BowlItems",
                columns: table => new
                {
                    BowlItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BowlID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(12,4)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BowlItem__33A2521F173F2C20", x => x.BowlItemID);
                    table.ForeignKey(
                        name: "FK__BowlItems__BowlI__0D7A0286",
                        column: x => x.BowlID,
                        principalTable: "Bowls",
                        principalColumn: "BowlID");
                    table.ForeignKey(
                        name: "FK__BowlItems__Ingre__0E6E26BF",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ReorderFromOrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carts__51BCD7975DD56AD0", x => x.CartID);
                    table.ForeignKey(
                        name: "FK__Carts__CustomerI__5BAD9CC8",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Carts__ReorderFr__5E8A0973",
                        column: x => x.ReorderFromOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ComboID = table.Column<int>(type: "int", nullable: true),
                    BowlID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(21,2)", nullable: true, computedColumnSql: "([Quantity]*[UnitPrice])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__57ED06A1F9889860", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK__OrderItem__BowlI__47A6A41B",
                        column: x => x.BowlID,
                        principalTable: "Bowls",
                        principalColumn: "BowlID");
                    table.ForeignKey(
                        name: "FK__OrderItem__Combo__46B27FE2",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "ComboID");
                    table.ForeignKey(
                        name: "FK__OrderItem__Order__44CA3770",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK__OrderItem__Produ__45BE5BA9",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A58DDDAC50A", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payments__OrderI__4B7734FF",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "ShippingDetails",
                columns: table => new
                {
                    ShippingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: true),
                    CourierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShipDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shipping__5FACD460B94DCF45", x => x.ShippingID);
                    table.ForeignKey(
                        name: "FK__ShippingD__Addre__5224328E",
                        column: x => x.AddressID,
                        principalTable: "CustomerAddresses",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK__ShippingD__Order__51300E55",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "VoucherRedemptions",
                columns: table => new
                {
                    RedemptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherID = table.Column<int>(type: "int", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RedeemedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Amount = table.Column<decimal>(type: "decimal(12,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VoucherR__410680D153A85392", x => x.RedemptionID);
                    table.ForeignKey(
                        name: "FK__VoucherRe__Custo__57DD0BE4",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__VoucherRe__Order__56E8E7AB",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK__VoucherRe__Vouch__55F4C372",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceHistory",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreProductID = table.Column<int>(type: "int", nullable: true),
                    OldPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    NewPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ChangedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductP__4D7B4ADDCB576F66", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK__ProductPr__Store__01D345B0",
                        column: x => x.StoreProductID,
                        principalTable: "StoreProducts",
                        principalColumn: "StoreProductID");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ComboID = table.Column<int>(type: "int", nullable: true),
                    BowlID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__488B0B2AE12A2984", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK__CartItems__BowlI__6442E2C9",
                        column: x => x.BowlID,
                        principalTable: "Bowls",
                        principalColumn: "BowlID");
                    table.ForeignKey(
                        name: "FK__CartItems__CartI__6166761E",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "CartID");
                    table.ForeignKey(
                        name: "FK__CartItems__Combo__634EBE90",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "ComboID");
                    table.ForeignKey(
                        name: "FK__CartItems__Produ__625A9A57",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItemIngredients",
                columns: table => new
                {
                    OrderItemIngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(12,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__6DC447D5B6D4F1CC", x => x.OrderItemIngredientID);
                    table.ForeignKey(
                        name: "FK__OrderItem__Ingre__0B5CAFEA",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__OrderItem__Order__0A688BB1",
                        column: x => x.OrderItemID,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BowlItems_IngredientID",
                table: "BowlItems",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "UQ_Bowl_Ingredient",
                table: "BowlItems",
                columns: new[] { "BowlID", "IngredientID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bowls_CustomerID",
                table: "Bowls",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BowlID",
                table: "CartItems",
                column: "BowlID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ComboID",
                table: "CartItems",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductID",
                table: "CartItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerID",
                table: "Carts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ReorderFromOrderID",
                table: "Carts",
                column: "ReorderFromOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_ProductID",
                table: "ComboItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_Combo_Product",
                table: "ComboItems",
                columns: new[] { "ComboID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerID",
                table: "CustomerAddresses",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__5C7E359EA883D4D0",
                table: "Customers",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__A9D10534B04F9BB3",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ComboID",
                table: "Feedbacks",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CustomerID",
                table: "Feedbacks",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ProductID",
                table: "Feedbacks",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_CategoriesIngredientsID",
                table: "Ingredients",
                column: "CategoriesIngredientsID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_IngredientID",
                table: "InventoryTransactions",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_StoreID",
                table: "InventoryTransactions",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistory_CustomerID",
                table: "LoginHistory",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemIngredients_IngredientID",
                table: "OrderItemIngredients",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemIngredients_OrderItemID",
                table: "OrderItemIngredients",
                column: "OrderItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BowlID",
                table: "OrderItems",
                column: "BowlID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ComboID",
                table: "OrderItems",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductID",
                table: "OrderItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PromotionID",
                table: "Orders",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreID",
                table: "Orders",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VoucherID",
                table: "Orders",
                column: "VoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderID",
                table: "Payments",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientID",
                table: "ProductIngredients",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "UQ_Product_Ingredient",
                table: "ProductIngredients",
                columns: new[] { "ProductID", "IngredientID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistory_StoreProductID",
                table: "ProductPriceHistory",
                column: "StoreProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCategories_CategoryID",
                table: "PromotionCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "UQ_Promotion_Category",
                table: "PromotionCategories",
                columns: new[] { "PromotionID", "CategoryID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionProducts_ProductID",
                table: "PromotionProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_Promotion_Product",
                table: "PromotionProducts",
                columns: new[] { "PromotionID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionStores_StoreID",
                table: "PromotionStores",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "UQ_Promotion_Store",
                table: "PromotionStores",
                columns: new[] { "PromotionID", "StoreID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_CustomerID",
                table: "RefreshToken",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "UQ_Store_Date_Type",
                table: "Revenue",
                columns: new[] { "StoreID", "RevenueDate", "RevenueType" },
                unique: true,
                filter: "[StoreID] IS NOT NULL AND [RevenueType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPaymentMethods_CustomerID",
                table: "SavedPaymentMethods",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPaymentMethods_PaymentMethodID",
                table: "SavedPaymentMethods",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_AddressID",
                table: "ShippingDetails",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_OrderID",
                table: "ShippingDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_StoreID",
                table: "Staff",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "UQ__Staff__5C7E359E387EA356",
                table: "Staff",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Staff__A9D105349DC29AD2",
                table: "Staff",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventory_IngredientID",
                table: "StoreInventory",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "UQ_Store_Ingredient",
                table: "StoreInventory",
                columns: new[] { "StoreID", "IngredientID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_ProductID",
                table: "StoreProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_Store_Product",
                table: "StoreProducts",
                columns: new[] { "StoreID", "ProductID" },
                unique: true,
                filter: "[StoreID] IS NOT NULL AND [ProductID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_CustomerID",
                table: "UserActions",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_ProductID",
                table: "UserActions",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_Customer_OTP",
                table: "UserOTPs",
                columns: new[] { "CustomerID", "OTPCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_CustomerID",
                table: "UserPreferences",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_PreferredCategoryID",
                table: "UserPreferences",
                column: "PreferredCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherCategories_CategoryID",
                table: "VoucherCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "UQ_Voucher_Category",
                table: "VoucherCategories",
                columns: new[] { "VoucherID", "CategoryID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherProducts_ProductID",
                table: "VoucherProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "UQ_Voucher_Product",
                table: "VoucherProducts",
                columns: new[] { "VoucherID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherRedemptions_CustomerID",
                table: "VoucherRedemptions",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherRedemptions_OrderID",
                table: "VoucherRedemptions",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "UQ_Voucher_Customer_Order",
                table: "VoucherRedemptions",
                columns: new[] { "VoucherID", "CustomerID", "OrderID" },
                unique: true,
                filter: "[VoucherID] IS NOT NULL AND [CustomerID] IS NOT NULL AND [OrderID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Vouchers__A25C5AA70BFFD24E",
                table: "Vouchers",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherStores_StoreID",
                table: "VoucherStores",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "UQ_Voucher_Store",
                table: "VoucherStores",
                columns: new[] { "VoucherID", "StoreID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BowlItems");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ComboItems");

            migrationBuilder.DropTable(
                name: "CourierServices");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "InventoryTransactions");

            migrationBuilder.DropTable(
                name: "LoginHistory");

            migrationBuilder.DropTable(
                name: "OrderItemIngredients");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "ProductPriceHistory");

            migrationBuilder.DropTable(
                name: "PromotionCategories");

            migrationBuilder.DropTable(
                name: "PromotionProducts");

            migrationBuilder.DropTable(
                name: "PromotionStores");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Revenue");

            migrationBuilder.DropTable(
                name: "SavedPaymentMethods");

            migrationBuilder.DropTable(
                name: "ShippingDetails");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "StoreInventory");

            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "UserOTPs");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "VoucherCategories");

            migrationBuilder.DropTable(
                name: "VoucherProducts");

            migrationBuilder.DropTable(
                name: "VoucherRedemptions");

            migrationBuilder.DropTable(
                name: "VoucherStores");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "StoreProducts");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Bowls");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CategoriesIngredients");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
