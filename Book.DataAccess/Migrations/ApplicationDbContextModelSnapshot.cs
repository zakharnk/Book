﻿// <auto-generated />
using Book.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Book.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
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

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Stephen King",
                            Description = "Stephen King’s terrifying, classic #1 New York Times bestseller, “a landmark in American literature” (Chicago Sun-Times)—about seven adults who return to their hometown to confront a nightmare they had first stumbled on as teenagers…an evil without a name: It.",
                            ISBN = "978-1982127794",
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
                            Description = "The Great Gatsby is considered F. Scott Fitzgerald’s magnum opus, exploring themes of decadence, idealism, social stigmas, patriarchal norms, and the deleterious effects of unencumbered wealth in capitalistic society, set against the backdrop of the Jazz Age and the Roaring Twenties. At its heart, it’s a cautionary tale, a revealing look into the darker side to the American Dream.",
                            ISBN = "979-8351145013",
                            ListPrice = 6.0,
                            Price = 5.0,
                            PriceFor100 = 4.0,
                            PriceFor50 = 4.2999999999999998,
                            Title = "The Great Gatsby"
                        },
                        new
                        {
                            Id = 3,
                            Author = "William Shakespeare",
                            Description = "Hamlet is a tragedy written by William Shakespeare sometime between 1599 and 1601. Set in Denmark, the play depicts Prince Hamlet and his revenge against his uncle, Claudius, who has murdered Hamlet's father in order to seize his throne and marry Hamlet's mother.",
                            ISBN = "979-8630242716",
                            ListPrice = 7.25,
                            Price = 7.0,
                            PriceFor100 = 6.0,
                            PriceFor50 = 6.5,
                            Title = "Hamlet"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Fyodor Dostoyevsky",
                            Description = "One of the world’s greatest novels, Crime and Punishment is the story of a murder and its consequences—an unparalleled tale of suspense set in the midst of nineteenth-century Russia’s troubled transition to the modern age. ",
                            ISBN = "979-8630242716",
                            ListPrice = 14.0,
                            Price = 13.5,
                            PriceFor100 = 11.0,
                            PriceFor50 = 13.0,
                            Title = "Crime and Punishment"
                        },
                        new
                        {
                            Id = 5,
                            Author = "J.R.R. Tolkien",
                            Description = "In The Hobbit, Bilbo Baggins enjoys a comfortable, unambitious life, rarely traveling farther than the pantry of his hobbit-hole in Bag End. But his contentment is disturbed when the wizard Gandalf and a company of thirteen dwarves arrive on his doorstep to whisk him away on a journey to raid the treasure hoard of Smaug the Magnificent, a large and very dangerous dragon....",
                            ISBN = "978-0544174221",
                            ListPrice = 22.5,
                            Price = 22.0,
                            PriceFor100 = 18.0,
                            PriceFor50 = 20.0,
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Herman Melville",
                            Description = "The book is sailor Ishmael's narrative of the obsessive quest of Ahab, captain of the whaling ship Pequod, for revenge on Moby Dick, the giant white sperm whale that on the ship's previous voyage bit off Ahab's leg at the knee.",
                            ISBN = "979-8612823810",
                            ListPrice = 13.199999999999999,
                            Price = 12.9,
                            PriceFor100 = 10.0,
                            PriceFor50 = 12.0,
                            Title = "The Hobbit"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}