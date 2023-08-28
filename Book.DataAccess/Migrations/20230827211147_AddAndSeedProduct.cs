using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Book.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAndSeedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PriceFor50 = table.Column<double>(type: "float", nullable: false),
                    PriceFor100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "PriceFor100", "PriceFor50", "Title" },
                values: new object[,]
                {
                    { 1, "Stephen King", "Stephen King’s terrifying, classic #1 New York Times bestseller, “a landmark in American literature” (Chicago Sun-Times)—about seven adults who return to their hometown to confront a nightmare they had first stumbled on as teenagers…an evil without a name: It.", "978-1982127794", 21.0, 20.0, 15.0, 18.0, "It" },
                    { 2, "F. Scott Fitzgerald", "The Great Gatsby is considered F. Scott Fitzgerald’s magnum opus, exploring themes of decadence, idealism, social stigmas, patriarchal norms, and the deleterious effects of unencumbered wealth in capitalistic society, set against the backdrop of the Jazz Age and the Roaring Twenties. At its heart, it’s a cautionary tale, a revealing look into the darker side to the American Dream.", "979-8351145013", 6.0, 5.0, 4.0, 4.2999999999999998, "The Great Gatsby" },
                    { 3, "William Shakespeare", "Hamlet is a tragedy written by William Shakespeare sometime between 1599 and 1601. Set in Denmark, the play depicts Prince Hamlet and his revenge against his uncle, Claudius, who has murdered Hamlet's father in order to seize his throne and marry Hamlet's mother.", "979-8630242716", 7.25, 7.0, 6.0, 6.5, "Hamlet" },
                    { 4, "Fyodor Dostoyevsky", "One of the world’s greatest novels, Crime and Punishment is the story of a murder and its consequences—an unparalleled tale of suspense set in the midst of nineteenth-century Russia’s troubled transition to the modern age. ", "979-8630242716", 14.0, 13.5, 11.0, 13.0, "Crime and Punishment" },
                    { 5, "J.R.R. Tolkien", "In The Hobbit, Bilbo Baggins enjoys a comfortable, unambitious life, rarely traveling farther than the pantry of his hobbit-hole in Bag End. But his contentment is disturbed when the wizard Gandalf and a company of thirteen dwarves arrive on his doorstep to whisk him away on a journey to raid the treasure hoard of Smaug the Magnificent, a large and very dangerous dragon....", "978-0544174221", 22.5, 22.0, 18.0, 20.0, "The Hobbit" },
                    { 6, "Herman Melville", "The book is sailor Ishmael's narrative of the obsessive quest of Ahab, captain of the whaling ship Pequod, for revenge on Moby Dick, the giant white sperm whale that on the ship's previous voyage bit off Ahab's leg at the knee.", "979-8612823810", 13.199999999999999, 12.9, 10.0, 12.0, "The Hobbit" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
