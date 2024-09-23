using MoneyMe.Model.Enums;
using MoneyMe.Service.Interface;
using MoneyMe.Service.RepaymentTypes;

namespace MoneyMe.Service.Factory
{
    public sealed class RepaymentTypeFactory
    {
        public static IRepaymentTypeService GetRepaymentType(ProductType type)
        {
            return type switch
            {
                ProductType.A =>
                    new ProductAService(),

                ProductType.B =>
                    new ProductBService(),

                ProductType.C =>
                    new ProductCService(),

                _ => null
            };
        }
    }
}
