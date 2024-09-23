using MoneyMe.Repository.Entities;
using MoneyMe.Repository.Interface;

namespace MoneyMe.Repository.UnitOfWork
{
    public sealed class UnitOfWork(MoneyMeDbContext context) : IUnitOfWork
    {
        private readonly MoneyMeDbContext _context = context;

        private IGenericRepository<Customers> _customers;
        private IGenericRepository<NamePrefixes> _namePrefixes;
        private IGenericRepository<CustomerProfile> _customerProfile;
        private IGenericRepository<Products> _products;
        private IGenericRepository<CustomerProfileLoanApplication> _customerProfileLoanApp;


        public IGenericRepository<Customers> Customers
        {
            get { return _customers ??= new GenericRepository<Customers>(_context); }
        }

        public IGenericRepository<NamePrefixes> NamePrefixes
        {
            get { return _namePrefixes ??= new GenericRepository<NamePrefixes>(_context); }
        }

        public IGenericRepository<CustomerProfile> CustomerProfile
        {
            get { return _customerProfile ??= new GenericRepository<CustomerProfile>(_context); }
        }

        public IGenericRepository<Products> Products
        {
            get { return _products ??= new GenericRepository<Products>(_context); }
        }

        public IGenericRepository<CustomerProfileLoanApplication> CustomerProfileLoanApp
        {
            get { return _customerProfileLoanApp ??= new GenericRepository<CustomerProfileLoanApplication>(_context); }
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
