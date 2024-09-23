using MoneyMe.Repository.Entities;

namespace MoneyMe.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customers> Customers { get; }

        IGenericRepository<NamePrefixes> NamePrefixes { get; }

        IGenericRepository<CustomerProfile> CustomerProfile { get; }

        IGenericRepository<Products> Products { get; }

        IGenericRepository<CustomerProfileLoanApplication> CustomerProfileLoanApp { get; }


        Task<int> SaveChangesAsync();
    }
}
