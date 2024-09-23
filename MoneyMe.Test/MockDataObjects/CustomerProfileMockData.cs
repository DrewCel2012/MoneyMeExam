using MoneyMe.Model.DataTransferObjects;
using MoneyMe.Repository.Entities;

namespace MoneyMe.Test.MockDataObjects
{
    internal sealed class CustomerProfileMockData
    {
        public static IEnumerable<CustomerProfile> GetTestCustomerProfileList
        {
            get
            {
                return new List<CustomerProfile>
                {
                    new() 
                    {
                        Id = 1,
                        FirstName = "John 1",
                        LastName = "Doe 1"
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "John 2",
                        LastName = "Doe 2"
                    }
                };
            }
        }

        public static CustomerProfileDto GetTestCustomerProfileToAdd
        {
            get
            {
                return new CustomerProfileDto
                {
                    Id = 3,
                    FirstName = "John 3",
                    LastName = "Doe 3"
                };
            }
        }
    }
}
