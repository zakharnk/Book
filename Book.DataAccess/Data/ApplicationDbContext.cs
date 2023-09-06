using Book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "It",
                    Author = "Stephen King",
                    Description = "Stephen King’s terrifying, classic #1 New York Times bestseller, “a landmark in American literature” (Chicago Sun-Times)—about seven adults who return to their hometown to confront a nightmare they had first stumbled on as teenagers…an evil without a name: It.",
                    ISBN = "978-1982127794",
                    ListPrice = 21,
                    Price = 20,
                    PriceFor50 = 18,
                    PriceFor100 = 15,
                    CategoryId=1,
                    ImageUrl=""
                },

                new Product
                {
                    Id = 2,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Description = "The Great Gatsby is considered F. Scott Fitzgerald’s magnum opus, exploring themes of decadence, idealism, social stigmas, patriarchal norms, and the deleterious effects of unencumbered wealth in capitalistic society, set against the backdrop of the Jazz Age and the Roaring Twenties. At its heart, it’s a cautionary tale, a revealing look into the darker side to the American Dream.",
                    ISBN = "979-8351145013",
                    ListPrice = 6,
                    Price = 5,
                    PriceFor50 = 4,
                    PriceFor100 = 3,
                    CategoryId=1,
                    ImageUrl = ""
                },

                new Product
                {
                    Id = 3,
                    Title = "Hamlet",
                    Author = "William Shakespeare",
                    Description = "Hamlet is a tragedy written by William Shakespeare sometime between 1599 and 1601. Set in Denmark, the play depicts Prince Hamlet and his revenge against his uncle, Claudius, who has murdered Hamlet's father in order to seize his throne and marry Hamlet's mother.",
                    ISBN = "979-8630242716",
                    ListPrice = 7,
                    Price = 6,
                    PriceFor50 = 5,
                    PriceFor100 = 4,
                    CategoryId=3,
                    ImageUrl = ""
                },

                 new Product
                 {
                     Id = 4,
                     Title = "Crime and Punishment",
                     Author = "Fyodor Dostoyevsky",
                     Description = "One of the world’s greatest novels, Crime and Punishment is the story of a murder and its consequences—an unparalleled tale of suspense set in the midst of nineteenth-century Russia’s troubled transition to the modern age. ",
                     ISBN = "979-8630242716",
                     ListPrice = 14,
                     Price = 13,
                     PriceFor50 = 12,
                     PriceFor100 = 11,
                     CategoryId=1,
                     ImageUrl = ""
                 },

                  new Product
                  {
                      Id = 5,
                      Title = "The Hobbit",
                      Author = "J.R.R. Tolkien",
                      Description = "In The Hobbit, Bilbo Baggins enjoys a comfortable, unambitious life, rarely traveling farther than the pantry of his hobbit-hole in Bag End. But his contentment is disturbed when the wizard Gandalf and a company of thirteen dwarves arrive on his doorstep to whisk him away on a journey to raid the treasure hoard of Smaug the Magnificent, a large and very dangerous dragon....",
                      ISBN = "978-0544174221",
                      ListPrice = 22,
                      Price = 21,
                      PriceFor50 = 20,
                      PriceFor100 = 18,
                      CategoryId=2,
                      ImageUrl = ""
                  },

                  new Product
                  {
                      Id = 6,
                      Title = "Moby Dick",
                      Author = "Herman Melville",
                      Description = "The book is sailor Ishmael's narrative of the obsessive quest of Ahab, captain of the whaling ship Pequod, for revenge on Moby Dick, the giant white sperm whale that on the ship's previous voyage bit off Ahab's leg at the knee.",
                      ISBN = "979-8612823810",
                      ListPrice = 13,
                      Price = 12,
                      PriceFor50 = 11,
                      PriceFor100 = 10,
                      CategoryId=1,
                      ImageUrl = ""
                  });
        }
    }
}
