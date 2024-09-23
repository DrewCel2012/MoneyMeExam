using MoneyMe.Model.ViewModels;

namespace MoneyMe.Service.Interface
{
    public interface IProductsService : IDisposable
    {
        Task<IEnumerable<ProductsViewModel>> GetAllAsync();
    }
}
