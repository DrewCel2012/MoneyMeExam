using Mapster;
using MoneyMe.Model.ViewModels;
using MoneyMe.Repository.Entities;
using MoneyMe.Repository.Interface;
using MoneyMe.Service.Interface;

namespace MoneyMe.Service
{
    public sealed class ProductsService(IUnitOfWork unitOfWork) : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<IEnumerable<ProductsViewModel>> GetAllAsync()
        {
            IEnumerable<Products> products = await _unitOfWork.Products.GetAllAsync(
                    filter: f => f.IsActive == true,
                    orderBy: o => o.OrderBy(x => x.Id),
                    includes: null);

            return products.Adapt<IEnumerable<ProductsViewModel>>();
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
