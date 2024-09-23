using MoneyMe.Model.ViewModels;

namespace MoneyMe.Service.Interface
{
    public interface INamePrefixesService : IDisposable
    {
        Task<IEnumerable<NamePrefixesViewModel>> GetAllAsync();
    }
}
