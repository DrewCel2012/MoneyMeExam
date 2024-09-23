using Mapster;
using MoneyMe.Model.ViewModels;
using MoneyMe.Repository.Entities;
using MoneyMe.Repository.Interface;
using MoneyMe.Service.Interface;

namespace MoneyMe.Service
{
    public sealed class NamePrefixesService(IUnitOfWork unitOfWork) : INamePrefixesService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<IEnumerable<NamePrefixesViewModel>> GetAllAsync()
        {
            IEnumerable<NamePrefixes> namePrefixes = await _unitOfWork.NamePrefixes.GetAllAsync(
                    filter: f => f.IsActive == true,
                    orderBy: o => o.OrderBy(x => x.Id),
                    includes: null);

            return namePrefixes.Adapt<IEnumerable<NamePrefixesViewModel>>();
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
