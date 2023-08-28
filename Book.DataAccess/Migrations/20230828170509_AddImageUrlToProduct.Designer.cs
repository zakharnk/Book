﻿// <auto-generated />
using Book.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Book.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230828170509_AddImageUrlToProduct")]
    partial class AddImageUrlToProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.7.23375.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Book.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "SciFi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("Book.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("PriceFor100")
                        .HasColumnType("float");

                    b.Property<double>("PriceFor50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Stephen King",
                            CategoryId = 1,
                            Description = "Stephen King’s terrifying, classic #1 New York Times bestseller, “a landmark in American literature” (Chicago Sun-Times)—about seven adults who return to their hometown to confront a nightmare they had first stumbled on as teenagers…an evil without a name: It.",
                            ISBN = "978-1982127794",
                            ImageUrl = "",
                            ListPrice = 21.0,
                            Price = 20.0,
                            PriceFor100 = 15.0,
                            PriceFor50 = 18.0,
                            Title = "It"
                        },
                        new
                        {
                            Id = 2,
                            Author = "F. Scott Fitzgerald",
                            CategoryId = 1,
                            Description = "The Great Gatsby is considered F. Scott Fitzgerald’s magnum opus, exploring themes of decadence, idealism, social stigmas, patriarchal norms, and the deleterious effects of unencumbered wealth in capitalistic society, set against the backdrop of the Jazz Age and the Roaring Twenties. At its heart, it’s a cautionary tale, a revealing look into the darker side to the American Dream.",
                            ISBN = "979-8351145013",
                            ImageUrl = "",
                            ListPrice = 6.0,
                            Price = 5.0,
                            PriceFor100 = 3.0,
                            PriceFor50 = 4.0,
                            Title = "The Great Gatsby"
                        },
                        new
                        {
                            Id = 3,
                            Author = "William Shakespeare",
                            CategoryId = 3,
                            Description = "Hamlet is a tragedy written by William Shakespeare sometime between 1599 and 1601. Set in Denmark, the play depicts Prince Hamlet and his revenge against his uncle, Claudius, who has murdered Hamlet's father in order to seize his throne and marry Hamlet's mother.",
                            ISBN = "979-8630242716",
                            ImageUrl = "",
                            ListPrice = 7.0,
                            Price = 6.0,
                            PriceFor100 = 4.0,
                            PriceFor50 = 5.0,
                            Title = "Hamlet"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Fyodor Dostoyevsky",
                            CategoryId = 1,
                            Description = "One of the world’s greatest novels, Crime and Punishment is the story of a murder and its consequences—an unparalleled tale of suspense set in the midst of nineteenth-century Russia’s troubled transition to the modern age. ",
                            ISBN = "979-8630242716",
                            ImageUrl = "",
                            ListPrice = 14.0,
                            Price = 13.0,
                            PriceFor100 = 11.0,
                            PriceFor50 = 12.0,
                            Title = "Crime and Punishment"
                        },
                        new
                        {
                            Id = 5,
                            Author = "J.R.R. Tolkien",
                            CategoryId = 2,
                            Description = "In The Hobbit, Bilbo Baggins enjoys a comfortable, unambitious life, rarely traveling farther than the pantry of his hobbit-hole in Bag End. But his contentment is disturbed when the wizard Gandalf and a company of thirteen dwarves arrive on his doorstep to whisk him away on a journey to raid the treasure hoard of Smaug the Magnificent, a large and very dangerous dragon....",
                            ISBN = "978-0544174221",
                            ImageUrl = "",
                            ListPrice = 22.0,
                            Price = 21.0,
                            PriceFor100 = 18.0,
                            PriceFor50 = 20.0,
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Herman Melville",
                            CategoryId = 1,
                            Description = "The book is sailor Ishmael's narrative of the obsessive quest of Ahab, captain of the whaling ship Pequod, for revenge on Moby Dick, the giant white sperm whale that on the ship's previous voyage bit off Ahab's leg at the knee.",
                            ISBN = "979-8612823810",
                            ImageUrl = "",
                            ListPrice = 13.0,
                            Price = 12.0,
                            PriceFor100 = 10.0,
                            PriceFor50 = 11.0,
                            Title = "Moby Dick"
                        });
                });

            modelBuilder.Entity("Book.Models.Product", b =>
                {
                    b.HasOne("Book.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}