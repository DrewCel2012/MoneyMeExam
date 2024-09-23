using Microsoft.EntityFrameworkCore.Query;
using MoneyMe.Model.ViewModels;
using MoneyMe.Repository.Entities;
using MoneyMe.Repository.Interface;
using MoneyMe.Service;
using MoneyMe.Test.MockDataObjects;
using Moq;
using System.Linq.Expressions;

namespace MoneyMe.Test
{
    public sealed class CustomerProfileServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;

        public CustomerProfileServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }


        [Fact]
        public void GetAllAsync_ShouldReturnListOfCustomerProfile()
        {
            ShouldReturnListOfCustomerProfile().Wait();
        }

        [Fact]
        public void GetByIDAsync_ShouldReturnSingleCustomerProfileById()
        {
            ShouldReturnSingleCustomerProfileById(1).Wait();
        }

        [Fact]
        public void AddAsync_ShouldAddSingleCustomerProfileOnceAndReturnTrue()
        {
            ShouldAddSingleCustomerProfileOnceAndReturnTrue().Wait();
        }


        private async Task ShouldAddSingleCustomerProfileOnceAndReturnTrue()
        {
            var customer = CustomerProfileMockData.GetTestCustomerProfileToAdd;

            //Arrange:
            _mockUnitOfWork.Setup(x => x.CustomerProfile.AddAsync(It.IsAny<CustomerProfile>()));
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            var customerProfileService = new CustomerProfileService(_mockUnitOfWork.Object);


            //Act:
            var result = await customerProfileService.AddAsync(customer);


            //Verification:
            _mockUnitOfWork.Verify(x => x.CustomerProfile.AddAsync(It.IsAny<CustomerProfile>()), Times.Once());


            //Assert:
            Assert.True(result);
        }

        private async Task ShouldReturnSingleCustomerProfileById(int id)
        {
            var customer = CustomerProfileMockData.GetTestCustomerProfileList.FirstOrDefault(x => x.Id == id);

            //Arrange:
            _mockUnitOfWork.Setup(x => x.CustomerProfile.GetByFilterAsync(It.IsAny<Expression<Func<CustomerProfile, bool>>>(),
                                                                          It.IsAny<Func<IQueryable<CustomerProfile>, IIncludableQueryable<CustomerProfile, object>>>()))
                                                                          .Returns(Task.FromResult(customer));

            var customerProfileService = new CustomerProfileService(_mockUnitOfWork.Object);


            //Act:
            var result = await customerProfileService.GetByIDAsync<CustomerProfileViewModel>(id);


            //Assert:
            Assert.NotNull(result);
            Assert.True(customer.Id == id);
        }

        private async Task ShouldReturnListOfCustomerProfile()
        {
            var customerList = CustomerProfileMockData.GetTestCustomerProfileList;

            //Arrange:
            _mockUnitOfWork.Setup(x => x.CustomerProfile.GetAllAsync(It.IsAny<Expression<Func<CustomerProfile, bool>>>(),
                                                                     It.IsAny<Func<IQueryable<CustomerProfile>, IOrderedQueryable<CustomerProfile>>>(),
                                                                     null))
                                                                     .Returns(Task.FromResult(customerList));            

            var customerProfileService = new CustomerProfileService(_mockUnitOfWork.Object);


            //Act:
            var result = await customerProfileService.GetAllAsync();


            //Assert:
            Assert.NotNull(result);
            Assert.True(result.Count() > 1);
        }
    }
}