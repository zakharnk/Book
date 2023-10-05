using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;

namespace Book.DataAccess.Repository
{ 
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IAppUserRepository AppUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IRoleRepository Role { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            AppUser = new AppUserRepository(_db);
            OrderHeader=new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            Role = new RoleRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
